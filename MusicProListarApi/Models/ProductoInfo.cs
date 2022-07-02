using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicProListarApi.Models
{
    public class ProductoInfo
    {
        public decimal IdProd { get; set; }
        public string Nombre { get; set; }
        public string Modelo { get; set; }
        public decimal Precio { get; set; }
        public decimal CantidadStock { get; set; }
        public decimal SubCatId { get; set; }
        public string SubCat { get; set; }
        public decimal MarcaId { get; set; }
        public string Marca { get; set; }
        public decimal CategoriaId { get; set; }
        public string Categoria { get; set; }
        public string Imagen { get; set; }

    }
}
