using System.ComponentModel.DataAnnotations;

namespace SanblasBackend.DTOs;

public class EventoDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El título es obligatorio.")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripción es obligatoria.")]
    public string Descripcion { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
    public DateTime FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    [Required(ErrorMessage = "El lugar es obligatorio.")]
    public string Lugar { get; set; } = string.Empty;

    public bool Publicado { get; set; } = true;
}
