using System.Text.Json.Serialization;

namespace SanblasBackend.DTOs.DtosRegistroSacramentos
{
    public class BautismoDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;
        
        [JsonPropertyName("cedula")]
        public int Cedula { get; set; }
        
        [JsonPropertyName("primerApellido")]
        public string PrimerApellido { get; set; } = string.Empty;
        
        [JsonPropertyName("segundoApellido")]
        public string SegundoApellido { get; set; } = string.Empty;
        
        [JsonPropertyName("nombreParroquia")]
        public string NombreParroquia { get; set; } = string.Empty;
        
        [JsonPropertyName("fechaBautismo")]
        public string FechaBautismo { get; set; } = string.Empty; 
        
        [JsonPropertyName("annioBautismo")]
        public int AnnioBautismo { get; set; }
        
        [JsonPropertyName("prebispero")]
        public string Prebispero { get; set; } = string.Empty;
        
        [JsonPropertyName("fechaNacimiento")]
        public string FechaNacimiento { get; set; } = string.Empty; 
        
        [JsonPropertyName("horaNacimiento")]
        public string HoraNacimiento { get; set; } = string.Empty;  
        
        [JsonPropertyName("nombreAbuelosPaternos")]
        public string NombreAbuelosPaternos { get; set; } = string.Empty;
        
        [JsonPropertyName("nombreAbuelosMaternos")]
        public string NombreAbuelosMaternos { get; set; } = string.Empty;
    }
}