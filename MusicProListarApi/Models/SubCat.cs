using System;
using System.Collections.Generic;

#nullable disable

namespace MusicProListarApi.Models
{
    public partial class SubCat
    {
        public decimal IdSub { get; set; }
        public string Nombre { get; set; }
        public decimal CategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}
