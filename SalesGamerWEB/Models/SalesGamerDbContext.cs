using Microsoft.EntityFrameworkCore;

namespace SalesGamerWEB.Models
{
    public class SalesGamerDbContext : DbContext
    {
        public SalesGamerDbContext(DbContextOptions<SalesGamerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        // Agrega otros DbSet según sea necesario para tu aplicación
    }
}
