using Microsoft.EntityFrameworkCore;
using PrjModule25_Parser.Models;

namespace PrjModule25_Parser.Service
{
    public sealed class ApplicationDb : DbContext
    {
        public ApplicationDb(DbContextOptions<ApplicationDb> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<ProductData> Products { get; set; }
        public DbSet<ShopData> Shops { get; set; }
        public DbSet<ShopSource> Sources { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}