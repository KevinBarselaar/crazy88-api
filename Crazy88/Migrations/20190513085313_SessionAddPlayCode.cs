using Microsoft.EntityFrameworkCore.Migrations;

namespace Crazy88Test.Migrations
{
    public partial class SessionAddPlayCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayCode",
                table: "Session",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayCode",
                table: "Session");
        }
    }
}
