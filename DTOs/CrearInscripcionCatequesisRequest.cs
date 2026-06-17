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
    [AllowedValues("Primero", "Sétimo", ErrorMessage = InscripcionCatequesisValidaciones.MensajeNivelInvalido)]
    public string NivelAInscribirse { get; set; } = string.Empty;
}

public class DatosCatequizandoRequest
{
    [Required(ErrorMessage = "El nombre del catequizando es obligatorio.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "Los apellidos del catequizando son obligatorios.")]
    public string Apellidos { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
    [NotFutureDate(ErrorMessage = InscripcionCatequesisValidaciones.MensajeFechaNacimientoFutura)]
    public DateOnly FechaNacimiento { get; set; }

    [Required(ErrorMessage = "La dirección exacta del catequizando es obligatoria.")]
    public string DireccionExacta { get; set; } = string.Empty;
}

public class DatosBautismoRequest : IValidatableObject
{
    [Required(ErrorMessage = "La parroquia de bautismo es obligatoria.")]
    public string Parroquia { get; set; } = string.Empty;

    public DateOnly? Fecha { get; set; }

    public string? Tomo { get; set; }
    public string? Folio { get; set; }
    public string? Asiento { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Fecha.HasValue && !InscripcionCatequesisValidaciones.ValidarFechaNoFutura(Fecha))
        {
            yield return new ValidationResult(
                InscripcionCatequesisValidaciones.MensajeFechaBautismoFutura,
                [nameof(Fecha)]);
        }
    }
}

public class DatosAdecuacionRequest : IValidatableObject
{
    [Required(ErrorMessage = "Debe indicar si requiere adecuación en el centro educativo.")]
    public bool? RequiereAdecuacionCentroEducativo { get; set; }

    public string? DescripcionAdecuacion { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (RequiereAdecuacionCentroEducativo == true && string.IsNullOrWhiteSpace(DescripcionAdecuacion))
        {
            yield return new ValidationResult(
                "La descripción de adecuación es obligatoria cuando requiere adecuación en el centro educativo.",
                [nameof(DescripcionAdecuacion)]);
        }
    }
}

public class DatosCondicionSaludRequest : IValidatableObject
{
    [Required(ErrorMessage = "Debe indicar si el catequizando es portador de enfermedad crónica.")]
    public bool? PortadorEnfermedadCronica { get; set; }

    public string? DescripcionEnfermedad { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (PortadorEnfermedadCronica == true && string.IsNullOrWhiteSpace(DescripcionEnfermedad))
        {
            yield return new ValidationResult(
                "La descripción de la enfermedad es obligatoria cuando es portador de enfermedad crónica.",
                [nameof(DescripcionEnfermedad)]);
        }
    }
}

public class DatosMadreRequest
{
    [Required(ErrorMessage = "El nombre de la madre o encargada es obligatorio.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "Los apellidos de la madre o encargada son obligatorios.")]
    public string Apellidos { get; set; } = string.Empty;

    [Required(ErrorMessage = "La dirección exacta de la madre o encargada es obligatoria.")]
    public string DireccionExacta { get; set; } = string.Empty;

    [Required(ErrorMessage = "La ciudad de la madre o encargada es obligatoria.")]
    public string Ciudad { get; set; } = string.Empty;

    [Required(ErrorMessage = "La provincia de la madre o encargada es obligatoria.")]
    public string Provincia { get; set; } = string.Empty;

    [Required(ErrorMessage = "El teléfono de la madre o encargada es obligatorio.")]
    public string Telefono { get; set; } = string.Empty;
}
