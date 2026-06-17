using Microsoft.EntityFrameworkCore;
using SanblasBackend.Data;
using SanblasBackend.DTOs.DtosRegistroSacramentos;
using SanblasBackend.Models.EntitiesRegistroSacramentos;

namespace SanblasBackend.Services
{
    public class ConfirmacionService : IConfirmacionService
    {
        private readonly GlobalContex _context;

        public ConfirmacionService(GlobalContex context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ConfirmacionDto>> GetAllAsync()
        {
            return await _context.Confirmaciones
                .Select(c => new ConfirmacionDto
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    DiaConfirmacion = c.DiaConfirmacion,
                    MesConfirmacion = c.MesConfirmacion,
                    AnnioConfirmacion = c.AnnioConfirmacion,
                    LugarConfirmacion = c.LugarConfirmacion
                })
                .ToListAsync();
        }

        public async Task<ConfirmacionDto?> GetByIdAsync(int id)
        {
            var confirmacion = await _context.Confirmaciones.FindAsync(id);
            if (confirmacion == null)
                return null;

            return new ConfirmacionDto
            {
                Id = confirmacion.Id,
                Nombre = confirmacion.Nombre,
                DiaConfirmacion = confirmacion.DiaConfirmacion,
                MesConfirmacion = confirmacion.MesConfirmacion,
                AnnioConfirmacion = confirmacion.AnnioConfirmacion,
                LugarConfirmacion = confirmacion.LugarConfirmacion
            };
        }

        public async Task<ConfirmacionDto> CreateAsync(ConfirmacionDto dto)
        {
            var confirmacion = new Confirmacion
            {
                Nombre = dto.Nombre,
                DiaConfirmacion = dto.DiaConfirmacion,
                MesConfirmacion = dto.MesConfirmacion,
                AnnioConfirmacion = dto.AnnioConfirmacion,
                LugarConfirmacion = dto.LugarConfirmacion
            };

            _context.Confirmaciones.Add(confirmacion);
            await _context.SaveChangesAsync();

            dto.Id = confirmacion.Id;
            return dto;
        }

        public async Task<ConfirmacionDto?> UpdateAsync(int id, ConfirmacionDto dto)
        {
            if (id != dto.Id)
                return null;

            var confirmacion = await _context.Confirmaciones.FindAsync(id);
            if (confirmacion == null)
                return null;

            confirmacion.Nombre = dto.Nombre;
            confirmacion.DiaConfirmacion = dto.DiaConfirmacion;
            confirmacion.MesConfirmacion = dto.MesConfirmacion;
            confirmacion.AnnioConfirmacion = dto.AnnioConfirmacion;
            confirmacion.LugarConfirmacion = dto.LugarConfirmacion;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var confirmacion = await _context.Confirmaciones.FindAsync(id);
            if (confirmacion == null)
                return false;

            _context.Confirmaciones.Remove(confirmacion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}