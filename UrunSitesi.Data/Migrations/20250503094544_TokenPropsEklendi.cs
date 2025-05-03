using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrunSitesi.Data.Migrations
{
    /// <inheritdoc />
    public partial class TokenPropsEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpireDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "RefreshToken", "RefreshTokenExpireDate", "UserGuid" },
                values: new object[] { new DateTime(2025, 5, 3, 12, 45, 43, 516, DateTimeKind.Local).AddTicks(3093), null, null, new Guid("11b408b2-aad2-4e8d-9c47-f7fa8ab00296") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpireDate",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2025, 3, 22, 13, 57, 42, 130, DateTimeKind.Local).AddTicks(3468), new Guid("d57a77f7-5c3b-4278-ac97-d5ada5188e54") });
        }
    }
}
