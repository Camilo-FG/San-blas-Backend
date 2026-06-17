namespace SanblasBackend.DTOs.DtosRegistroSacramentos
{
    public class ConfirmacionDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string DiaConfirmacion { get; set; } = string.Empty;
        public string MesConfirmacion { get; set; } = string.Empty;
        public int AnnioConfirmacion { get; set; }
        public string LugarConfirmacion { get; set; } = string.Empty;
    }
}