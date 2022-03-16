using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kafici",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kafici", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Proizvodi",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cena = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvodi", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Stolovi",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    oznaka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    stoliceGore = table.Column<int>(type: "int", nullable: false),
                    stoliceDesno = table.Column<int>(type: "int", nullable: false),
                    stoliceDole = table.Column<int>(type: "int", nullable: false),
                    stoliceLevo = table.Column<int>(type: "int", nullable: false),
                    kaficid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stolovi", x => x.id);
                    table.ForeignKey(
                        name: "FK_Stolovi_Kafici_kaficid",
                        column: x => x.kaficid,
                        principalTable: "Kafici",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stolice",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    oznaka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    slobodna = table.Column<bool>(type: "bit", nullable: false),
                    stoid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stolice", x => x.id);
                    table.ForeignKey(
                        name: "FK_Stolice_Stolovi_stoid",
                        column: x => x.stoid,
                        principalTable: "Stolovi",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Porudzbine",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    obradjena = table.Column<bool>(type: "bit", nullable: false),
                    stolicaid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Porudzbine", x => x.id);
                    table.ForeignKey(
                        name: "FK_Porudzbine_Stolice_stolicaid",
                        column: x => x.stolicaid,
                        principalTable: "Stolice",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "spojPorudzbine",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    proizvodid = table.Column<int>(type: "int", nullable: true),
                    porudzbinaid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spojPorudzbine", x => x.id);
                    table.ForeignKey(
                        name: "FK_spojPorudzbine_Porudzbine_porudzbinaid",
                        column: x => x.porudzbinaid,
                        principalTable: "Porudzbine",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_spojPorudzbine_Proizvodi_proizvodid",
                        column: x => x.proizvodid,
                        principalTable: "Proizvodi",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbine_stolicaid",
                table: "Porudzbine",
                column: "stolicaid");

            migrationBuilder.CreateIndex(
                name: "IX_spojPorudzbine_porudzbinaid",
                table: "spojPorudzbine",
                column: "porudzbinaid");

            migrationBuilder.CreateIndex(
                name: "IX_spojPorudzbine_proizvodid",
                table: "spojPorudzbine",
                column: "proizvodid");

            migrationBuilder.CreateIndex(
                name: "IX_Stolice_stoid",
                table: "Stolice",
                column: "stoid");

            migrationBuilder.CreateIndex(
                name: "IX_Stolovi_kaficid",
                table: "Stolovi",
                column: "kaficid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "spojPorudzbine");

            migrationBuilder.DropTable(
                name: "Porudzbine");

            migrationBuilder.DropTable(
                name: "Proizvodi");

            migrationBuilder.DropTable(
                name: "Stolice");

            migrationBuilder.DropTable(
                name: "Stolovi");

            migrationBuilder.DropTable(
                name: "Kafici");
        }
    }
}
