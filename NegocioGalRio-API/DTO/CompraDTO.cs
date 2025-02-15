using NegocioGalRio_API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NegocioGalRio_API.DTO
{
    public class CompraDTO
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal Total { get; set; }
        public List<DetalleCompraDTO> DetalleCompras { get; set; } = new();
    }

    public class DetalleCompraDTO
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioCompraUnidad { get; set; }
        public decimal Subtotal { get; set; }
    }


    public class CompraInsert
    {
        [Required(ErrorMessage = "El campo IdProveedor es obligatorio.")]
        public int IdProveedor { get; set; }
        [Required(ErrorMessage = "El campo FechaCompra es obligatorio.")]
        public DateTime FechaCompra { get; set; }
        public List<DetalleCompraInsert> DetalleCompras { get; set; } = new();
    }

    public class DetalleCompraInsert
    {
        [Required(ErrorMessage = "El campo IdProducto es obligatorio.")]
        public int IdProducto { get; set; }
        [Required(ErrorMessage = "El campo Cantidad es obligatorio.")]
        public int Cantidad { get; set; }
        [Required(ErrorMessage = "El campo PrecioCompraUnidad es obligatorio.")]
        public decimal PrecioCompraUnidad { get; set; }
    }
}
