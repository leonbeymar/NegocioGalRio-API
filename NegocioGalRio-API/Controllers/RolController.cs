using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NegocioGalRio_API.Contexts;
using NegocioGalRio_API.DTO;
using NegocioGalRio_API.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NegocioGalRio_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly NegocioContext _context;

        //private readonly RolService _rolService;
        public RolController(NegocioContext context)
        {
            //_rolService = rolService;
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<RolDto>> GetRoles()
        {
            var roles = _context.Roles
                .Select(r => new RolDto
                {
                    Id = r.Id,
                    Nombre = r.Nombre
                });

            return Ok(roles);
        }


        [HttpGet("{idRol}")]
        public ActionResult<RolDto> GetRolById(int idRol)
        {

            var rol = _context.Roles.FirstOrDefault(x => x.Id == idRol);

            if (rol == null)
                return NotFound($"El rol con Id: {idRol} no se encuentra en la lista");

            var rolDto = new RolDto()
            {
                Id = rol.Id,
                Nombre = rol.Nombre
            };

            return Ok(rolDto); // Usamos Ok() para devolver la respuesta HTTP 200 con los datos
        }


        [HttpPost]
        public ActionResult<RolDto> PostRol(RolInsertDto rolInsert)
        {

            var rol = _context.Roles.ToList().FirstOrDefault(x => x.Nombre == rolInsert.Nombre);
            if (rol != null)
                return BadRequest("Ya existe un rol con tal Nombre");

            var rolNuevo = new Rol()
            {
                Nombre = rolInsert.Nombre
            };

            _context.Roles.Add(rolNuevo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetRolById), new { idRol = rolNuevo.Id }, rolNuevo);
        }

        [HttpDelete("{idRol}")]
        public ActionResult<bool> DeleteRol(int idRol)
        {
            var rol = _context.Roles.FirstOrDefault(x => x.Id == idRol);

            if (rol == null)
                return NotFound($"El rol con Id: {idRol} no se encuentra en la lista");

            var usuarioRol = _context.Roles
                .Include(r => r.Usuarios)
                .Where(x => x.Id == idRol).FirstOrDefault();

            if (usuarioRol.Usuarios.Count != 0)
                return BadRequest("No se puede eliminar. El rol esta relacionado con algun usuario");

            _context.Roles.Remove(rol);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
