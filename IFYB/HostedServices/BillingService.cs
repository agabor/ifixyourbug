using System.Text.Json;
using System.Threading.Channels;
using IFYB.Entities;
using IFYB.Models;
using IFYB.Services;
using Microsoft.Extensions.Options;
using SzamlazzHu;

namespace IFYB.HostedServices;

public class BillingService : BackgroundService
{
    private readonly Channel<Order> billingChanel;
    private readonly ILogger<BillingService> logger;
    private readonly IServiceProvider serviceProvider;
    private readonly OfferDto offer;
    private readonly BillingOptions billingOptions;

    public BillingService(Channel<Order> billingChanel, IOptions<BillingOptions> billingOptions, ILogger<BillingService> logger, IServiceProvider serviceProvider, OfferDto offer)
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
                request.Customer.CustomerAddress.Country = order.Country;
                request.Customer.CustomerAddress.PostalCode = order.PostalCode;
                request.Customer.CustomerAddress.City = order.City;
                request.Customer.CustomerAddress.StreetAddress = $"{order.Line1} {order.Line2}";
                request.Customer.TaxNumber = order.TaxId;

                decimal price = request.Header.Currency == "EUR" ? offer.EurPrice : offer.UsdPrice;
                string vatRate = GetVatRate(order);
                decimal vatAmount = vatRate == "27" ? price * 0.27M : 0M;

                request.Items = new List<InvoiceItem> {
                    new InvoiceItem {
                        Name = "Bug Fixing Service",
                        Quantity = 1,
                        UnitOfQuantity = "piece",
                        UnitPrice = price,
                        VatRate = vatRate,
                        NetPrice = price,
                        VatAmount = vatAmount,
                        GrossAmount = price + vatAmount
                    }
                };

                var api = new SzamlazzHuApi();
                var response = await api.CreateInvoice(request);
                var scope = serviceProvider.CreateScope();
                var emailService = scope.ServiceProvider.GetRequiredService<EmailService>();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var orderFromScope = dbContext.Orders.First(o => o.Id == order.Id);
                using var stream = new MemoryStream(response.InvoicePdf);
                emailService.SendInvoice(orderFromScope, stream, response.InvoiceNumber);
            } catch (Exception e) {
                logger.Log(LogLevel.Error, e, e.Message);
            }
        }
        logger.Log(LogLevel.Information, "BillingService finished");
    }

    private string GetVatRate(Order order)
    {
        if (!string.IsNullOrWhiteSpace(order.TaxIdType)) {
            if (order.Country?.ToUpper() == "HU")
                return billingOptions.VatHunCompany;
            if (order.TaxIdType == "eu_vat")
                return billingOptions.VatEuCompany;
            return billingOptions.Vat3rdCompany;
        }
        if (order.Country?.ToUpper() == "HU")
            return billingOptions.VatHunPrivate;
        var euCountryCodes = new List<string> { "AT", "BE", "BG", "HR", "CY", "CZ", "DK", "EE", "FI", "FR", "DE", "GR", "HU", "IE", "IT", "LV", "LT", "LU", "MT", "NL", "PL", "PT", "RO", "SK", "SI", "ES", "SE" };
        if (euCountryCodes.Contains(order.Country?.ToUpper() ?? ""))
            return billingOptions.VatEuPrivate;
        return billingOptions.Vat3rdPrivate;
    }
}