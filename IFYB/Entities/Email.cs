using System.ComponentModel.DataAnnotations.Schema;

namespace IFYB.Entities;
public class Email
{
    public int Id { get; set; }
    public string ToEmail {get; set; }
    public string Subject {get; set; }
    public string Text {get; set; }
    public string Html {get; set; }
    public bool Sent {get; set; }
    public DateTime Created {get; set; }
    public int RetryCount { get; set; }
    public string? FileName {get; set; }

    [NotMapped]
    public byte[]? File {get; set; }

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