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

            builder.Property(x => x.ApartmentNumber).IsRequired();
            builder.HasIndex(x => x.ApartmentNumber).IsUnique();

            builder.Property(x => x.ReservationId).IsRequired();

            builder.Property(x => x.HotelId).IsRequired();

            builder.HasData(
                new Apartment
                {
                    Id = 1,
                    DailyRentInEuro = 25,
                    NumberOfRooms = 2,
                    ApartmentNumber = 1,

                    CustomerId = 0,
                    ReservationId = 0,
                    HotelId = 1,
                },
                new Apartment
                {
                    Id = 2,
                    DailyRentInEuro = 35,
                    NumberOfRooms = 3,
                    ApartmentNumber = 2,

                    CustomerId = 1,
                    ReservationId = 1,
                    HotelId = 1,
                });
        }
    }
}
