namespace SanblasBackend.Models;

public class PagoInscripcionCatequesis
{
    public int Id { get; set; }
    public int InscripcionCatequesisId { get; set; }
    public InscripcionCatequesis InscripcionCatequesis { get; set; } = null!;
    public string MetodoPago { get; set; } = "SINPE Móvil";
    public string NumeroComprobanteSinpe { get; set; } = string.Empty;
    public string ComprobanteArchivo { get; set; } = string.Empty;
    public decimal Monto { get; set; }
}
