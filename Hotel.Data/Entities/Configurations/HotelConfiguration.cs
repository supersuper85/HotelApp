using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelApp.Data.Entities.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Name).HasMaxLength(100);

            builder.HasMany(x => x.Apartments).WithOne().HasForeignKey(x => x.HotelId);
            builder.HasMany(x => x.Reservations).WithOne().HasForeignKey(x => x.HotelId); 

            builder.HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Roman",
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Transilvania",
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Roscu",
                });
        }
    }
}
