using System;
using System.Collections.Generic;

#nullable disable

namespace MusicProListarApi.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            SubCats = new HashSet<SubCat>();
        }

        public decimal IdCategoria { get; set; }
        public string Categorias { get; set; }

        public virtual ICollection<SubCat> SubCats { get; set; }
    }
}
