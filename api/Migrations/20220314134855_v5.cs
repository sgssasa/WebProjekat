using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "kolicina",
                table: "spojPorudzbine",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "kolicina",
                table: "spojPorudzbine");
        }
    }
}
