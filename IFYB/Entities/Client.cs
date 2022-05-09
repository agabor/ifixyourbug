using IFYB.Models;

namespace IFYB.Entities;

public class Client
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string Email { get; set; }
    public string? InvoiceName { get; set; }
    public string? InvoiceAddress { get; set; }
    public string? TaxNumber { get; set; }
    public List<Order> Orders { get; set; } = null!;
    public List<GitAccess> GitAccesses { get; set; } = null!;
    public Client(string email)
    {
        Email = email;
    }
}