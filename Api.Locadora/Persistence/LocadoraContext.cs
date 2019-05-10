using System;
using System.Collections.Generic;
using System.Linq;
using Api.Locadora.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Api.Locadora.Persistence
{
    public class LocadoraContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Locacoes> Locacoes { get; set; }
        public DbSet<Propriedade> Propriedades { get; set; }



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
                                client.Versao = versao;
                                client.Versao++;

                                if (member.Metadata.Name.Equals(nameof(Cliente.Nome)))
                                {
                                    var valor = orginalValues.Nome?.Valor ?? "";

                                    if (client.Nome.Valor.Equals(valor))
                                        continue;

                                    versionamento.Nome = member.Metadata.Name;
                                    versionamento.Versao = client.Nome.Versao = client.Versao;
                                    versionamento.Valor = client.Nome.Valor;
                                    client.Nome.Versoes.Add(versionamento);
                                }

                                if (member.Metadata.Name.Equals(nameof(Cliente.Perfil)))
                                {
                                    var valor = orginalValues.Perfil?.Valor ?? "";

                                    if (client.Perfil.Valor.Equals(valor))
                                        continue;

                                    versionamento.Nome = member.Metadata.Name;
                                    versionamento.Versao = client.Perfil.Versao = client.Versao;
                                    versionamento.Valor = client.Perfil.Valor;
                                    client.Perfil.Versoes.Add(versionamento);
                                }

                                break;
                            }
                        case Carro carro:
                            {
                                var originalValues = (Carro)values ?? new Carro();
                                carro.Versao = versao;
                                carro.Versao++;

                                if (member.Metadata.Name.Equals(nameof(Carro.Modelo)))
                                {
                                    var valor = originalValues.Modelo?.Valor ?? "";

                                    if (carro.Modelo.Valor.Equals(valor))
                                        continue;

                                    versionamento.Nome = member.Metadata.Name;
                                    versionamento.Versao = carro.Modelo.Versao = carro.Versao;
                                    versionamento.Valor = carro.Modelo.Valor;
                                    carro.Modelo.Versoes.Add(versionamento);
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
            if (entry.State == EntityState.Modified)
            {
                versao = entry.GetDatabaseValues().GetValue<int>(nameof(IHistorico.Versao));
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

                        return  value.Modelo.Valor.Equals(carro.Modelo.Valor);
                    }
                }
            }

            original = null;
            return false;
        }
    }
}

