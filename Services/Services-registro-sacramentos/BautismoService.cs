using Microsoft.EntityFrameworkCore;
using SanblasBackend.Data;
using SanblasBackend.DTOs.DtosRegistroSacramentos;
using SanblasBackend.Models.EntitiesRegistroSacramentos;

namespace SanblasBackend.Services
{
    public class BautismoService : IBautismoService
    {
        private readonly GlobalContex _context;

        public BautismoService(GlobalContex context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BautismoDto>> GetAllAsync()
        {
            return await _context.Bautismos
                .Select(b => new BautismoDto
                {
                    Id = b.Id,
                    Nombre = b.Nombre,
                    Cedula = b.Cedula,
                    PrimerApellido = b.PrimerApellido,
                    SegundoApellido = b.SegundoApellido,
                    NombreParroquia = b.NombreParroquia,
                    FechaBautismo = b.FechaBautismo.ToString("yyyy-MM-dd"),
                    AnnioBautismo = b.AnnioBautismo,
                    Prebispero = b.Prebispero,
                    FechaNacimiento = b.FechaNacimiento.ToString("yyyy-MM-dd"),
                    HoraNacimiento = b.HoraNacimiento.ToString(@"hh\:mm"),
                    NombreAbuelosPaternos = b.NombreAbuelosPaternos,
                    NombreAbuelosMaternos = b.NombreAbuelosMaternos
                })
                .ToListAsync();
        }

        public async Task<BautismoDto?> GetByIdAsync(int id)
        {
            var bautismo = await _context.Bautismos.FindAsync(id);
            if (bautismo == null)
                return null;

            return new BautismoDto
            {
                Id = bautismo.Id,
                Nombre = bautismo.Nombre,
                Cedula = bautismo.Cedula,
                PrimerApellido = bautismo.PrimerApellido,
                SegundoApellido = bautismo.SegundoApellido,
                NombreParroquia = bautismo.NombreParroquia,
                FechaBautismo = bautismo.FechaBautismo.ToString("yyyy-MM-dd"),
                AnnioBautismo = bautismo.AnnioBautismo,
                Prebispero = bautismo.Prebispero,
                FechaNacimiento = bautismo.FechaNacimiento.ToString("yyyy-MM-dd"),
                HoraNacimiento = bautismo.HoraNacimiento.ToString(@"hh\:mm"),
                NombreAbuelosPaternos = bautismo.NombreAbuelosPaternos,
                NombreAbuelosMaternos = bautismo.NombreAbuelosMaternos
            };
        }

        public async Task<BautismoDto> CreateAsync(BautismoDto dto)
        {
            var bautismo = new Bautismo
            {
                Nombre = dto.Nombre,
                Cedula = dto.Cedula,
                PrimerApellido = dto.PrimerApellido,
                SegundoApellido = dto.SegundoApellido,
                NombreParroquia = dto.NombreParroquia,
                FechaBautismo = DateTime.Parse(dto.FechaBautismo),
                AnnioBautismo = dto.AnnioBautismo,
                Prebispero = dto.Prebispero,
                FechaNacimiento = DateTime.Parse(dto.FechaNacimiento),
                HoraNacimiento = TimeSpan.Parse(dto.HoraNacimiento),
                NombreAbuelosPaternos = dto.NombreAbuelosPaternos,
                NombreAbuelosMaternos = dto.NombreAbuelosMaternos
            };

            _context.Bautismos.Add(bautismo);
            await _context.SaveChangesAsync();

            dto.Id = bautismo.Id;
            return dto;
        }

        public async Task<BautismoDto?> UpdateAsync(int id, BautismoDto dto)
        {
            if (id != dto.Id)
                return null;

            var bautismo = await _context.Bautismos.FindAsync(id);
            if (bautismo == null)
                return null;

            bautismo.Nombre = dto.Nombre;
            bautismo.Cedula = dto.Cedula;
            bautismo.PrimerApellido = dto.PrimerApellido;
            bautismo.SegundoApellido = dto.SegundoApellido;
            bautismo.NombreParroquia = dto.NombreParroquia;
            bautismo.FechaBautismo = DateTime.Parse(dto.FechaBautismo);
            bautismo.AnnioBautismo = dto.AnnioBautismo;
            bautismo.Prebispero = dto.Prebispero;
            bautismo.FechaNacimiento = DateTime.Parse(dto.FechaNacimiento);
            bautismo.HoraNacimiento = TimeSpan.Parse(dto.HoraNacimiento);
            bautismo.NombreAbuelosPaternos = dto.NombreAbuelosPaternos;
            bautismo.NombreAbuelosMaternos = dto.NombreAbuelosMaternos;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var bautismo = await _context.Bautismos.FindAsync(id);
            if (bautismo == null)
                return false;

            _context.Bautismos.Remove(bautismo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}