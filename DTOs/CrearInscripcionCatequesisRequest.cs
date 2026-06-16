using System.ComponentModel.DataAnnotations;
using SanblasBackend.Utils;

namespace SanblasBackend.DTOs;

public class CrearInscripcionCatequesisRequest
{
    [Required(ErrorMessage = "Los datos de inscripción son obligatorios.")]
    public DatosInscripcionRequest DatosInscripcion { get; set; } = null!;

    [Required(ErrorMessage = "Los datos del catequizando son obligatorios.")]
    public DatosCatequizandoRequest DatosCatequizando { get; set; } = null!;

    [Required(ErrorMessage = "Los datos de bautismo son obligatorios.")]
    public DatosBautismoRequest DatosBautismo { get; set; } = null!;

    [Required(ErrorMessage = "Los datos de adecuación educativa son obligatorios.")]
    public DatosAdecuacionRequest DatosAdecuacion { get; set; } = null!;

    [Required(ErrorMessage = "Los datos de condición de salud son obligatorios.")]
    public DatosCondicionSaludRequest DatosCondicionSalud { get; set; } = null!;

    [Required(ErrorMessage = "Los datos de la madre son obligatorios.")]
    public DatosMadreRequest DatosMadre { get; set; } = null!;
}

public class DatosInscripcionRequest
{
    [Required(ErrorMessage = "El centro de catequesis es obligatorio.")]
    public string CentroCatequesis { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nivel a inscribirse es obligatorio.")]
    [AllowedValues("Primero", "Sétimo", ErrorMessage = "El nivel solo puede ser 'Primero' o 'Sétimo'.")]
    public string NivelAInscribirse { get; set; } = string.Empty;
}

public class DatosCatequizandoRequest
{
    [Required(ErrorMessage = "El nombre del catequizando es obligatorio.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "Los apellidos del catequizando son obligatorios.")]
    public string Apellidos { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
    [NotFutureDate]
    public DateOnly FechaNacimiento { get; set; }

    public string? DireccionExacta { get; set; }
}

public class DatosBautismoRequest
{
    public string? Parroquia { get; set; }
    public DateOnly? Fecha { get; set; }
    public string? Tomo { get; set; }
    public string? Folio { get; set; }
    public string? Asiento { get; set; }
}

public class DatosAdecuacionRequest
{
    public bool? RequiereAdecuacionCentroEducativo { get; set; }
    public string? DescripcionAdecuacion { get; set; }
}

public class DatosCondicionSaludRequest
{
    public bool? PortadorEnfermedadCronica { get; set; }
    public string? DescripcionEnfermedad { get; set; }
}

public class DatosMadreRequest
{
    [Required(ErrorMessage = "El nombre de la madre es obligatorio.")]
    public string Nombre { get; set; } = string.Empty;

    public string? Apellidos { get; set; }
    public string? DireccionExacta { get; set; }
    public string? Ciudad { get; set; }
    public string? Provincia { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    public string Telefono { get; set; } = string.Empty;
}
