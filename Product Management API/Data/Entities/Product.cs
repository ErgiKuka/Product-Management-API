using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Product_Management_API.Data.Entities
{
    public class Product
    {
       public int ProductId { get; set; }
       public string ProductName { get; set; }
        public string? Description { get; set; } = "";
       public decimal Price { get; set; }
       public int StockQuantity { get; set; } = 0;
       public int CategoryId { get; set; }
       public Category Category { get; set; }
       public DateTime CreatedAt { get; set; }
       public DateTime? UpdatedAt { get; set; }



        internal static void ConfigureForDb(EntityTypeBuilder<Product> entity)
        {
            entity.ToTable("Product");
            entity.HasKey(b => b.ProductId);

            entity.Property(b => b.ProductName)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(p => p.Description)
                  .HasMaxLength(1000);

            entity.Property(b => b.StockQuantity)
                  .IsRequired();

            entity.Property(b => b.CategoryId)
                  .IsRequired();

            entity.HasOne(b => b.Category)
                  .WithMany(c => c.Products)
                  .HasForeignKey(b => b.CategoryId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Property(b => b.CreatedAt)
                  .IsRequired();

            //entity.Property(b => b.UpdatedAt)
            //      .IsRequired();

        }
    }
}
