using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelApp.Data.Migrations
{
    public partial class ResetSeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "RegistrationDate", "ReleaseDate" },
                values: new object[] { new DateTime(2022, 7, 20, 14, 1, 55, 513, DateTimeKind.Local).AddTicks(4774), new DateTime(2022, 7, 21, 12, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "RegistrationDate", "ReleaseDate" },
                values: new object[] { new DateTime(2022, 7, 19, 11, 35, 14, 433, DateTimeKind.Utc).AddTicks(4744), new DateTime(2022, 7, 20, 11, 35, 14, 433, DateTimeKind.Utc).AddTicks(4746) });
        }
    }
}
