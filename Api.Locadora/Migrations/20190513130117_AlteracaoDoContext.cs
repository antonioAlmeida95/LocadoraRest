using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Locadora.Migrations
{
    public partial class AlteracaoDoContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carros_Propriedades_ModeloId",
                table: "Carros");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Propriedades_NomeId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Propriedades_PerfilId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Versionamento_Propriedades_PropriedadeId",
                table: "Versionamento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Propriedades",
                table: "Propriedades");

            migrationBuilder.RenameTable(
                name: "Propriedades",
                newName: "Propriedade");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Propriedade",
                table: "Propriedade",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carros_Propriedade_ModeloId",
                table: "Carros",
                column: "ModeloId",
                principalTable: "Propriedade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Propriedade_NomeId",
                table: "Clientes",
                column: "NomeId",
                principalTable: "Propriedade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Propriedade_PerfilId",
                table: "Clientes",
                column: "PerfilId",
                principalTable: "Propriedade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Versionamento_Propriedade_PropriedadeId",
                table: "Versionamento",
                column: "PropriedadeId",
                principalTable: "Propriedade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carros_Propriedade_ModeloId",
                table: "Carros");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Propriedade_NomeId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Propriedade_PerfilId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Versionamento_Propriedade_PropriedadeId",
                table: "Versionamento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Propriedade",
                table: "Propriedade");

            migrationBuilder.RenameTable(
                name: "Propriedade",
                newName: "Propriedades");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Propriedades",
                table: "Propriedades",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carros_Propriedades_ModeloId",
                table: "Carros",
                column: "ModeloId",
                principalTable: "Propriedades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Propriedades_NomeId",
                table: "Clientes",
                column: "NomeId",
                principalTable: "Propriedades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Propriedades_PerfilId",
                table: "Clientes",
                column: "PerfilId",
                principalTable: "Propriedades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Versionamento_Propriedades_PropriedadeId",
                table: "Versionamento",
                column: "PropriedadeId",
                principalTable: "Propriedades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
