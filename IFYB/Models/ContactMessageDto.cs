namespace IFYB.Models;
public class ContactMessageDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Text { get; set; }
    public DateTime DateTime { get; set; }

    public ContactMessageDto(string name, string email, string text, DateTime dateTime)
    {
        Name = name;
        Email = email;
        Text = text;
        DateTime = dateTime;
    }
}