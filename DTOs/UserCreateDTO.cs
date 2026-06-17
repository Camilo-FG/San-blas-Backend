using SanblasBackend.Models;
using System.ComponentModel.DataAnnotations;

namespace SanblasBackend.DTOs
{
    public class UserCreateDto
    {
        [Required(ErrorMessage = "El nombre de usuario no puede estar vacio.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del Email es incorrecto.")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El numero de telefono tiene que ser de 8 digitos.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        public string Password { get; set; } = string.Empty;

        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}