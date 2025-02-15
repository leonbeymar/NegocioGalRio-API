using System.ComponentModel.DataAnnotations;

namespace NegocioGalRio_API.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = null!;
        public List<Producto> Productos { get; set; } = new List<Producto>();
    }

}
