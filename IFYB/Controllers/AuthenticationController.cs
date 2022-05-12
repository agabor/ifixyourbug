using Microsoft.AspNetCore.Mvc;
using IFYB.Entities;
using IFYB.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace IFYB.Controllers;

[ApiController]
[Route("authenticate")]
public class AuthenticationController : ControllerBase
{
    public ApplicationDbContext DbContext { get; }
    public IConfiguration Configuration { get; private set; }
    public SecurityKey SecurityKey { get; }

    public AuthenticationController(ApplicationDbContext dbContext, IConfiguration configuration, SecurityKey securityKey)
    {
        DbContext = dbContext;
        Configuration = configuration;
        SecurityKey = securityKey;
    }

    [HttpPost]
    [Produces(typeof(IdDto))]
    public IActionResult StartSession([FromBody] EmailDto dto)
    {
        DbContext.Database.EnsureCreated();
        var client = DbContext.Clients.FirstOrDefault(c => c.Email == dto.Email);
        if (client == null) {
            client = new Client(dto.Email);
            DbContext.Clients.Add(client);
        }
        var passwordHasher = new PasswordHasher<Client>();
        client.Password = passwordHasher.HashPassword(client, "123456");
        DbContext.SaveChanges();
        return Ok(new IdDto(client.Id));
    }

    [HttpPost]
    [Route("{clientId}")]
    [Produces(typeof(JwtDto))]
    public IActionResult Authenticate(int clientId, [FromBody] PasswordDto dto)
    {
        DbContext.Database.EnsureCreated();
        var client = DbContext.Clients.FirstOrDefault(c => c.Id == clientId);
        if (client == null)
            return Forbid();
        var passwordHasher = new PasswordHasher<Client>();
        switch (passwordHasher.VerifyHashedPassword(client, client.Password, dto.Password))
        {
            case PasswordVerificationResult.Success:
                return Ok(GenerateJWT(new Claim(ClaimTypes.Email, client.Email)));
            case PasswordVerificationResult.Failed:
                return Forbid();
            case PasswordVerificationResult.SuccessRehashNeeded:
                client.Password = passwordHasher.HashPassword(client, dto.Password);
                DbContext.SaveChanges();
                return Ok(GenerateJWT(new Claim(ClaimTypes.Email, client.Email)));
        }
        return Forbid();
    }

    [HttpPost]
    [Route("admin")]
    [Produces(typeof(IdDto))]
    public IActionResult StartAdminSession([FromBody] EmailDto dto)
    {
        DbContext.Database.EnsureCreated();
        var admin = DbContext.Admins.FirstOrDefault(c => c.Email == dto.Email);
        if (admin == null) {
            return Forbid();
        }
        var passwordHasher = new PasswordHasher<Admin>();
        admin.Password = passwordHasher.HashPassword(admin, "123456");
        DbContext.SaveChanges();
        return Ok(new IdDto(admin.Id));
    }

    [HttpPost]
    [Route("admin/{adminId}")]
    [Produces(typeof(JwtDto))]
    public IActionResult AuthenticateAdmin(int adminId, [FromBody] PasswordDto dto)
    {
        DbContext.Database.EnsureCreated();
        var admin = DbContext.Admins.FirstOrDefault(c => c.Id == adminId);
        if (admin == null)
            return Forbid();
        var passwordHasher = new PasswordHasher<Admin>();
        switch (passwordHasher.VerifyHashedPassword(admin, admin.Password, dto.Password))
        {
            case PasswordVerificationResult.Success:
                return Ok(GenerateJWT(new Claim(ClaimTypes.IsPersistent, "yes")));
            case PasswordVerificationResult.Failed:
                return Forbid();
            case PasswordVerificationResult.SuccessRehashNeeded:
                admin.Password = passwordHasher.HashPassword(admin, dto.Password);
                DbContext.SaveChanges();
                return Ok(GenerateJWT(new Claim(ClaimTypes.IsPersistent, "yes")));
        }
        return Forbid();
    }

    private JwtDto GenerateJWT(Claim claim)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                claim
            }),
            Issuer = Configuration["JwtIssuer"],
            Audience = Configuration["JwtAudience"],
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new JwtDto(tokenHandler.WriteToken(token));
    }
}
