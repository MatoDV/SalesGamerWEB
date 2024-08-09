namespace SalesGamerWEB.Models
{
    public class Carrito
    {
        public int Id { get; set; }
        public string nombre_producto { get; set; }
        public int Cantidad { get; set; }
        public float PrecioTotal { get; set; }
        public int UsuarioId { get; set; } // Cambiado a int para la clave foránea
        public virtual Usuario Usuario { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int? OfertaId { get; set; }
        public Oferta Oferta { get; set; }



        public Carrito(int id, string nombreProducto, int cantidad, float precioTotal, int usuarioId, int productoId, int ofertaId)
        {
            Id = id;
            nombre_producto = nombreProducto;
            Cantidad = cantidad;
            PrecioTotal = precioTotal;
            UsuarioId = usuarioId;
            ProductoId = productoId;
            OfertaId = ofertaId;
        }

        public Carrito() { }
    }
}
