namespace IFYB.Models;

public class OfferDto
{
    public decimal EurPrice { get; set; }
    public decimal UsdPrice { get; set; }
    public int Workdays { get; set; } = 3;
}