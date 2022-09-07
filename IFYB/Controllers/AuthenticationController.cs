using Microsoft.AspNetCore.Mvc;
using IFYB.Entities;
using IFYB.Models;
using Microsoft.IdentityModel.Tokens;
using IFYB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace IFYB.Controllers;

[ApiController]
[Route("api/authenticate")]
public class AuthenticationController : BaseController
{
    private readonly AuthenticationService authenticationService;

    public AuthenticationController(ApplicationDbContext dbContext, AuthenticationService authenticationService) : base(dbContext)
    {
        this.authenticationService = authenticationService;
    }


    [HttpGet]
    [Route("check-jwt")]
    [Authorize(Policy = Policies.ClientOnly)]
    public IActionResult CheckJwt()
    {
        Client? client = GetClient();
        if (client != null)
            return base.Ok(authenticationService.GenerateJWT(client));
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
                client = dbContext.Clients.Add(client).Entity;
            }
            else
            {
                return Unauthorized();
            }
        }
        authenticationService.CreatePassword(dto, client);
        return Ok(new IdDto(client.Id));
    }


    [HttpPost]
    [Route("{clientId}")]
    [Produces(typeof(JwtDto))]
    public IActionResult Authenticate(int clientId, [FromBody] PasswordDto dto)
    {
        var client = dbContext.Clients.FirstOrDefault(c => c.Id == clientId);
        if (client == null || string.IsNullOrWhiteSpace(client?.Password))
            return BadRequest();
        JwtDto? jwtDto = authenticationService.TryAuthenticate(dto, client);
        if (jwtDto != null) {
            return Ok(jwtDto);
        }

        bool passwordExpired = authenticationService.ExpirePasswordIfNeeded(client);
        return new UnauthorizedObjectResult(new { PasswordExpired = passwordExpired });
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
        authenticationService.CreatePassword(dto, admin);
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
        JwtDto? jwtDto = authenticationService.TryAuthenticate(dto, admin);
        if (jwtDto != null)
            return Ok(jwtDto);
        return new UnauthorizedObjectResult(new { PasswordExpired = authenticationService.ExpirePasswordIfNeeded(admin) });
    }

}
