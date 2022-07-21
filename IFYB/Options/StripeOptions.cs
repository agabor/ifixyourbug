namespace IFYB;

public class StripeOptions
{
    public const string Stripe = "Stripe";
    public string ApiKey { get; set; } = String.Empty;
    public string EurPriceId { get; set; } = String.Empty;
    public string UsdPriceId { get; set; } = String.Empty;
    public string HookKey { get; set; } = String.Empty;
}
