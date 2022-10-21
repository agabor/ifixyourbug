namespace IFYB.Models;
public class ContactEmailDto
{
    public string Subject { get; set; }
    public string Text { get; set; }

    public ContactEmailDto(string subject, string text)
    {
        Subject = subject;
        Text = text;
    }
}