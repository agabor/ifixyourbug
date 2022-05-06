namespace IFYB;

class Client
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string? InvoiceName { get; set; }
    public string? InvoiceAddress { get; set; }
    public string? TaxNumber { get; set; }

    public Client(string name, string email)
    {
        Name = name;
        Email = email;
    }
}