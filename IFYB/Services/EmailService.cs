
using System.Net.Mail;
using System.Net.Mime;
using System.Text.Json;
using IFYB.Entities;
using Microsoft.Extensions.Options;
using Scriban;

namespace IFYB.Services;
public class EmailService
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<EmailService> logger;
    private readonly AppOptions appOptions;
    private readonly SmtpClient smtpClient;
    public EmailService(SmtpClient smtpClient, ApplicationDbContext dbContext, IOptions<AppOptions> appOptions, ILogger<EmailService> logger) {
        this.smtpClient = smtpClient;
        this.dbContext = dbContext;
        this.logger = logger;
        this.appOptions = appOptions.Value;
    }


    private void SendEmail(string toEmail, string subject, string text, string html, Stream? file = null, string? fileName = null)
    {
        if (!appOptions.SendEmails)
            return;
        var from = new MailAddress("gabor@ifixyourbug.com", "I Fix Your Bug", System.Text.Encoding.UTF8);
        var to = new MailAddress(toEmail);
        var message = new MailMessage(from, to);
        message.Subject = subject;
        message.Body = text;
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.SubjectEncoding = System.Text.Encoding.UTF8;
        if (file != null)
            message.Attachments.Add(new Attachment(file, fileName, "application/pdf"));

        var mimeType = new System.Net.Mime.ContentType("text/html");
        AlternateView alternate = AlternateView.CreateAlternateViewFromString(html, mimeType);
        message.AlternateViews.Add(alternate);

        smtpClient.Send(message);
    }


    public void SendOrderStateEmail(Order order, string? message = null)
    {
        dbContext.Entry(order).Reference(o => o.Client).Load();
        var client = order.Client!;
        switch (order.State)
        {
            case OrderState.Accepted:
                string paymentLink = $"{appOptions.BaseUrl}/checkout/{order.PaymentToken}";
                SendEmail(client.Email, "OrderAccept", order, new { client.Name, PaymentLink = paymentLink });
                break;
            case OrderState.Rejected:
                SendEmail(client.Email, "OrderReject", order, new { client.Name, Message = message });
                break;
            case OrderState.Completed:
                SendEmail(client.Email, "OrderComplete", order, new { client.Name });
                break;
            case OrderState.Refundable:
                SendEmail(client.Email, "OrderRefund", order, new { client.Name });
                break;
        }
    }

    public void SendEmail(string toEmail, string jsonTemplate, Order? order, object data, Stream? file = null, string? fileName = null) {
        string? link = order != null ? $"{appOptions.BaseUrl}/my-orders/{order.Number.Remove(0,1)}" : null;
        var json = Template.Parse(System.IO.File.ReadAllText($"Email/{jsonTemplate}.sbn")).Render(data);
        var emailContent = JsonSerializer.Deserialize<EmailContent>(json, new JsonSerializerOptions{ PropertyNameCaseInsensitive = true });
        if (emailContent == null) {
            logger.Log(LogLevel.Error, "Could not parse e-mail content.");
            return;
        }
        emailContent.OrderLink = link;
        var text = Template.Parse(System.IO.File.ReadAllText("Email/textEmail.sbn")).Render(emailContent);
        var html = Template.Parse(System.IO.File.ReadAllText("Email/htmlEmail.sbn")).Render(emailContent);
        SendEmail(toEmail, emailContent.Title, text, html, file, fileName);
    }

    internal void SendInvoice(Order order, MemoryStream stream, string invoiceNumber)
    {
        dbContext.Entry(order).Reference(o => o.Client).Load();
        var client = order.Client!;
        SendEmail(client.Email, "OrderPayed", order, new { client.Name }, stream, $"{invoiceNumber}.pdf");
    }
}