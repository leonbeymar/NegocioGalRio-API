using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NegocioGalRio_API.Models
{
    public class Venta
    {
        public int Id { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public decimal Total { get; set; }
        public List<DetalleVenta> DetalleVentas { get; set; }

    }
}
