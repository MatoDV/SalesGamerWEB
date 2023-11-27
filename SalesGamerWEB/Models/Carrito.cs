namespace SalesGamerWEB.Models
{
    public class Carrito
    {
        public int Id { get; set; }
        public string nombre_producto { get; set; }
        public int cantidad { get; set; }
        public float precio_total { get; set; }
        public Usuario Usuario_id { get; set; }

        public Carrito(int id,string Nombre_prod,int Cantidad,float Precio_total,Usuario Usuario_ID)
        {
            this.Id = id;
            this.nombre_producto = Nombre_prod;
            this.cantidad = Cantidad;
            this.precio_total = Precio_total;
            this.Usuario_id = Usuario_ID;
        }
        public Carrito() { }
    }
}
