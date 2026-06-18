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
        private readonly ILogger<DonacionService> _logger;

        public DonacionService(
            GlobalContex context,
            IEmailService emailService,
            ILogger<DonacionService> logger)
        {
            _context = context;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<IEnumerable<Donacion>> GetAllDonaciones()
        {
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
                Fecha = DateTime.UtcNow,
                Anonimo = dto.Anonimo,
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                Detalle = dto.Detalle,
                Estado = "Pendiente",
            };

            _context.Donaciones.Add(donacion);
            await _context.SaveChangesAsync();

            return donacion;
        }

        public async Task<(Donacion? Donacion, bool CorreoEnviado)> UpdateEstado(int id, string nuevoEstado)
        {
            var donacion = await _context.Donaciones.FindAsync(id);
            if (donacion == null) return (null, false);

            var estadoNormalizado = NormalizarEstado(nuevoEstado);
            donacion.Estado = estadoNormalizado;
            await _context.SaveChangesAsync();

            var correoEnviado = false;
            if (estadoNormalizado == "Rechazado")
            {
                correoEnviado = await EnviarCorreoRechazoAsync(donacion);
            }

            return (donacion, correoEnviado);
        }

        private static string NormalizarEstado(string estado)
        {
            var valor = estado?.Trim() ?? "Pendiente";

            if (valor.Equals("Aceptada", StringComparison.OrdinalIgnoreCase) ||
                valor.Equals("Aprobado", StringComparison.OrdinalIgnoreCase) ||
                valor.Equals("Aprobada", StringComparison.OrdinalIgnoreCase))
            {
                return "Aprobado";
            }

            if (valor.Equals("Denegada", StringComparison.OrdinalIgnoreCase) ||
                valor.Equals("Rechazado", StringComparison.OrdinalIgnoreCase) ||
                valor.Equals("Rechazada", StringComparison.OrdinalIgnoreCase))
            {
                return "Rechazado";
            }

            return "Pendiente";
        }

        private async Task<bool> EnviarCorreoRechazoAsync(Donacion donacion)
        {
            if (string.IsNullOrWhiteSpace(donacion.Correo))
            {
                _logger.LogWarning(
                    "No se envió correo de rechazo: la donación {Id} no tiene correo.",
                    donacion.Id);
                return false;
            }

            var plantilla = await LeerPlantillaAsync("DonacionRechazada.html");
            if (plantilla is null) return false;

            var cuerpo = plantilla
                .Replace("{{Nombre}}", donacion.Nombre)
                .Replace("{{Detalle}}", donacion.Detalle);

            var enviado = await _emailService.SendEmailAsync(
                donacion.Correo,
                "Su solicitud de donación fue rechazada - Parroquia San Blas",
                cuerpo);

            if (!enviado)
            {
                _logger.LogWarning(
                    "No se pudo enviar correo de rechazo a {Correo} para donación {Id}",
                    donacion.Correo,
                    donacion.Id);
            }

            return enviado;
        }

        private async Task<string?> LeerPlantillaAsync(string nombreArchivo)
        {
            var pathPlantilla = Path.Combine(AppContext.BaseDirectory, "Template", nombreArchivo);
            if (!File.Exists(pathPlantilla))
            {
                _logger.LogError("Plantilla de correo no encontrada: {Path}", pathPlantilla);
                return null;
            }

            return await File.ReadAllTextAsync(pathPlantilla);
        }
    }
}
