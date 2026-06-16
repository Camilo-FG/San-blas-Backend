namespace SanblasBackend.Models;

public class BautismoCatequizando
{
    public int Id { get; set; }
    public int InscripcionCatequesisId { get; set; }
    public InscripcionCatequesis InscripcionCatequesis { get; set; } = null!;
    public string? Parroquia { get; set; }
    public DateOnly? Fecha { get; set; }
    public string? Tomo { get; set; }
    public string? Folio { get; set; }
    public string? Asiento { get; set; }
}
