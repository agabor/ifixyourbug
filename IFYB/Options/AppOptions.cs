namespace IFYB;

public class AppOptions
{
    public const string Host = "Host";
    public string BaseUrl { get; set; } = String.Empty;
    public bool SendEmails { get; set; } = false;
    public string ImgFolder { get; set; } = String.Empty;

}