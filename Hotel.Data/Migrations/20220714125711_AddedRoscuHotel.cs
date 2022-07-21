using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelApp.Data.Migrations
{
    public partial class AddedRoscuHotel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hotel",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Roscu" });

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "RegistrationDate", "ReleaseDate" },
                values: new object[] { new DateTime(2022, 7, 14, 12, 57, 11, 162, DateTimeKind.Utc).AddTicks(7779), new DateTime(2022, 7, 15, 12, 57, 11, 162, DateTimeKind.Utc).AddTicks(7782) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hotel",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "RegistrationDate", "ReleaseDate" },
                values: new object[] { new DateTime(2022, 7, 14, 11, 22, 33, 426, DateTimeKind.Utc).AddTicks(7784), new DateTime(2022, 7, 15, 11, 22, 33, 426, DateTimeKind.Utc).AddTicks(7785) });
        }
    }
}
