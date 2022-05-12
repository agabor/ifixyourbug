using Microsoft.AspNetCore.Mvc;
using IFYB.Entities;
using IFYB.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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
                return Ok(GenerateJWT(client));
            case PasswordVerificationResult.Failed:
                return Forbid();
            case PasswordVerificationResult.SuccessRehashNeeded:
                client.Password = passwordHasher.HashPassword(client, dto.Password);
                DbContext.SaveChanges();
                return Ok(GenerateJWT(client));
        }
        return Forbid();
    }

    private JwtDto GenerateJWT(Client client)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Email, client.Email)
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
