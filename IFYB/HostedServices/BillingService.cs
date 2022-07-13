using System.Text.Json;
using System.Threading.Channels;
using IFYB.Entities;
using Microsoft.Extensions.Options;
using SzamlazzHu;

namespace IFYB.HostedServices;

public class BillingService : BackgroundService
{
    private readonly Channel<Order> billingChanel;
    private readonly ILogger<BillingService> logger;
    private readonly BillingOptions billingOptions;
    private readonly SzamlazzHuOptions szamlazzHuOptions;

    public BillingService(Channel<Order> billingChanel, IOptions<SzamlazzHuOptions> szamlazzHuOptions, IOptions<BillingOptions> billingOptions, ILogger<BillingService> logger)
    {
        this.billingChanel = billingChanel;
        this.logger = logger;
        this.billingOptions = billingOptions.Value;
        this.szamlazzHuOptions = szamlazzHuOptions.Value;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.Log(LogLevel.Information, "BillingService started");
        await foreach(var order in billingChanel.Reader.ReadAllAsync(stoppingToken))
        {
            try {
                logger.Log(LogLevel.Information, "Billing started");
                var request = new CreateInvoiceRequest();
                request.AuthenticationData.ApiKey = szamlazzHuOptions.ApiKey;

                request.Seller.BankName = billingOptions.BankName;
                request.Seller.BankAccount = billingOptions.BankAccount;

                request.Customer.Name = order.CustomerName;
                request.Customer.PostalAddress.PostalCode = order.PostalCode;
                request.Customer.PostalAddress.City = order.City;
                request.Customer.PostalAddress.StreetAddress = $"{order.Line1} {order.Line2}";
                request.Customer.TaxNumber = order.TaxId;

                request.Items = new List<InvoiceItem> {
                    new InvoiceItem {
                        Name = "Bug Fixing Service",
                        Quantity = 1,
                        UnitOfQuantity = "piece",
                        UnitPrice = billingOptions.UnitPrice,
                        VatRate = "EU",
                        NetPrice = billingOptions.UnitPrice,
                        VatAmount = 0,
                        GrossAmount = billingOptions.UnitPrice
                    }
                };

                var api = new SzamlazzHuApi();
                var response = await api.CreateInvoice(request);
                logger.Log(LogLevel.Information, JsonSerializer.Serialize(response));
            } catch (Exception e) {
                logger.Log(LogLevel.Error, e, e.Message);
            }
        }
        logger.Log(LogLevel.Information, "BillingService finished");
    }
}