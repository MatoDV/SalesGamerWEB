namespace SalesGamerWEB.Models
{
    public class Producto_Categoria
    {
        public Producto id_Producto { get; set; }
        public Categoria id_Categoria { get; set; }
        public Producto_Categoria(Producto ID_producto,Categoria ID_categoria)
        {
            this.id_Producto = ID_producto;
            this.id_Categoria = ID_categoria;
        }
        public Producto_Categoria() { }
    }
}
