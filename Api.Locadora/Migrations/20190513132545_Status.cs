using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Locadora.Migrations
{
    public partial class Status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Clientes",
                nullable: false,
                defaultValue: "Inadimplente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Clientes");
        }
    }
}
