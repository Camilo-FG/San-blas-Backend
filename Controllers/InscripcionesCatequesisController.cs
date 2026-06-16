using Microsoft.AspNetCore.Mvc;
using SanblasBackend.DTOs;
using SanblasBackend.Services;

namespace SanblasBackend.Controllers;

[ApiController]
[Route("api/inscripciones-catequesis")]
public class InscripcionesCatequesisController : ControllerBase
{
    private readonly IInscripcionCatequesisService _inscripcionCatequesisService;

    public InscripcionesCatequesisController(IInscripcionCatequesisService inscripcionCatequesisService)
    {
        _inscripcionCatequesisService = inscripcionCatequesisService;
    }

    [HttpPost]
    public async Task<IActionResult> CrearInscripcion([FromBody] CrearInscripcionCatequesisRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                mensaje = "Errores de validación.",
                errores = ModelState
                    .Where(entry => entry.Value?.Errors.Count > 0)
                    .ToDictionary(
                        entry => entry.Key,
                        entry => entry.Value!.Errors.Select(error => error.ErrorMessage).ToArray())
            });
        }

        try
        {
            var response = await _inscripcionCatequesisService.CrearInscripcionAsync(request);
            return StatusCode(StatusCodes.Status201Created, response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
}
