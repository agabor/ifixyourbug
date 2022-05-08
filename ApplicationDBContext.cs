using IFYB;
using Microsoft.EntityFrameworkCore;

class ApplicationDBContext : DbContext
{
    DbSet<Client> Clients { get; set; } = null!;
    DbSet<Client> Orders { get; set; } = null!;

}