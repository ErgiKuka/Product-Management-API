using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Product_Management_API.Data.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string? PhoneNumber { get; set; }

        internal static void ConfigureForDb(EntityTypeBuilder<Customer> entity)
        {
            entity.ToTable("Customer");

            entity.HasKey(c => c.CustomerId);

            entity.Property(c => c.FirstName)
                  .HasMaxLength(50)
                  .IsRequired();

            entity.Property(c => c.LastName)
                  .HasMaxLength(50)
                  .IsRequired();

            entity.Property(c => c.Email)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(c => c.Address)
                  .HasMaxLength(250)
                  .IsRequired();

            entity.Property(c => c.PhoneNumber)
                  .HasMaxLength(15);
        }
    }
}
