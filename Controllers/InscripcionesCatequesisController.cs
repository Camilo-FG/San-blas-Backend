using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SanblasBackend.DTOs;
using SanblasBackend.Services;
using SanblasBackend.Utils;

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

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ListarInscripciones([FromQuery] string? estado)
    {
        string? estadoNormalizado = null;

        if (!string.IsNullOrWhiteSpace(estado))
        {
            if (!InscripcionCatequesisValidaciones.EsEstadoValido(estado, out estadoNormalizado))
            {
                return BadRequest(new { mensaje = InscripcionCatequesisValidaciones.MensajeEstadoInvalido });
            }
        }

        var inscripciones = await _inscripcionCatequesisService.ObtenerInscripcionesAsync(estadoNormalizado);
        return Ok(inscripciones);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ObtenerInscripcionPorId(int id)
    {
        if (!InscripcionCatequesisValidaciones.EsIdValido(id))
        {
            return BadRequest(new { mensaje = InscripcionCatequesisValidaciones.MensajeIdInvalido });
        }

        var inscripcion = await _inscripcionCatequesisService.ObtenerInscripcionPorIdAsync(id);

        if (inscripcion is null)
        {
            return NotFound(new { mensaje = InscripcionCatequesisValidaciones.MensajeNoEncontrado });
        }

        return Ok(inscripcion);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CrearInscripcion([FromBody] CrearInscripcionCatequesisRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(CrearRespuestaValidacion(ModelState));
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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ActualizarEstado(
        int id,
        [FromBody] ActualizarEstadoInscripcionCatequesisRequest request)
    {
        if (!InscripcionCatequesisValidaciones.EsIdValido(id))
        {
            return BadRequest(new { mensaje = InscripcionCatequesisValidaciones.MensajeIdInvalido });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(CrearRespuestaValidacion(ModelState));
        }

        if (!InscripcionCatequesisValidaciones.EsEstadoValido(request.Estado, out var estadoNormalizado))
        {
            return BadRequest(new { mensaje = InscripcionCatequesisValidaciones.MensajeEstadoInvalido });
        }

        request.Estado = estadoNormalizado!;

        var response = await _inscripcionCatequesisService.ActualizarEstadoAsync(id, request);

        if (response is null)
        {
            return NotFound(new { mensaje = InscripcionCatequesisValidaciones.MensajeNoEncontrado });
        }

        return Ok(response);
    }

    private static object CrearRespuestaValidacion(ModelStateDictionary modelState)
    {
        var errores = modelState
            .Where(entry => entry.Value?.Errors.Count > 0)
            .ToDictionary(
                entry => entry.Key,
                entry => entry.Value!.Errors.Select(error => error.ErrorMessage).ToArray());

        var mensaje = errores.Values
            .SelectMany(errorMessages => errorMessages)
            .FirstOrDefault() ?? "Errores de validación.";

        return new { mensaje, errores };
    }
}
