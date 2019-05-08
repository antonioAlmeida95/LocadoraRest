using System;
using System.Collections.Generic;
using System.Linq;
using Api.Locadora.Models;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Api.Locadora.Persistence
{
    public class LocadoraContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Locacoes> Locacoes { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host = localhost; Database = Loja; Username = postgres; Password = postzeus2011");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Locacoes>()
                .HasKey(lc => new { lc.ClienteId, lc.CarroId });

            modelBuilder.Entity<Cliente>()
                .HasMany(l => l.Locados).WithOne(c => c.Cliente).HasForeignKey(c => c.ClienteId).IsRequired();

            modelBuilder.Entity<Carro>()
                .HasMany(l => l.Locacoes).WithOne(c => c.Carro).HasForeignKey(c => c.CarroId).IsRequired();

            modelBuilder.Entity<Propriedade>()
                .HasMany(p => p.HistorioVersionamento).WithOne(h => h.Propriedade).HasForeignKey(k => k.PropriedadeId)
                .IsRequired();
        }

        public override int SaveChanges()
        {

            try
            {
                OnBeforeChanging();
                return base.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private void OnBeforeChanging()
        {
            ChangeTracker.DetectChanges();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Versionamento || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var versionamento = new Versionamento();
                var versao = 0;

                foreach (var property in entry.Properties)
                {
                    if (entry.Entity is Cliente cliente && entry.State == EntityState.Modified
                                                        && property.Metadata.Name.Equals(nameof(Cliente.Versao)))
                    {
                        versao = entry.GetDatabaseValues().GetValue<int>(property.Metadata.Name);
                        cliente.Versao = ++versao;
                    }

                
                    versionamento.Tipo = entry.Metadata.DisplayName();
                    versionamento.Status = entry.State.ToString();
                    versionamento.Versao = versao;
                    versionamento.Data = DateTimeOffset.UtcNow;
                }

                foreach (var member in entry.Members)
                {
                    if (entry.Entity is Cliente cliente)
                    {
                        if (member.Metadata.Name.Equals(nameof(Cliente.Nome)))
                        {
                            versionamento.Nome = member.Metadata.Name;
                            cliente.Nome.Versao = cliente.Versao;
                            versionamento.Valor = cliente.Nome.Valor;
                            cliente.Nome.HistorioVersionamento.Add(versionamento);
                        }
                    }
                }
            }

        }

    }
}

