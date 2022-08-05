using Microsoft.AspNetCore.Mvc;
using IFYB.Entities;
using IFYB.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Net.Mail;
using System.IO;
using Scriban;
using IFYB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace IFYB.Controllers;

[ApiController]
[Route("api/authenticate")]
public class AuthenticationController : BaseController
{
    private readonly AppOptions appOptions;
    private readonly JwtOptions jwtOptions;

    private readonly  SecurityKey securityKey;
    private readonly  EmailCreationService emailService;
    private readonly  IEmailSenderService emailSenderService;

    public AuthenticationController(ApplicationDbContext dbContext, IOptions<AppOptions> appOptions, IOptions<JwtOptions> jwtOptions, SecurityKey securityKey, EmailCreationService emailService, IEmailSenderService emailSenderService) : base(dbContext)
    {
        this.appOptions = appOptions.Value;
        this.jwtOptions = jwtOptions.Value;
        this.securityKey = securityKey;
        this.emailService = emailService;
        this.emailSenderService = emailSenderService;
    }


    [HttpGet]
    [Route("check-jwt")]
    [Authorize(Policy = Policies.ClientOnly)]
    public IActionResult CheckJwt()
    {
        if (GetClient() != null)
            return Ok();
        return Unauthorized();
    }

    [HttpPost]
    [Produces(typeof(IdDto))]
    public IActionResult StartSession([FromBody] EmailDto dto)
    {
        var client = dbContext.Clients.FirstOrDefault(c => c.Email == dto.Email);
        if (client == null) {
            if(dto.PrivacyPolicyAccepted) {
                client = new Client(dto.Email);
                dbContext.Clients.Add(client);
            } else {
                return Unauthorized();
            }
        }
        var passwordHasher = new PasswordHasher<Client>();
        if (!appOptions.SendEmails) {
            client.Password = passwordHasher.HashPassword(client, "123456");
        } else
        {
            int charCount = 'Z' - 'A';
            string password = string.Empty;
            for (int i = 0; i < 6; ++i)
            {
                int code = Random.Shared.Next() % charCount;
                password += (char)('A' + code);
            }
            client.Password = passwordHasher.HashPassword(client, password);
            string textPassword = $"{password.Substring(0, 3)}-{password.Substring(3)}";
            var email = emailService.CreateEmail(dto.Email, "Authentication", null, new { Password = textPassword });
            emailSenderService.SendEmail(email!);
        }
        dbContext.SaveChanges();
        return Ok(new IdDto(client.Id));
    }

    [HttpPost]
    [Route("{clientId}")]
    [Produces(typeof(JwtDto))]
    public IActionResult Authenticate(int clientId, [FromBody] PasswordDto dto)
    {
        var client = dbContext.Clients.FirstOrDefault(c => c.Id == clientId);
        if (client == null)
            return Forbid();
        var passwordHasher = new PasswordHasher<Client>();
        switch (passwordHasher.VerifyHashedPassword(client, client.Password, dto.Password))
        {
            case PasswordVerificationResult.Success:
                return Ok(GenerateJWT(client.Email, Roles.Client));
            case PasswordVerificationResult.Failed:
                return Forbid();
            case PasswordVerificationResult.SuccessRehashNeeded:
                client.Password = passwordHasher.HashPassword(client, dto.Password);
                dbContext.SaveChanges();
                return Ok(GenerateJWT(client.Email, Roles.Client));
        }
        return Forbid();
    }

    [HttpGet]
    [Route("admin/check-jwt")]
    [Authorize(Policy = Policies.AdminOnly)]
    public IActionResult CheckAdminJwt()
    {
        return Ok();
    }

    [HttpPost]
    [Route("admin")]
    [Produces(typeof(IdDto))]
    public IActionResult StartAdminSession([FromBody] EmailDto dto)
    {
        var admin = dbContext.Admins.FirstOrDefault(c => c.Email == dto.Email);
        if (admin == null) {
            return Forbid();
        }
        var passwordHasher = new PasswordHasher<Admin>();
        admin.Password = passwordHasher.HashPassword(admin, "123456");
        dbContext.SaveChanges();
        return Ok(new IdDto(admin.Id));
    }

    [HttpPost]
    [Route("admin/{adminId}")]
    [Produces(typeof(JwtDto))]
    public IActionResult AuthenticateAdmin(int adminId, [FromBody] PasswordDto dto)
    {
        var admin = dbContext.Admins.FirstOrDefault(c => c.Id == adminId);
        if (admin == null)
            return Forbid();
        var passwordHasher = new PasswordHasher<Admin>();
        switch (passwordHasher.VerifyHashedPassword(admin, admin.Password, dto.Password))
        {
            case PasswordVerificationResult.Success:
                return Ok(GenerateJWT(admin.Email, Roles.Admin));
            case PasswordVerificationResult.Failed:
                return Forbid();
            case PasswordVerificationResult.SuccessRehashNeeded:
                admin.Password = passwordHasher.HashPassword(admin, dto.Password);
                dbContext.SaveChanges();
                return Ok(GenerateJWT(admin.Email, Roles.Admin));
        }
        return Forbid();
    }

    private JwtDto GenerateJWT(string email, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            }),
            Issuer = jwtOptions.Issuer,
            Audience = jwtOptions.Audience,
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new JwtDto(tokenHandler.WriteToken(token));
    }
}
