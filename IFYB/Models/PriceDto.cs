namespace IFYB.Models;

public class PriceDto
{
    public decimal EurPrice { get; set; }
    public decimal UsdPrice { get; set; }
    public PriceDto(decimal eurPrice, decimal usdPrice)
    {
        EurPrice = eurPrice;
        UsdPrice = usdPrice;
    }
}