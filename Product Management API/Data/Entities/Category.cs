using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Product_Management_API.Data.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }


        internal static void ConfigureForDb(EntityTypeBuilder<Category> entity)
        {
            entity.ToTable("Category");
            entity.HasKey(b => b.CategoryId);

            entity.Property(b => b.CategoryName)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(p => p.Description)
                  .HasMaxLength(1000)
                  .IsRequired(false);

        }
    }
}
