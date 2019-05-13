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
                if (entry.Entity is Versionamento || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                if (Modified(entry, out var values))
                    continue;

                var versao = GetVersao(entry);

                foreach (var member in entry.Members)
                {

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
                                var orginalValues = (Cliente)values ?? new Cliente();
                                client.Versao = versao + 1;

                                if (member.Metadata.Name.Equals(nameof(Cliente.Nome)))
                                {
                                    if (client.Nome.Valor.Equals(orginalValues.Nome?.Valor)) continue;

                                    client.Nome.AdcionarVersao(versionamento, member.Metadata.Name, client.Versao);
                                }

                                if (member.Metadata.Name.Equals(nameof(Cliente.Perfil)))
                                {
                                    if (client.Perfil.Valor.Equals(orginalValues.Perfil?.Valor)) continue;

                                    client.Perfil.AdcionarVersao(versionamento, member.Metadata.Name, client.Versao);
                                }
                                break;
                            }
                        case Carro carro:
                            {
                                var originalValues = (Carro)values ?? new Carro();
                                carro.Versao = versao + 1;

                                if (member.Metadata.Name.Equals(nameof(Carro.Modelo)))
                                {
                                    if (carro.Modelo.Valor.Equals(originalValues.Modelo?.Valor)) continue;

                                    carro.Modelo.AdcionarVersao(versionamento, member.Metadata.Name, carro.Versao);
                                }
                                break;
                            }
                    }
                }
            }

        }

        private int GetVersao(EntityEntry entry)
        {
            var versao = 0;
            if (entry != null && entry.State == EntityState.Modified)
            {
                versao = entry.GetDatabaseValues().GetValue<int>(nameof(Entity.Versao));
            }

            return versao;
        }

        private bool Modified(EntityEntry entry, out object original)
        {
            if (entry.State == EntityState.Modified)
            {
                switch (entry.Entity)
                {
                    case Cliente cliente:
                        {
                            var value = this.Clientes.AsNoTracking()
                                .Include(c => c.Nome)
                                .Include(c => c.Perfil)
                                .FirstOrDefault(c => c.Id == cliente.Id);
                            original = value;

                            if (value == null) return false;

                            cliente.Versao = value.Versao;
                            cliente.Nome.Versao = value.Nome.Versao;
                            cliente.Perfil.Versao = value.Perfil.Versao;

                            return (value.Nome.Valor.Equals(cliente.Nome.Valor)
                                    && value.Perfil.Valor.Equals(cliente.Perfil.Valor));
                        }
                    case Carro carro:
                        {
                            var value = this.Carros.AsNoTracking()
                                .Include(c => c.Modelo)
                                .FirstOrDefault(c => c.Id == carro.Id);
                            original = value;

                            if (value == null) return false;

                            carro.Versao = value.Versao;
                            carro.Modelo.Versao = value.Modelo.Versao;

                            return value.Modelo.Valor.Equals(carro.Modelo.Valor);
                        }
                }
            }

            original = null;
            return false;
        }
    }
}

