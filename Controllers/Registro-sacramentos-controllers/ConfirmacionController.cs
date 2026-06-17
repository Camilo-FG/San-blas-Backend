using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SanblasBackend.Data;
using SanblasBackend.DTOs.DtosRegistroSacramentos;
using SanblasBackend.Models.EntitiesRegistroSacramentos;

namespace SanblasBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfirmacionController : ControllerBase
    {
        private readonly GlobalContex _context;

        public ConfirmacionController(GlobalContex context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConfirmacionDto>>> GetConfirmaciones()
        {
            var confirmaciones = await _context.Confirmaciones
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

            return Ok(confirmaciones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConfirmacionDto>> GetConfirmacion(int id)
        {
            var confirmacion = await _context.Confirmaciones.FindAsync(id);

            if (confirmacion == null)
                return NotFound();

            var dto = new ConfirmacionDto
            {
                Id = confirmacion.Id,
                Nombre = confirmacion.Nombre,
                DiaConfirmacion = confirmacion.DiaConfirmacion,
                MesConfirmacion = confirmacion.MesConfirmacion,
                AnnioConfirmacion = confirmacion.AnnioConfirmacion,
                LugarConfirmacion = confirmacion.LugarConfirmacion
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ConfirmacionDto>> CreateConfirmacion([FromBody] ConfirmacionDto dto)
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

            return CreatedAtAction(nameof(GetConfirmacion), new { id = confirmacion.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConfirmacion(int id, [FromBody] ConfirmacionDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El ID de la URL no coincide con el ID del cuerpo");

            var confirmacion = await _context.Confirmaciones.FindAsync(id);
            if (confirmacion == null)
                return NotFound();

            confirmacion.Nombre = dto.Nombre;
            confirmacion.DiaConfirmacion = dto.DiaConfirmacion;
            confirmacion.MesConfirmacion = dto.MesConfirmacion;
            confirmacion.AnnioConfirmacion = dto.AnnioConfirmacion;
            confirmacion.LugarConfirmacion = dto.LugarConfirmacion;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfirmacion(int id)
        {
            var confirmacion = await _context.Confirmaciones.FindAsync(id);
            if (confirmacion == null)
                return NotFound();

            _context.Confirmaciones.Remove(confirmacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}