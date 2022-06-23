using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicProWeb.Models;

namespace MusicProWeb.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductoController(ModelContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Producto
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Productos.Include(p => p.Marca).Include(p => p.SubCat).Include(p => p.Categoria);
            return View(await modelContext.ToListAsync());
        }

        // GET: Producto/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Marca)
                .Include(p => p.SubCat)
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.IdProd == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Producto/Create
        public IActionResult Create()
        {
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "IdMarca", "Nombre");
            ViewData["SubCatId"] = new SelectList(_context.SubCats, "IdSub", "Nombre");
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "IdCategoria", "Categorias");
            return View();
        }

        // POST: Producto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProd,Nombre,Modelo,Precio,CantidadStock,ImageFile,SubCatId,MarcaId,CategoriaId")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                //Guardamos la imagen del producto a wwwroot/image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(producto.ImageFile.FileName);
                string extension = Path.GetExtension(producto.ImageFile.FileName);
                producto.Imagen= fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create)) 
                {
                    await producto.ImageFile.CopyToAsync(fileStream);
                }



                    _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "IdMarca", "Nombre", producto.MarcaId);
            ViewData["SubCatId"] = new SelectList(_context.SubCats, "IdSub", "Nombre", producto.SubCatId);
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "IdCategoria", "Categorias", producto.CategoriaId);

            return View(producto);
        }

        // GET: Producto/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Marca)
                .Include(p => p.SubCat)
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.IdProd == id);
          // var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "IdMarca", "Nombre", producto.MarcaId);
            ViewData["SubCatId"] = new SelectList(_context.SubCats, "IdSub", "Nombre", producto.SubCatId);
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "IdCategoria", "Categorias", producto.CategoriaId);
            return View(producto);
        }

        // POST: Producto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("IdProd,Nombre,Modelo,Precio,CantidadStock,Imagen,SubCatId,MarcaId,CategoriaId")] Producto producto)
        {
            if (id != producto.IdProd)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.IdProd))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "IdMarca", "Nombre", producto.MarcaId);
            ViewData["SubCatId"] = new SelectList(_context.SubCats, "IdSub", "Nombre", producto.SubCatId);
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "IdCategoria", "Categorias", producto.CategoriaId);
            return View(producto);
        }

        // GET: Producto/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Marca)
                .Include(p => p.SubCat)
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.IdProd == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var producto = await _context.Productos
                .Include(p => p.Marca)
                .Include(p => p.SubCat)
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.IdProd == id);
            //Borrar la imagen de wwwroot/image
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image", producto.Imagen);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Exists(imagePath);
            //Borrar el registro de la imagen
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(decimal id)
        {
            return _context.Productos.Any(e => e.IdProd == id);
        }
    }
}
