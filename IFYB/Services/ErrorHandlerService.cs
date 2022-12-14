using System;
using System.Diagnostics;
using IFYB.Entities;

namespace IFYB.Services;

public class ErrorHandlerService
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<ErrorHandlerService> logger;

    public ErrorHandlerService(ApplicationDbContext dbContext, ILogger<ErrorHandlerService> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public void OnException(Exception e, string? data)
    {
        ServerError error = e.InnerException == null ? ExceptionToError(e) : ExceptionToError(e.InnerException);
        error.Data = data;
        dbContext.ServerErrors.Add(error);
        dbContext.SaveChanges();
        logger.Log(LogLevel.Error, e, e.Message);
    }

    private static ServerError ExceptionToError(Exception e)
    {
        var st = new StackTrace(e, true);
        var frame = st.GetFrame(0);
        return new ServerError {
            DateTime = DateTime.UtcNow,
            Message = e.Message,
            StackTrace = e.StackTrace,
            File = frame?.GetFileName(),
            Line = frame?.GetFileLineNumber() ?? 0,
            Source = e.Source,
            HResult = e.HResult
        };
    }

}