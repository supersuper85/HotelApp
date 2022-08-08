using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuditApp.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Audit",
                columns: new[] { "Id", "ActionType", "EntityId", "EntityName", "NewValues", "OldValues", "TimeStamp" },
                values: new object[] { 1, "INSERT", 1, "Customer", "{\"id\":1, \"name\":\"asd\", \"cnp\":\"1234567812345\"}", null, new DateTime(2022, 8, 3, 7, 1, 31, 690, DateTimeKind.Utc).AddTicks(3000) });

            migrationBuilder.InsertData(
                table: "Audit",
                columns: new[] { "Id", "ActionType", "EntityId", "EntityName", "NewValues", "OldValues", "TimeStamp" },
                values: new object[] { 2, "DELETE", 1, "Customer", null, "{\"id\":1, \"name\":\"asd\", \"cnp\":\"1234567812345\"}", new DateTime(2022, 8, 3, 7, 1, 31, 690, DateTimeKind.Utc).AddTicks(3007) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audit");
        }
    }
}
