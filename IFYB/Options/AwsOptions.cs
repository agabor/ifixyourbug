namespace IFYB;

public class AwsOptions
{
    public const string Aws = "Aws";
    public string SmtpHost { get; set; } = String.Empty;
    public int SmtpPort { get; set; } = 0;
    public string SmtpUserName { get; set; } = String.Empty;
    public string SmtpPassword { get; set; } = String.Empty;
}
