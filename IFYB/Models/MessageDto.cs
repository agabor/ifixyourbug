namespace IFYB.Models;
public class MessageDto
{
    public int ClientId { get; set; }
    public int? OrderId { get; set; }
    public DateTime DateTime { get; set; }
    public bool FromClient { get; set; }
    public string Text { get; set; }

    public MessageDto(int clientId, int? orderId, DateTime dateTime, bool fromClient, string text)
    {
        ClientId = clientId;
        OrderId = orderId;
        DateTime = dateTime;
        FromClient = fromClient;
        Text = text;
    }
}