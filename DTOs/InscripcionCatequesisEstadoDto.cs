using System.ComponentModel.DataAnnotations;

namespace SanblasBackend.DTOs;

public class InscripcionCatequesisEstadoDto
{
    [Required(ErrorMessage = "El estado es obligatorio.")]
    [AllowedValues("Pendiente", "Aprobada", "Rechazada", ErrorMessage = "El estado solo puede ser 'Pendiente', 'Aprobada' o 'Rechazada'.")]
    public string Estado { get; set; } = string.Empty;
}
