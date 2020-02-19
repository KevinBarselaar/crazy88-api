using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Crazy88Test.Migrations
{
    public partial class SessionExpirationDateTimeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Session");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiringDateTime",
                table: "Session",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiringDateTime",
                table: "Session");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Session",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "Session",
                nullable: true);
        }
    }
}
