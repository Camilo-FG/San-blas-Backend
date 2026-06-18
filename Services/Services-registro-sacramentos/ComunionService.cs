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
            var comunion = await _context.Comuniones.FindAsync(id);
            if (comunion == null)
                return null;

            return new ComunionDto
            {
                Id = comunion.Id,
                Nombre = comunion.Nombre,
                DiaComunion = comunion.DiaComunion,
                MesComunion = comunion.MesComunion,
                AnnioComunion = comunion.AnnioComunion,
                LugarComunion = comunion.LugarComunion
            };
        }

        public async Task<ComunionDto> CreateAsync(ComunionDto dto)
        {
            var comunion = new Comunion
            {
                Nombre = dto.Nombre,
                DiaComunion = dto.DiaComunion,
                MesComunion = dto.MesComunion,
                AnnioComunion = dto.AnnioComunion,
                LugarComunion = dto.LugarComunion
            };

            _context.Comuniones.Add(comunion);
            await _context.SaveChangesAsync();

            dto.Id = comunion.Id;
            return dto;
        }

        public async Task<ComunionDto?> UpdateAsync(int id, ComunionDto dto)
        {
            if (id != dto.Id)
                return null;

            var comunion = await _context.Comuniones.FindAsync(id);
            if (comunion == null)
                return null;

            comunion.Nombre = dto.Nombre;
            comunion.DiaComunion = dto.DiaComunion;
            comunion.MesComunion = dto.MesComunion;
            comunion.AnnioComunion = dto.AnnioComunion;
            comunion.LugarComunion = dto.LugarComunion;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var comunion = await _context.Comuniones.FindAsync(id);
            if (comunion == null)
                return false;

            _context.Comuniones.Remove(comunion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}