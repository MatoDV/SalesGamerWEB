namespace SalesGamerWEB.Models
{
    public class Oferta
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo_oferta { get; set; }
        public DateOnly Fecha_inicio { get; set; }
        public DateOnly Fecha_final { get; set; }
        public string Condiciones { get; set; }

        public Oferta(int id, string nombre, string tipo_oferta, DateOnly fecha_inicio, DateOnly fecha_Final, string condiciones)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Tipo_oferta = tipo_oferta;
            this.Fecha_inicio = fecha_inicio;
            this.Fecha_final = fecha_Final;
            this.Condiciones = condiciones;
        }
        public Oferta()
        {
        }
    }
}
