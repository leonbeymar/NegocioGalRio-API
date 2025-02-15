using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NegocioGalRio_API.Contexts;
using NegocioGalRio_API.DTO;
using NegocioGalRio_API.Models;

namespace NegocioGalRio_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly NegocioContext _context;
        private readonly IMapper _mapper;

        public CategoriaController(NegocioContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet("GetAll")]
        public ActionResult<List<CategoriaDTO>> GetCategoriaAll()
        {
            var categorias = _context.Categorias;

            var categoriasDto = _mapper.Map<List<CategoriaDTO>>(categorias);
            
            return Ok(categoriasDto);
        }

        [HttpGet]
        public ActionResult<CategoriaDTO> GetCategoriaById([FromQuery]int idCategoria, [FromQuery]string? nombre)
        {
            var categoria = _context.Categorias.Where(x => x.Id == idCategoria || x.Nombre == nombre).ToList();
            if (categoria == null) 
                return NotFound($"No existe alguna categoria");

            var categoriaDto = _mapper.Map<List<CategoriaDTO>>(categoria);
            return Ok(categoriaDto);
        }
        [HttpPost]
        public ActionResult<Categoria> AddCategoria(CategoriaInsert categoriaInsert)
        {
            if (_context.Categorias.Any(x => x.Nombre == categoriaInsert.Nombre))
                return BadRequest("Ya existe una categoria con el nombre ingresado");

            var newCategoria = _mapper.Map<Categoria>(categoriaInsert);

            try
            {
                _context.Categorias.Add(newCategoria);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500,$"Error al guardar la categoria. Error: { ex.ToString()}");
            }
            return CreatedAtAction(nameof(GetCategoriaById), new { idCategoria = newCategoria.Id }, newCategoria);
        }

        [HttpDelete("id={idCategoria}")]
        public ActionResult DeleteCategoria(int idCategoria)
        {
            var categoria = _context.Categorias.FirstOrDefault(x => x.Id == idCategoria);
            if (categoria == null)
                return NotFound($"No existe la Categoria con Id {idCategoria}.");
            try
            {
                _context.Remove(categoria);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la categoria. Error: {ex.ToString()}");
            }
            return NoContent();
        }
    }
}
