namespace.SanblasBackend.Models.EntitiesRegistroSacramentos;


public class Comunion
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string DiaComunion { get; set; } = string.Empty;
    public string MesComunion { get; set; } = string.Empty;
    public int AnnioComunion { get; set; }
    public string LugarComunion { get; set; } = string.Empty;
}