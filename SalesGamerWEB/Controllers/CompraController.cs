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

    }
}
