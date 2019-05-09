using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Locadora.Migrations
{
    public partial class Altera : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Versao",
                table: "Carros",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Versao",
                table: "Carros");
        }
    }
}
