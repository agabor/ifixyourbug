using IFYB.Models;

namespace IFYB.Entities;

public class Admin : IAunthenticable
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string? Password { get; set; }
    public int FailedLoginAtemptCount { get; set; }
    public string Role => Roles.Admin;

    public Admin(string email)
    {
        Email = email;
    }
}