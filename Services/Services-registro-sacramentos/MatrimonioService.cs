using Microsoft.EntityFrameworkCore;
using SanblasBackend.Data;
using SanblasBackend.DTOs.DtosRegistroSacramentos;
using SanblasBackend.Models.EntitiesRegistroSacramentos;

namespace SanblasBackend.Services
{
    public class MatrimonioService : IMatrimonioService
    {
        private readonly GlobalContex _context;

        public MatrimonioService(GlobalContex context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MatrimonioDto>> GetAllAsync()
        {
            return await _context.Matrimonios
                .Select(m => new MatrimonioDto
                {
                    Id = m.Id,
                    NombreContrayente = m.NombreContrayente,
                    NombreContrayente2 = m.NombreContrayente2,
                    DiaMatrimonio = m.DiaMatrimonio,
                    MesMatrimonio = m.MesMatrimonio,
                    AnnioMatrimonio = m.AnnioMatrimonio,
                    LugarMatrimonio = m.LugarMatrimonio,
                    Tomo = m.Tomo,
                    Folio = m.Folio
                })
                .ToListAsync();
        }

        public async Task<MatrimonioDto?> GetByIdAsync(int id)
        {
            var matrimonio = await _context.Matrimonios.FindAsync(id);
            if (matrimonio == null)
                return null;

            return new MatrimonioDto
            {
                Id = matrimonio.Id,
                NombreContrayente = matrimonio.NombreContrayente,
                NombreContrayente2 = matrimonio.NombreContrayente2,
                DiaMatrimonio = matrimonio.DiaMatrimonio,
                MesMatrimonio = matrimonio.MesMatrimonio,
                AnnioMatrimonio = matrimonio.AnnioMatrimonio,
                LugarMatrimonio = matrimonio.LugarMatrimonio,
                Tomo = matrimonio.Tomo,
                Folio = matrimonio.Folio
            };
        }

        public async Task<MatrimonioDto> CreateAsync(MatrimonioDto dto)
        {
            var matrimonio = new Matrimonio
            {
                NombreContrayente = dto.NombreContrayente,
                NombreContrayente2 = dto.NombreContrayente2,
                DiaMatrimonio = dto.DiaMatrimonio,
                MesMatrimonio = dto.MesMatrimonio,
                AnnioMatrimonio = dto.AnnioMatrimonio,
                LugarMatrimonio = dto.LugarMatrimonio,
                Tomo = dto.Tomo,
                Folio = dto.Folio
            };

            _context.Matrimonios.Add(matrimonio);
            await _context.SaveChangesAsync();

            dto.Id = matrimonio.Id;
            return dto;
        }

        public async Task<MatrimonioDto?> UpdateAsync(int id, MatrimonioDto dto)
        {
            if (id != dto.Id)
                return null;

            var matrimonio = await _context.Matrimonios.FindAsync(id);
            if (matrimonio == null)
                return null;

            matrimonio.NombreContrayente = dto.NombreContrayente;
            matrimonio.NombreContrayente2 = dto.NombreContrayente2;
            matrimonio.DiaMatrimonio = dto.DiaMatrimonio;
            matrimonio.MesMatrimonio = dto.MesMatrimonio;
            matrimonio.AnnioMatrimonio = dto.AnnioMatrimonio;
            matrimonio.LugarMatrimonio = dto.LugarMatrimonio;
            matrimonio.Tomo = dto.Tomo;
            matrimonio.Folio = dto.Folio;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var matrimonio = await _context.Matrimonios.FindAsync(id);
            if (matrimonio == null)
                return false;

            _context.Matrimonios.Remove(matrimonio);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}