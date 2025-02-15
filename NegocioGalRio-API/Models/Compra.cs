using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NegocioGalRio_API.Models
{
    public class Compra
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Proveedor")]
        public int IdProveedor { get; set; }
        public Proveedor Proveedor { get; set; }
        public DateTime FechaCompra { get; set; }
        [Required]
        public decimal Total { get; set; }
        public List<DetalleCompra> DetalleCompras { get; set; }
    }
}
