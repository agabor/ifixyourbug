namespace IFYB.Models;

public class PriceDto
{
    public string EurPriceId { get; set; }
    public string UsdPriceId { get; set; }
    public decimal EurPrice { get; set; }
    public decimal UsdPrice { get; set; }
    public PriceDto(string eurPriceId, string usdPriceId, decimal eurPrice, decimal usdPrice)
    {
        EurPriceId = eurPriceId;
        UsdPriceId = usdPriceId;
        EurPrice = eurPrice;
        UsdPrice = usdPrice;
    }
}