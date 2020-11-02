namespace MServices.Domain.Infra.DB
{
    using Microsoft.EntityFrameworkCore;

    using MServices.Domain.Models;

    public class FrotaContext : DbContext
    {
        public FrotaContext()
        {
        }

        public FrotaContext(DbContextOptions<FrotaContext> options)
            : base(options)
        {
        }

        public DbSet<Veiculo> Veiculos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("connectionString-definir-gerar-migration");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FrotaContext).Assembly);
            modelBuilder.Entity<Veiculo>().HasKey(a => new { a.Id, a.Chassi });
        }
    }
}
