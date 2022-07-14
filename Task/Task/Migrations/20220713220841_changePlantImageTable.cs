using Microsoft.EntityFrameworkCore.Migrations;

namespace Task.Migrations
{
    public partial class changePlantImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Primary",
                table: "PlantImages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Primary",
                table: "PlantImages");
        }
    }
}
