using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SanblasBackend.DTOs.DtosRegistroSacramentos;
using SanblasBackend.Services;

namespace SanblasBackend.Controllers.Registro_sacramentos_controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ConfirmacionController : ControllerBase
    {
        private readonly IConfirmacionService _service;

        public ConfirmacionController(IConfirmacionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConfirmacionDto>>> GetConfirmaciones()
        {
            var confirmaciones = await _service.GetAllAsync();
            return Ok(confirmaciones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConfirmacionDto>> GetConfirmacion(int id)
        {
            var confirmacion = await _service.GetByIdAsync(id);
            if (confirmacion == null)
                return NotFound();

            return Ok(confirmacion);
        }

        [HttpPost]
        public async Task<ActionResult<ConfirmacionDto>> CreateConfirmacion([FromBody] ConfirmacionDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetConfirmacion), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConfirmacion(int id, [FromBody] ConfirmacionDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El ID de la URL no coincide con el ID del cuerpo");

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfirmacion(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}