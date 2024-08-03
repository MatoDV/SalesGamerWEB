using Microsoft.AspNetCore.Mvc;
using SalesGamerWEB.Models;

namespace SalesGamerWEB.Controllers
{
    public class CompraController : Controller
    {
        private readonly SalesGamerDbContext _context;

        public CompraController(SalesGamerDbContext context)
        {
            _context = context;
        }

        // GET: /Compra/Details/5
        public IActionResult Details(int id)
        {
            var producto = _context.Productos.FirstOrDefault(p => p.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
