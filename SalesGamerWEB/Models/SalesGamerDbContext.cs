﻿using Microsoft.EntityFrameworkCore;

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
        public virtual DbSet<Distribuidor> Distribuidores { get; set; }
        public virtual DbSet<Oferta> Ofertas { get; set; }
        public virtual DbSet<Carrito> Carritos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carrito>(entity =>
            {
                entity.ToTable("Carrito");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.nombre_producto)
                    .HasMaxLength(50);

                entity.Property(e => e.Cantidad)
                    .HasColumnType("int");

                entity.Property(e => e.PrecioTotal)
                    .HasColumnType("float");

                entity.HasOne(e => e.Usuario)
                    .WithMany()
                    .HasForeignKey(e => e.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Oferta) // Nueva relación con Producto
                    .WithMany()
                    .HasForeignKey(e => e.OfertaId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Producto) // Nueva relación con Producto
                    .WithMany()
                    .HasForeignKey(e => e.ProductoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Producto");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nombre_producto)
                    .HasMaxLength(50);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100);

                entity.Property(e => e.Precio)
                    .HasColumnType("int");

                entity.Property(e => e.Cantidad)
                    .HasColumnType("int");

                // Relaciones con otras entidades
                entity.HasOne(e => e.Distribuidor)
                    .WithMany()
                    .HasForeignKey(e => e.Distribuidor_id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Oferta)
                    .WithMany()
                    .HasForeignKey(e => e.Oferta_id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Categoria)
                    .WithMany()
                    .HasForeignKey(e => e.Categoria_id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Distribuidor>(entity =>
            {
                entity.ToTable("Distribuidor");

                entity.HasKey(e => e.Id);

                // Configura las propiedades y relaciones aquí
            });

            modelBuilder.Entity<Oferta>(entity =>
            {
                entity.ToTable("Oferta");

                entity.HasKey(e => e.Id);

                // Configura las propiedades y relaciones aquí
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("Categoria");

                entity.HasKey(e => e.Id);

                // Configura las propiedades y relaciones aquí
            });
        }


    }
}
