using SanblasBackend.DTOs;

namespace SanblasBackend.Services;

public interface IExportarInscripcionesCatequesisService
{
    Task<ArchivoExportacionExcel> ExportarAsync(
        string estado,
        CancellationToken cancellationToken = default);
}
