using Microsoft.AspNetCore.Mvc;
using MusicProWeb.Models;
using MusicProWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicProWeb.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ModelContext _context;
        private readonly Carrito _carrito;
    public CarritoController(ModelContext modelContext, Carrito carrito)
        {
            _context = modelContext;
            _carrito = carrito;
        }
        public ViewResult Index()
        {
            var productos = _carrito.GetCarritoProductos();
            _carrito.CarritoProductos = productos;

            var CarritoVM = new CarritoViewModel
            {
                Carrito = _carrito,
                CarritoTotal = _carrito.GetCarritoTotal()
            };
            return View(CarritoVM);
        }

        public RedirectToActionResult AgregarACarrito(int idprod)
        {
            var selectedProducto = _context.Productos.FirstOrDefault(p => p.IdProd == idprod);
            if (selectedProducto !=null)
            {
                _carrito.AgregarCarrito(selectedProducto, 1);

            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoverDeCarrito(int idprod)
        {
            var selectedProducto = _context.Productos.FirstOrDefault(p => p.IdProd == idprod);
            if (selectedProducto !=null)
            {
                _carrito.RemoverDelCarrito(selectedProducto);
            }
            return RedirectToAction("Index");
        }
    }
}
