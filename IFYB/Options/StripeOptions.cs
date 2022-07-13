namespace IFYB;

public class StripeOptions
{
    public const string Stripe = "Stripe";
    public string ApiKey { get; set; } = String.Empty;
    public string PriceId { get; set; } = String.Empty;
    public string HookKey { get; set; } = String.Empty;
}
