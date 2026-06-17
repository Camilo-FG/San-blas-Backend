namespace.SanblasBackend.Models.EntitiesRegistroSacramentos;


public class Matrimonio
{
    public int Id { get; set; }
    public string NombreContrayente { get; set; } = string.Empty;
    public string NombreContrayente2 { get; set; } = string.Empty;
    public string DiaMatrimonio { get; set; } = string.Empty;
    public string MesMatrimonio { get; set; } = string.Empty;
    public int AnnioMatrimonio { get; set; }
    public string LugarMatrimonio { get; set; } = string.Empty;
    public int Tomo { get; set; }
    public int Folio { get; set; }
}