using System.ComponentModel.DataAnnotations;

namespace SanblasBackend.DTOs
{
    public class UserUpdateDto
    {
        [MinLength(3, ErrorMessage = "El nombre debe tener al menos 3 caracteres.")]
        [MaxLength(50, ErrorMessage = "El nombre no puede exceder 50 caracteres.")]
        public string? UserName { get; set; }

        [EmailAddress(ErrorMessage = "El formato del Email es incorrecto.")]
        public string? Email { get; set; }

        [RegularExpression(@"^\d{8}$", ErrorMessage = "El número de teléfono debe tener 8 dígitos.")]
        public string? PhoneNumber { get; set; }

        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string? ConfirmPassword { get; set; }

        public bool? UserRole { get; set; } //solo el admin puede moficar esto
        public bool? State { get; set; }    //solo el admin puede moficar esto
    }
}