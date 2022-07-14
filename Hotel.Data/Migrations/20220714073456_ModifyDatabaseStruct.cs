using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelApp.Data.Migrations
{
    public partial class ModifyDatabaseStruct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Apartment_ApartmentId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Apartment_ApartmentId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Customer_CustomerId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_ApartmentId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_CustomerId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Customer_ApartmentId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Reservation");

            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Apartment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_HotelId",
                table: "Customer",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ReservationId",
                table: "Customer",
                column: "ReservationId",
                unique: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Hotel_HotelId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Reservation_ReservationId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_HotelId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_ReservationId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Apartment");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ApartmentId",
                table: "Reservation",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_CustomerId",
                table: "Reservation",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ApartmentId",
                table: "Customer",
                column: "ApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Apartment_ApartmentId",
                table: "Customer",
                column: "ApartmentId",
                principalTable: "Apartment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Apartment_ApartmentId",
                table: "Reservation",
                column: "ApartmentId",
                principalTable: "Apartment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Customer_CustomerId",
                table: "Reservation",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
