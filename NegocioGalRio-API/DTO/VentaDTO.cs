using NegocioGalRio_API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace NegocioGalRio_API.DTO
{
    public class VentaDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public List<DetalleVentaDTO> DetalleVentas { get; set; }
    }

    public class DetalleVentaDTO
    {
        public int Id { get; set; }
        public int IdVenta { get; set; }
        public ProductoDTO Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }

    public class VentaInsert {
        public DateTime Fecha { get; set; }
        public List<DetalleVentaInsert> DetalleVentas { get; set; }
    }

    public class DetalleVentaInsert
    {
        public int IdProducto { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}
