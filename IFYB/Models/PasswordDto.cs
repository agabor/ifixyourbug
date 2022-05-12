namespace IFYB.Models;

public class PasswordDto
{
    public string Password { get; set; }
    public PasswordDto(string password)
    {
        Password = password;
    }
}