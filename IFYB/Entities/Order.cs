using IFYB.Models;

namespace IFYB.Entities;

public class Order
{
    public int Id { get; set; }
    public int Number { get; set; }
    public DateTime CreationTime { get; set; }
    public int CreationDay { get; set; }
    public string? ApplicationUrl { get; set; }
    public string BugDescription { get; set; }
    public OrderState State { get; set; }
    public OrderFlag Flag { get; set; }
    public int ClientId { get; set; }
    public Client? Client { get; set; }
    public List<Message>? Messages { get; set; }
    public List<Image>? Images { get; set; }
    public int GitAccessId { get; set; }
    public GitAccess GitAccess { get; set; } = null!;
    public string? PaymentToken { get; set; }
    public string? StripeSessionId { get; set; }
    public string? StripeCustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string? TaxId { get; set; }
    public string? TaxIdType { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? Line1 { get; set; }
    public string? Line2 { get; set; }
    public string? PostalCode { get; set; }
    public string? AddressState { get; set; }
    public string? Currency { get; set; }
    public string? PriceId { get; set; }
    public decimal? Price { get; set; }
    public long? AmountTotal { get; set; }
    public long? AmountSubtotal { get; set; }
    public long? AmountTax { get; set; }
    public string? TaxCountry { get; set; }
    public string? TaxState { get; set; }
    public string? AutomaticTax { get; set; }
    public string? InvoiceNumber { get; set; }
    public DateTime? PayedAt { get; set; }

    public Order(int number, string? applicationUrl, string bugDescription, OrderFlag flag, int gitAccessId)
    {
        Number = number;
        ApplicationUrl = applicationUrl;
        BugDescription = bugDescription;
        Flag = flag;
        GitAccessId = gitAccessId;
        CreationTime = DateTime.UtcNow;
        CreationDay = (DateTime.UtcNow - DateTime.UnixEpoch).Days;
    }

    public static Order FromDto(OrderDto dto)
    {
        return new Order(dto.Number, dto.ApplicationUrl, dto.BugDescription, dto.Flag, dto.GitAccessId);
    }

    public OrderDto ToDto()
    {
        return new OrderDto(Number, CreationTime, ApplicationUrl, BugDescription, State, Flag, Messages?.Select(m => m.ToDto()).ToList(), Images?.Select(i => i.ToDto()).ToList(), GitAccessId, PaymentToken, ClientId, Currency!, Price!.Value);
    }
}

public enum OrderState { Submitted, Accepted, Rejected, Payed, Completed, Refundable, Canceled, Editable }
public enum OrderFlag { Bugfix, CodeReview }