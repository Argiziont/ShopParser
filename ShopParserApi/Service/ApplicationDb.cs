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
            //Database.EnsureCreated();
            //Database.Migrate();
        }

        public DbSet<ProductData> Products { get; set; }
        public DbSet<CompanyData> Companies { get; set; }
        public DbSet<CompanySource> Sources { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductPaymentOption> ProductPaymentOptions { get; set; }
        public DbSet<ProductDeliveryOption> ProductDeliveryOptions { get; set; }
        public DbSet<PresenceData> Presence { get; set; }
    }
}