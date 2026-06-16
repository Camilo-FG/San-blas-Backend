namespace SanblasBackend.DTOs;

public class CrearInscripcionCatequesisResponse
{
    public int Id { get; set; }
    public string Mensaje { get; set; } = "Inscripción a catequesis registrada correctamente";
    public string Estado { get; set; } = string.Empty;
    public DateTime FechaSolicitud { get; set; }
}
