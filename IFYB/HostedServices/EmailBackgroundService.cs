using System.Threading.Channels;
using IFYB.Entities;
using IFYB.Services;

namespace IFYB.HostedServices;

public class EmailBackgroundService : BackgroundService
{
    private readonly Channel<Email> emailChanel;
    private readonly IServiceProvider serviceProvider;
    private readonly ILogger<EmailBackgroundService> logger;

    public EmailBackgroundService(Channel<Email> emailChanel, IServiceProvider serviceProvider, ILogger<EmailBackgroundService> logger)
    {
        this.emailChanel = emailChanel;
        this.serviceProvider = serviceProvider;
        this.logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var emails = new List<Email>();
        await foreach(var email in emailChanel.Reader.ReadAllAsync(stoppingToken))
        {
            emails.Add(email);
            if (emailChanel.Reader.CanCount && emailChanel.Reader.Count > 0)
                continue;
            SendEmails(emails);
            emails.Clear();
        }
    }

    private void SendEmails(List<Email> emails)
    {
        logger.Log(LogLevel.Information, $"Sending {emails.Count} email(s).");
        using var scope = serviceProvider.CreateScope();
        var emailSenderService = scope.ServiceProvider.GetRequiredService<IEmailSenderService>();
        foreach(var email in emails)
            if (!emailSenderService.SendEmail(email))
            {
                logger.Log(LogLevel.Warning, "Failed to send e-mail.");
            }
    }
}