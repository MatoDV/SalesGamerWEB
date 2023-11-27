namespace SalesGamerWEB.Models
{
    public class Distribuidor
    {
        public int Id { get; set; }
        public string Nombre_empresa { get; set; }
        public string stock {  get; set; }
        public Distribuidor(int id,string nombre_empresa,string Stock) 
        {
            this.Id = id;
            this.Nombre_empresa = nombre_empresa;   
            this.stock = Stock;
        }
        public Distribuidor() { }
    }
}
