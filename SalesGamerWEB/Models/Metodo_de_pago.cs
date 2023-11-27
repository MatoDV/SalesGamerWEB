namespace SalesGamerWEB.Models
{
    public class Metodo_de_pago
    {
        public int Id { get; set; }
        public string tipo_tarjeta {  get; set; }
        public int numero_tarjeta { get; set; }
        public DateOnly fecha_vencimiento { get; set; }
        public string nombre_titular { get; set; }
        public Producto id_Producto { get; set; }
        public Metodo_de_pago(int id,string Tipo_tarjeta,int Numero_tarjeta,DateOnly Fecha_vencimiento,string Nombre_titular,Producto ID_producto) 
        {
            this.Id = id;
            this.tipo_tarjeta = Tipo_tarjeta;
            this.numero_tarjeta=Numero_tarjeta;
            this.fecha_vencimiento = Fecha_vencimiento;
            this.nombre_titular = Nombre_titular;
            this.id_Producto = ID_producto;
        }
        public Metodo_de_pago() { }
    }
}
