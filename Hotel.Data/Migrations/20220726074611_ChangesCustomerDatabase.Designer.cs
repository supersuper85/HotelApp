﻿// <auto-generated />
using System;
using HotelApp.Data.Entities.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotelApp.Data.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20220726074611_ChangesCustomerDatabase")]
    partial class ChangesCustomerDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<float>("DailyRentInEuro")
                        .HasColumnType("real");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfRooms")
                        .HasColumnType("int");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.HasIndex("RoomNumber")
                        .IsUnique();

                    b.ToTable("Apartment");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CustomerId = 0,
                            DailyRentInEuro = 25f,
                            HotelId = 1,
                            NumberOfRooms = 2,
                            ReservationId = 0,
                            RoomNumber = 1
                        },
                        new
                        {
                            Id = 2,
                            CustomerId = 1,
                            DailyRentInEuro = 35f,
                            HotelId = 1,
                            NumberOfRooms = 3,
                            ReservationId = 1,
                            RoomNumber = 2
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

                    b.Property<int?>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

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
                            RegistrationDate = new DateTime(2022, 7, 26, 7, 46, 11, 480, DateTimeKind.Utc).AddTicks(7827),
                            ReleaseDate = new DateTime(2022, 7, 27, 12, 0, 0, 0, DateTimeKind.Unspecified)
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

            modelBuilder.Entity("HotelApp.Data.Entities.Customer", b =>
                {
                    b.HasOne("HotelApp.Data.Entities.Hotel", null)
                        .WithMany("Customers")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Restrict);
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

                    b.Navigation("Customers");

                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
