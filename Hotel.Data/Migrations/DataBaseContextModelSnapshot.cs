﻿// <auto-generated />
using System;
using HotelApp.Data.Entities.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotelApp.Data.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HotelApp.Data.Entities.Apartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ApartmentNumber")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<float>("DailyRentInEuro")
                        .HasColumnType("real");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<float>("NumberOfRooms")
                        .HasColumnType("real");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentNumber")
                        .IsUnique();

                    b.HasIndex("HotelId");

                    b.ToTable("Apartment");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ApartmentNumber = 1,
                            CustomerId = 0,
                            DailyRentInEuro = 25f,
                            HotelId = 1,
                            NumberOfRooms = 2f,
                            ReservationId = 0
                        },
                        new
                        {
                            Id = 2,
                            ApartmentNumber = 2,
                            CustomerId = 1,
                            DailyRentInEuro = 35f,
                            HotelId = 1,
                            NumberOfRooms = 3f,
                            ReservationId = 1
                        });
                });

            modelBuilder.Entity("HotelApp.Data.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("CNP")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Customer");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Age = 20,
                            CNP = "1234567891011",
                            Name = "Cristi"
                        });
                });

            modelBuilder.Entity("HotelApp.Data.Entities.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Hotel");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Roman"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Transilvania"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Roscu"
                        });
                });

            modelBuilder.Entity("HotelApp.Data.Entities.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ApartmentId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("HotelId", "ApartmentId")
                        .IsUnique();

                    b.ToTable("Reservation");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ApartmentId = 2,
                            CustomerId = 1,
                            HotelId = 1,
                            RegistrationDate = new DateTime(2022, 8, 8, 11, 12, 24, 277, DateTimeKind.Utc).AddTicks(9861),
                            ReleaseDate = new DateTime(2022, 8, 9, 12, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("HotelApp.Data.Entities.Apartment", b =>
                {
                    b.HasOne("HotelApp.Data.Entities.Hotel", null)
                        .WithMany("Apartments")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("HotelApp.Data.Entities.Reservation", b =>
                {
                    b.HasOne("HotelApp.Data.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HotelApp.Data.Entities.Hotel", null)
                        .WithMany("Reservations")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("HotelApp.Data.Entities.Hotel", b =>
                {
                    b.Navigation("Apartments");

                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
