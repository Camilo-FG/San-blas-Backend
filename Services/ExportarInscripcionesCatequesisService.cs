using ClosedXML.Excel;
using SanblasBackend.DTOs;

namespace SanblasBackend.Services;

public class ExportarInscripcionesCatequesisService : IExportarInscripcionesCatequesisService
{
    internal static readonly string[] ColumnasExportadas =
    [
        "Nombre",
        "Apellidos",
        "Fecha de nacimiento",
        "Centro de catequesis",
        "Nivel a inscribirse",
        "Estado de inscripción",
        "Fecha de inscripción",
    ];

    private readonly IInscripcionCatequesisService _inscripcionCatequesisService;

    public ExportarInscripcionesCatequesisService(
        IInscripcionCatequesisService inscripcionCatequesisService)
    {
        _inscripcionCatequesisService = inscripcionCatequesisService;
    }

    public async Task<ArchivoExportacionExcel> ExportarAsync(
        string estado,
        CancellationToken cancellationToken = default)
    {
        var inscripciones = await _inscripcionCatequesisService
            .ObtenerInscripcionesParaExportacionAsync(estado, cancellationToken);

        return new ArchivoExportacionExcel
        {
            Contenido = GenerarContenidoExcel(inscripciones, estado),
            NombreArchivo = GenerarNombreArchivo(estado),
        };
    }

    internal static byte[] GenerarContenidoExcel(
        IReadOnlyList<InscripcionCatequesisExportacionFila> filas,
        string estado)
    {
        using var workbook = new XLWorkbook();
        var nombreHoja = estado.Equals("Aprobada", StringComparison.OrdinalIgnoreCase)
            ? "Inscripciones aprobadas"
            : $"Inscripciones {estado}";

        var worksheet = workbook.Worksheets.Add(nombreHoja);

        for (var columnIndex = 0; columnIndex < ColumnasExportadas.Length; columnIndex++)
        {
            worksheet.Cell(1, columnIndex + 1).Value = ColumnasExportadas[columnIndex];
        }

        worksheet.Range(1, 1, 1, ColumnasExportadas.Length).Style.Font.Bold = true;

        var rowIndex = 2;
        foreach (var fila in filas)
        {
            worksheet.Cell(rowIndex, 1).Value = fila.Nombre;
            worksheet.Cell(rowIndex, 2).Value = fila.Apellidos;
            worksheet.Cell(rowIndex, 3).Value = fila.FechaNacimiento.ToDateTime(TimeOnly.MinValue);
            worksheet.Cell(rowIndex, 3).Style.DateFormat.Format = "dd/MM/yyyy";
            worksheet.Cell(rowIndex, 4).Value = fila.CentroCatequesis;
            worksheet.Cell(rowIndex, 5).Value = fila.NivelAInscribirse;
            worksheet.Cell(rowIndex, 6).Value = fila.Estado;
            worksheet.Cell(rowIndex, 7).Value = fila.FechaSolicitud;
            worksheet.Cell(rowIndex, 7).Style.DateFormat.Format = "dd/MM/yyyy HH:mm";
            rowIndex++;
        }

        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    internal static string GenerarNombreArchivo(string estado)
    {
        var estadoArchivo = estado.ToLowerInvariant()
            .Replace('á', 'a')
            .Replace('é', 'e')
            .Replace('í', 'i')
            .Replace('ó', 'o')
            .Replace('ú', 'u');

        return $"inscripciones_{estadoArchivo}_{DateTime.UtcNow:yyyy-MM-dd}.xlsx";
    }
}
