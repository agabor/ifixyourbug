namespace IFYB.Models;

public class ClientDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public ClientDto(string name, string email)
    {
        Name = name;
        Email = email;
    }
}