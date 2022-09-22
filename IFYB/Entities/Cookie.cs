using IFYB.Models;

namespace IFYB.Entities;

public class Cookie
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; } = DateTime.UtcNow;
    public string UserAgent { get; set; } = string.Empty;
    public bool Analytics { get; set; }
    public bool Advertisement { get; set; }

     public Cookie(bool analytics, bool advertisement)
    {
        Analytics = analytics;
        Advertisement = advertisement;
    }

    public static Cookie FromDto(CookieDto dto)
    {
        return new Cookie(dto.Analytics, dto.Advertisement);
    }
}