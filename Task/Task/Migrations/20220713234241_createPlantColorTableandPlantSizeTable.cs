using Microsoft.EntityFrameworkCore.Migrations;

namespace Task.Migrations
{
    public partial class createPlantColorTableandPlantSizeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlantColors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantId = table.Column<int>(nullable: false),
                    ColorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantColors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantColors_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantColors_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlantSizes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantId = table.Column<int>(nullable: false),
                    SizeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantSizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantSizes_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantSizes_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlantColors_ColorId",
                table: "PlantColors",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantColors_PlantId",
                table: "PlantColors",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantSizes_PlantId",
                table: "PlantSizes",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantSizes_SizeId",
                table: "PlantSizes",
                column: "SizeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlantColors");

            migrationBuilder.DropTable(
                name: "PlantSizes");
        }
    }
}
