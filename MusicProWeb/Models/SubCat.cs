using System;
using System.Collections.Generic;

#nullable disable

namespace MusicProWeb.Models
{
    public partial class SubCat
    {
        public SubCat()
        {
            Productos = new HashSet<Producto>();
        }

        public decimal IdSub { get; set; }
        public string Nombre { get; set; }
        public decimal CategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
