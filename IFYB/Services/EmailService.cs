
using System.Net.Mail;
using System.Net.Mime;
using IFYB.Entities;
using Microsoft.Extensions.Options;
using Scriban;

namespace IFYB.Services;
public class EmailService
{
    private readonly ApplicationDbContext dbContext;
    private readonly AppOptions appOptions;
    private readonly SmtpClient smtpClient;
    public EmailService(SmtpClient smtpClient, ApplicationDbContext dbContext, IOptions<AppOptions> appOptions){
        this.smtpClient = smtpClient;
        this.dbContext = dbContext;
        this.appOptions = appOptions.Value;
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

        smtpClient.Send(message);
    }

    public void SendEmailWithPdf(string toEmail, string subject, string text, string html, Stream file, string fileName)
    {
        var from = new MailAddress("gabor@ifixyourbug.com", "I Fix Your Bug", System.Text.Encoding.UTF8);
        var to = new MailAddress(toEmail);
        var message = new MailMessage(from, to);
        message.Subject = subject;
        message.Body = text;
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.SubjectEncoding = System.Text.Encoding.UTF8;
        message.Attachments.Add(new Attachment(file, fileName, "application/pdf"));

        var mimeType = new System.Net.Mime.ContentType("text/html");
        AlternateView alternate = AlternateView.CreateAlternateViewFromString(html, mimeType);
        message.AlternateViews.Add(alternate);

        smtpClient.Send(message);
    }


    public void SendOrderStateEmail(Order order, string? message = null)
    {
        string link = $"{appOptions.BaseUrl}/my-orders/{order.Number.Remove(0,1)}";
        string subject = "";
        string text = "";
        string html = "";

        dbContext.Entry(order).Reference(o => o.Client).Load();
        var client = order.Client!;
        switch (order.State)
        {
            case OrderState.Accepted:
                subject = $"We will process your order!";
                string paymentLink = $"{appOptions.BaseUrl}/checkout/{order.PaymentToken}";
                text = Template.Parse(System.IO.File.ReadAllText("Email/PlainText/OrderAccept.sbn")).Render(new { client.Name, PaymentLink = paymentLink, Link = link, Message = message });
                html = Template.Parse(System.IO.File.ReadAllText("Email/OrderAccept.sbn")).Render(new { client.Name, PaymentLink = paymentLink, Link = link, Message = message });
                break;
            case OrderState.Rejected:
                subject = $"We rejected your order!";
                text = Template.Parse(System.IO.File.ReadAllText("Email/PlainText/OrderReject.sbn")).Render(new { client.Name, Link = link, Message = message });
                html = Template.Parse(System.IO.File.ReadAllText("Email/OrderReject.sbn")).Render(new { client.Name, Link = link, Message = message });
                break;
            case OrderState.Completed:
                subject = $"We've fixed your bug!";
                text = Template.Parse(System.IO.File.ReadAllText("Email/PlainText/OrderComplete.sbn")).Render(new { client.Name, Link = link, Message = message });
                html = Template.Parse(System.IO.File.ReadAllText("Email/OrderComplete.sbn")).Render(new { client.Name, Link = link, Message = message });
                break;
            case OrderState.Refundable:
                subject = $"We can't fix your bug!";
                text = Template.Parse(System.IO.File.ReadAllText("Email/PlainText/OrderRefund.sbn")).Render(new { client.Name, Link = link, Message = message });
                html = Template.Parse(System.IO.File.ReadAllText("Email/OrderRefund.sbn")).Render(new { client.Name, Link = link, Message = message });
                break;
        }

        if (subject != "")
        {
            SendEmail(client.Email, subject, text, html);
        }
    }

    internal void SendInvoice(Order order, MemoryStream stream, string invoiceNumber)
    {
        dbContext.Entry(order).Reference(o => o.Client).Load();
        var client = order.Client!;
        var subject = "Thank you for your payment!";
        var text = Template.Parse(System.IO.File.ReadAllText("Email/PlainText/OrderPayed.sbn")).Render(new { client.Name });
        var html = Template.Parse(System.IO.File.ReadAllText("Email/OrderPayed.sbn")).Render(new { client.Name });
        SendEmailWithPdf(client.Email, subject, text, html, stream, $"{invoiceNumber}.pdf");
    }
}