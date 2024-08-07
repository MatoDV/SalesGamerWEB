using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesGamerWEB.Models;
using System.Linq;

namespace SalesGamerWEB.Controllers
{
    public class CompraController : Controller
    {
        private readonly SalesGamerDbContext _context;

        public CompraController(SalesGamerDbContext context)
        {
            _context = context;
        }

        // GET: /Compra
        public IActionResult Index(int id)
        {
            var producto = _context.Productos
                .Include(p => p.Distribuidor)
                .Include(p => p.Oferta)
                .Include(p => p.Categoria)
                .FirstOrDefault(p => p.Id == id);

            if (producto == null)
            {
                return NotFound();
            }

            // Pasar el producto a la vista
            return View(producto);
        }

        [HttpPost]
        public IActionResult AñadirAlCarrito(int productId, int productQuanity)
        {
            // Lógica para añadir el producto al carrito
            // Aquí deberías agregar el producto al carrito del usuario

            // Por ejemplo, podrías añadir la lógica para guardar el producto en una tabla de carrito en la base de datos

            // Redirigir de vuelta a la página de detalles del producto o a otra página
            return RedirectToAction("Carrito","Index", new { id = productId });
        }

    }
}
