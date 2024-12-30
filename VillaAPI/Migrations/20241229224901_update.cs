using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CraetedDtae",
                value: new DateTime(2024, 12, 30, 0, 48, 58, 711, DateTimeKind.Local).AddTicks(8115));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CraetedDtae",
                value: new DateTime(2024, 12, 30, 0, 48, 58, 711, DateTimeKind.Local).AddTicks(8162));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CraetedDtae",
                value: new DateTime(2024, 12, 30, 0, 48, 58, 711, DateTimeKind.Local).AddTicks(8165));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CraetedDtae",
                value: new DateTime(2024, 12, 27, 3, 49, 58, 50, DateTimeKind.Local).AddTicks(1066));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CraetedDtae",
                value: new DateTime(2024, 12, 27, 3, 49, 58, 50, DateTimeKind.Local).AddTicks(1126));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CraetedDtae",
                value: new DateTime(2024, 12, 27, 3, 49, 58, 50, DateTimeKind.Local).AddTicks(1130));
        }
    }
}
