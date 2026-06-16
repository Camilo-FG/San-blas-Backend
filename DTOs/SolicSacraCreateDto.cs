using SanblasBackend.Models;
using System.ComponentModel.DataAnnotations;

namespace SanblasBackend.DTOs
{
    //se crea el Dto para la creacion de la solicitud de sacramentos
    public class SolicSacraCreateDto
    {

        [Required(ErrorMessage = "El Nombre no puede ir vacio.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El primer apellido no puede ir vacio.")]
        public string PrimerApellido { get; set; }=string.Empty;

        [Required(ErrorMessage = "El segundo apellido no puede ir vacio.")]
        public string SegundoApellido { get; set; }= string.Empty;

        [Required]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "La cedula tiene que ser de 9 digitos.")]
        public string Cedula { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del Email es incorrecto.")]
        public string Correo {  get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El telefono tiene que ser de 8 digitos.")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tiene que seleccionar el tipo de sacramento.")]
        [EnumDataType(typeof(FormSacra.Sacramentos), ErrorMessage = "El tipo de sacramento no es válido.")]
        public FormSacra.Sacramentos TipoSacramento { get; set; }

        [Required(ErrorMessage = "Tiene que indicar el motivo por el cual solicita el sacramento")]
        public string Motivo { get; set; } = string.Empty;

        


    }
}
