using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SanblasBackend.DTOs;
using SanblasBackend.Services;

namespace SanblasBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormSacraController : ControllerBase
    {
        private readonly IFormSacraService _formSacraService;

        public FormSacraController(IFormSacraService formSacraService)
        {
            _formSacraService = formSacraService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var results = await _formSacraService.GetAllSolicitudes();
            return Ok(results);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _formSacraService.GetSolicitudById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateSolicitud([FromBody] SolicSacraCreateDto dto)
        {
            try
            {
                var result = await _formSacraService.CreateSolicitud(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.id }, result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                    inner = ex.InnerException?.Message,
                    innerInner = ex.InnerException?.InnerException?.Message
                });
            }
        }

        [HttpPatch("{id}/estado")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEstado(int id, [FromBody] string nuevoEstado)
        {
            var result = await _formSacraService.UpdateEstado(id, nuevoEstado);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
