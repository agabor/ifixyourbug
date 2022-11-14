using IFYB.Entities;
namespace IFYB.Models;

public class OrderDto
{
    public int Number { get; set; }
    public DateTime CreationTime { get; set; }
    public string? ApplicationUrl { get; set; }
    public string BugDescription { get; set; }
    public OrderState State { get; set; }
    public OrderFlag Flag { get; set; }
    public List<MessageDto>? Messages { get; set; }
    public List<ImageDto>? Images { get; set; }
    public int GitAccessId { get; set; }
    public string? PaymentToken { get; set; }
    public int ClientId { get; set; }
    public string Currency { get; set; }
    public decimal Price { get; set; }

    public OrderDto(int number, DateTime creationTime, string? applicationUrl, string bugDescription, OrderState state, OrderFlag flag, List<MessageDto>? messages, List<ImageDto>? images, int gitAccessId, string? paymentToken, int clientId, string currency, decimal price)
    {
        Number = number;
        ApplicationUrl = applicationUrl;
        BugDescription = bugDescription;
        State = state;
        Flag = flag;
        Messages = messages;
        Images = images;
        GitAccessId = gitAccessId;
        PaymentToken = paymentToken;
        ClientId = clientId;
        Currency = currency;
        Price = price;
        CreationTime = creationTime;
    }
}
