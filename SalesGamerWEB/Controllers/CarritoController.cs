using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesGamerWEB.Models;

namespace SalesGamerWEB.Controllers
{
    public class CarritoController : Controller
    {
        private readonly SalesGamerDbContext _context;

        public CarritoController(SalesGamerDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

    }
}
