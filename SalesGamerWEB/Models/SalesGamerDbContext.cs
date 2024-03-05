using Microsoft.EntityFrameworkCore;

namespace SalesGamerWEB.Models
{
    public class SalesGamerDbContext : DbContext
    {
        public SalesGamerDbContext(DbContextOptions<SalesGamerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        // Agrega otros DbSet según sea necesario para tu aplicación
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Producto"); // Nombre de la tabla en la base de datos

                entity.HasKey(e => e.Id); // Definir la clave primaria

                entity.Property(e => e.Nombre_producto)
                    .HasMaxLength(50); // Definir la longitud máxima de la columna Nombre_producto

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100); // Definir la longitud máxima de la columna Descripcion

                entity.Property(e => e.Precio)
                    .HasColumnType("int"); // Definir el tipo de dato y precisión para la columna Precio

                entity.Property(e => e.Cantidad)
                    .HasColumnType("int"); // Definir el tipo de dato para la columna Cantidad

                entity.Property(e => e.img)
                    .HasColumnType("varbinary(max)"); // Definir el tipo de dato para la columna img

                // Relaciones con otras entidades
                entity.HasOne(e => e.Distribuidor_id)
                    .WithMany()
                    .HasForeignKey(e => e.Distribuidor_id); // Relación con la entidad Distribuidor

                entity.HasOne(e => e.Oferta_id)
                    .WithMany()
                    .HasForeignKey(e => e.Oferta_id); // Relación con la entidad Oferta

                entity.HasOne(e => e.Categoria_id)
                    .WithMany()
                    .HasForeignKey(e => e.Categoria_id); // Relación con la entidad Categoria
            });
        }

    }
}
