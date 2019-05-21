using System;
using System.Collections.Generic;
using System.Linq;
using Api.Locadora.Models;
using Api.Locadora.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Api.Locadora.Persistence
{
    public class LocadoraContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Carro> Carros { get; set; }

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
                .HasMany(p => p.Versoes).WithOne(h => h.Propriedade).HasForeignKey(k => k.PropriedadeId)
                .IsRequired();

            modelBuilder.Entity<Propriedade>()
                .HasMany(pro => pro.NomeClientes).WithOne(cl => cl.Nome).HasForeignKey(c => c.NomeId)
                .IsRequired();

            modelBuilder.Entity<Propriedade>()
                .HasMany(prop => prop.PerfilClientes).WithOne(cl => cl.Perfil).HasForeignKey(c => c.PerfilId)
                .IsRequired();

            modelBuilder.Entity<Propriedade>()
                .HasMany(prop => prop.ModeloCarros).WithOne(car => car.Modelo).HasForeignKey(c => c.ModeloId)
                .IsRequired();

            modelBuilder.Entity<Cliente>()
                .Property(c => c.Tipo)
                .HasConversion(c => c.ToString(),
                    c => (Status)Enum.Parse(typeof(Status), c));
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
                if (entry.Entity is Versionamento || entry.State == EntityState.Detached ||
                    entry.State == EntityState.Unchanged)
                    continue;

                var versao = entry.OriginalValues.GetValue<int>(nameof(Propriedade.Versao)) + 1;
                var unchanged = true;

                foreach (var reference in entry.References)
                {
                    var originalValues = reference.TargetEntry.OriginalValues.ToObject()
                                            as Propriedade ?? new Propriedade();
                    var versionamento = new Versionamento
                    {
                        Tipo = entry.Metadata.DisplayName(),
                        Status = entry.State.ToString(),
                        Data = DateTimeOffset.UtcNow
                    };

                    switch (entry.Entity)
                    {
                        case Cliente client:
                        {
                            if(unchanged)
                                client.Versao = (int) entry.OriginalValues[nameof(Cliente.Versao)];
                                
                                if (reference.Metadata.Name.Equals(nameof(Cliente.Nome)))
                                {
                                    client.Nome.Versao =
                                        reference.TargetEntry.OriginalValues.GetValue<int>(nameof(Propriedade.Versao));

                                    if (client.Nome.Valor.Equals(originalValues?.Valor)
                                        && entry.State == EntityState.Modified) continue;
                 
                                    unchanged = false;
                                    client.Nome.AdcionarVersao(versionamento, reference.Metadata.Name, client.Versao = versao);
                                }

                                if (reference.Metadata.Name.Equals(nameof(Cliente.Perfil)))
                                {
                                    client.Perfil.Versao =
                                        reference.TargetEntry.OriginalValues.GetValue<int>(nameof(Propriedade.Versao));

                                    if (client.Perfil.Valor.Equals(originalValues?.Valor)
                                        && entry.State == EntityState.Modified) continue;
                                    

                                    unchanged = false;
                                    client.Perfil.AdcionarVersao(versionamento, reference.Metadata.Name, client.Versao = versao);
                                }
                                break;
                            }
                        case Carro carro:
                            {
                                
                                carro.Versao = (int)entry.OriginalValues[nameof(Carro.Versao)];

                                if (reference.Metadata.Name.Equals(nameof(Carro.Modelo)))
                                {
                                    carro.Modelo.Versao =
                                        reference.TargetEntry.OriginalValues.GetValue<int>(nameof(Propriedade.Versao));

                                    if (carro.Modelo.Valor.Equals(originalValues?.Valor) 
                                        && entry.State == EntityState.Modified) continue;

                                    carro.Modelo.AdcionarVersao(versionamento, reference.Metadata.Name, carro.Versao = versao);
                                }
                                break;
                            }
                    }
                }

            }

        }
    }
}

