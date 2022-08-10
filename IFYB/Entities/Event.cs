namespace IFYB.Entities;
public class Event
{
    public int Id { get; set; }
    public int? ClientId { get; set; }
    public Client? Client { get; set; }
    public int? AdminId { get; set; }
    public Admin? Admin { get; set; }
    public DateTime DateTime { get; set; }
    public string Text { get; set; } = string.Empty;
}