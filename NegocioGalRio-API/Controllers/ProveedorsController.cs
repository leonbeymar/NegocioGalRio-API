using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NegocioGalRio_API.Contexts;
using NegocioGalRio_API.DTO;
using NegocioGalRio_API.Models;

namespace NegocioGalRio_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorsController : ControllerBase
    {
        private readonly NegocioContext _context;
        private readonly IMapper _mapper;

        public ProveedorsController(NegocioContext context, IMapper mapper){
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Proveedor>> GetProveedorsAll()
        {
            return _context.Proveedors.ToList();
        }

        [HttpGet]
        public ActionResult<List<Proveedor>> GetProveedor([FromQuery]int idProveedor, [FromQuery]string? nombre)
        {
            var proveedor = _context.Proveedors.Where(x => x.Id == idProveedor || x.Nombre == nombre).ToList();

            if (proveedor == null)
                return NotFound("No se encontro el proveedor.");

            return Ok(proveedor);
        }

        [HttpPost]
        public ActionResult<Proveedor> PostProveedor(ProveedorInsert proveedorInsert)
        {
            var proveedor = _mapper.Map<Proveedor>(proveedorInsert);
            _context.Proveedors.Add(proveedor);
             _context.SaveChanges();

            return CreatedAtAction(nameof(GetProveedor), new { idProveedor = proveedor.Id }, proveedor);
        }

        [HttpPut("{idProveedor}")]
        public ActionResult PutProveedor(int idProveedor, ProveedorInsert proveedorUpd)
        {
            var proveedor = _context.Proveedors.FirstOrDefault(x => x.Id == idProveedor);

            if (proveedor == null)
                return NotFound($"No se encontro el Proveedor con Id {idProveedor}");

            try
            {
                _mapper.Map(proveedorUpd, proveedor);
                _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrio un error al momento de actualizar el proveedor, Error: {ex.Message}");
            }

            return NoContent();
        }


        [HttpDelete("{idProveedor}")]
        public ActionResult DeleteProveedor(int idProveedor)
        {
            var proveedor = _context.Proveedors.FirstOrDefault(x => x.Id == idProveedor);

            if (proveedor == null)
                return NotFound($"No se encontro el Proveedor con Id {idProveedor}");

            _context.Proveedors.Remove(proveedor);
            _context.SaveChangesAsync();

            try
            {
                _context.Proveedors.Remove(proveedor);
                _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrio un error al momento de eliminar el proveedor, Error: {ex.Message}");
            }

            return NoContent();
        }
    }
}
