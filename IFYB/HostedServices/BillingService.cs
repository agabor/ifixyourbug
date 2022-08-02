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
    private readonly ILogger<BillingService> logger;
    private readonly IServiceProvider serviceProvider;
    private readonly OfferDto offer;
    private readonly BillingOptions billingOptions;

    public BillingService(Channel<Entities.Order> billingChanel, IOptions<BillingOptions> billingOptions, ILogger<BillingService> logger, IServiceProvider serviceProvider, OfferDto offer)
    {
        this.billingChanel = billingChanel;
        this.logger = logger;
        this.serviceProvider = serviceProvider;
        this.offer = offer;
        this.billingOptions = billingOptions.Value;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.Log(LogLevel.Information, "BillingService started");
        await foreach(var order in billingChanel.Reader.ReadAllAsync(stoppingToken))
        {
            try {
                logger.Log(LogLevel.Information, "Billing started");
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
                        GrossAmount = price + vatAmount
                    }
                };

                var customerService = new CustomerService();
                var customer = customerService.Get(order.Client!.StripeId, new CustomerGetOptions{ Expand = new List<string> { "tax" } });
                var tax = customer.Tax;

                request.Header.Comment = "Paid";
                if (order.TaxIdType != "eu_vat" && tax.AutomaticTax != "supported")
                    request.Header.Comment += "\nVAT reverse charge";

                var api = new SzamlazzHuApi();
                var response = await api.CreateInvoice(request);
                var scope = serviceProvider.CreateScope();
                var emailService = scope.ServiceProvider.GetRequiredService<EmailService>();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var orderFromScope = dbContext.Orders.First(o => o.Id == order.Id);
                orderFromScope.TaxCountry = tax.Location.Country;
                orderFromScope.TaxState = tax.Location.State;
                orderFromScope.AutomaticTax = tax.AutomaticTax;
                orderFromScope.InvoiceNumber = response.InvoiceNumber;
                dbContext.SaveChanges();


                using var stream = new MemoryStream(response.InvoicePdf);
              
                emailService.SendInvoice(orderFromScope, stream, response.InvoiceNumber);
            } catch (Exception e) {
                logger.Log(LogLevel.Error, e, e.Message);
            }
        }
        logger.Log(LogLevel.Information, "BillingService finished");
    }
}