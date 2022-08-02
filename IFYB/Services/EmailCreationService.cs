using System.Net.Mime;
using System.Text.Json;
using IFYB.Entities;
using Microsoft.Extensions.Options;
using Scriban;

namespace IFYB.Services;
public class EmailCreationService
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<EmailCreationService> logger;
    private readonly EmailSenderService emailSenderService;
    private readonly AppOptions appOptions;
    public EmailCreationService(ApplicationDbContext dbContext, IOptions<AppOptions> appOptions, ILogger<EmailCreationService> logger, EmailSenderService emailSenderService) {
        this.dbContext = dbContext;
        this.logger = logger;
        this.emailSenderService = emailSenderService;
        this.appOptions = appOptions.Value;
    }



    public Email? CreateOrderStateEmail(Order order, string? message = null)
    {
        dbContext.Entry(order).Reference(o => o.Client).Load();
        var client = order.Client!;
        switch (order.State)
        {
            case OrderState.Accepted:
                string paymentLink = $"{appOptions.BaseUrl}/checkout/{order.PaymentToken}";
                return CreateEmail(client.Email, "OrderAccept", order, new { client.Name, PaymentLink = paymentLink });
            case OrderState.Rejected:
                return CreateEmail(client.Email, "OrderReject", order, new { client.Name, Message = message });
            case OrderState.Completed:
                return CreateEmail(client.Email, "OrderComplete", order, new { client.Name });
            case OrderState.Refundable:
                return CreateEmail(client.Email, "OrderRefund", order, new { client.Name });
        }
        return null;
    }

    public Email? CreateEmail(string toEmail, string jsonTemplate, Order? order, object data, Stream? file = null, string? fileName = null) {
        string? link = order != null ? $"{appOptions.BaseUrl}/my-orders/{order.Number.Remove(0,1)}" : null;
        var json = Template.Parse(System.IO.File.ReadAllText($"Email/{jsonTemplate}.sbn")).Render(data);
        var emailContent = JsonSerializer.Deserialize<EmailContent>(json, new JsonSerializerOptions{ PropertyNameCaseInsensitive = true });
        if (emailContent == null) {
            logger.Log(LogLevel.Error, "Could not parse e-mail content.");
            return null;
        }
        emailContent.OrderLink = link;
        var text = Template.Parse(System.IO.File.ReadAllText("Email/textEmail.sbn")).Render(emailContent);
        var html = Template.Parse(System.IO.File.ReadAllText("Email/htmlEmail.sbn")).Render(emailContent);
        var email = new Email(toEmail, emailContent.Title, text, html);
        email = dbContext.Emails.Add(email).Entity;
        dbContext.SaveChanges();
        return email;
    }
}
