using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "stoliceDesno",
                table: "Stolovi");

            migrationBuilder.DropColumn(
                name: "stoliceDole",
                table: "Stolovi");

            migrationBuilder.DropColumn(
                name: "stoliceGore",
                table: "Stolovi");

            migrationBuilder.DropColumn(
                name: "stoliceLevo",
                table: "Stolovi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "stoliceDesno",
                table: "Stolovi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "stoliceDole",
                table: "Stolovi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "stoliceGore",
                table: "Stolovi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "stoliceLevo",
                table: "Stolovi",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
