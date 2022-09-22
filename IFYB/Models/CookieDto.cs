namespace IFYB.Models;
public class CookieDto
{
    public bool Analytics { get; set; }
    public bool Advertisement { get; set; }
    public string Referrer { get; set; }

    public CookieDto(string referrer, bool analytics, bool advertisement)
    {
        Referrer = referrer;
        Analytics = analytics;
        Advertisement = advertisement;
    }
}