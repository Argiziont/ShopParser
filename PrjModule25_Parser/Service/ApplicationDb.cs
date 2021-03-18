using Microsoft.EntityFrameworkCore;
using PrjModule25_Parser.Models;

namespace PrjModule25_Parser.Service
{
    public class ApplicationDb : DbContext
    {
        public ApplicationDb() : base()
        { }

        public DbSet<ProductData> Products { get; set; }
        public DbSet<ShopData> Shops { get; set; }
        public DbSet<ShopSource> Sources { get; set; }
        
    }
}