using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelApp.Data.Migrations
{
    public partial class NewCustomerConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customer_ApartmentId_HotelId_CNP",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_HotelId",
                table: "Customer");

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "RegistrationDate", "ReleaseDate" },
                values: new object[] { new DateTime(2022, 7, 15, 12, 12, 57, 604, DateTimeKind.Utc).AddTicks(5497), new DateTime(2022, 7, 16, 12, 12, 57, 604, DateTimeKind.Utc).AddTicks(5498) });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ApartmentId_HotelId",
                table: "Customer",
                columns: new[] { "ApartmentId", "HotelId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_HotelId_CNP",
                table: "Customer",
                columns: new[] { "HotelId", "CNP" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customer_ApartmentId_HotelId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_HotelId_CNP",
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

            migrationBuilder.CreateIndex(
                name: "IX_Customer_HotelId",
                table: "Customer",
                column: "HotelId");
        }
    }
}
