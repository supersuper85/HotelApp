using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelApp.Data.Migrations
{
    public partial class AddedHotelTabel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "Reservation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "Apartment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Hotel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_HotelId",
                table: "Reservation",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Apartment_HotelId",
                table: "Apartment",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartment_Hotel_HotelId",
                table: "Apartment",
                column: "HotelId",
                principalTable: "Hotel",
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
                name: "FK_Reservation_Hotel_HotelId",
                table: "Reservation");

            migrationBuilder.DropTable(
                name: "Hotel");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_HotelId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Apartment_HotelId",
                table: "Apartment");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Apartment");
        }
    }
}
