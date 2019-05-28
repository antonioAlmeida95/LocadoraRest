using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Api.Locadora.Migrations
{
    public partial class HistoricoVersao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "Versionamento");

            migrationBuilder.DropTable(
                name: "Propriedade");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_NomeId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_PerfilId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Carros_ModeloId",
                table: "Carros");

            migrationBuilder.DropColumn(
                name: "NomeId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "PerfilId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "ModeloId",
                table: "Carros");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Clientes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Perfil",
                table: "Clientes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Modelo",
                table: "Carros",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarroHistorico",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Versao = table.Column<int>(nullable: false),
                    DHModificacao = table.Column<DateTimeOffset>(nullable: false),
                    Modelo = table.Column<string>(nullable: true),
                    Ano = table.Column<int>(nullable: false),
                    Velocidade = table.Column<int>(nullable: false),
                    Diaria = table.Column<int>(nullable: false),
                    CarroId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarroHistorico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarroHistorico_Carros_CarroId",
                        column: x => x.CarroId,
                        principalTable: "Carros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClienteHistorico",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Versao = table.Column<int>(nullable: false),
                    DHModificacao = table.Column<DateTimeOffset>(nullable: false),
                    Tipo = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Perfil = table.Column<string>(nullable: true),
                    ClienteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteHistorico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClienteHistorico_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarroHistorico_CarroId",
                table: "CarroHistorico",
                column: "CarroId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteHistorico_ClienteId",
                table: "ClienteHistorico",
                column: "ClienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarroHistorico");

            migrationBuilder.DropTable(
                name: "ClienteHistorico");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Perfil",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Modelo",
                table: "Carros");

            migrationBuilder.AddColumn<int>(
                name: "NomeId",
                table: "Clientes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PerfilId",
                table: "Clientes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModeloId",
                table: "Carros",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Propriedade",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Valor = table.Column<string>(nullable: true),
                    Versao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propriedade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Versionamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Data = table.Column<DateTimeOffset>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    PropriedadeId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Tipo = table.Column<string>(nullable: true),
                    Valor = table.Column<string>(nullable: true),
                    Versao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versionamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Versionamento_Propriedade_PropriedadeId",
                        column: x => x.PropriedadeId,
                        principalTable: "Propriedade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_NomeId",
                table: "Clientes",
                column: "NomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_PerfilId",
                table: "Clientes",
                column: "PerfilId");

            migrationBuilder.CreateIndex(
                name: "IX_Carros_ModeloId",
                table: "Carros",
                column: "ModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Versionamento_PropriedadeId",
                table: "Versionamento",
                column: "PropriedadeId");

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
        }
    }
}
