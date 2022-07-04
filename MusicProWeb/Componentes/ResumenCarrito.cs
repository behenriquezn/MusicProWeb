using Microsoft.AspNetCore.Mvc;
using MusicProWeb.Models;
using MusicProWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicProWeb.Componentes
{
    public class ResumenCarrito:ViewComponent
    {
        private readonly Carrito _carrito;
        public ResumenCarrito(Carrito carrito)
        {
            _carrito = carrito;
        }

        public IViewComponentResult Invoke()
        {
            var productos = _carrito.GetCarritoProductos();
            _carrito.CarritoProductos = productos;

            var carritoViewModel = new CarritoViewModel
            {
                Carrito = _carrito,
                CarritoTotal = _carrito.GetCarritoTotal()
            };

            return View(carritoViewModel);
        }
    }
}
