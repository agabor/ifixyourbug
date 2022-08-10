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
        if (client == null)
        {
            if (dto.PrivacyPolicyAccepted)
            {
                client = new Client(dto.Email);
                client.RegistrationTime = DateTime.UtcNow;
                dbContext.Clients.Add(client);
            }
            else
            {
                return Unauthorized();
            }
        }
        CreatePassword(dto, client);
        return Ok(new IdDto(client.Id));
    }

    private void CreatePassword<T>(EmailDto dto, T user) where T : class, IAunthenticable
    {
        var passwordHasher = new PasswordHasher<T>();
        if (!appOptions.SendEmails)
        {
            user.Password = passwordHasher.HashPassword(user, "123456");
        }
        else
        {
            int charCount = 'Z' - 'A';
            string password = string.Empty;
            for (int i = 0; i < 6; ++i)
            {
                int code = Random.Shared.Next() % charCount;
                password += (char)('A' + code);
            }
            user.Password = passwordHasher.HashPassword(user, password);
            string textPassword = $"{password.Substring(0, 3)}-{password.Substring(3)}";
            var email = emailService.CreateEmail(dto.Email, "Authentication", null, new { Password = textPassword });
            emailSenderService.SendEmail(email!);
        }
        dbContext.SaveChanges();
    }

    [HttpPost]
    [Route("{clientId}")]
    [Produces(typeof(JwtDto))]
    public IActionResult Authenticate(int clientId, [FromBody] PasswordDto dto)
    {
        var client = dbContext.Clients.FirstOrDefault(c => c.Id == clientId);
        if (client == null || string.IsNullOrWhiteSpace(client?.Password))
            return BadRequest();
        JwtDto? jwtDto = TryAuthenticate(dto, client, Roles.Client);
        if (jwtDto != null)
            return Ok(jwtDto);
        return new UnauthorizedObjectResult(new { PasswordExpired = ExpirePasswordIfNeeded(client) });
    }

    private bool ExpirePasswordIfNeeded(IAunthenticable client)
    {
        client.FailedLoginAtemptCount += 1;
        if (client.FailedLoginAtemptCount == 3)
        {
            ResetPassword(client);
            return true;
        }
        dbContext.SaveChanges();
        return false;
    }

    private void ResetPassword(IAunthenticable user)
    {
        user.FailedLoginAtemptCount = 0;
        user.Password = null;
        dbContext.SaveChanges();
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
        CreatePassword(dto, admin);
        return Ok(new IdDto(admin.Id));
    }

    [HttpPost]
    [Route("admin/{adminId}")]
    [Produces(typeof(JwtDto))]
    public IActionResult AuthenticateAdmin(int adminId, [FromBody] PasswordDto dto)
    {
        var admin = dbContext.Admins.FirstOrDefault(c => c.Id == adminId);
        if (admin == null || string.IsNullOrWhiteSpace(admin?.Password))
            return BadRequest();
        JwtDto? jwtDto = TryAuthenticate(dto, admin, Roles.Admin);
        if (jwtDto != null)
            return Ok(jwtDto);
        return new UnauthorizedObjectResult(new { PasswordExpired = ExpirePasswordIfNeeded(admin) });
    }

    private JwtDto? TryAuthenticate<T>(PasswordDto dto, T admin, string role) where T : class, IAunthenticable
    {
        var passwordHasher = new PasswordHasher<T>();
        switch (passwordHasher.VerifyHashedPassword(admin, admin.Password, dto.Password))
        {
            case PasswordVerificationResult.Success:
                ResetPassword(admin);
                return GenerateJWT(admin.Email, role);
            case PasswordVerificationResult.Failed:
                break;
            case PasswordVerificationResult.SuccessRehashNeeded:
                admin.Password = passwordHasher.HashPassword(admin, dto.Password);
                admin.FailedLoginAtemptCount = 0;
                dbContext.SaveChanges();
                return GenerateJWT(admin.Email, role);
        }

        return null;
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
