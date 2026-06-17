using System.ComponentModel.DataAnnotations;
using SanblasBackend.Utils;

namespace SanblasBackend.DTOs;

public class ActualizarEstadoInscripcionCatequesisRequest : IValidatableObject
{
    [Required(ErrorMessage = "El estado es obligatorio.")]
    [AllowedValues("Pendiente", "Aprobada", "Rechazada", ErrorMessage = InscripcionCatequesisValidaciones.MensajeEstadoInvalido)]
    public string Estado { get; set; } = string.Empty;

    public string? Observacion { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Estado.Equals("Rechazada", StringComparison.OrdinalIgnoreCase)
            && string.IsNullOrWhiteSpace(Observacion))
        {
            yield return new ValidationResult(
                "La observación administrativa es obligatoria cuando el estado es Rechazada.",
                [nameof(Observacion)]);
        }
    }
}
