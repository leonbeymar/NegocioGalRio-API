using System.ComponentModel.DataAnnotations;

namespace NegocioGalRio_API.DTO
{
    public class CategoriaDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
    }
    public class CategoriaInsert
    {
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string? Nombre { get; set; }
    }
}
