using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelApp.Data.Entities.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.CustomerId).IsRequired();

            builder.Property(x => x.RegistrationDate).IsRequired();

            builder.Property(x => x.ReleaseDate).IsRequired();

            builder.Property(x => x.HotelId).IsRequired();

            builder
                .HasOne(r=>r.Apartment)
                .WithMany()
                .HasForeignKey(r => r.ApartmentId);


        }
    }
}
