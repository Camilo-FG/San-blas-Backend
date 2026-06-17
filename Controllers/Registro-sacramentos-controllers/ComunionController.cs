using Microsoft.AspNetCore.Mvc;
using SanblasBackend.DTOs.DtosRegistroSacramentos;
using SanblasBackend.Services;

namespace SanblasBackend.Controllers.Registro_sacramentos_controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComunionController : ControllerBase
    {
        private readonly IComunionService _service;

        public ComunionController(IComunionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComunionDto>>> GetComuniones()
        {
            var comuniones = await _service.GetAllAsync();
            return Ok(comuniones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComunionDto>> GetComunion(int id)
        {
            var com union = await _service.GetByIdAsync(id);
            if (com union == null)
                return NotFound();

            return Ok(com union);
        }

        [HttpPost]
        public async Task<ActionResult<ComunionDto>> CreateComunion([FromBody] ComunionDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetComunion), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComunion(int id, [FromBody] ComunionDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El ID de la URL no coincide con el ID del cuerpo");

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComunion(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}