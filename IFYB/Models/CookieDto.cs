namespace IFYB.Models;
public class CookieDto
{
    public bool Analytics { get; set; }
    public bool Advertisement { get; set; }

    public CookieDto(bool analytics, bool advertisement)
    {
        Analytics = analytics;
        Advertisement = advertisement;
    }
}