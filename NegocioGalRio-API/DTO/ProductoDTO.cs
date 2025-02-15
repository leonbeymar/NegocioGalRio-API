using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NegocioGalRio_API.DTO
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Marca { get; set; } = null!;
        public decimal PrecioVenta { get; set; }
        public decimal PrecioCompraUnidad { get; set; }
        public int Stock { get; set; }
        public int IdCategoria { get; set; }
    }

    public class ProductoInsert
    {
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string? Nombre { get; set; } = null!;
        [Required(ErrorMessage = "El campo Marca es obligatorio.")]
        public string? Marca { get; set; } = null!;
        [Required(ErrorMessage = "El campo PrecioVenta es obligatorio.")]
        public decimal? PrecioVenta { get; set; }
        [Required(ErrorMessage = "El campo PrecioCompraUnidad es obligatorio.")]
        //public decimal? PrecioCompraUnidad { get; set; }
        //[Required(ErrorMessage = "El campo Stock es obligatorio.")]
        public int? Stock { get; set; }
        [Required(ErrorMessage = "El campo IdCategoria es obligatorio.")]
        public int? IdCategoria { get; set; }
    }

    public class ProductoUpdate
    {
        public decimal PrecioVenta { get; set; }
        //public decimal PrecioCompraUnidad { get; set; }
        public int Stock { get; set; }
        public int IdCategoria { get; set; }
    }
}
