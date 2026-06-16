namespace SanblasBackend.Models;

public class AdecuacionCatequizando
{
    public int Id { get; set; }
    public int InscripcionCatequesisId { get; set; }
    public InscripcionCatequesis InscripcionCatequesis { get; set; } = null!;
    public bool? RequiereAdecuacionCentroEducativo { get; set; }
    public string? DescripcionAdecuacion { get; set; }
}
