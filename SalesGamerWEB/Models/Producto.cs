namespace SalesGamerWEB.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public String Nombre_producto { get; set; }
        public String Descripcion { get; set; }
        public int Precio { get; set; }
        public int Cantidad { get; set; }
        public Distribuidor Distribuidor_id { get; set; }
        public Oferta Oferta_id { get; set; }



        public Producto(int id, string nombre, string desc, int precio, int cantidad, Distribuidor distribuidor_id, Oferta oferta_id)
        {
            Id = id;
            Nombre_producto = nombre;
            Descripcion = desc;
            Precio = precio;
            Cantidad = cantidad;
            Distribuidor_id = distribuidor_id;
            Oferta_id = oferta_id;
        }

        public Producto()
        {
        }
    }
}
