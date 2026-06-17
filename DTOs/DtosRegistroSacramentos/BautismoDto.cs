namespace SanblasBackend.DTOs.DtosRegistroSacramentos
{
    public class BautismoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Cedula { get; set; }
        public string PrimerApellido { get; set; } = string.Empty;
        public string SegundoApellido { get; set; } = string.Empty;
        public string NombreParroquia { get; set; } = string.Empty;
        public string FechaBautismo { get; set; } = string.Empty; 
        public int AnnioBautismo { get; set; }
        public string Prebispero { get; set; } = string.Empty;
        public string FechaNacimiento { get; set; } = string.Empty; 
        public string HoraNacimiento { get; set; } = string.Empty;  
        public string NombreAbuelosPaternos { get; set; } = string.Empty;
        public string NombreAbuelosMaternos { get; set; } = string.Empty;
    }
}