namespace SalesGamerWEB.Models
{
    public class Producto_Carrito
    {
        public Producto id_Producto { get; set; }
        public Carrito id_Carrito { get; set; }
        public Producto_Carrito(Producto ID_producto,Carrito ID_carrito)
        {
        this.id_Producto = ID_producto;
        this.id_Carrito = ID_carrito;
        }
        public Producto_Carrito() { }
    }
}
