using Microsoft.EntityFrameworkCore;
using SanblasBackend.Data;
using SanblasBackend.DTOs;
using SanblasBackend.Models;

namespace SanblasBackend.Services
{
    public class DonacionService : IDonacionService
    {
        private readonly GlobalContex _context;

        public DonacionService(GlobalContex context)
        {
            _context = context;
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

            return donacion;
        }
    }
}