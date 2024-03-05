using System.ComponentModel.DataAnnotations;

namespace SalesGamerWEB.Models.ViewModels
{
    public class ProductoViewModel
    {
        public int Id { get; set; }
        public String Nombre_producto { get; set; }
        public String Descripcion { get; set; }
        public int Precio { get; set; }
        public int Cantidad { get; set; }
        public List<Distribuidor> Distribuidor_id { get; set; }
        public List<Oferta> Oferta_id { get; set; }
        public byte[] img { get; set; }
        public List<Categoria> Categoria_id { get; set; }

    }
}
