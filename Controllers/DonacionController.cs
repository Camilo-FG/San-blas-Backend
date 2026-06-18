using Microsoft.AspNetCore.Mvc;
using SanblasBackend.DTOs;
using SanblasBackend.Models;
using SanblasBackend.Services;

namespace SanblasBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonacionController : ControllerBase
    {
        private readonly IDonacionService _donacionService;

        // Inyección del servicio
        public DonacionController(IDonacionService donacionService)
        {
            _donacionService = donacionService;
        }

        //api/Donacion (Para el panel de administración)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _donacionService.GetAllDonaciones();
            return Ok(result);
        }

        //{id} (Buscar una donación específica)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _donacionService.GetDonacionById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        //api/Donacion (Endpoint PÚBLICO para registrar una donación desde el Formulario Frontend)
        [HttpPost]
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

        //api/Donacion/{id}/estado (Endpoint de Gestión)
        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> UpdateEstado(int id, [FromBody] string nuevoEstado)
        {
            var result = await _donacionService.UpdateEstado(id, nuevoEstado);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}