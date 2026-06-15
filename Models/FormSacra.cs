namespace SanblasBackend.Models
{
    //se crea la clase para la solicitud de sacramentos
    public class FormSacra
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Cedula {  get; set; }
        public string Correo { get; set; }
        public string Telefono {  get; set; }
        public enum Sacramentos { Bautismo, Confirmacion, Matrimonio }
        public Sacramentos TipoSacramento {  get; set; }
        public string Motivo { get; set; }
        public string? Estado {  get; set; }




    }
}
