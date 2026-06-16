namespace SanblasBackend.Models;

public class InscripcionCatequesis
{
    public int Id { get; set; }
    public string CentroCatequesis { get; set; } = string.Empty;
    public string NivelAInscribirse { get; set; } = string.Empty;
    public string Estado { get; set; } = "Pendiente";
    public DateTime FechaSolicitud { get; set; }

    public Catequizando Catequizando { get; set; } = null!;
    public BautismoCatequizando Bautismo { get; set; } = null!;
    public AdecuacionCatequizando Adecuacion { get; set; } = null!;
    public CondicionSaludCatequizando CondicionSalud { get; set; } = null!;
    public MadreCatequizando Madre { get; set; } = null!;
}
