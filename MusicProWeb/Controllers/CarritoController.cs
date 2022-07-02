using Microsoft.AspNetCore.Mvc;
using MusicProWeb.Models;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicProWeb.Helpers;

namespace MusicProWeb.Controllers
{
    public class CarritoController : Controller
    {

        private ModelContext _context = new ModelContext();

        public CarritoController(ModelContext context)
        {
            _context = context;

        }
        [Route("")]
        [Route("index")]
        public ActionResult Index()
        {
            List<Item> carrito = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session,
                "carrito");
            ViewBag.carrito = carrito;
            ViewBag.countItems = carrito.Count;
            ViewBag.Total = carrito.Sum(it => it.Producto.Precio * it.Cantidad);
            return View();
        }
        [Route("comprar/{id}")]
        public IActionResult Comprar(decimal id)
        {
            var producto = _context.Productos.Find(id);
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "carrito") == null)
            {
                List<Item> carrito = new List<Item>();
                carrito.Add(new Item
                {
                    Producto = producto,
                    Cantidad = 1
                });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "carrito", carrito);
            }
            else
            {
                List<Item> carrito = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session,
                    "carrito");
                int index = exists(id, carrito);
                if (index == -1)
                {
                    carrito.Add(new Item
                    {
                        Producto = producto,
                        Cantidad = 1
                    });
                }
                else
                {
                    int newCantidad = carrito[index].Cantidad++;
                    carrito[index].Cantidad = newCantidad;
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "carrito", carrito);
            }
            return RedirectToAction("Index", "Carrito");

        }
        private int exists(decimal id, List<Item> carrito)
        {
            for (var i = 0; i < carrito.Count; i++)
            {
                if (carrito[i].Producto.IdProd == id)
                {
                    return i;
                }
            }
            return -1;
        }

    }
}