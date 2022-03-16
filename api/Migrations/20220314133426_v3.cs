using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stolice_Stolovi_stoid",
                table: "Stolice");

            migrationBuilder.AddForeignKey(
                name: "FK_Stolice_Stolovi_stoid",
                table: "Stolice",
                column: "stoid",
                principalTable: "Stolovi",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stolice_Stolovi_stoid",
                table: "Stolice");

            migrationBuilder.AddForeignKey(
                name: "FK_Stolice_Stolovi_stoid",
                table: "Stolice",
                column: "stoid",
                principalTable: "Stolovi",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
