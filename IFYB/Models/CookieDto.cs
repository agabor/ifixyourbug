namespace IFYB.Models;
public class CookieDto
{
    public string Referrer { get; set; }
    public string Search { get; set; }
    public bool Analytics { get; set; }
    public bool Advertisement { get; set; }

    public CookieDto(string referrer, string search, bool analytics, bool advertisement)
    {
        Referrer = referrer;
        Search = search;
        Analytics = analytics;
        Advertisement = advertisement;
    }
}