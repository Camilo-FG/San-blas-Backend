namespace SanblasBackend.DTOs;

public class InscripcionCatequesisExportacionFila
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public DateOnly FechaNacimiento { get; set; }
    public string CentroCatequesis { get; set; } = string.Empty;
    public string NivelAInscribirse { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public DateTime FechaSolicitud { get; set; }
}
