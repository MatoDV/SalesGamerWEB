namespace SalesGamerWEB.Models
{
    public class Carrito
    {
        public int Id { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public float PrecioTotal { get; set; }
        public int UsuarioId { get; set; } // Cambiado a int para la clave foránea
        public virtual Usuario Usuario { get; set; }

        public Carrito(int id, string nombreProducto, int cantidad, float precioTotal, int usuarioId)
        {
            Id = id;
            NombreProducto = nombreProducto;
            Cantidad = cantidad;
            PrecioTotal = precioTotal;
            UsuarioId = usuarioId;
        }

        public Carrito() { }
    }
}
