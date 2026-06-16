namespace SanblasBackend.Models;

public class CondicionSaludCatequizando
{
    public int Id { get; set; }
    public int InscripcionCatequesisId { get; set; }
    public InscripcionCatequesis InscripcionCatequesis { get; set; } = null!;
    public bool? PortadorEnfermedadCronica { get; set; }
    public string? DescripcionEnfermedad { get; set; }
}
