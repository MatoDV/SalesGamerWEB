using Microsoft.AspNetCore.Routing.Constraints;

namespace SalesGamerWEB.Models
{
    public class Pedido
    {
        public int Id;
        public DateOnly fecha;
        public string estado_pedido;
        public int cantidad;
        public int precio_unitario;
        public float subtotal;
        public Usuario Usuario_id;
        public Pedido(int id,DateOnly Fecha,string Estado_pedido,int Cantidad,int Precio_unitario,float Subtotal,Usuario usuario_id) 
        { 
            this.Id = id;
            this.fecha = Fecha;
            this.estado_pedido = Estado_pedido;
            this.cantidad = Cantidad;
            this.precio_unitario = Precio_unitario;
            this.subtotal = Subtotal;
            this.Usuario_id = usuario_id;
        }
        public Pedido() 
        {
        }
    }
}
