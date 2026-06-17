namespace SanblasBackend.Models.EntitiesRegistroSacramentos;

public class Bautismo
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Cedula { get; set; }
    public string PrimerApellido { get; set; } = string.Empty;
    public string SegundoApellido { get; set; } = string.Empty;
    public string NombreParroquia { get; set; } = string.Empty;
    public DateTime FechaBautismo { get; set; }
    public int AnnioBautismo { get; set; }
    public string Prebispero { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public TimeSpan HoraNacimiento { get; set; }
    public string NombreAbuelosPaternos { get; set; } = string.Empty;
    public string NombreAbuelosMaternos { get; set; } = string.Empty;
}

