using IFYB.Models;

namespace IFYB.Entities;

public class Cookie
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; } = DateTime.UtcNow;
    public string UserAgent { get; set; } = string.Empty;
    public string Referrer { get; set; }
    public string Search { get; set; }
    public string TimeZone { get; set; }
    public bool Analytics { get; set; }
    public bool Advertisement { get; set; }

     public Cookie(string referrer, string search, string timeZone, bool analytics, bool advertisement)
    {
        Referrer = referrer;
        Search = search;
        TimeZone = timeZone;
        Analytics = analytics;
        Advertisement = advertisement;
    }

    public static Cookie FromDto(CookieDto dto)
    {
        return new Cookie(dto.Referrer, dto.Search, dto.TimeZone, dto.Analytics, dto.Advertisement);
    }
}