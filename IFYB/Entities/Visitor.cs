using IFYB.Models;

namespace IFYB.Entities;

public class Visitor
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; } = DateTime.UtcNow;
    public string UserAgent { get; set; } = string.Empty;
    public string Referrer { get; set; }
    public string Search { get; set; }
    public string TimeZone { get; set; }
    public bool Analytics { get; set; }
    public bool Advertisement { get; set; }

     public Visitor(string referrer, string search, string timeZone, bool analytics, bool advertisement)
    {
        Referrer = referrer;
        Search = search;
        TimeZone = timeZone;
        Analytics = analytics;
        Advertisement = advertisement;
    }

    public static Visitor FromDto(VisitorDto dto)
    {
        return new Visitor(dto.Referrer, dto.Search, dto.TimeZone, dto.Analytics, dto.Advertisement);
    }
}