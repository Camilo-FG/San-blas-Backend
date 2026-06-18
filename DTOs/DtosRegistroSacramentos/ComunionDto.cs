using System.Text.Json.Serialization;

namespace SanblasBackend.DTOs.DtosRegistroSacramentos
{
    public class ComunionDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("diaComunion")]
        public string DiaComunion { get; set; } = string.Empty;

        [JsonPropertyName("mesComunion")]
        public string MesComunion { get; set; } = string.Empty;

        [JsonPropertyName("annioComunion")]
        public int AnnioComunion { get; set; }

        [JsonPropertyName("lugarComunion")]
        public string LugarComunion { get; set; } = string.Empty;
    }
}