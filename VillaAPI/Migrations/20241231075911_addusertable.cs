using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class addusertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CraetedDtae",
                value: new DateTime(2024, 12, 31, 9, 59, 10, 268, DateTimeKind.Local).AddTicks(8348));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CraetedDtae",
                value: new DateTime(2024, 12, 31, 9, 59, 10, 268, DateTimeKind.Local).AddTicks(8404));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CraetedDtae",
                value: new DateTime(2024, 12, 31, 9, 59, 10, 268, DateTimeKind.Local).AddTicks(8407));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CraetedDtae",
                value: new DateTime(2024, 12, 30, 3, 23, 27, 445, DateTimeKind.Local).AddTicks(1065));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CraetedDtae",
                value: new DateTime(2024, 12, 30, 3, 23, 27, 445, DateTimeKind.Local).AddTicks(1152));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CraetedDtae",
                value: new DateTime(2024, 12, 30, 3, 23, 27, 445, DateTimeKind.Local).AddTicks(1156));
        }
    }
}
