using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SanblasBackend.DTOs;
using SanblasBackend.Services;

namespace SanblasBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonacionController : ControllerBase
    {
        private readonly IDonacionService _donacionService;

        public DonacionController(IDonacionService donacionService)
        {
            _donacionService = donacionService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _donacionService.GetAllDonaciones();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _donacionService.GetDonacionById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateDonacion([FromBody] DonacionCreateDto dto)
        {
            try
            {
                var result = await _donacionService.CreateDonacion(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
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
            var result = await _donacionService.UpdateEstado(id, nuevoEstado);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
