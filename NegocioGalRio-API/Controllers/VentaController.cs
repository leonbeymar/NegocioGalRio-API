using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NegocioGalRio_API.Contexts;
using NegocioGalRio_API.DTO;
using NegocioGalRio_API.Models;
using NuGet.Packaging.Signing;

namespace NegocioGalRio_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : ControllerBase
    {

        private readonly NegocioContext _context;
        private readonly IMapper _mapper;

        public VentaController(NegocioContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<VentaDTO>> GetVentas()
        {
            var ventas = _context.Ventas.Include(dv => dv.DetalleVentas).ThenInclude(dv => dv.Producto);
            var ventasdto = _mapper.Map<List<VentaDTO>>(ventas);
            return Ok(ventasdto);
        }

        [HttpGet("{idVenta}")]
        public ActionResult<List<VentaInsert>> GetVentaById(int idVenta)
        {
            var ventas = _context.Ventas.Include(dv => dv.DetalleVentas).FirstOrDefault(x => x.Id == idVenta);

            if (ventas == null)
                return NotFound($"No se encontro la venta con el id: {idVenta}");
            return Ok(ventas);
        }

        [HttpPost]
        public ActionResult<bool> AddVenta(VentaInsert ventaInsert)
        {

            var comentario = string.Empty;
            foreach (var dtVenta in ventaInsert.DetalleVentas)
            {
                var producto = _context.Productos.FirstOrDefault(x => x.Id == dtVenta.IdProducto);
                if (producto == null)
                {
                    comentario += $"No se encontro el IdProducto:{dtVenta.IdProducto} \n";
                }
                else
                {
                    if (dtVenta.Cantidad < 0)
                        return BadRequest("La cantidad de productos debe ser mayor a cero");
                    if (dtVenta.PrecioUnitario < 0)
                        return BadRequest("El precio de compra debe ser mayor a cero");
                }
            }

            if (!string.IsNullOrEmpty(comentario))
                return NotFound(comentario);


            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    decimal totalVenta = ventaInsert.DetalleVentas.Sum(dtVenta => (dtVenta.PrecioUnitario * dtVenta.Cantidad));

                    // Mapear Venta sin los DetalleVentas
                    var venta = _mapper.Map<Venta>(ventaInsert);
                    venta.Total = totalVenta;
                    venta.DetalleVentas = new List<DetalleVenta>(); // Asegurar que la lista esté vacía

                    _context.Ventas.Add(venta);
                    _context.SaveChanges(); // Guarda solo la ventas para obtener el Id

                    foreach (var dtVenta in ventaInsert.DetalleVentas)
                    {
                        var detalleVenta = _mapper.Map<DetalleVenta>(dtVenta);
                        detalleVenta.Subtotal = dtVenta.PrecioUnitario * dtVenta.Cantidad;
                        detalleVenta.IdVenta = venta.Id;

                        _context.DetalleVentas.Add(detalleVenta);

                        // Actualizar el producto
                        var producto = _context.Productos.FirstOrDefault(x => x.Id == detalleVenta.IdProducto);
                        producto.PrecioCompraUnidad = detalleVenta.PrecioUnitario;
                        producto.Stock -= detalleVenta.Cantidad; // Bajar el stock, no sobrescribir
                        _context.Productos.Update(producto);
                    }

                    _context.SaveChanges(); // Guarda todos los DetalleVenta y Productos
                    transaction.Commit();
                    var ventaDTO = _mapper.Map<VentaDTO>(venta);
                    return CreatedAtAction(nameof(GetVentaById), new { idVenta = venta.Id }, ventaDTO);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(500, $"Error al guardar la venta: {ex.Message}");
                }

            }
        }

    }
}
