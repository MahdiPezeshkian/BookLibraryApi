using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibraryApi.Migrations
{
    public partial class FourthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "rule",
                table: "Users",
                newName: "role");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "role",
                table: "Users",
                newName: "rule");
        }
    }
}
