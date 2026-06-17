using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SanblasBackend.Data;
using SanblasBackend.DTOs.DtosRegistroSacramentos;
using SanblasBackend.Models.EntitiesRegistroSacramentos;

namespace SanblasBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComunionController : ControllerBase
    {
        private readonly GlobalContex _context;

        public ComunionController(GlobalContex context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComunionDto>>> GetComuniones()
        {
            var comuniones = await _context.Comuniones
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

            return Ok(comuniones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComunionDto>> GetComunion(int id)
        {
            var com union = await _context.Comuniones.FindAsync(id);

            if (comunion == null)
                return NotFound();

            var dto = new ComunionDto
            {
                Id = com union.Id,
                Nombre = com union.Nombre,
                DiaComunion = com union.DiaComunion,
                MesComunion = com union.MesComunion,
                AnnioComunion = com union.AnnioComunion,
                LugarComunion = com union.LugarComunion
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ComunionDto>> CreateComunion([FromBody] ComunionDto dto)
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

            return CreatedAtAction(nameof(GetComunion), new { id = com union.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComunion(int id, [FromBody] ComunionDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El ID de la URL no coincide con el ID del cuerpo");

            var com union = await _context.Comuniones.FindAsync(id);
            if (com union == null)
                return NotFound();

            com union.Nombre = dto.Nombre;
            com union.DiaComunion = dto.DiaComunion;
            com union.MesComunion = dto.MesComunion;
            com union.AnnioComunion = dto.AnnioComunion;
            com union.LugarComunion = dto.LugarComunion;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComunion(int id)
        {
            var com union = await _context.Comuniones.FindAsync(id);
            if (com union == null)
                return NotFound();

            _context.Comuniones.Remove(com union);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}