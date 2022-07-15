using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelApp.Data.Migrations
{
    public partial class Seeding2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Apartment",
                keyColumn: "Id",
                keyValue: 2,
                column: "CustomerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "RegistrationDate", "ReleaseDate" },
                values: new object[] { new DateTime(2022, 7, 14, 11, 22, 33, 426, DateTimeKind.Utc).AddTicks(7784), new DateTime(2022, 7, 15, 11, 22, 33, 426, DateTimeKind.Utc).AddTicks(7785) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Apartment",
                keyColumn: "Id",
                keyValue: 2,
                column: "CustomerId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "RegistrationDate", "ReleaseDate" },
                values: new object[] { new DateTime(2022, 7, 14, 9, 13, 19, 218, DateTimeKind.Utc).AddTicks(2573), new DateTime(2022, 7, 15, 9, 13, 19, 218, DateTimeKind.Utc).AddTicks(2575) });
        }
    }
}
