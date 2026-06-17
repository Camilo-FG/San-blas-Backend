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
        public DbSet<InscripcionCatequesis> InscripcionesCatequesis { get; set; }
        public DbSet<Catequizando> Catequizandos { get; set; }
        public DbSet<BautismoCatequizando> BautismosCatequizando { get; set; }
        public DbSet<AdecuacionCatequizando> AdecuacionesCatequizando { get; set; }
        public DbSet<CondicionSaludCatequizando> CondicionesSaludCatequizando { get; set; }
        public DbSet<MadreCatequizando> MadresCatequizando { get; set; }
        public DbSet<Donacion> Donaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FormSacra>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.Nombre).IsRequired();
                entity.Property(e => e.Cedula).IsRequired();
                entity.Property(e => e.TipoSacramento).HasConversion<string>();
            });

            modelBuilder.Entity<InscripcionCatequesis>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CentroCatequesis).IsRequired();
                entity.Property(e => e.NivelAInscribirse).IsRequired();
                entity.Property(e => e.Estado).IsRequired().HasDefaultValue("Pendiente");
                entity.Property(e => e.FechaSolicitud).IsRequired();

                entity.HasOne(e => e.Catequizando)
                    .WithOne(c => c.InscripcionCatequesis)
                    .HasForeignKey<Catequizando>(c => c.InscripcionCatequesisId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Bautismo)
                    .WithOne(b => b.InscripcionCatequesis)
                    .HasForeignKey<BautismoCatequizando>(b => b.InscripcionCatequesisId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Adecuacion)
                    .WithOne(a => a.InscripcionCatequesis)
                    .HasForeignKey<AdecuacionCatequizando>(a => a.InscripcionCatequesisId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.CondicionSalud)
                    .WithOne(c => c.InscripcionCatequesis)
                    .HasForeignKey<CondicionSaludCatequizando>(c => c.InscripcionCatequesisId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Madre)
                    .WithOne(m => m.InscripcionCatequesis)
                    .HasForeignKey<MadreCatequizando>(m => m.InscripcionCatequesisId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Catequizando>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired();
                entity.Property(e => e.Apellidos).IsRequired();
                entity.Property(e => e.FechaNacimiento).IsRequired();
            });

            modelBuilder.Entity<MadreCatequizando>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired();
                entity.Property(e => e.Telefono).IsRequired();
            });
            modelBuilder.Entity<Donacion>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Fecha).IsRequired();
                entity.Property(e => e.Nombre).IsRequired();
                entity.Property(e => e.Correo).IsRequired();
                entity.Property(e => e.Detalle).IsRequired();
                entity.Property(e => e.Estado).IsRequired().HasDefaultValue("Pendiente");
            });
        }
    }
}
