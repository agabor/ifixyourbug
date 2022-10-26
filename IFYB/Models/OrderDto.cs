using IFYB.Entities;
namespace IFYB.Models;

public class OrderDto
{
    public int Number { get; set; }
    public DateTime CreationTime { get; set; }
    public string? ApplicationUrl { get; set; }
    public string BugDescription { get; set; }
    public OrderState State { get; set; }
    public List<MessageDto>? Messages { get; set; }
    public List<ImageDto>? Images { get; set; }
    public int GitAccessId { get; set; }
    public string? PaymentToken { get; set; }
    public int ClientId { get; set; }
    public decimal EurPrice { get; set; }
    public decimal UsdPrice { get; set; }

    public OrderDto(int number, DateTime creationTime, string? applicationUrl, string bugDescription, OrderState state, List<MessageDto>? messages, List<ImageDto>? images, int gitAccessId, string? paymentToken, int clientId, decimal eurPrice, decimal usdPrice)
    {
        Number = number;
        ApplicationUrl = applicationUrl;
        BugDescription = bugDescription;
        State = state;
        Messages = messages;
        Images = images;
        GitAccessId = gitAccessId;
        PaymentToken = paymentToken;
        ClientId = clientId;
        EurPrice = eurPrice;
        UsdPrice = usdPrice;
        CreationTime = creationTime;
    }
}
