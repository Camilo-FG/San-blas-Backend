using SanblasBackend.DTOs;

namespace SanblasBackend.Services;

public interface IInscripcionCatequesisService
{
    Task<CrearInscripcionCatequesisResponse> CrearInscripcionAsync(CrearInscripcionCatequesisRequest request);
    Task<IEnumerable<InscripcionCatequesisResumenResponse>> ObtenerInscripcionesAsync(string? estado);
    Task<InscripcionCatequesisDetalleResponse?> ObtenerInscripcionPorIdAsync(int id);
}
