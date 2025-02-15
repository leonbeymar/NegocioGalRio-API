using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NegocioGalRio_API.Models
{
    public class Producto
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = null!;
        [Required]
        public string Marca { get; set; } = null!;
        [Required]
        public decimal PrecioVenta { get; set; } = 0.00M;
        [Required]
        public decimal PrecioCompraUnidad { get; set; } = 0.00M;
        [Required]
        public int Stock { get; set; }
        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; }
        public Categoria Categoria { get; set; } = new Categoria();
    }
}
