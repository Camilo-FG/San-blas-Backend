namespace SanblasBackend.Models;

public class Donacion
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public bool Anonimo { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Correo { get; set; } = string.Empty;

    public string? Telefono { get; set; }

    public string Detalle { get; set; } = string.Empty;

    public string Estado { get; set; } = "Pendiente";
}