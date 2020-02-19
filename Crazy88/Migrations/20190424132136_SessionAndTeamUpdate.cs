using Microsoft.EntityFrameworkCore.Migrations;

namespace Crazy88Test.Migrations
{
    public partial class SessionAndTeamUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Session",
                newName: "Time");

            migrationBuilder.AddColumn<int>(
                name: "Session",
                table: "Team",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Session",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Session",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Session");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Session",
                newName: "DateTime");
        }
    }
}
