using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IFYB.Migrations
{
    public partial class AddSearchToCookie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Search",
                table: "Cookies",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Search",
                table: "Cookies");
        }
    }
}
