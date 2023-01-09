

using Microsoft.EntityFrameworkCore;
using TechnicalSupportService.Models;

namespace TechnicalSupportService.DB
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }

        public DbSet<Claim> claims { get; set; }
        public DbSet<Intervention> interventions { get; set; }

    }
}
