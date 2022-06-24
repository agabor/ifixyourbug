
using System.Net.Mail;

namespace IFYB.Services;
public class EmailService
{

    public SmtpClient SmtpClient { get; }
    public EmailService( SmtpClient smtpClient){
        SmtpClient = smtpClient;
    }

    public void SendEmail(string toEmail, string subject, string text, string html)
    {
        var from = new MailAddress("gabor@ifixyourbug.com", "I Fix Your Bug", System.Text.Encoding.UTF8);
        var to = new MailAddress(toEmail);
        var message = new MailMessage(from, to);
        message.Subject = subject;
        message.Body = text;
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.SubjectEncoding = System.Text.Encoding.UTF8;

        var mimeType = new System.Net.Mime.ContentType("text/html");
        AlternateView alternate = AlternateView.CreateAlternateViewFromString(html, mimeType);
        message.AlternateViews.Add(alternate);

        SmtpClient.Send(message);
    }
}