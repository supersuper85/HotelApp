using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelApp.Data.Migrations
{
    public partial class AddedUniquConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customer_CNP",
                table: "Customer");

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "RegistrationDate", "ReleaseDate" },
                values: new object[] { new DateTime(2022, 7, 15, 11, 26, 6, 129, DateTimeKind.Utc).AddTicks(126), new DateTime(2022, 7, 16, 11, 26, 6, 129, DateTimeKind.Utc).AddTicks(129) });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ApartmentId_HotelId_CNP",
                table: "Customer",
                columns: new[] { "ApartmentId", "HotelId", "CNP" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customer_ApartmentId_HotelId_CNP",
                table: "Customer");

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "RegistrationDate", "ReleaseDate" },
                values: new object[] { new DateTime(2022, 7, 15, 10, 46, 4, 693, DateTimeKind.Utc).AddTicks(6846), new DateTime(2022, 7, 16, 10, 46, 4, 693, DateTimeKind.Utc).AddTicks(6847) });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CNP",
                table: "Customer",
                column: "CNP",
                unique: true);
        }
    }
}
