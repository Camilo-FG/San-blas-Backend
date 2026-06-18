using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SanblasBackend.DTOs.DtosRegistroSacramentos;
using SanblasBackend.Services;

namespace SanblasBackend.Controllers.Registro_sacramentos_controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class BautismoController : ControllerBase
    {
        private readonly IBautismoService _service;

        public BautismoController(IBautismoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BautismoDto>>> GetBautismos()
        {
            var bautismos = await _service.GetAllAsync();
            return Ok(bautismos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BautismoDto>> GetBautismo(int id)
        {
            var bautismo = await _service.GetByIdAsync(id);
            if (bautismo == null)
                return NotFound();

            return Ok(bautismo);
        }

        [HttpPost]
        public async Task<ActionResult<BautismoDto>> CreateBautismo([FromBody] BautismoDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetBautismo), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBautismo(int id, [FromBody] BautismoDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El ID de la URL no coincide con el ID del cuerpo");

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBautismo(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}