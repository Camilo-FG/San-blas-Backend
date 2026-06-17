using Microsoft.EntityFrameworkCore;
using SanblasBackend.Data;
using SanblasBackend.DTOs;
using SanblasBackend.Models;

namespace SanblasBackend.Services
{
    public class DonacionService : IDonacionService
    {
        private readonly GlobalContex _context;
        private readonly IEmailService _emailService;

        public DonacionService(GlobalContex context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<IEnumerable<Donacion>> GetAllDonaciones()
        {
            //Se ordena de mas reciente a mas antigua
            return await _context.Donaciones.OrderByDescending(d => d.Fecha).ToListAsync();
        }

        public async Task<Donacion?> GetDonacionById(int id)
        {
            return await _context.Donaciones.FindAsync(id);
        }

        public async Task<Donacion> CreateDonacion(DonacionCreateDto dto)
        {
            var donacion = new Donacion
            {
                Fecha = DateTime.UtcNow, // La fecha se asigna en el momento en que llega
                Anonimo = dto.Anonimo,
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                Detalle = dto.Detalle,
                Estado = "Pendiente" // Estado por defecto
            };

            _context.Donaciones.Add(donacion);
            await _context.SaveChangesAsync();

            return donacion;
        }

        public async Task<Donacion?> UpdateEstado(int id, string nuevoEstado)
        {
            var donacion = await _context.Donaciones.FindAsync(id);
            if (donacion == null) return null;

            donacion.Estado = nuevoEstado;
            await _context.SaveChangesAsync();

            try
            {
                string colorTexto = nuevoEstado == "Aprobado" ? "#155724" : "#721c24";
                string colorFondo = nuevoEstado == "Aprobado" ? "#d4edda" : "#f8d7da";

                string pathPlantilla = Path.Combine(AppContext.BaseDirectory, "Templates", "DonacionEstado.html");
                string cuerpoCorreo = await File.ReadAllTextAsync(pathPlantilla);

                cuerpoCorreo = cuerpoCorreo
                    .Replace("{{Nombre}}", donacion.Nombre)
                    .Replace("{{Estado}}", nuevoEstado)
                    .Replace("{{Detalle}}", donacion.Detalle)
                    .Replace("{{ColorTexto}}", colorTexto)
                    .Replace("{{ColorFondo}}", colorFondo);

                await _emailService.SendEmailAsync(
                    donacion.Correo,
                    "Actualización de su donación - Parroquia San Blas",
                    cuerpoCorreo
                );
            }
            catch (Exception)
            {
                // Ignorar si el correo falla temporalmente para no frenar la base de datos
            }

            return donacion;
        }
    }
}