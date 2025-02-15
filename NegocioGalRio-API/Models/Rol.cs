using System.ComponentModel.DataAnnotations;

namespace NegocioGalRio_API.Models
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = null!;

        public List<Usuario> Usuarios { get; set; } = new();

    }
}
