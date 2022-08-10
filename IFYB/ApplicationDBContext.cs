
using IFYB.Entities;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<GitAccess> GitAccesses { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;
    public DbSet<Admin> Admins { get; set; } = null!;
    public DbSet<Email> Emails { get; set; } = null!;
    public DbSet<ServerError> ServerErrors { get; set; } = null!;
    public DbSet<ClientError> ClientErrors { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Message>()
            .HasOne(m => m.Order)
            .WithMany(o => o.Messages)
            .IsRequired(false);
        modelBuilder.Entity<Admin>().HasData(new Admin("gabor@ifixyourbug.com") {
            Id = 1
        });
    }

}