using System.Text.Json;
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
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    private readonly IInscripcionCatequesisService _inscripcionCatequesisService;
    private readonly IFileStorageService _fileStorageService;

    public InscripcionesCatequesisController(
        IInscripcionCatequesisService inscripcionCatequesisService,
        IFileStorageService fileStorageService)
    {
        _inscripcionCatequesisService = inscripcionCatequesisService;
        _fileStorageService = fileStorageService;
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
    [Consumes("application/json")]
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

    [HttpPost]
    [AllowAnonymous]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> CrearInscripcionConArchivos(
        [FromForm] string payload,
        [FromForm] IFormFile feBautismoArchivo,
        [FromForm] IFormFile comprobanteArchivo)
    {
        if (string.IsNullOrWhiteSpace(payload))
            return BadRequest(new { mensaje = "Los datos de la inscripción son obligatorios." });

        CrearInscripcionCatequesisRequest? request;
        try
        {
            request = JsonSerializer.Deserialize<CrearInscripcionCatequesisRequest>(payload, JsonOptions);
        }
        catch
        {
            return BadRequest(new { mensaje = "El formato de los datos de inscripción no es válido." });
        }

        if (request is null)
            return BadRequest(new { mensaje = "Los datos de la inscripción son obligatorios." });

        try
        {
            if (feBautismoArchivo is null || feBautismoArchivo.Length == 0)
                return BadRequest(new { mensaje = "La fe de bautismo es obligatoria." });

            if (comprobanteArchivo is null || comprobanteArchivo.Length == 0)
                return BadRequest(new { mensaje = "El comprobante de pago es obligatorio." });

            request.DatosInscripcion.FeBautismoArchivo =
                await _fileStorageService.SaveCatequesisFileAsync(feBautismoArchivo, "fe-bautismo");

            request.DatosPago.ComprobanteArchivo =
                await _fileStorageService.SaveCatequesisFileAsync(comprobanteArchivo, "comprobante");

            if (!TryValidateModel(request))
                return BadRequest(CrearRespuestaValidacion(ModelState));

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
