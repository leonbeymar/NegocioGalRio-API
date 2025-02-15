using NegocioGalRio_API.Models;

namespace NegocioGalRio_API.DTO
{
    public class RolDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }

    public class RolInsertDto
    {
        public string Nombre { get; set; } = null!;
    }


    public class UsuarioDto
    {
        public int Id { get; set; }
        public string DNI { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Email { get; set; } = null!;
        public List<RolDto> Roles { get; set; } = new List<RolDto> ();
    }


    public class UsuarioInsertDto
    {
        public string DNI { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Contrasenia { get; set; } = null!;
    }

    public class RolAddUsuario
    {
        public List<RolInsertDto> Roles { get; set; } = new List<RolInsertDto>();
    }
}
