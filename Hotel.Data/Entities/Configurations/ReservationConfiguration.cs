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

            builder.Property(x => x.RegistrationDate).IsRequired();

            builder.Property(x => x.ReleaseDate).IsRequired();

            builder.Property(x => x.HotelId).IsRequired();


            builder.HasData(new Reservation
            {
                Id = 1,
                RegistrationDate = DateTime.UtcNow,
                ReleaseDate = DateTime.UtcNow.AddDays(1),

                ApartmentId = 2,
                HotelId = 1,
            });
        }
    }
}
