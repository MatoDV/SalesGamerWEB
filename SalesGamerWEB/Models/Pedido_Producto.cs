namespace SalesGamerWEB.Models
{
    public class Pedido_Producto
    {
        public Producto id_Producto { get; set; }
        public Pedido id_Pedido { get; set; }
        public Pedido_Producto(Producto ID_producto,Pedido ID_pedido) 
        {
        this.id_Producto = ID_producto;
        this.id_Pedido = ID_pedido;
        }
        public Pedido_Producto() { }
    }
}
