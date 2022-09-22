namespace IFYB.Models;
public class CookieDto
{
    public DateTime DateTime { get; set; }
    public string UserAgent { get; set; } = string.Empty;
    public bool Analytics { get; set; }
    public bool Advertisement { get; set; }

    public CookieDto(bool analytics, bool advertisement)
    {
        Analytics = analytics;
        Advertisement = advertisement;
    }
}