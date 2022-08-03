
using System.Net.Mail;
using IFYB.Entities;
using Microsoft.Extensions.Options;

namespace IFYB.Services;

public class EmailSenderService
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<EmailCreationService> logger;
    private readonly AppOptions appOptions;
    private readonly SmtpClient smtpClient;
    public EmailSenderService(SmtpClient smtpClient, ApplicationDbContext dbContext, IOptions<AppOptions> appOptions, ILogger<EmailCreationService> logger) {
        this.smtpClient = smtpClient;
        this.dbContext = dbContext;
        this.logger = logger;
        this.appOptions = appOptions.Value;
    }

    public void SendEmail(Email email)
    {
        if (!appOptions.SendEmails)
            return;
        var from = new MailAddress("gabor@ifixyourbug.com", "I Fix Your Bug", System.Text.Encoding.UTF8);
        var to = new MailAddress(email.ToEmail);
        var message = new MailMessage(from, to);
        message.Subject = email.Subject;
        message.Body = email.Text;
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.SubjectEncoding = System.Text.Encoding.UTF8;
        if (email.File != null)
            message.Attachments.Add(new Attachment(email.File, email.FileName, "application/pdf"));

        var mimeType = new System.Net.Mime.ContentType("text/html");
        AlternateView alternate = AlternateView.CreateAlternateViewFromString(email.Html, mimeType);
        message.AlternateViews.Add(alternate);

        smtpClient.Send(message);
        email.Sent = true;
        dbContext.SaveChanges();
    }
}