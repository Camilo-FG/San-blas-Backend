namespace SanblasBackend.Models;

public class Catequizando
{
    public int Id { get; set; }
    public int InscripcionCatequesisId { get; set; }
    public InscripcionCatequesis InscripcionCatequesis { get; set; } = null!;
    public string Nombre { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public DateOnly FechaNacimiento { get; set; }
    public string? DireccionExacta { get; set; }
}
