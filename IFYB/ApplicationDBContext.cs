
using IFYB.Entities;
using Microsoft.EntityFrameworkCore;

public class ApplicationDBContext : DbContext
{
    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;

    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {

    }

}