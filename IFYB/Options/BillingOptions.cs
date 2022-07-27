namespace IFYB;

public class BillingOptions
{
    public const string Billing = "Billing";
    public string ApiKey { get; set; } = String.Empty;
    public string InvoiceNumberPrefix { get; set; } = String.Empty;
    public string VatHunCompany { get; set; } = String.Empty;
    public string VatHunPrivate { get; set; } = String.Empty;
    public string VatEuCompany { get; set; } = String.Empty;
    public string VatEuPrivate { get; set; } = String.Empty;
    public string Vat3rdCompany { get; set; } = String.Empty;
    public string Vat3rdPrivate { get; set; } = String.Empty;
}
