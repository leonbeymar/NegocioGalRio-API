using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NegocioGalRio_API.Models
{
    public class DetalleCompra
    {
        public int Id { get; set; }
        [ForeignKey("Compra")]
        public int IdCompra { get; set; }
        public Compra Compra { get; set; }

        [ForeignKey("Producto")]
        public int IdProducto { get; set; }
        public Producto Producto { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public decimal PrecioCompraUnidad { get; set; }
        public decimal Subtotal { get; set; }
    }
}
