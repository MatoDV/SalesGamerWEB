namespace SalesGamerWEB.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre_producto { get; set; }
        public string Descripcion { get; set; }
        public int Precio { get; set; }
        public int Cantidad { get; set; }
        public int Distribuidor_id { get; set; } // Clave foránea
        public Distribuidor Distribuidor { get; set; } // Navegación opcional
        public int Oferta_id { get; set; } // Clave foránea
        public Oferta Oferta { get; set; } // Navegación opcional
        public int Categoria_id { get; set; } // Clave foránea
        public Categoria Categoria { get; set; } // Navegación opcional

        public Producto(int id, string nombre, string desc, int precio, int cantidad, int distribuidorId, int ofertaId, int categoriaId)
        {
            Id = id;
            Nombre_producto = nombre;
            Descripcion = desc;
            Precio = precio;
            Cantidad = cantidad;
            Distribuidor_id = distribuidorId;
            Oferta_id = ofertaId;
            Categoria_id = categoriaId;
        }

        public Producto(int id, string nombre, string desc, int precio)
        {
            Id = id;
            Nombre_producto = nombre;
            Descripcion = desc;
            Precio = precio;
        }

        public Producto() { }
    }
}
