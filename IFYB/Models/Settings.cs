namespace IFYB.Models;

public class Settings
{
    public decimal EurPrice { get; set; }
    public decimal UsdPrice { get; set; }
    public int Workdays { get; set; }
    public List<GitService> GitServices { get; set; } = new List<GitService>();
    public string SshKey { get; set; } = string.Empty;
}