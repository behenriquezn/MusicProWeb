using MusicProWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicProWeb.ViewModels
{
    public class CarritoViewModel
    {
        public Carrito Carrito { get; set; }

        public decimal CarritoTotal { get; set; }
    }
}
