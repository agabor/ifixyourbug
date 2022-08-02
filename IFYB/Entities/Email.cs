using System.ComponentModel.DataAnnotations.Schema;

public class Email
{
    public string ToEmail {get; set; }
    public string Subject {get; set; }
    public string Text {get; set; }
    public string Html {get; set; }
    public bool Sent {get; set; }
    public DateTime Created {get; set; }

    [NotMapped]
    public Stream? File {get; set; }

    [NotMapped]
    public string? FileName {get; set; }

    public Email(string toEmail, string subject, string text, string html)
    {
        ToEmail = toEmail;
        Subject = subject;
        Text = text;
        Html = html;
        Sent = false;
        Created = DateTime.UtcNow;
    }
}