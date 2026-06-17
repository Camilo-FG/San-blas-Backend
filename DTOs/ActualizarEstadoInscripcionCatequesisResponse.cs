namespace SanblasBackend.DTOs;

public class ActualizarEstadoInscripcionCatequesisResponse
{
    public int Id { get; set; }
    public string Mensaje { get; set; } = "Estado de inscripción actualizado correctamente";
    public string Estado { get; set; } = string.Empty;
    public string? ObservacionAdministrativa { get; set; }
    public DateTime FechaActualizacionEstado { get; set; }
}
