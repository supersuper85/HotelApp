using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelApp.Data.Migrations
{
    public partial class RestrictBehaviourBack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartment_Hotel_HotelId",
                table: "Apartment");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Hotel_HotelId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Reservation_ReservationId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Hotel_HotelId",
                table: "Reservation");

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "RegistrationDate", "ReleaseDate" },
                values: new object[] { new DateTime(2022, 7, 19, 11, 31, 13, 695, DateTimeKind.Utc).AddTicks(9267), new DateTime(2022, 7, 20, 11, 31, 13, 695, DateTimeKind.Utc).AddTicks(9268) });

            migrationBuilder.AddForeignKey(
                name: "FK_Apartment_Hotel_HotelId",
                table: "Apartment",
                column: "HotelId",
                principalTable: "Hotel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Hotel_HotelId",
                table: "Customer",
                column: "HotelId",
                principalTable: "Hotel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Reservation_ReservationId",
                table: "Customer",
                column: "ReservationId",
                principalTable: "Reservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Hotel_HotelId",
                table: "Reservation",
                column: "HotelId",
                principalTable: "Hotel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartment_Hotel_HotelId",
                table: "Apartment");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Hotel_HotelId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Reservation_ReservationId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Hotel_HotelId",
                table: "Reservation");

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "RegistrationDate", "ReleaseDate" },
                values: new object[] { new DateTime(2022, 7, 18, 12, 39, 9, 25, DateTimeKind.Utc).AddTicks(9546), new DateTime(2022, 7, 19, 12, 39, 9, 25, DateTimeKind.Utc).AddTicks(9548) });

            migrationBuilder.AddForeignKey(
                name: "FK_Apartment_Hotel_HotelId",
                table: "Apartment",
                column: "HotelId",
                principalTable: "Hotel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Hotel_HotelId",
                table: "Customer",
                column: "HotelId",
                principalTable: "Hotel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Reservation_ReservationId",
                table: "Customer",
                column: "ReservationId",
                principalTable: "Reservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Hotel_HotelId",
                table: "Reservation",
                column: "HotelId",
                principalTable: "Hotel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
