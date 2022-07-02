using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MusicProWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MusicProWeb.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;
        public HomeController(ModelContext context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
        }
        //public async Task<IActionResult> Index()
        //public IActionResult Index()
        //{
        //    //var httpClient = new HttpClient();
        //    //var json = await httpClient.GetStringAsync("https://localhost:44389/api/producto");
        //    //List<Producto> listaproductos = JsonConvert.DeserializeObject<List<Producto>>(json);

        //    //return View(listaproductos);

        //    return View(_context.Productos.);
        //}

        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Productos.Include(p => p.Marca).Include(p => p.SubCat).Include(p => p.Categoria);
            return View(await modelContext.ToListAsync());
        }
        public IActionResult Privacy()
        {
            return View();
        }


        //public JsonResult ListarProductos() {

        //    List<Producto> oLista = new List<Producto>();

        //    oLista = new 
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //GET Productos detalle
        public async Task<IActionResult> Detalles(decimal? id)
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
//        //POST Productos detalle
//        [HttpPost]
//        [ActionName("Detalles")]
//        public async Task<IActionResult> ProductoDetalles(decimal? id)
//        {
//            List<Producto> productos = new List<Producto>();
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var producto = await _context.Productos
//                //.Include(p => p.Marca)
//                //.Include(p => p.SubCat)
//                //.Include(p => p.Categoria)
//                .FirstOrDefaultAsync(m => m.IdProd == id);
//            if (producto == null)
//            {
//                return NotFound();
//            }
//            productos = HttpContext.Session.Get<List<Producto>>("productos");
//            if (productos == null)
//            {
//                productos = new List<Producto>();
//            }
//            productos.Add(producto);
//            HttpContext.Session.Set("productos", productos);

//            return RedirectToAction(nameof(Index));
//        }
//        //GET Remove action methdo
//        [ActionName("Remover")]
//        public IActionResult RemoverCarrito(int? id)
//        {
//            List<Producto> productos = HttpContext.Session.Get<List<Producto>>("productos");
//            if (productos != null)
//            {
//                var producto = productos
//                   //.Include(p => p.Marca)
//                   //.Include(p => p.SubCat)
//                   //.Include(p => p.Categoria)
//                   .FirstOrDefault(m => m.IdProd == id);
//                if (producto != null)
//                {
//                    productos.Remove(producto);
//                    HttpContext.Session.Set("productos", productos);
//                }
//            }
//            return RedirectToAction(nameof(Index));
//        }

//        [HttpPost]

//        public IActionResult Remover(int? id)
//        {
//            List<Producto> productos = HttpContext.Session.Get<List<Producto>>("productos");
//            if (productos != null)
//            {
//                var producto =  productos
//                   //.Include(p => p.Marca)
//                   //.Include(p => p.SubCat)
//                   //.Include(p => p.Categoria)
//                   .FirstOrDefault(m => m.IdProd == id);
//                if (producto != null)
//                {
//                    productos.Remove(producto);
//                    HttpContext.Session.Set("productos", productos);
//                }
//            }
//            return RedirectToAction(nameof(Index));
//        }
//        //GET DEL CARRITO DE COMPRAS
//        public IActionResult Carrito()
//        {
//            List<Producto> productos = HttpContext.Session.Get<List<Producto>>("productos");
//            if (productos == null)
//            {
//                productos = new List<Producto>();
//            }
//            return View(productos);
//        }

    }
}
