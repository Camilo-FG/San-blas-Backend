using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SanblasBackend.Data;
using SanblasBackend.DTOs.DtosRegistroSacramentos;
using SanblasBackend.Models.EntitiesRegistroSacramentos;

namespace SanblasBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatrimonioController : ControllerBase
    {
        private readonly GlobalContex _context;

        public MatrimonioController(GlobalContex context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatrimonioDto>>> GetMatrimonios()
        {
            var matrimonios = await _context.Matrimonios
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

            return Ok(matrimonios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MatrimonioDto>> GetMatrimonio(int id)
        {
            var matrimonio = await _context.Matrimonios.FindAsync(id);

            if (matrimonio == null)
                return NotFound();

            var dto = new MatrimonioDto
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

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<MatrimonioDto>> CreateMatrimonio([FromBody] MatrimonioDto dto)
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

            return CreatedAtAction(nameof(GetMatrimonio), new { id = matrimonio.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMatrimonio(int id, [FromBody] MatrimonioDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El ID de la URL no coincide con el ID del cuerpo");

            var matrimonio = await _context.Matrimonios.FindAsync(id);
            if (matrimonio == null)
                return NotFound();

            matrimonio.NombreContrayente = dto.NombreContrayente;
            matrimonio.NombreContrayente2 = dto.NombreContrayente2;
            matrimonio.DiaMatrimonio = dto.DiaMatrimonio;
            matrimonio.MesMatrimonio = dto.MesMatrimonio;
            matrimonio.AnnioMatrimonio = dto.AnnioMatrimonio;
            matrimonio.LugarMatrimonio = dto.LugarMatrimonio;
            matrimonio.Tomo = dto.Tomo;
            matrimonio.Folio = dto.Folio;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatrimonio(int id)
        {
            var matrimonio = await _context.Matrimonios.FindAsync(id);
            if (matrimonio == null)
                return NotFound();

            _context.Matrimonios.Remove(matrimonio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}