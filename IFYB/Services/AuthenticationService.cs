using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IFYB.Entities;
using IFYB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IFYB.Services;

public class AuthenticationService
{
    private readonly ApplicationDbContext dbContext;
    private readonly JwtOptions jwtOptions;
    private readonly  SecurityKey securityKey;
    private readonly AppOptions appOptions;
    private readonly  EmailCreationService emailService;
    private readonly  IEmailSenderService emailSenderService;
    private readonly  EventLogService<AuthenticationService> eventLogService;

    public AuthenticationService(IOptions<AppOptions> appOptions, IOptions<JwtOptions> jwtOptions, SecurityKey securityKey, ApplicationDbContext dbContext, EmailCreationService emailService, IEmailSenderService emailSenderService, EventLogService<AuthenticationService> eventLogService)
    {
        this.jwtOptions = jwtOptions.Value;
        this.securityKey = securityKey;
        this.dbContext = dbContext;
        this.appOptions = appOptions.Value;
        this.emailService = emailService;
        this.emailSenderService = emailSenderService;
        this.eventLogService = eventLogService;
    }

    public void ResetPassword(IAunthenticable user)
    {
        user.FailedLoginAtemptCount = 0;
        user.Password = null;
        dbContext.SaveChanges();
    }
    public bool ExpirePasswordIfNeeded(IAunthenticable client)
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

    public JwtDto? TryAuthenticate<T>(PasswordDto dto, T user) where T : class, IAunthenticable
    {
        var passwordHasher = new PasswordHasher<T>();
        switch (passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password))
        {
            case PasswordVerificationResult.SuccessRehashNeeded:
            case PasswordVerificationResult.Success:
                ResetPassword(user);
                eventLogService.LogUserEvent(user, "Successful authentication");
                return GenerateJWT(user.Email, user.Role);
            case PasswordVerificationResult.Failed:
                break;
        }
        eventLogService.LogUserEvent(user, "Authentication failed");
        return null;
    }

    public JwtDto GenerateJWT(string email, string role)
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
    public void CreatePassword<T>(EmailDto dto, T user) where T : class, IAunthenticable
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
        eventLogService.LogUserEvent(user, "Password is created");
    }
}