using Microsoft.EntityFrameworkCore;
using ShopParserApi.Models;
using ShopParserApi.Models.Helpers;

namespace ShopParserApi.Service
{
    public sealed class ApplicationDb : DbContext
    {
        public ApplicationDb(DbContextOptions<ApplicationDb> options)
            : base(options)
        {
            // Database.EnsureDeleted();
            Database.EnsureCreated();
            //Database.Migrate();
        }

        public DbSet<ProductData> Products { get; set; }
        public DbSet<ShopData> Shops { get; set; }
        public DbSet<ShopSource> Sources { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
    }
}