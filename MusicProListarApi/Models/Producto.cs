using System;
using System.Collections.Generic;

#nullable disable

namespace MusicProListarApi.Models
{
    public partial class Producto
    {
        public decimal IdProd { get; set; }
        public string Nombre { get; set; }
        public string Modelo { get; set; }
        public decimal Precio { get; set; }
        public decimal CantidadStock { get; set; }
        public decimal SubCatId { get; set; }
        public decimal MarcaId { get; set; }
        public decimal CategoriaId { get; set; }
        public string Imagen { get; set; }

        public virtual Marca Marca { get; set; }
        public virtual SubCat SubCat { get; set; }
    }
}
