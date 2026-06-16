namespace SanblasBackend.DTOs;

public class InscripcionCatequesisDetalleResponse
{
    public int Id { get; set; }
    public string CentroCatequesis { get; set; } = string.Empty;
    public string NivelAInscribirse { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public DateTime FechaSolicitud { get; set; }
    public CatequizandoDetalleResponse Catequizando { get; set; } = null!;
    public BautismoDetalleResponse Bautismo { get; set; } = null!;
    public AdecuacionDetalleResponse Adecuacion { get; set; } = null!;
    public CondicionSaludDetalleResponse CondicionSalud { get; set; } = null!;
    public MadreDetalleResponse Madre { get; set; } = null!;
}

public class CatequizandoDetalleResponse
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public DateOnly FechaNacimiento { get; set; }
    public string DireccionExacta { get; set; } = string.Empty;
}

public class BautismoDetalleResponse
{
    public string Parroquia { get; set; } = string.Empty;
    public DateOnly? Fecha { get; set; }
    public string Tomo { get; set; } = string.Empty;
    public string Folio { get; set; } = string.Empty;
    public string Asiento { get; set; } = string.Empty;
}

public class AdecuacionDetalleResponse
{
    public bool? RequiereAdecuacionCentroEducativo { get; set; }
    public string DescripcionAdecuacion { get; set; } = string.Empty;
}

public class CondicionSaludDetalleResponse
{
    public bool? PortadorEnfermedadCronica { get; set; }
    public string DescripcionEnfermedad { get; set; } = string.Empty;
}

public class MadreDetalleResponse
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string DireccionExacta { get; set; } = string.Empty;
    public string Ciudad { get; set; } = string.Empty;
    public string Provincia { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
}
