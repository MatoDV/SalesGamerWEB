using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SalesGamerWEB.Models
{
    public interface ICarritoService
    {
        void AgregarProducto(int productoId, int usuarioId, int cantidad);
        List<Carrito> ObtenerCarrito(int usuarioId);
        void RemoverProducto(int carritoId);
        void LimpiarCarrito(int usuarioId);
    }

    public class CarritoService : ICarritoService
    {
        private readonly SalesGamerDbContext _dbContext;

        public CarritoService(SalesGamerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AgregarProducto(int productoId, int usuarioId, int cantidad)
        {
            var producto = _dbContext.Productos.Find(productoId);
            if (producto == null) return;

            var carritoItem = _dbContext.Carritos
                .FirstOrDefault(c => c.ProductoId == productoId && c.UsuarioId == usuarioId);

            if (carritoItem != null)
            {
                // Si el producto ya está en el carrito, solo actualiza la cantidad
                carritoItem.Cantidad += cantidad;
                carritoItem.PrecioTotal = carritoItem.Cantidad * producto.Precio;
            }
            else
            {
                // Si el producto no está en el carrito, añádelo
                carritoItem = new Carrito
                {
                    nombre_producto = producto.Nombre_producto,
                    Cantidad = cantidad,
                    PrecioTotal = cantidad * producto.Precio,
                    UsuarioId = usuarioId,
                    ProductoId = productoId,
                };
                _dbContext.Carritos.Add(carritoItem);
            }

            _dbContext.SaveChanges();
        }

        public List<Carrito> ObtenerCarrito(int usuarioId)
        {
            // Obtener el carrito desde la base de datos
            return _dbContext.Carritos
                .Include(c => c.Producto)
                .Where(c => c.UsuarioId == usuarioId)
                .ToList();
        }

        public void RemoverProducto(int carritoId)
        {
            var carritoItem = _dbContext.Carritos.Find(carritoId);
            if (carritoItem != null)
            {
                _dbContext.Carritos.Remove(carritoItem);
                _dbContext.SaveChanges();
            }
        }

        public void LimpiarCarrito(int usuarioId)
        {
            var carritoItems = _dbContext.Carritos.Where(c => c.UsuarioId == usuarioId).ToList();
            _dbContext.Carritos.RemoveRange(carritoItems);
            _dbContext.SaveChanges();
        }
    }
}
