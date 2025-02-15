using System.ComponentModel.DataAnnotations;

namespace NegocioGalRio_API.DTO
{
    public class ProveedorInsert
    {
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "El campo Telefono es obligatorio.")]
        public string Telefono { get; set; } = null!;
        [Required(ErrorMessage = "El campo Direccion es obligatorio.")]
        public string Direccion { get; set; } = null!;
    }
}
