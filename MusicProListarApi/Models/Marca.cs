using System;
using System.Collections.Generic;

#nullable disable

namespace MusicProListarApi.Models
{
    public partial class Marca
    {
        public Marca()
        {
            Productos = new HashSet<Producto>();
        }

        public decimal IdMarca { get; set; }
        public string Nombre { get; set; }
        public string Proveedor { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
