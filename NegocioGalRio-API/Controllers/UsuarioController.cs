using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NegocioGalRio_API.Contexts;
using NegocioGalRio_API.DTO;
using NegocioGalRio_API.Models;

namespace NegocioGalRio_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly NegocioContext _context;
        public UsuarioController(NegocioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Usuario>> GetUsuarios()
        {
            var usuarios = _context.Usuarios
            .Select(x => new UsuarioDto()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                DNI = x.DNI,
                Email = x.Email,
                Roles = x.Roles.Select(r => new RolDto()
                {
                    Id = r.Id,
                    Nombre = r.Nombre
                }).ToList()
            }).ToList();

            return Ok(usuarios);
        }

        [HttpGet("{idUsuario}")]
        public ActionResult<UsuarioDto> GetUsuarioById(int idUsuario)
        {
            var usuario = _context.Usuarios.Include(r => r.Roles).ToList().FirstOrDefault(x => x.Id == idUsuario);
            if (usuario == null)
                return NotFound($"El usuario con Id:{idUsuario} no existe.");

            var usarioDto = new UsuarioDto()
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                DNI = usuario.DNI,
                Email = usuario.Email,
                Roles = usuario.Roles.Select( r => new RolDto()
                {
                    Id=r.Id,
                    Nombre = r.Nombre,
                }).ToList()
            };

            return Ok(usarioDto);
        }

        [HttpPost]
        public ActionResult<UsuarioDto> PostUsuario(UsuarioInsertDto usuarioInsert)
        {
            var usuario = _context.Usuarios.FirstOrDefault(x => x.DNI == usuarioInsert.DNI);
            if (usuario != null)
            {
                return BadRequest($"Ya existe usuario con ese DNI {usuarioInsert.DNI}");
            }


            var nuevoUsuario = new Usuario()
            {
                Nombre = usuarioInsert.Nombre,
                Apellido = usuarioInsert.Apellido,
                DNI = usuarioInsert.DNI,
                Email = usuarioInsert.Email,
                Contrasenia = usuarioInsert.Contrasenia
            };
            _context.Usuarios.Add(nuevoUsuario);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUsuarioById), new { idUsuario = nuevoUsuario.Id }, nuevoUsuario);
        }

        [HttpPut("{idUsuario}")]
        public ActionResult<Usuario> UpdateUsuario(UsuarioInsertDto usuarioInsert, int idUsuario)
        {
            var usuario = _context.Usuarios.Include(r => r.Roles).FirstOrDefault(x => x.Id == idUsuario);
            if (usuario == null)
                return NotFound($"El usuario con Id:{idUsuario} no existe.");

            usuario.Nombre = usuarioInsert.Nombre;
            usuario.Apellido = usuarioInsert.Apellido;
            usuario.DNI = usuarioInsert.DNI;
            usuario.Email = usuarioInsert.Email;
            usuario.Contrasenia = usuarioInsert.Contrasenia;
            
            _context.SaveChanges();

            return Ok(usuario);
        }

        [HttpPost("{idUsuario}/Rol/{idRol}")]
        public ActionResult<UsuarioDto> AddRolUsuario(int idUsuario, int idRol)
        {
            var usuario = _context.Usuarios
                .Include(r => r.Roles)
                .FirstOrDefault(x => x.Id == idUsuario);

            if (usuario == null)
                return NotFound($"El usuario con Id:{idUsuario} no existe.");
            var rol = _context.Roles.Find(idRol);

            if (rol == null)
                return NotFound($"El rol con Id:{idRol} no existe.");

            if(usuario.Roles.Any(r => r.Id == idRol))
                return BadRequest($"El rol ya se encuentra asignado al usuario.");

            usuario.Roles.Add(rol);
            _context.SaveChanges();

            var usarioDto = new UsuarioDto()
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                DNI = usuario.DNI,
                Email = usuario.Email,
                Roles = usuario.Roles.Select(r => new RolDto()
                {
                    Id = r.Id,
                    Nombre = r.Nombre,
                }).ToList()
            };
            

            return Ok(usarioDto);
        }


        [HttpDelete("{idUsuario}/Rol/{idRol}")]
        public ActionResult<UsuarioDto> RemoveRolUsuario(int idUsuario, int idRol)
        {
            var usuario = _context.Usuarios.Include(r => r.Roles).FirstOrDefault(x => x.Id == idUsuario);
            if (usuario == null)
                return NotFound($"El usuario con Id:{idUsuario} no existe.");

            var rol = _context.Roles.Find(idRol);

            if (rol == null)
                return NotFound($"El rol con Id:{idRol} no existe.");

            if (!usuario.Roles.Any(r => r.Id == idRol))
                return BadRequest($"El usuario no contiene el rol con id {idRol}");

            usuario.Roles.Remove(rol);
            _context.SaveChanges();

            var usarioDto = new UsuarioDto()
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                DNI = usuario.DNI,
                Email = usuario.Email,
                Roles = usuario.Roles.Select(r => new RolDto()
                {
                    Id = r.Id,
                    Nombre = r.Nombre,
                }).ToList()
            };


            return Ok(usarioDto);
        }

        [HttpDelete("{idUsuario}")]
        public ActionResult<bool> DeleteUsuario(int idUsuario)
        {
            var usuario = _context.Usuarios.FirstOrDefault(x => x.Id == idUsuario);
            if (usuario == null)
                return NotFound($"El usuario con Id:{idUsuario} no existe.");

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return NoContent();

        }
    }
}
