using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelApp.Data.Entities.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(100);

            builder.Property(x => x.Age).IsRequired();

            builder.Property(x => x.ApartmentId).IsRequired();

            builder.Property(x => x.ReservationId).IsRequired();

            builder.HasOne(c => c.Reservation)
                .WithOne(c => c.Customer)
                .HasForeignKey<Reservation>(c => c.CustomerId);
        }
    }
}
