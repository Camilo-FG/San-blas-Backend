using System.ComponentModel.DataAnnotations;

namespace SanblasBackend.DTOs;

public class InscripcionCatequesisUpdateDto
{
    public string? CentroCatequesis { get; set; }

    [Required(ErrorMessage = "El nivel a inscribirse es obligatorio.")]
    [AllowedValues("Primer Nivel", "Sétimo Nivel", ErrorMessage = "El nivel solo puede ser 'Primer Nivel' o 'Sétimo Nivel'.")]
    public string NivelAInscribirse { get; set; } = string.Empty;

    public string? FeBautismoArchivo { get; set; }

    [Required(ErrorMessage = "El nombre del catequizando es obligatorio.")]
    public string NombreCatequizando { get; set; } = string.Empty;

    [Required(ErrorMessage = "Los apellidos del catequizando son obligatorios.")]
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

    [Required(ErrorMessage = "El nombre de la madre es obligatorio.")]
    public string NombreMadre { get; set; } = string.Empty;

    [Required(ErrorMessage = "Los apellidos de la madre son obligatorios.")]
    public string ApellidosMadre { get; set; } = string.Empty;

    public string? DireccionExactaMadre { get; set; }
    public string? CiudadMadre { get; set; }
    public string? ProvinciaMadre { get; set; }

    [Required(ErrorMessage = "El teléfono de la madre es obligatorio.")]
    public string TelefonoMadre { get; set; } = string.Empty;
}
