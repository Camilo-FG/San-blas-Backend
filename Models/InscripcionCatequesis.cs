namespace SanblasBackend.Models;

public class InscripcionCatequesis
{
    public int Id { get; set; }
    public string? CentroCatequesis { get; set; }
    public string NivelAInscribirse { get; set; } = string.Empty;
    public string? FeBautismoArchivo { get; set; }
    public string NombreCatequizando { get; set; } = string.Empty;
    public string ApellidosCatequizando { get; set; } = string.Empty;
    public DateOnly? FechaNacimiento { get; set; }
    public string? DireccionExactaCatequizando { get; set; }
    public string? ParroquiaBautismo { get; set; }
    public DateOnly? FechaBautismo { get; set; }
    public string? Tomo { get; set; }
    public string? Folio { get; set; }
    public string? Asiento { get; set; }
    public bool? RequiereAdecuacionCentroEducativo { get; set; }
    public string? DescripcionAdecuacion { get; set; }
    public bool? PortadorEnfermedadCronica { get; set; }
    public string? DescripcionEnfermedad { get; set; }
    public string NombreMadre { get; set; } = string.Empty;
    public string ApellidosMadre { get; set; } = string.Empty;
    public string? DireccionExactaMadre { get; set; }
    public string? CiudadMadre { get; set; }
    public string? ProvinciaMadre { get; set; }
    public string TelefonoMadre { get; set; } = string.Empty;
    public string Estado { get; set; } = "Pendiente";
    public DateTime FechaRegistro { get; set; }
}
