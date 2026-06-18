using Microsoft.AspNetCore.Mvc;
using SanblasBackend.DTOs.DtosRegistroSacramentos;
using SanblasBackend.Services;

namespace SanblasBackend.Controllers.Registro_sacramentos_controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatrimonioController : ControllerBase
    {
        private readonly IMatrimonioService _service;

        public MatrimonioController(IMatrimonioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatrimonioDto>>> GetMatrimonios()
        {
            var matrimonios = await _service.GetAllAsync();
            return Ok(matrimonios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MatrimonioDto>> GetMatrimonio(int id)
        {
            var matrimonio = await _service.GetByIdAsync(id);
            if (matrimonio == null)
                return NotFound();

            return Ok(matrimonio);
        }

        [HttpPost]
        public async Task<ActionResult<MatrimonioDto>> CreateMatrimonio([FromBody] MatrimonioDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetMatrimonio), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMatrimonio(int id, [FromBody] MatrimonioDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El ID de la URL no coincide con el ID del cuerpo");

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatrimonio(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}