using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelApp.Data.Migrations
{
    public partial class Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hotel",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Roman" });

            migrationBuilder.InsertData(
                table: "Hotel",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Transilvania" });

            migrationBuilder.InsertData(
                table: "Apartment",
                columns: new[] { "Id", "CustomerId", "DailyRentInEuro", "HotelId", "NumberOfRooms", "ReservationId", "RoomNumber" },
                values: new object[] { 1, 0, 25f, 1, 2, 0, 1 });

            migrationBuilder.InsertData(
                table: "Apartment",
                columns: new[] { "Id", "CustomerId", "DailyRentInEuro", "HotelId", "NumberOfRooms", "ReservationId", "RoomNumber" },
                values: new object[] { 2, 0, 35f, 1, 3, 1, 2 });

            migrationBuilder.InsertData(
                table: "Reservation",
                columns: new[] { "Id", "ApartmentId", "HotelId", "RegistrationDate", "ReleaseDate" },
                values: new object[] { 1, 2, 1, new DateTime(2022, 7, 14, 9, 13, 19, 218, DateTimeKind.Utc).AddTicks(2573), new DateTime(2022, 7, 15, 9, 13, 19, 218, DateTimeKind.Utc).AddTicks(2575) });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Age", "ApartmentId", "HotelId", "Name", "ReservationId" },
                values: new object[] { 1, 20, 2, 1, "Cristi", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Apartment",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Apartment",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotel",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotel",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
