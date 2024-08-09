using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesGamerWEB.Models;
using System.Linq;
using System.Security.Claims;

namespace SalesGamerWEB.Controllers
{
    [Authorize]
    public class CarritoController : Controller
    {
        private readonly ICarritoService _carritoService;

        public CarritoController(ICarritoService carritoService)
        {
            _carritoService = carritoService;
        }

        [HttpPost]
        public IActionResult AgregarAlCarrito(int productoId, int cantidad)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); // Obtén el ID del usuario autenticado
            _carritoService.AgregarProducto(productoId, usuarioId,cantidad);
            return RedirectToAction("Index", "Carrito");
        }

        public IActionResult Index()
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); // Obtén el ID del usuario autenticado
            var carrito = _carritoService.ObtenerCarrito(usuarioId);
            return View(carrito);
        }
    }
}
