using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelApp.Data.Migrations
{
    public partial class ChangesCustomerDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hotel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Apartment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DailyRentInEuro = table.Column<float>(type: "real", nullable: false),
                    NumberOfRooms = table.Column<int>(type: "int", nullable: false),
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apartment_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CNP = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApartmentId = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservation_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservation_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Age", "CNP", "HotelId", "Name" },
                values: new object[] { 1, 20, "1234567891011", null, "Cristi" });

            migrationBuilder.InsertData(
                table: "Hotel",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Roman" },
                    { 2, "Transilvania" },
                    { 3, "Roscu" }
                });

            migrationBuilder.InsertData(
                table: "Apartment",
                columns: new[] { "Id", "CustomerId", "DailyRentInEuro", "HotelId", "NumberOfRooms", "ReservationId", "RoomNumber" },
                values: new object[] { 1, 0, 25f, 1, 2, 0, 1 });

            migrationBuilder.InsertData(
                table: "Apartment",
                columns: new[] { "Id", "CustomerId", "DailyRentInEuro", "HotelId", "NumberOfRooms", "ReservationId", "RoomNumber" },
                values: new object[] { 2, 1, 35f, 1, 3, 1, 2 });

            migrationBuilder.InsertData(
                table: "Reservation",
                columns: new[] { "Id", "ApartmentId", "CustomerId", "HotelId", "RegistrationDate", "ReleaseDate" },
                values: new object[] { 1, 2, 1, 1, new DateTime(2022, 7, 26, 7, 46, 11, 480, DateTimeKind.Utc).AddTicks(7827), new DateTime(2022, 7, 27, 12, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Apartment_HotelId",
                table: "Apartment",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Apartment_RoomNumber",
                table: "Apartment",
                column: "RoomNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_HotelId",
                table: "Customer",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_Name",
                table: "Hotel",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_CustomerId",
                table: "Reservation",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_HotelId_ApartmentId",
                table: "Reservation",
                columns: new[] { "HotelId", "ApartmentId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apartment");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Hotel");
        }
    }
}
