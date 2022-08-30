namespace IFYB.Models;

public class Settings
{
    public decimal EurPrice { get; set; }
    public decimal UsdPrice { get; set; }
    public int Workdays { get; set; }
    public string SshKey { get; set; } = string.Empty;
}