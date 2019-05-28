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

            modelBuilder.Entity<Cliente>()
                .Property(c => c.Tipo)
                .HasConversion(c => c.ToString(),
                    c => (Status)Enum.Parse(typeof(Status), c));

            modelBuilder.Entity<Cliente>()
                .HasMany(c => (IList<ClienteHistorico>) c.Historicos).WithOne(h => h.Cliente)
                .HasForeignKey(k => k.ClienteId).IsRequired().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Carro>()
                .HasMany(c => (IList<CarroHistorico>) c.Historicos).WithOne(h => h.Carro)
                .HasForeignKey(k => k.CarroId).IsRequired().OnDelete(DeleteBehavior.Restrict);
        }

        public override int SaveChanges()
        {
            var modefiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is TrackableEntity
                        && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in modefiedEntries)
            {
                entry.Property(nameof(IHistorico.Versao)).CurrentValue = 
                    (int)entry.Property(nameof(IHistorico.Versao)).OriginalValue + 1;

                ((TrackableEntity) entry.Entity).InserirNovoHistorico();
            }

            return base.SaveChanges();
        }

       
    }
}

