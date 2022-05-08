using IFYB.Models;

namespace IFYB.Entities;

public class Client
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string? InvoiceName { get; set; }
    public string? InvoiceAddress { get; set; }
    public string? TaxNumber { get; set; }
    public List<Order>? Orders { get; set; }
    public Client(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public static Client FromDto(ClientDto dto)
    {
        return new Client(dto.Name, dto.Email);
    }

    public ClientDto ToDto()
    {
        return new ClientDto(Name, Email);
    }
}