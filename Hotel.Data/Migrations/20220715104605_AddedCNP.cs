using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelApp.Data.Migrations
{
    public partial class AddedCNP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CNP",
                table: "Customer",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 1,
                column: "CNP",
                value: "1234567891011");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customer_CNP",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CNP",
                table: "Customer");

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "RegistrationDate", "ReleaseDate" },
                values: new object[] { new DateTime(2022, 7, 14, 12, 57, 11, 162, DateTimeKind.Utc).AddTicks(7779), new DateTime(2022, 7, 15, 12, 57, 11, 162, DateTimeKind.Utc).AddTicks(7782) });
        }
    }
}
