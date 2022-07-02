using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicProWeb.Models
{
    public class Item
    {
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }

    }


}
