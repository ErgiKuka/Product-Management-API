using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Product_Management_API.Data.Entities
{
    public class Orders
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; }

        public Customer Customer { get; set; }
        public ICollection<OrderItems> OrderItems { get; set; } = new List<OrderItems>();

        internal static void ConfigureForDb(EntityTypeBuilder<Orders> entity)
        {
            entity.ToTable("Orders");
            entity.HasKey(o => o.OrderId);

            entity.Property(o => o.OrderStatus)
                  .HasMaxLength(50)
                  .IsRequired();

            entity.Property(o => o.ShippingAddress)
                  .HasMaxLength(250)
                  .IsRequired();

            entity.Property(o => o.TotalAmount)
                  .HasColumnType("decimal(18,2)")
                  .IsRequired();

            entity.HasOne(o => o.Customer)
                  .WithMany(c => c.Orders)
                  .HasForeignKey(o => o.CustomerId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(o => o.OrderItems)
                  .WithOne(oi => oi.orders)
                  .HasForeignKey(oi => oi.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
