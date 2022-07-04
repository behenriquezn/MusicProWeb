namespace MusicProWeb.Models
{
    public class CarritoProducto
    {
        public int Id { get; set; }

        public Producto Producto { get; set; }

        public int Cantidad { get; set; }

        public string CarritoId { get; set; }
        public decimal ProductoIdProd { get; set; }
    }
}