using System;
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

                if (Modified(entry, out var original))
                    continue;

                foreach (var member in entry.Members)
                {

                    var versionamento = new Versionamento();
                    var versao = 0;

                    versao = GetVersao(entry);
                    versao++;
                    versionamento.Tipo = entry.Metadata.DisplayName();
                    versionamento.Status = entry.State.ToString();
                    versionamento.Versao = versao;
                    versionamento.Data = DateTimeOffset.UtcNow;

                    switch (entry.Entity)
                    {
                        case Cliente client:
                            {
                                var cliente = (Cliente)original ?? new Cliente();

                                if (member.Metadata.Name.Equals(nameof(Cliente.Nome)))
                                {
                                    client.Versao = versao;

                                    var valor = cliente.Nome.Valor ?? "";

                                    if (client.Nome.Valor.Equals(valor))
                                        continue;

                                    versionamento.Nome = member.Metadata.Name;
                                    client.Nome.Versao = client.Versao;
                                    versionamento.Valor = client.Nome.Valor;
                                    client.Nome.Versoes.Add(versionamento);
                                }

                                if (member.Metadata.Name.Equals(nameof(Cliente.Perfil)))
                                {
                                    client.Versao = versao;
                                    var valor = cliente.Perfil.Valor ?? "";

                                    if (client.Perfil.Valor.Equals(valor))
                                        continue;


                                    versionamento.Nome = member.Metadata.Name;
                                    client.Perfil.Versao = client.Versao;
                                    versionamento.Valor = client.Perfil.Valor;
                                    client.Perfil.Versoes.Add(versionamento);
                                }

                                break;
                            }
                        case Carro carro:
                            {
                                var car = (Carro)original ?? new Carro();

                                if (member.Metadata.Name.Equals(nameof(Carro.Modelo)))
                                {
                                    var valor = car.Modelo.Valor ?? "";
                                    if (carro.Modelo.Valor.Equals(valor))
                                        continue;

                                    carro.Versao = versao;
                                    versionamento.Nome = member.Metadata.Name;
                                    carro.Modelo.Versao = carro.Versao;
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
                if (entry.Entity is Cliente cliente)
                {
                    var value = this.Clientes
                       .Include(c => c.Nome)
                       .Include(c => c.Perfil)
                       .FirstOrDefault(c => c.Id == cliente.Id);
                    original = value;

                    return value != null && (value.Nome.Valor.Equals(cliente.Nome.Valor)
                                             && value.Perfil.Valor.Equals(cliente.Perfil.Valor));
                }

                if (entry.Entity is Carro carro)
                {
                    var value = this.Carros
                        .Include(c => c.Modelo)
                        .FirstOrDefault(c => c.Id == carro.Id);
                    original = value;

                    return value != null && value.Modelo.Valor.Equals(carro.Modelo.Valor);
                }
            }

            original = null;
            return false;
        }
    }
}

