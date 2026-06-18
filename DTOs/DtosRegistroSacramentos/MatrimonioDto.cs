using System.Text.Json.Serialization;

namespace SanblasBackend.DTOs.DtosRegistroSacramentos
{
    public class MatrimonioDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nombreContrayente")]
        public string NombreContrayente { get; set; } = string.Empty;

        [JsonPropertyName("nombreContrayente2")]
        public string NombreContrayente2 { get; set; } = string.Empty;

        [JsonPropertyName("diaMatrimonio")]
        public string DiaMatrimonio { get; set; } = string.Empty;

        [JsonPropertyName("mesMatrimonio")]
        public string MesMatrimonio { get; set; } = string.Empty;

        [JsonPropertyName("annioMatrimonio")]
        public int AnnioMatrimonio { get; set; }

        [JsonPropertyName("lugarMatrimonio")]
        public string LugarMatrimonio { get; set; } = string.Empty;

        [JsonPropertyName("tomo")]
        public int Tomo { get; set; }

        [JsonPropertyName("folio")]
        public int Folio { get; set; }
    }
}