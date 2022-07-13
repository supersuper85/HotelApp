using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelApp.Data.Entities.Configurations
{
    public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.DailyRentInEuro).IsRequired();

            builder.Property(x => x.NumberOfRooms).IsRequired();

            builder.Property(x => x.RoomNumber).IsRequired();
            builder.HasIndex(x => x.RoomNumber).IsUnique();

            builder.Property(x => x.ReservationId).IsRequired();

            builder.Property(x => x.HotelId).IsRequired();
        }
    }
}
