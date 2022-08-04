
using System.Net.Mail;
using System.Threading.Channels;
using IFYB.Entities;

namespace IFYB.Services;

public interface IEmailSenderService
{
    bool SendEmail(Email email);
}

public class EmailSenderService : IEmailSenderService
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<EmailSenderService> logger;
    private readonly Channel<Email> emailChannel;
    private readonly ErrorHandlerService errorHandlerService;
    private readonly SmtpClient smtpClient;
    public EmailSenderService(SmtpClient smtpClient, ApplicationDbContext dbContext, ILogger<EmailSenderService> logger, Channel<Email> emailChannel, ErrorHandlerService errorHandlerService) {
        this.smtpClient = smtpClient;
        this.dbContext = dbContext;
        this.logger = logger;
        this.emailChannel = emailChannel;
        this.errorHandlerService = errorHandlerService;
    }

    public bool SendEmail(Email email)
    {
        var from = new MailAddress("gabor@ifixyourbug.com", "I Fix Your Bug", System.Text.Encoding.UTF8);
        var to = new MailAddress(email.ToEmail);
        var message = new MailMessage(from, to);
        message.Subject = email.Subject;
        message.Body = email.Text;
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.SubjectEncoding = System.Text.Encoding.UTF8;
        var mimeType = new System.Net.Mime.ContentType("text/html");
        AlternateView alternate = AlternateView.CreateAlternateViewFromString(email.Html, mimeType);
        message.AlternateViews.Add(alternate);


        try {
            if (email.File != null)
            {
                using var stream = new MemoryStream(email.File);
                message.Attachments.Add(new Attachment(stream, email.FileName, "application/pdf"));
                smtpClient.Send(message);
            } else {
                smtpClient.Send(message);
            }
        } catch (Exception e) {
            errorHandlerService.OnException(e, null);
            logger.Log(LogLevel.Warning, e, e.Message);
            if (email.RetryCount < 3) {
                email.RetryCount += 1;
                logger.Log(LogLevel.Information, $"Retry to send e-mail - {email.RetryCount}");
                emailChannel.Writer.TryWrite(email);
            }
            return false;
        }
        email.Sent = true;
        dbContext.SaveChanges();
        return true;
    }
}

public class FakeEmailSenderService : IEmailSenderService
{
    public bool SendEmail(Email email)
    {
        return true;
    }
}
