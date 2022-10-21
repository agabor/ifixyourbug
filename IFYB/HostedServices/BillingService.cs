using System.Globalization;
using System.Text.Json;
using System.Threading.Channels;
using IFYB.Entities;
using IFYB.Models;
using IFYB.Services;
using Microsoft.Extensions.Options;
using Stripe;
using SzamlazzHu;

namespace IFYB.HostedServices;

public class BillingService : BackgroundService
{
    private readonly Channel<Entities.Order> billingChanel;
    private readonly Channel<Email> emailChanel;
    private readonly IServiceProvider serviceProvider;
    private readonly Settings offer;
    private readonly BillingOptions billingOptions;

    public BillingService(Channel<Entities.Order> billingChanel, IOptions<BillingOptions> billingOptions, IServiceProvider serviceProvider, Settings offer, Channel<Email> emailChanel)
    {
        this.billingChanel = billingChanel;
        this.serviceProvider = serviceProvider;
        this.offer = offer;
        this.billingOptions = billingOptions.Value;
        this.emailChanel = emailChanel;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach(var order in billingChanel.Reader.ReadAllAsync(stoppingToken))
        {
            using var scope = serviceProvider.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<EventLogService<EmailBackgroundService>>();
            try {
                logger.LogClientEvent(order.ClientId, "Billing started");
                var request = new CreateInvoiceRequest();
                request.AuthenticationData.ApiKey = billingOptions.ApiKey;

                request.Header.Paid = true;
                request.Header.PaymentType = "credit card";
                request.Header.InvoiceNumberPrefix = billingOptions.InvoiceNumberPrefix;
                request.Header.Language = InvoiceLanguage.English;
                request.Header.Currency = order.Currency?.ToUpper() ?? "EUR";

                request.Customer.Name = order.CustomerName;
                if (order.Country != null)
                    request.Customer.CustomerAddress.Country = new RegionInfo(order.Country).EnglishName;
                request.Customer.CustomerAddress.PostalCode = order.PostalCode;
                request.Customer.CustomerAddress.City = order.City;
                request.Customer.CustomerAddress.StreetAddress = $"{order.Line1} {order.Line2}";
                request.Customer.TaxNumber = order.TaxId;

                decimal price = request.Header.Currency == "EUR" ? offer.EurPrice : offer.UsdPrice;
                decimal vatAmount = (decimal)order.AmountTax! / 100M;
                double vatPercentage = ((double)order.AmountTax / (double)order.AmountSubtotal!) * 100.0;

                request.Items = new List<SzamlazzHu.InvoiceItem> {
                    new SzamlazzHu.InvoiceItem {
                        Name = "Bug Fixing Service",
                        Quantity = 1,
                        UnitOfQuantity = "piece",
                        UnitPrice = price,
                        VatRate = vatPercentage.ToString("F1",  CultureInfo.InvariantCulture),
                        NetPrice = price,
                        VatAmount = vatAmount,
                        GrossAmount = price + vatAmount,
                        Comment = $"Order number: #{order.Number}"
                    }
                };

                var customerService = new CustomerService();
                var customer = customerService.Get(order.StripeCustomerId, new CustomerGetOptions{ Expand = new List<string> { "tax" } });
                var tax = customer.Tax;

                request.Header.Comment = "Paid";
                if (order.TaxIdType != "eu_vat" && tax.AutomaticTax != "supported")
                    request.Header.Comment += "\nVAT reverse charge";

                var api = new SzamlazzHuApi();
                var response = await api.CreateInvoice(request);
                var emailCreationService = scope.ServiceProvider.GetRequiredService<EmailCreationService>();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var orderFromScope = dbContext.Orders.First(o => o.Id == order.Id);
                orderFromScope.TaxCountry = tax.Location.Country;
                orderFromScope.TaxState = tax.Location.State;
                orderFromScope.AutomaticTax = tax.AutomaticTax;
                orderFromScope.InvoiceNumber = response.InvoiceNumber;
                dbContext.SaveChanges();
                
                dbContext.Entry(orderFromScope).Reference(o => o.Client).Load();
                var client = orderFromScope.Client!;
                var email = emailCreationService.CreateEmail(client.Id, client.Email, "OrderPayed", orderFromScope, new { Name = client.Name, Workdays = offer.Workdays });
                email!.File = response.InvoicePdf;
                email!.FileName = $"{response.InvoiceNumber}.pdf";
                emailChanel.Writer.TryWrite(email);

                var admins = dbContext.Admins.ToList();
                foreach(var admin in admins) {
                    var adminEmail = emailCreationService.CreateEmail(admin.Id, admin.Email, "OrderPayedToAdmin", orderFromScope, new { Name = client.Name, Workdays = offer.Workdays }, true);
                    adminEmail!.File = response.InvoicePdf;
                    adminEmail!.FileName = $"{response.InvoiceNumber}.pdf";
                    emailChanel.Writer.TryWrite(adminEmail);
                }
                
            } catch (Exception e) {
                var errorHandlerService = scope.ServiceProvider.GetRequiredService<ErrorHandlerService>();
                errorHandlerService.OnException(e, null);
            }
        }
    }
}
