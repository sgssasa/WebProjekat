using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Porudzbine_Stolice_stolicaid",
                table: "Porudzbine");

            migrationBuilder.AddForeignKey(
                name: "FK_Porudzbine_Stolice_stolicaid",
                table: "Porudzbine",
                column: "stolicaid",
                principalTable: "Stolice",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Porudzbine_Stolice_stolicaid",
                table: "Porudzbine");

            migrationBuilder.AddForeignKey(
                name: "FK_Porudzbine_Stolice_stolicaid",
                table: "Porudzbine",
                column: "stolicaid",
                principalTable: "Stolice",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
