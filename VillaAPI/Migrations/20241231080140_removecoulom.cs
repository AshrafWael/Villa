using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class removecoulom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CraetedDtae",
                value: new DateTime(2024, 12, 31, 10, 1, 39, 676, DateTimeKind.Local).AddTicks(2857));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CraetedDtae",
                value: new DateTime(2024, 12, 31, 10, 1, 39, 676, DateTimeKind.Local).AddTicks(2904));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CraetedDtae",
                value: new DateTime(2024, 12, 31, 10, 1, 39, 676, DateTimeKind.Local).AddTicks(2908));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
    }
}
