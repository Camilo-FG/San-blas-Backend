namespace SanblasBackend.Models;

public class Evento
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public string Lugar { get; set; } = string.Empty;
    public bool Publicado { get; set; } = true;
}
