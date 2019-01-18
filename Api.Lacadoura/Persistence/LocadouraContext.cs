using Api.Locadoura.Model;
using Microsoft.EntityFrameworkCore;

namespace Api.Locadoura.Persistencia
{
    public class LocadouraContext: DbContext
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
                .HasKey(lc => new {lc.ClienteId, lc.CarroId});

            modelBuilder.Entity<Cliente>()
                .HasMany(l => l.Locados).WithOne(c => c.Cliente).HasForeignKey(c => c.ClienteId).IsRequired();
            modelBuilder.Entity<Carro>()
                .HasMany(l => l.Locacoes).WithOne(c => c.Carro).HasForeignKey(c => c.CarroId).IsRequired();
        }
    }
}
