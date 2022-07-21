using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelApp.Data.Migrations
{
    public partial class ReservationDeleteBehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "RegistrationDate", "ReleaseDate" },
                values: new object[] { new DateTime(2022, 7, 18, 10, 10, 34, 998, DateTimeKind.Utc).AddTicks(1509), new DateTime(2022, 7, 19, 10, 10, 34, 998, DateTimeKind.Utc).AddTicks(1510) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "RegistrationDate", "ReleaseDate" },
                values: new object[] { new DateTime(2022, 7, 15, 12, 12, 57, 604, DateTimeKind.Utc).AddTicks(5497), new DateTime(2022, 7, 16, 12, 12, 57, 604, DateTimeKind.Utc).AddTicks(5498) });
        }
    }
}
