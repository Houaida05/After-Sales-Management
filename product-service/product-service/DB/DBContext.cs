using Microsoft.EntityFrameworkCore;
using product_service.Models;

namespace product_service.DB
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<SparePart> SpareParts { get; set; }

    }
}
