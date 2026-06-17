using System.ComponentModel.DataAnnotations;

namespace SanblasBackend.DTOs;

public class ActualizarEstadoInscripcionCatequesisRequest : IValidatableObject
{
    [Required(ErrorMessage = "El estado es obligatorio.")]
    [AllowedValues("Pendiente", "Aprobada", "Rechazada", ErrorMessage = "El estado solo puede ser 'Pendiente', 'Aprobada' o 'Rechazada'.")]
    public string Estado { get; set; } = string.Empty;

    public string? Observacion { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Estado.Equals("Rechazada", StringComparison.OrdinalIgnoreCase)
            && string.IsNullOrWhiteSpace(Observacion))
        {
            yield return new ValidationResult(
                "La observación es obligatoria cuando el estado es Rechazada.",
                [nameof(Observacion)]);
        }
    }
}
