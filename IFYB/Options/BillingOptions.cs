namespace IFYB;

public class BillingOptions
{
    public const string Billing = "Billing";
    public string ApiKey { get; set; } = String.Empty;
    public string InvoiceNumberPrefix { get; set; } = String.Empty;
}
