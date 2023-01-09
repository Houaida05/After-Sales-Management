using ClientWebService.Model;
using Microsoft.EntityFrameworkCore;

namespace ClientWebService.DB

{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Claim> Claims{ get; set; }
        public DbSet<Product> Products { get; set; }
    }
}

