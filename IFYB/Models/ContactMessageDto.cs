namespace IFYB.Models;
public class ContactMessageDto
{
    public string Name { get; set; }
    public string Email { get; set; }

    public string Text { get; set; }

    public ContactMessageDto(string name, string email, string text)
    {
        Name = name;
        Email = email;
        Text = text;
    }
}