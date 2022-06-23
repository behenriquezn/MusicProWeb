using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
#nullable disable

namespace MusicProWeb.Models
{
    public partial class Producto
    {
        public decimal IdProd { get; set; }
        public string Nombre { get; set; }
        public string Modelo { get; set; }
        public decimal Precio { get; set; }
        public decimal CantidadStock { get; set; }


        public string Imagen { get; set; }
        public decimal SubCatId { get; set; }
        public decimal MarcaId { get; set; }
        public decimal CategoriaId { get; set; }


        [NotMapped]
        [DisplayName("Upload Image")]
        public IFormFile ImageFile { get; set; }
        public virtual Marca Marca { get; set; }
        public virtual SubCat SubCat { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}
