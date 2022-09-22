using IFYB.Models;

namespace IFYB.Entities;

public class Cookie
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public string UserAgent { get; set; } = string.Empty;
    public bool Analytics { get; set; }
    public bool Advertisement { get; set; }

     public Cookie(DateTime dateTime, string userAgent, bool analytics, bool advertisement)
    {
        DateTime = dateTime;
        UserAgent = userAgent;
        Analytics = analytics;
        Advertisement = advertisement;
    }

    public static Cookie FromDto(CookieDto dto)
    {
        return new Cookie(dto.DateTime, dto.UserAgent, dto.Analytics, dto.Advertisement);
    }
}