using System.ComponentModel.DataAnnotations;

namespace SanblasBackend.Utils;

public class NotFutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return ValidationResult.Success;
        }

        if (value is DateOnly date && date > DateOnly.FromDateTime(DateTime.Today))
        {
            var mensaje = string.IsNullOrWhiteSpace(ErrorMessage)
                ? "La fecha no puede ser futura."
                : ErrorMessage;

            return new ValidationResult(mensaje);
        }

        return ValidationResult.Success;
    }
}
