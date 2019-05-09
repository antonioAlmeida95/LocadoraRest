using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Api.Locadora.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carros",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Modelo = table.Column<string>(nullable: true),
                    Ano = table.Column<int>(nullable: false),
                    Velocidade = table.Column<int>(nullable: false),
                    Diaria = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carros", x => x.Id);
                });

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
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Versao = table.Column<int>(nullable: false),
                    NomeId = table.Column<int>(nullable: false),
                    PerfilId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_Propriedade_NomeId",
                        column: x => x.NomeId,
                        principalTable: "Propriedade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clientes_Propriedade_PerfilId",
                        column: x => x.PerfilId,
                        principalTable: "Propriedade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Versionamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Versao = table.Column<int>(nullable: false),
                    Data = table.Column<DateTimeOffset>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Tipo = table.Column<string>(nullable: true),
                    Valor = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    PropriedadeId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Locacoes",
                columns: table => new
                {
                    ClienteId = table.Column<int>(nullable: false),
                    CarroId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locacoes", x => new { x.ClienteId, x.CarroId });
                    table.ForeignKey(
                        name: "FK_Locacoes_Carros_CarroId",
                        column: x => x.CarroId,
                        principalTable: "Carros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Locacoes_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
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
                name: "IX_Locacoes_CarroId",
                table: "Locacoes",
                column: "CarroId");

            migrationBuilder.CreateIndex(
                name: "IX_Versionamento_PropriedadeId",
                table: "Versionamento",
                column: "PropriedadeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locacoes");

            migrationBuilder.DropTable(
                name: "Versionamento");

            migrationBuilder.DropTable(
                name: "Carros");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Propriedade");
        }
    }
}
