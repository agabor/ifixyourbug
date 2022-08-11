using IFYB.Entities;

namespace IFYB.Services;


public class EventLogService<T>
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<T> logger;

    public EventLogService(ApplicationDbContext dbContext, ILogger<T> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public void Log(LogLevel logLevel, string text)
    {
        dbContext.Events.Add(new Event { DateTime = DateTime.UtcNow, Text = text });
        dbContext.SaveChanges();
        logger.Log(logLevel, text);
    }
    public void LogClientEvent(int clientId, string text, LogLevel logLevel = LogLevel.Information)
    {
        dbContext.Events.Add(new Event { ClientId = clientId, DateTime = DateTime.UtcNow, Text = text });
        dbContext.SaveChanges();
        logger.Log(logLevel, text);
    }
    public void LogAdminEvent(int adminId, string text, LogLevel logLevel = LogLevel.Information)
    {
        dbContext.Events.Add(new Event { AdminId = adminId, DateTime = DateTime.UtcNow, Text = text });
        dbContext.SaveChanges();
        logger.Log(logLevel, text);
    }

    public void LogUserEvent(IAunthenticable user, string text)
    {
        if (user.Role == Roles.Client)
            dbContext.Events.Add(new Event { ClientId = user.Id, DateTime = DateTime.UtcNow, Text = text });
        if (user.Role == Roles.Admin)
            dbContext.Events.Add(new Event { AdminId = user.Id, DateTime = DateTime.UtcNow, Text = text });
        dbContext.SaveChanges();
    }
}