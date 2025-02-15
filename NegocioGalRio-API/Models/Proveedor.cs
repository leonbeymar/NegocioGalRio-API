using System.ComponentModel.DataAnnotations;

namespace NegocioGalRio_API.Models
{
    public class Proveedor
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = null!;
        [Required]
        public string Telefono { get; set; } = null!;
        [Required]
        public string Direccion { get; set; } = null!;
    }
}
