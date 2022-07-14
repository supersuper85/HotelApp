﻿using Microsoft.EntityFrameworkCore;
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

            builder.HasData(new Customer
            {
                Id = 1,
                Age = 20,
                Name = "Cristi",

                ApartmentId = 2,
                ReservationId = 1,
                HotelId = 1
            });
        }
    }
}
