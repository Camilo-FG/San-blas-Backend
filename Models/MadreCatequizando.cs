namespace SanblasBackend.Models;

public class MadreCatequizando
{
    public int Id { get; set; }
    public int InscripcionCatequesisId { get; set; }
    public InscripcionCatequesis InscripcionCatequesis { get; set; } = null!;
    public string Nombre { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string? DireccionExacta { get; set; }
    public string? Ciudad { get; set; }
    public string? Provincia { get; set; }
    public string Telefono { get; set; } = string.Empty;
}
