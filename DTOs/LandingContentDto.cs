using System.ComponentModel.DataAnnotations;

namespace SanblasBackend.DTOs;

public class LandingSectionDto
{
    public string SectionKey { get; set; } = string.Empty;
    public object Data { get; set; } = new();
    public DateTime? UpdatedAt { get; set; }
}

public class UpdateLandingSectionRequest
{
    [Required(ErrorMessage = "Los datos de la sección son obligatorios.")]
    public object Data { get; set; } = new();
}
