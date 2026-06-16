using SanblasBackend.DTOs;

namespace SanblasBackend.Services;

public interface IInscripcionCatequesisService
{
    Task<CrearInscripcionCatequesisResponse> CrearInscripcionAsync(CrearInscripcionCatequesisRequest request);
}
