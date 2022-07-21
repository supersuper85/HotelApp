using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelApp.Data.Migrations
{
    public partial class ApartmentIdAndHotelIdConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservation_HotelId",
                table: "Reservation");

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "RegistrationDate", "ReleaseDate" },
                values: new object[] { new DateTime(2022, 7, 19, 11, 35, 14, 433, DateTimeKind.Utc).AddTicks(4744), new DateTime(2022, 7, 20, 11, 35, 14, 433, DateTimeKind.Utc).AddTicks(4746) });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_HotelId_ApartmentId",
                table: "Reservation",
                columns: new[] { "HotelId", "ApartmentId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservation_HotelId_ApartmentId",
                table: "Reservation");

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "RegistrationDate", "ReleaseDate" },
                values: new object[] { new DateTime(2022, 7, 19, 11, 31, 13, 695, DateTimeKind.Utc).AddTicks(9267), new DateTime(2022, 7, 20, 11, 31, 13, 695, DateTimeKind.Utc).AddTicks(9268) });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_HotelId",
                table: "Reservation",
                column: "HotelId");
        }
    }
}
