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
            builder.HasIndex(x => x.NumberOfRooms).IsUnique();
        }
    }
}
/*public float DailyRentInEuro { get; set; }
public int NumberOfRooms { get; set; }
public int RoomNumber { get; set; }
public int ReservationId { get; set; }
public int HotelId { get; set; }*/