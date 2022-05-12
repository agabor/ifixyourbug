
using IFYB.Entities;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<GitAccess> GitAccesses { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;
    public DbSet<Admin> Admins { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

}