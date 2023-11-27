namespace SalesGamerWEB.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public String Nombre_categoria { get; set; }

        public Categoria(int id, string nombre_cat)
        {
            Id = id;
            Nombre_categoria = nombre_cat;
        }

        public Categoria()
        {
        }
    }
}
