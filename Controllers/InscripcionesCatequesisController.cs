using Microsoft.AspNetCore.Mvc;
using SanblasBackend.DTOs;
using SanblasBackend.Services;

namespace SanblasBackend.Controllers;

[ApiController]
[Route("api/inscripciones-catequesis")]
public class InscripcionesCatequesisController : ControllerBase
{
    private static readonly HashSet<string> EstadosValidos = new(StringComparer.OrdinalIgnoreCase)
    {
        "Pendiente",
        "Aprobada",
        "Rechazada"
    };

    private readonly IInscripcionCatequesisService _inscripcionCatequesisService;

    public InscripcionesCatequesisController(IInscripcionCatequesisService inscripcionCatequesisService)
    {
        _inscripcionCatequesisService = inscripcionCatequesisService;
    }

    [HttpGet]
    public async Task<IActionResult> ListarInscripciones([FromQuery] string? estado)
    {
        string? estadoNormalizado = null;

        if (!string.IsNullOrWhiteSpace(estado))
        {
            estadoNormalizado = EstadosValidos
                .FirstOrDefault(e => e.Equals(estado, StringComparison.OrdinalIgnoreCase));

            if (estadoNormalizado is null)
            {
                return BadRequest(new
                {
                    mensaje = "El estado debe ser Pendiente, Aprobada o Rechazada."
                });
            }
        }

        var inscripciones = await _inscripcionCatequesisService.ObtenerInscripcionesAsync(estadoNormalizado);
        return Ok(inscripciones);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerInscripcionPorId(int id)
    {
        var inscripcion = await _inscripcionCatequesisService.ObtenerInscripcionPorIdAsync(id);

        if (inscripcion is null)
        {
            return NotFound(new { mensaje = $"No se encontró una inscripción con id {id}." });
        }

        return Ok(inscripcion);
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

    [HttpPut("{id:int}/estado")]
    public async Task<IActionResult> ActualizarEstado(
        int id,
        [FromBody] ActualizarEstadoInscripcionCatequesisRequest request)
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

        var estadoNormalizado = EstadosValidos
            .FirstOrDefault(e => e.Equals(request.Estado, StringComparison.OrdinalIgnoreCase));

        if (estadoNormalizado is null)
        {
            return BadRequest(new
            {
                mensaje = "El estado debe ser Pendiente, Aprobada o Rechazada."
            });
        }

        request.Estado = estadoNormalizado;

        var response = await _inscripcionCatequesisService.ActualizarEstadoAsync(id, request);

        if (response is null)
        {
            return NotFound(new { mensaje = $"No se encontró una inscripción con id {id}." });
        }

        return Ok(response);
    }
}
