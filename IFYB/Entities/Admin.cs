using IFYB.Models;

namespace IFYB.Entities;

public class Admin
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string? Password { get; set; }

    public Admin(string email)
    {
        Email = email;
    }
}