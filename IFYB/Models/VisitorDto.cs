namespace IFYB.Models;
public class VisitorDto
{
    public string Referrer { get; set; }
    public string Search { get; set; }
    public string TimeZone { get; set; }
    public bool Analytics { get; set; }
    public bool Advertisement { get; set; }

    public VisitorDto(string referrer, string search, string timeZone, bool analytics, bool advertisement)
    {
        Referrer = referrer;
        Search = search;
        TimeZone = timeZone;
        Analytics = analytics;
        Advertisement = advertisement;
    }
}