using IFYB;
using Microsoft.EntityFrameworkCore;

public class ApplicationDBContext : DbContext
{
    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Client> Orders { get; set; } = null!;

    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {

    }

}