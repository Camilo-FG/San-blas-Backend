using Microsoft.EntityFrameworkCore;
using SanblasBackend.Models;
using SanblasBackend.Models.EntitiesRegistroSacramentos;
using SanblasBackend.Models.EntitiesUsuarios;
using System.Linq;

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
        public DbSet<PagoInscripcionCatequesis> PagosInscripcionCatequesis { get; set; }
        public DbSet<PersonaInscribeCatequesis> PersonasInscribeCatequesis { get; set; }
        public DbSet<Donacion> Donaciones { get; set; }
        public DbSet<Evento> Eventos { get; set; }

         public DbSet<Bautismo> Bautismos { get; set; }
        public DbSet<Comunion> Comuniones { get; set; }
        public DbSet<Confirmacion> Confirmaciones { get; set; }
        public DbSet<Matrimonio> Matrimonios { get; set; }
        public DbSet<User> Users { get; set; }

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
                entity.Property(e => e.FeBautismoArchivo).IsRequired();

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

                entity.HasOne(e => e.Pago)
                    .WithOne(p => p.InscripcionCatequesis)
                    .HasForeignKey<PagoInscripcionCatequesis>(p => p.InscripcionCatequesisId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.PersonaInscribe)
                    .WithOne(p => p.InscripcionCatequesis)
                    .HasForeignKey<PersonaInscribeCatequesis>(p => p.InscripcionCatequesisId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PagoInscripcionCatequesis>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.MetodoPago).IsRequired();
                entity.Property(e => e.NumeroComprobanteSinpe).IsRequired();
                entity.Property(e => e.ComprobanteArchivo).IsRequired();
                entity.Property(e => e.Monto).IsRequired();
            });

            modelBuilder.Entity<PersonaInscribeCatequesis>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired();
                entity.Property(e => e.Apellidos).IsRequired();
                entity.Property(e => e.Parentesco).IsRequired();
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

            modelBuilder.Entity<Evento>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo).IsRequired();
                entity.Property(e => e.Descripcion).IsRequired();
                entity.Property(e => e.FechaInicio).IsRequired();
                entity.Property(e => e.Lugar).IsRequired();
                entity.Property(e => e.Publicado).IsRequired().HasDefaultValue(true);
            });

            modelBuilder.Entity<Bautismo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PrimerApellido).IsRequired().HasMaxLength(100);
                entity.Property(e => e.SegundoApellido).IsRequired().HasMaxLength(100);
                entity.Property(e => e.NombreParroquia).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Prebispero).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Cedula).IsRequired();
                entity.Property(e => e.FechaBautismo).IsRequired();
                entity.Property(e => e.AnnioBautismo).IsRequired();
                entity.Property(e => e.FechaNacimiento).IsRequired();
                entity.Property(e => e.HoraNacimiento).IsRequired();
                entity.Property(e => e.NombreAbuelosPaternos).IsRequired().HasMaxLength(200);
                entity.Property(e => e.NombreAbuelosMaternos).IsRequired().HasMaxLength(200);
            });

            modelBuilder.Entity<Comunion>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DiaComunion).IsRequired().HasMaxLength(10);
                entity.Property(e => e.MesComunion).IsRequired().HasMaxLength(20);
                entity.Property(e => e.AnnioComunion).IsRequired();
                entity.Property(e => e.LugarComunion).IsRequired().HasMaxLength(150);
            });

            modelBuilder.Entity<Confirmacion>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DiaConfirmacion).IsRequired().HasMaxLength(10);
                entity.Property(e => e.MesConfirmacion).IsRequired().HasMaxLength(20);
                entity.Property(e => e.AnnioConfirmacion).IsRequired();
                entity.Property(e => e.LugarConfirmacion).IsRequired().HasMaxLength(150);
            });

            modelBuilder.Entity<Matrimonio>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NombreContrayente).IsRequired().HasMaxLength(100);
                entity.Property(e => e.NombreContrayente2).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DiaMatrimonio).IsRequired().HasMaxLength(10);
                entity.Property(e => e.MesMatrimonio).IsRequired().HasMaxLength(20);
                entity.Property(e => e.AnnioMatrimonio).IsRequired();
                entity.Property(e => e.LugarMatrimonio).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Tomo).IsRequired();
                entity.Property(e => e.Folio).IsRequired();
            });

            //usuarios (mapeo al esquema actual de la tabla en PostgreSQL)
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.UserName)
                    .HasColumnName("Username")
                    .IsRequired();

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.PhoneNumber)
                    .HasConversion(
                        v => int.Parse(string.Concat(v.Where(char.IsDigit))),
                        v => v.ToString("D8")
                    );

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.Role)
                    .HasColumnName("Role")
                    .IsRequired();

                entity.Property(e => e.State).IsRequired();
                entity.Property(e => e.CreationDate).IsRequired();
            });
        }
    }
}
