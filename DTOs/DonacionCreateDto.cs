using System.ComponentModel.DataAnnotations;

namespace SanblasBackend.DTOs;

public class DonacionCreateDto
{
    public bool Anonimo { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
    public string Correo { get; set; } = string.Empty;

    public string? Telefono { get; set; }

    [Required(ErrorMessage = "El detalle de la donación es obligatorio.")]
    [MaxLength(300, ErrorMessage = "El detalle no puede superar los 300 caracteres.")]
    public string Detalle { get; set; } = string.Empty;
}