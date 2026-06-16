using Microsoft.EntityFrameworkCore;
using SanblasBackend.Data;
using SanblasBackend.DTOs;
using SanblasBackend.Models;

namespace SanblasBackend.Services
{
    public class FormSacraService : IFormSacraService
    {
        private readonly GlobalContex _context;

        public FormSacraService(GlobalContex context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FormSacra>> GetAllSolicitudes()
        {
            return await _context.FormSacras.ToListAsync();
        }

        public async Task<FormSacra?> GetSolicitudById(int id)
        {
            return await _context.FormSacras.FindAsync(id);
        }

        public async Task<FormSacra> CreateSolicitud(SolicSacraCreateDto dto)
        {
            var solicitud = new FormSacra
            {
                Nombre = dto.Nombre,
                PrimerApellido = dto.PrimerApellido,
                SegundoApellido = dto.SegundoApellido,
                Cedula = dto.Cedula,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                TipoSacramento = dto.TipoSacramento,
                Motivo = dto.Motivo,
                Estado = "Pendiente"
            };

            _context.FormSacras.Add(solicitud);
            await _context.SaveChangesAsync();

            return solicitud;
        }

        public async Task<FormSacra?> UpdateEstado(int id, string nuevoEstado)
        {
            var solicitud = await _context.FormSacras.FindAsync(id);
            if (solicitud == null) return null;

            solicitud.Estado = nuevoEstado;
            await _context.SaveChangesAsync();

            return solicitud;
        }
    }
}
