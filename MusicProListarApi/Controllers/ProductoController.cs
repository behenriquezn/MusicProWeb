using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicProListarApi.Models;

namespace MusicProListarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ModelContext _context;

        public ProductoController(ModelContext context)
        {
            _context = context;
        }

        // GET: api/Producto
        [HttpGet]
        public async Task<ActionResult <IEnumerable<Producto>>> GetProductos()
        {
        //    var listProd = (from p in _context.Productos
        //                    join pm in _context.Marcas on p.MarcaId equals pm.IdMarca
        //                    join psc in _context.SubCats on p.SubCatId equals psc.IdSub
        //                    join pc in _context.Categoria on p.CategoriaId equals pc.IdCategoria
        //                    select new ProductoInfo()
        //                    {
        //                        IdProd = p.IdProd,
        //                        Nombre = p.Nombre,
        //                        Modelo = p.Modelo,
        //                        Precio = p.Precio,
        //                        CantidadStock = p.CantidadStock,
        //                        SubCatId = p.SubCatId,
        //                        SubCat = p.SubCat,
        //                        MarcaId = p.MarcaId,
        //                        Marca = pm.Nombre,
        //                        CategoriaId = p.CategoriaId,
        //                        Categoria = pc.Categorias,
        //                        Imagen = p.Imagen
                                
        //                    }
        //                    );
        //    return await listProd.ToListAsync();
            var modelContext = _context.Productos.Include(p => p.Marca).Include(p => p.SubCat).Include(p => p.Categoria);
            return await modelContext.ToListAsync();
            // return await _context.Productos.ToListAsync();
        }

        // GET: api/Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(decimal id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // PUT: api/Producto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(decimal id, Producto producto)
        {
            if (id != producto.IdProd)
            {
                return BadRequest();
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Producto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductoExists(producto.IdProd))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProducto", new { id = producto.IdProd }, producto);
        }

        // DELETE: api/Producto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(decimal id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductoExists(decimal id)
        {
            return _context.Productos.Any(e => e.IdProd == id);
        }
    }
}
