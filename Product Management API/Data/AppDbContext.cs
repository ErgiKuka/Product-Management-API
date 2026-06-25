using Microsoft.EntityFrameworkCore;
using Product_Management_API.Data.Entities;

namespace Product_Management_API.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<Product>  Products { get; set; }
        public DbSet<Category> Categories { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Data.Entities.Product.ConfigureForDb(modelBuilder.Entity<Product>());
            Data.Entities.Category.ConfigureForDb(modelBuilder.Entity<Category>());
        }
    }
}
