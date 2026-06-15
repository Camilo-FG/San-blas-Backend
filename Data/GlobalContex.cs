using Microsoft.EntityFrameworkCore;
using SanblasBackend.Models;

namespace SanblasBackend.Data
{
    public class GlobalContex : DbContext
    {
        public GlobalContex(DbContextOptions<GlobalContex> options) : base(options)
        {
        }

        public DbSet<FormSacra> FormSacras { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones adicionales si son necesarias
            modelBuilder.Entity<FormSacra>(entity =>
            {

                entity.HasKey(e => e.id);
                entity.Property(e => e.Nombre).IsRequired();
                entity.Property(e => e.Cedula).IsRequired();
                entity.Property(e => e.TipoSacramento).HasConversion<string>();
            });
        }
    }
}
