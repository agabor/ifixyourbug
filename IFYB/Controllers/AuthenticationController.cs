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

namespace IFYB.Controllers;

[ApiController]
[Route("api/authenticate")]
public class AuthenticationController : BaseController
{
    private IConfiguration Configuration { get; }
    private SecurityKey SecurityKey { get; }
    private SmtpClient SmtpClient { get; }

    public AuthenticationController(ApplicationDbContext dbContext, IConfiguration configuration, SecurityKey securityKey, SmtpClient smtpClient) : base(dbContext)
    {
        Configuration = configuration;
        SecurityKey = securityKey;
        SmtpClient = smtpClient;
    }


    [HttpGet]
    [Route("check-jwt")]
    public IActionResult CheckJwt()
    {
        if (GetClient() == null)
            return Forbid();
        return Ok();
    }

    [HttpPost]
    [Produces(typeof(IdDto))]
    public IActionResult StartSession([FromBody] EmailDto dto)
    {
        dbContext.Database.EnsureCreated();
        var client = dbContext.Clients.FirstOrDefault(c => c.Email == dto.Email);
        if (client == null) {
            client = new Client(dto.Email);
            dbContext.Clients.Add(client);
        }
        var passwordHasher = new PasswordHasher<Client>();
#if DEBUG
        client.Password = passwordHasher.HashPassword(client, "123456");
#else
        int charCount = 10 + 'Z' - 'A';
        string password = string.Empty;
        for (int i = 0; i < 6; ++i) {
            int code = Random.Shared.Next() % charCount;
            if (code < 10)
            {
                password += (char)('0' + code);
            } else {
                password += (char)('A' + code - 10);
            }
        }
        client.Password = passwordHasher.HashPassword(client, password);
        var from = new MailAddress("gabor@ifixyourbug.com", "I Fix Your Bug", System.Text.Encoding.UTF8);
        var to = new MailAddress(dto.Email);
        MailMessage message = new MailMessage(from, to);
        message.Subject = $"Confirmation code: {password}";
        message.Body = $"Confirm your email address\nYour confirmation code is below — enter it in your open browser window and we’ll help you get signed in.\n\n{password}\n\nIf you didn’t request this email, there’s nothing to worry about — you can safely ignore it.";
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.SubjectEncoding = System.Text.Encoding.UTF8;
        
        string body = Template.Parse(System.IO.File.ReadAllText("Email/Authentication.sbn")).Render(new { Password = password });

        var mimeType = new System.Net.Mime.ContentType("text/html");
        AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
        message.AlternateViews.Add(alternate);

        SmtpClient.Send(message);
#endif
        dbContext.SaveChanges();
        return Ok(new IdDto(client.Id));
    }

    [HttpPost]
    [Route("{clientId}")]
    [Produces(typeof(JwtDto))]
    public IActionResult Authenticate(int clientId, [FromBody] PasswordDto dto)
    {
        dbContext.Database.EnsureCreated();
        var client = dbContext.Clients.FirstOrDefault(c => c.Id == clientId);
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
                dbContext.SaveChanges();
                return Ok(GenerateJWT(new Claim(ClaimTypes.Email, client.Email)));
        }
        return Forbid();
    }

    [HttpGet]
    [Route("admin/check-jwt")]
    public IActionResult CheckAdminJwt()
    {
        if (User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value != "admin")
            return Forbid();
        return Ok();
    }

    [HttpPost]
    [Route("admin")]
    [Produces(typeof(IdDto))]
    public IActionResult StartAdminSession([FromBody] EmailDto dto)
    {
        dbContext.Database.EnsureCreated();
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
        dbContext.Database.EnsureCreated();
        var admin = dbContext.Admins.FirstOrDefault(c => c.Id == adminId);
        if (admin == null)
            return Forbid();
        var passwordHasher = new PasswordHasher<Admin>();
        switch (passwordHasher.VerifyHashedPassword(admin, admin.Password, dto.Password))
        {
            case PasswordVerificationResult.Success:
                return Ok(GenerateJWT(new Claim(ClaimTypes.Role, "admin")));
            case PasswordVerificationResult.Failed:
                return Forbid();
            case PasswordVerificationResult.SuccessRehashNeeded:
                admin.Password = passwordHasher.HashPassword(admin, dto.Password);
                dbContext.SaveChanges();
                return Ok(GenerateJWT(new Claim(ClaimTypes.Role, "admin")));
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
