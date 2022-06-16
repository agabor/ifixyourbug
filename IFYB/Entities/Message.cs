using IFYB.Models;

namespace IFYB.Entities;

public class Message
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public Client? Client { get; set; }
    public int? OrderId { get; set; }
    public Order? Order { get; set; }
    public DateTime DateTime { get; set; }
    public bool FromClient { get; set; }
    public string Text { get; set; }

    public Message(string text)
    {
        Text = text;
    }
    public MessageDto ToDto()
    {
        return new MessageDto(ClientId, OrderId, DateTime, FromClient, Text);
    }
}