using Microsoft.EntityFrameworkCore;
using pruebaTecnica.Models;

namespace pruebaTecnica.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options
        ) : base(options)
        {
        }

        public DbSet<Articulo> Articulos => Set<Articulo>();

        public DbSet<ArticuloResultado> ArticulosResultado =>
            Set<ArticuloResultado>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Articulo>()
                .HasKey(a => a.IdArticulo);

            modelBuilder.Entity<Articulo>()
                .HasIndex(a => a.CodigoBarra)
                .IsUnique();

            modelBuilder.Entity<ArticuloResultado>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);

                entity.Property(a => a.PrecioDetal)
                    .HasPrecision(10, 2);
            });
        }
    }
}
