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
    public class CompraController : ControllerBase
    {
        private readonly NegocioContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public CompraController(NegocioContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<List<CompraDTO>> GetCompra() {
            var compra = _context.Compras
                                .Include(c => c.DetalleCompras).ToList();

            var compraDTO = _mapper.Map<List<CompraDTO>>(compra);
            return Ok(compraDTO);
        }

        [HttpGet("{idCompra}")]
        public ActionResult<CompraDTO> GetCompraById(int idCompra)
        {
            var compra = _context.Compras
                                .Include(c => c.DetalleCompras).FirstOrDefault(x => x.Id == idCompra);

            if (compra == null) 
                return NotFound($"No se encontro la compra con el id: {idCompra}");

            var compraDTO = _mapper.Map<CompraDTO>(compra);
            return Ok(compraDTO);
        }


        [HttpPost]
        public ActionResult<bool> AddCompra(CompraInsert compraInsert) {

            if(!_context.Proveedors.Any(x => x.Id == compraInsert.IdProveedor))
                return NotFound($"No se encontro el IdProveedor: {compraInsert.IdProveedor}");

            var comentario = string.Empty;
            foreach (var dtCompra in compraInsert.DetalleCompras)
            {
                var producto = _context.Productos.FirstOrDefault(x => x.Id == dtCompra.IdProducto);
                if (producto == null) {
                    comentario += $"No se encontro el IdProducto:{dtCompra.IdProducto} \n";
                }
                else {
                    if (dtCompra.Cantidad < 0)
                        return BadRequest("La cantidad de productos debe ser mayor a cero");
                    if (dtCompra.PrecioCompraUnidad < 0)
                        return BadRequest("El precio de compra debe ser mayor a cero");
                }
            }

            if(!string.IsNullOrEmpty(comentario))
                return NotFound(comentario);


            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    decimal totalCompra = compraInsert.DetalleCompras.Sum(dtCompra => (dtCompra.PrecioCompraUnidad * dtCompra.Cantidad));

                    // Mapear Compra sin los DetalleCompras
                    var compra = _mapper.Map<Compra>(compraInsert);
                    compra.Total = totalCompra;
                    compra.DetalleCompras = new List<DetalleCompra>(); // Asegurar que la lista esté vacía

                    _context.Compras.Add(compra);
                    _context.SaveChanges(); // Guarda solo la Compra para obtener el Id

                    foreach (var dtCompra in compraInsert.DetalleCompras)
                    {
                        var detalleCompra = _mapper.Map<DetalleCompra>(dtCompra);
                        detalleCompra.Subtotal = dtCompra.PrecioCompraUnidad * dtCompra.Cantidad;
                        detalleCompra.IdCompra = compra.Id;

                        _context.DetalleCompras.Add(detalleCompra);

                        // Actualizar el producto
                        var producto = _context.Productos.FirstOrDefault(x => x.Id == detalleCompra.IdProducto);
                        producto.PrecioCompraUnidad = detalleCompra.PrecioCompraUnidad;
                        producto.Stock += detalleCompra.Cantidad; // Incrementar el stock, no sobrescribir

                        string porcentajeStr = _configuration["PorcentajeVenta"];
                        if (!decimal.TryParse(porcentajeStr, out decimal porcentaje))
                            return StatusCode(500,"No se pudo leer el porcentaje de nuestra configuracion");
                        producto.PrecioVenta = detalleCompra.PrecioCompraUnidad * (1 + (porcentaje * 0.01M));
                        _context.Productos.Update(producto);
                    }

                    _context.SaveChanges(); // Guarda todos los DetalleCompra y Productos
                    transaction.Commit();
                    var compraDTO = _mapper.Map<CompraDTO>(compra);
                    return CreatedAtAction(nameof(GetCompraById), new { idCompra = compra.Id}, compraDTO);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(500, $"Error al guardar la compra: {ex.Message}");
                }

            }
        }

    }
}
