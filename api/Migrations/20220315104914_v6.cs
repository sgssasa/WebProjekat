using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class v6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tip",
                table: "Proizvodi",
                newName: "slika");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "slika",
                table: "Proizvodi",
                newName: "tip");
        }
    }
}
