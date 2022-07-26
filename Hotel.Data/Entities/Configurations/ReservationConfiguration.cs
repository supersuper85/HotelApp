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

            builder.HasIndex(x => new { x.HotelId, x.ApartmentId }).IsUnique();


            builder.HasData(new Reservation
            {
                Id = 1,
                RegistrationDate = DateTime.UtcNow,
                ReleaseDate = GetReleaseDate(1),

                ApartmentId = 2,
                HotelId = 1,

                CustomerId = 1,
            }) ;
        }
        public DateTime GetReleaseDate(int numberOfDays)
        {
            var initialDate = DateTime.UtcNow.AddDays(numberOfDays);
            int releaseHour = 12;
            int releaseMinute = 0;
            int releaseSecond = 0;
            DateTime releaseDate = new DateTime(initialDate.Year, initialDate.Month, initialDate.Day, releaseHour, releaseMinute, releaseSecond);
            return releaseDate;
        }
    }
}
