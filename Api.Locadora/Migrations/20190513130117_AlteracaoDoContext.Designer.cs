﻿// <auto-generated />
using System;
using Api.Locadora.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Api.Locadora.Migrations
{
    [DbContext(typeof(LocadoraContext))]
    [Migration("20190513130117_AlteracaoDoContext")]
    partial class AlteracaoDoContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Api.Locadora.Models.Carro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Ano");

                    b.Property<int>("Diaria");

                    b.Property<int>("ModeloId");

                    b.Property<int>("Velocidade");

                    b.Property<int>("Versao");

                    b.HasKey("Id");

                    b.HasIndex("ModeloId");

                    b.ToTable("Carros");
                });

            modelBuilder.Entity("Api.Locadora.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("NomeId");

                    b.Property<int>("PerfilId");

                    b.Property<int>("Versao");

                    b.HasKey("Id");

                    b.HasIndex("NomeId");

                    b.HasIndex("PerfilId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Api.Locadora.Models.Locacoes", b =>
                {
                    b.Property<int>("ClienteId");

                    b.Property<int>("CarroId");

                    b.HasKey("ClienteId", "CarroId");

                    b.HasIndex("CarroId");

                    b.ToTable("Locacoes");
                });

            modelBuilder.Entity("Api.Locadora.Models.Propriedade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Valor");

                    b.Property<int>("Versao");

                    b.HasKey("Id");

                    b.ToTable("Propriedade");
                });

            modelBuilder.Entity("Api.Locadora.Models.Versionamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("Data");

                    b.Property<string>("Nome");

                    b.Property<int>("PropriedadeId");

                    b.Property<string>("Status");

                    b.Property<string>("Tipo");

                    b.Property<string>("Valor");

                    b.Property<int>("Versao");

                    b.HasKey("Id");

                    b.HasIndex("PropriedadeId");

                    b.ToTable("Versionamento");
                });

            modelBuilder.Entity("Api.Locadora.Models.Carro", b =>
                {
                    b.HasOne("Api.Locadora.Models.Propriedade", "Modelo")
                        .WithMany("ModeloCarros")
                        .HasForeignKey("ModeloId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Api.Locadora.Models.Cliente", b =>
                {
                    b.HasOne("Api.Locadora.Models.Propriedade", "Nome")
                        .WithMany("NomeClientes")
                        .HasForeignKey("NomeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Api.Locadora.Models.Propriedade", "Perfil")
                        .WithMany("PerfilClientes")
                        .HasForeignKey("PerfilId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Api.Locadora.Models.Locacoes", b =>
                {
                    b.HasOne("Api.Locadora.Models.Carro", "Carro")
                        .WithMany("Locacoes")
                        .HasForeignKey("CarroId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Api.Locadora.Models.Cliente", "Cliente")
                        .WithMany("Locados")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Api.Locadora.Models.Versionamento", b =>
                {
                    b.HasOne("Api.Locadora.Models.Propriedade", "Propriedade")
                        .WithMany("Versoes")
                        .HasForeignKey("PropriedadeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
