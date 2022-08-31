using System.Threading.Channels;
using IFYB.Entities;

namespace IFYB.Services;

public class EmailDispatchService
{
    private readonly EmailCreationService emailCreationService;
    private readonly Channel<Email> emailChannel;
    private readonly EventLogService<EmailDispatchService> logger;

    public EmailDispatchService(EmailCreationService emailCreationService, Channel<Email> emailChannel, EventLogService<EmailDispatchService> logger)
    {
        this.emailCreationService = emailCreationService;
        this.emailChannel = emailChannel;
        this.logger = logger;
    }

    public void DispatchOrderStateEmail(Order order, string? message = null)
    {
        var email = emailCreationService.CreateOrderStateEmail(order, message?.ToHtml());
        Dispatch(email);
    }

    public void DispatchEmail(string toEmail, string jsonTemplate, Order? order, object data, bool? toAdmin = false)
    {
        var email = emailCreationService.CreateEmail(toEmail, jsonTemplate, order, data, toAdmin);
        Dispatch(email);
    }

    private void Dispatch(Email? email)
    {
        if (email != null)
            if (!emailChannel.Writer.TryWrite(email))
                logger.Log(LogLevel.Error, "Could not write e-mail to channel.");
    }
}