namespace SanblasBackend.DTOs;

public class ArchivoExportacionExcel
{
    public byte[] Contenido { get; init; } = [];
    public string NombreArchivo { get; init; } = string.Empty;
    public string ContentType { get; init; } =
        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
}
