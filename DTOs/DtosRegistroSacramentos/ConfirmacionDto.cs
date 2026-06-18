using System.Text.Json.Serialization;

namespace SanblasBackend.DTOs.DtosRegistroSacramentos
{
    public class ConfirmacionDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("diaConfirmacion")]
        public string DiaConfirmacion { get; set; } = string.Empty;

        [JsonPropertyName("mesConfirmacion")]
        public string MesConfirmacion { get; set; } = string.Empty;

        [JsonPropertyName("annioConfirmacion")]
        public int AnnioConfirmacion { get; set; }

        [JsonPropertyName("lugarConfirmacion")]
        public string LugarConfirmacion { get; set; } = string.Empty;
    }
}