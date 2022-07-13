namespace IFYB;

public class BillingOptions
{
    public const string Billing = "Billing";
    public decimal UnitPrice { get; set; }
    public string BankName { get; set; } = string.Empty;
    public string BankAccount { get; set; } = string.Empty;
}
