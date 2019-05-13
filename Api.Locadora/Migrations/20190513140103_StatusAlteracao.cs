using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Locadora.Migrations
{
    public partial class StatusAlteracao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Clientes",
                nullable: false,
                oldClrType: typeof(string),
                oldDefaultValue: "Inadimplente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Clientes",
                nullable: false,
                defaultValue: "Inadimplente",
                oldClrType: typeof(string));
        }
    }
}
