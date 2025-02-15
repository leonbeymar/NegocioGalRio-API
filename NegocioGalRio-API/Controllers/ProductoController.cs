using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NegocioGalRio_API.Contexts;
using NegocioGalRio_API.DTO;
using NegocioGalRio_API.Models;

namespace NegocioGalRio_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly NegocioContext _context;
        private readonly IMapper _mapper;
        public ProductoController(NegocioContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<ProductoDTO>> GetAllProductos()
        {
            var productos = _context.Productos;

            var productosDto = _mapper.Map<List<ProductoDTO>>(productos);

            return Ok(productosDto);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductoDTO>> GetProductos([FromQuery] int? idProducto = null, [FromQuery] string? Nombre = null, [FromQuery] string? Marca = null, [FromQuery] int? idCategoria = null)
        {
            var productos = _context.Productos.Where(
                    x => x.Id == idProducto || x.Nombre == Nombre || x.Marca == Marca || x.IdCategoria == idCategoria
                ).ToList();

            var productosDto = _mapper.Map<List<ProductoDTO>>(productos);

            return Ok(productosDto);
        }

        [HttpPost]
        public ActionResult<ProductoDTO> AddProducto(ProductoInsert newProducto)
        {
            var producto = _mapper.Map<Producto>(newProducto);

            if (newProducto.PrecioVenta <= 0)
                return BadRequest("Ingrese un PrecioVenta razonable.");

            //if (newProducto.PrecioCompraUnidad <= 0)
            //    return BadRequest("Ingrese un PrecioCompra razonable.");

            //if (newProducto.PrecioCompraUnidad > newProducto.PrecioVenta)
            //    return BadRequest("El precio venta no puede ser menor que el precio compra.");

            if (newProducto.Stock < 0)
                return BadRequest("El el stock no puede ser negativo.");

            if (_context.Productos.Any(x => x.Nombre == newProducto.Nombre && x.Marca == newProducto.Marca))
                return BadRequest("Ya existe un producto con el mismo Nombre y Marca.");

            var categoria = _context.Categorias.FirstOrDefault(x => x.Id == producto.IdCategoria);
            if (categoria == null)
                return NotFound($"Categoría con ID {producto.IdCategoria} no encontrada.");

            producto.Categoria = categoria;

            try
            {
                _context.Productos.Add(producto);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar el producto. Mensaje: {ex.Message}");
            }
            
            var newProduct = _mapper.Map<ProductoDTO>(producto);

            return CreatedAtAction(nameof(GetProductos), new { idProducto = newProduct.Id }, newProduct);
        }


        [HttpPut("{idProducto}")]
        public ActionResult<ProductoDTO> UpdateProducto(int idProducto, [FromBody] ProductoUpdate productoUpd)
        {
            var producto = _context.Productos.FirstOrDefault(x => x.Id == idProducto);
            if (producto == null)
                return NotFound($"Producto con ID {idProducto} no encontrado.");

            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == productoUpd.IdCategoria);
            if (categoria == null)
                return NotFound($"Categoría con ID {productoUpd.IdCategoria} no encontrada.");

            if (productoUpd.PrecioVenta <= 0)
                return BadRequest("Ingrese un PrecioVenta razonable.");

            //if (productoUpd.PrecioCompraUnidad <= 0)
            //    return BadRequest("Ingrese un PrecioCompra razonable.");

            //if (productoUpd.PrecioCompraUnidad > productoUpd.PrecioVenta)
            //    return BadRequest("El precio venta no puede ser menor que el precio compra.");

            // Mapeo automático con AutoMapper
            _mapper.Map(productoUpd, producto);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el producto. Mensaje:{ex.Message}");
            }

            return NoContent(); // 204 No Content
        }

        [HttpDelete("{idProducto}")]
        public IActionResult DeleteProducto(int idProducto)
        {
            var producto = _context.Productos.Include(p => p.Categoria).FirstOrDefault(x => x.Id == idProducto);
            if (producto == null)
                return NotFound($"Producto con ID {idProducto} no encontrado.");

            try
            {
                _context.Remove(producto);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el producto, Mensaje:{ex.Message}");
            }

            return NoContent();
        }
    }

}