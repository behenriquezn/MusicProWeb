using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicProWeb.Models
{
    public class Carrito
    {
        private readonly ModelContext _context;
        private Carrito(ModelContext context)
        {
            _context = context;
        }
        public string CarritoId { get; set; }

        public List<CarritoProducto> CarritoProductos { get; set; }

        public static Carrito GetCarrito(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            var context = services.GetService<ModelContext>();
            string carritoId = session.GetString("CarritoId") ?? Guid.NewGuid().ToString();

            session.SetString("CarritoId", carritoId);
            return new Carrito(context) { CarritoId = carritoId };
        }

        public void AgregarCarrito(Producto producto, int cantidad)
        {
            var carritoProducto =
                _context.CarritoProductos.SingleOrDefault(s => s.Producto.IdProd == producto.IdProd && s.CarritoId == CarritoId);

            if (carritoProducto == null)
            {
                carritoProducto = new CarritoProducto
                {
                    CarritoId = CarritoId,
                    Producto = producto,
                    Cantidad = 1
                };

                _context.CarritoProductos.Add(carritoProducto);
            }
            else
            {
                carritoProducto.Cantidad++;
            }
            _context.SaveChanges();
        }

        public int RemoverDelCarrito(Producto producto)
        {
            var carritoProducto =
                _context.CarritoProductos.SingleOrDefault(s => s.Producto.IdProd == producto.IdProd && s.CarritoId == CarritoId);

            var cantidadlocal = 0;

            if (carritoProducto != null)
            {
                if (carritoProducto.Cantidad > 1)
                {
                    carritoProducto.Cantidad--;
                    cantidadlocal = carritoProducto.Cantidad;

                }
                else
                {
                    _context.CarritoProductos.Remove(carritoProducto);
                }

            }
            _context.SaveChanges();
            return cantidadlocal;
        }

        public List<CarritoProducto> GetCarritoProductos()
        {
            return CarritoProductos ??
                (CarritoProductos = _context.CarritoProductos.Where(c => c.CarritoId == CarritoId).Include(s => s.Producto).ToList());
        }

        public void LimpiarCarrito()
        {
            var productosCarrito = _context.CarritoProductos.Where(carrito => carrito.CarritoId == CarritoId);
            _context.CarritoProductos.RemoveRange(productosCarrito);
            _context.SaveChanges();
        }

        public decimal GetCarritoTotal()
        {
            var total = _context.CarritoProductos.Where(c => c.CarritoId == CarritoId).Select(c => c.Producto.Precio * c.Cantidad).Sum();
            return total;
        }
    }


}
