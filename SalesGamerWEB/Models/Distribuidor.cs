namespace SalesGamerWEB.Models
{
    public class Distribuidor
    {
        public int Id { get; set; }
        public string Nombre_empresa { get; set; }
        public int stock {  get; set; }
        public Distribuidor(int id,string nombre_empresa,int Stock) 
        {
            this.Id = id;
            this.Nombre_empresa = nombre_empresa;   
            this.stock = Stock;
        }
        public Distribuidor() { }
    }
}
