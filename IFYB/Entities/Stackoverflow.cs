using IFYB.Models;

namespace IFYB.Entities;

public class StackoverflowRequest
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public Client? Client { get; set; }
    public int Number { get; set; }
    public DateTime DateTime { get; set; }
    public int CreationDay { get; set; }
    public string Url { get; set; }
    public string Text { get; set; }
    public bool Solved { get; set; } = false;

    public StackoverflowRequest(int number, string url, string text)
    {
        Number = number;
        Text = text;
        Url = url;
        CreationDay = (DateTime.UtcNow - DateTime.UnixEpoch).Days;
    }
}