using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Locadora.Migrations
{
    public partial class Alter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Modelo",
                table: "Carros");

            migrationBuilder.AddColumn<int>(
                name: "ModeloId",
                table: "Carros",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Carros_ModeloId",
                table: "Carros",
                column: "ModeloId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carros_Propriedade_ModeloId",
                table: "Carros",
                column: "ModeloId",
                principalTable: "Propriedade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carros_Propriedade_ModeloId",
                table: "Carros");

            migrationBuilder.DropIndex(
                name: "IX_Carros_ModeloId",
                table: "Carros");

            migrationBuilder.DropColumn(
                name: "ModeloId",
                table: "Carros");

            migrationBuilder.AddColumn<string>(
                name: "Modelo",
                table: "Carros",
                nullable: true);
        }
    }
}
