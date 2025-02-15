using System.ComponentModel.DataAnnotations;

namespace NegocioGalRio_API.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string DNI { get; set; } = null!;
        [Required]
        public string Nombre { get; set;} = null!;
        [Required]
        public string Apellido { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Contrasenia { get; set;} = null!;
        public List<Rol>? Roles { get; set; } = new List<Rol>();
    }
}
