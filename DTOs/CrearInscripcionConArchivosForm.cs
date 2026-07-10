using Microsoft.AspNetCore.Http;

namespace SanblasBackend.DTOs;

public class CrearInscripcionConArchivosForm
{
    public string Payload { get; set; } = string.Empty;
    public IFormFile FeBautismoArchivo { get; set; } = null!;
    public IFormFile ComprobanteArchivo { get; set; } = null!;
}
