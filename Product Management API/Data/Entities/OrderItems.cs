using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Product_Management_API.Data.Entities
{
    public class OrderItems
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Orders orders { get; set; }
        public Product Product { get; set; }

        public ICollection<Orders> Orders { get; set; } = new List<Orders>();
        public ICollection<Product> Products { get; set; } = new List<Product>();

        internal static void ConfigureForDb(EntityTypeBuilder<OrderItems> entity)
        {
            entity.ToTable("OrderItems");
            entity.HasKey(oi => oi.OrderItemId);

            entity.Property(oi => oi.Quantity)
                  .IsRequired();

            entity.Property(oi => oi.UnitPrice)
                  .HasColumnType("decimal(18,2)")
                  .IsRequired();

            entity.HasOne(oi => oi.orders)
                  .WithMany(o => o.OrderItems)
                  .HasForeignKey(oi => oi.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(oi => oi.Product)
                  .WithMany(p => p.OrderItems)
                  .HasForeignKey(oi => oi.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
