using Microsoft.EntityFrameworkCore;
using SanblasBackend.Data;
using SanblasBackend.DTOs.DtosRegistroSacramentos;
using SanblasBackend.Models.EntitiesRegistroSacramentos;

namespace SanblasBackend.Services
{
    public class ComunionService : IComunionService
    {
        private readonly GlobalContex _context;

        public ComunionService(GlobalContex context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ComunionDto>> GetAllAsync()
        {
            return await _context.Comuniones
                .Select(c => new ComunionDto
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    DiaComunion = c.DiaComunion,
                    MesComunion = c.MesComunion,
                    AnnioComunion = c.AnnioComunion,
                    LugarComunion = c.LugarComunion
                })
                .ToListAsync();
        }

        public async Task<ComunionDto?> GetByIdAsync(int id)
        {
            var com union = await _context.Comuniones.FindAsync(id);
            if (com union == null)
                return null;

            return new ComunionDto
            {
                Id = com union.Id,
                Nombre = com union.Nombre,
                DiaComunion = com union.DiaComunion,
                MesComunion = com union.MesComunion,
                AnnioComunion = com union.AnnioComunion,
                LugarComunion = com union.LugarComunion
            };
        }

        public async Task<ComunionDto> CreateAsync(ComunionDto dto)
        {
            var com union = new Comunion
            {
                Nombre = dto.Nombre,
                DiaComunion = dto.DiaComunion,
                MesComunion = dto.MesComunion,
                AnnioComunion = dto.AnnioComunion,
                LugarComunion = dto.LugarComunion
            };

            _context.Comuniones.Add(com union);
            await _context.SaveChangesAsync();

            dto.Id = com union.Id;
            return dto;
        }

        public async Task<ComunionDto?> UpdateAsync(int id, ComunionDto dto)
        {
            if (id != dto.Id)
                return null;

            var com union = await _context.Comuniones.FindAsync(id);
            if (com union == null)
                return null;

            com union.Nombre = dto.Nombre;
            com union.DiaComunion = dto.DiaComunion;
            com union.MesComunion = dto.MesComunion;
            com union.AnnioComunion = dto.AnnioComunion;
            com union.LugarComunion = dto.LugarComunion;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var com union = await _context.Comuniones.FindAsync(id);
            if (com union == null)
                return false;

            _context.Comuniones.Remove(com union);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}