using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NegocioGalRio_API.Models
{
    public class DetalleVenta
    {
        public int Id { get; set; }
        [ForeignKey("Venta")]
        public int IdVenta { get; set; }
        public Venta Venta { get; set; }
        [ForeignKey("Producto")]
        public int IdProducto { get; set; }
        public Producto Producto { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public decimal PrecioUnitario { get; set; } 
        public decimal Subtotal {  get; set; }
    }
}
