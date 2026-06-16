namespace SanblasBackend.DTOs;

public class InscripcionCatequesisResumenResponse
{
    public int Id { get; set; }
    public string NombreCatequizando { get; set; } = string.Empty;
    public string CentroCatequesis { get; set; } = string.Empty;
    public string NivelAInscribirse { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public DateTime FechaSolicitud { get; set; }
    public string TelefonoEncargada { get; set; } = string.Empty;
}
