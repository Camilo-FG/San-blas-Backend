namespace SanblasBackend.Models;

public class PersonaInscribeCatequesis
{
    public int Id { get; set; }
    public int InscripcionCatequesisId { get; set; }
    public InscripcionCatequesis InscripcionCatequesis { get; set; } = null!;
    public string Nombre { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Parentesco { get; set; } = string.Empty;
}
