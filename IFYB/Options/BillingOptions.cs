namespace IFYB;

public class BillingOptions
{
    public const string Billing = "Billing";
    public string ApiKey { get; set; } = String.Empty;
    public decimal UnitPrice { get; set; }
    public string InvoiceNumberPrefix { get; set; } = String.Empty;
}
