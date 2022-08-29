using System.Net.Mime;
using System.Text.Json;
using IFYB.Entities;
using IFYB.Models;
using Microsoft.Extensions.Options;
using Scriban;

namespace IFYB.Services;
public class EmailCreationService
{
    private readonly ApplicationDbContext dbContext;
    private readonly EventLogService<EmailCreationService> eventLogService;
    private readonly OfferDto offerDto;
    private readonly AppOptions appOptions;
    public EmailCreationService(ApplicationDbContext dbContext, IOptions<AppOptions> appOptions, EventLogService<EmailCreationService> eventLogService, OfferDto offerDto) {
        this.dbContext = dbContext;
        this.eventLogService = eventLogService;
        this.offerDto = offerDto;
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
                return CreateEmail(client.Email, "OrderConfirm", order, new { client.Name, PaymentLink = paymentLink, Workdays = offerDto.Workdays });
            case OrderState.Rejected:
                return CreateEmail(client.Email, "OrderReject", order, new { client.Name, Message = message });
            case OrderState.Completed:
                return CreateEmail(client.Email, "OrderComplete", order, new { client.Name });
            case OrderState.Refundable:
                return CreateEmail(client.Email, "OrderRefund", order, new { client.Name });
            case OrderState.Canceled:
                return CreateEmail(client.Email, "OrderCancel", order, new { client.Name });
            case OrderState.Editable:
                return CreateEmail(client.Email, "OrderEditable", order, new { client.Name, Message = message });
        }
        return null;
    }

    public Email? CreateEmail(string toEmail, string jsonTemplate, Order? order, object data, bool? toAdmin = false) {
        string path = toAdmin == true ? "admin" : "my-orders";
        string? link = order != null ? $"{appOptions.BaseUrl}/{path}/{order.Number}" : null;
        var json = Template.Parse(System.IO.File.ReadAllText($"Email/{jsonTemplate}.sbn")).Render(data);
        var emailContent = JsonSerializer.Deserialize<EmailContent>(json, new JsonSerializerOptions{ PropertyNameCaseInsensitive = true });
        if (emailContent == null) {
            eventLogService.Log(LogLevel.Error, "Could not parse e-mail content.");
            return null;
        }
        emailContent.OrderNumber = order?.Number;
        emailContent.OrderLink = link;
        emailContent.ToAdmin = toAdmin;
        if (order?.State == OrderState.Submitted)
        {
            emailContent.EurPrice = order.EurPrice;
            emailContent.UsdPrice = order.UsdPrice;
        }
        var text = Template.Parse(System.IO.File.ReadAllText("Email/TextEmail.sbn")).Render(emailContent);
        var html = Template.Parse(System.IO.File.ReadAllText("Email/HtmlEmail.sbn")).Render(emailContent);
        var email = new Email(toEmail, emailContent.Title, text, html);
        email = dbContext.Emails.Add(email).Entity;
        dbContext.SaveChanges();
        return email;
    }
}
