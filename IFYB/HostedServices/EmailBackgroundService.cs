using System.Threading.Channels;
using IFYB.Entities;
using IFYB.Services;

namespace IFYB.HostedServices;

public class EmailBackgroundService : BackgroundService
{
    private readonly Channel<Email> emailChanel;
    private readonly IServiceProvider serviceProvider;

    public EmailBackgroundService(Channel<Email> emailChanel, IServiceProvider serviceProvider)
    {
        this.emailChanel = emailChanel;
        this.serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach(var email in emailChanel.Reader.ReadAllAsync(stoppingToken))
        {
            var scope = serviceProvider.CreateScope();
            var emailSenderService = scope.ServiceProvider.GetRequiredService<EmailSenderService>();
            emailSenderService.SendEmail(email);
        }
    }
}