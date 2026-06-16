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
            return new ValidationResult("La fecha de nacimiento no puede ser futura.");
        }

        return ValidationResult.Success;
    }
}
