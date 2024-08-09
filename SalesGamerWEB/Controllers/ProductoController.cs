using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesGamerWEB.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace SalesGamerWEB.Controllers
{
    public class ProductoController : Controller
    {
        private readonly SalesGamerDbContext _context;

        public ProductoController(SalesGamerDbContext context)
        {
            _context = context;
        }

        // GET: /Producto
        public IActionResult Index(int? categoria, int pagina = 1)
        {
            int pageSize = 9; // Número de productos por página

            // Construir la consulta para productos
            var productosQuery = _context.Productos
                .Include(p => p.Distribuidor)
                .Include(p => p.Oferta)
                .Include(p => p.Categoria)
                .AsQueryable();

            // Filtrar por categoría si se proporciona
            if (categoria.HasValue)
            {
                productosQuery = productosQuery.Where(p => p.Categoria_id == categoria.Value);
            }

            // Calcular el total de productos
            int totalProductos = productosQuery.Count();

            // Realizar la paginación
            var productos = productosQuery
                .OrderBy(p => p.Id) // Ordenar por Id (o cualquier otro campo)
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Obtener la lista de categorías para el filtro en la vista
            ViewBag.Categorias = _context.Categorias.ToList();

            // Configurar las variables de vista para la paginación
            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = (int)Math.Ceiling((double)totalProductos / pageSize);
            ViewBag.Categoria = categoria;

            return View(productos);
        }

        // Acción para agregar productos al carrito
        [HttpPost]
        public IActionResult AgregarAlCarrito(int id)
        {
            var producto = _context.Productos.Find(id);

            if (producto != null)
            {
                List<Producto> carrito = HttpContext.Session.GetObjectFromJson<List<Producto>>("Carrito") ?? new List<Producto>();
                carrito.Add(producto);
                HttpContext.Session.SetObjectAsJson("Carrito", carrito);
            }

            return RedirectToAction(nameof(Index));
        }

        // Acción para mostrar el carrito
        public IActionResult Carrito()
        {
            List<Producto> carrito = HttpContext.Session.GetObjectFromJson<List<Producto>>("Carrito") ?? new List<Producto>();
            return View(carrito);
        }

        // Detalles de un producto
        public IActionResult Details(int id)
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

            return View(producto);
        }

        // Crear un nuevo producto
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categorias = _context.Categorias.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Productos.Add(producto);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categorias = _context.Categorias.ToList();
            return View(producto);
        }

        // Editar un producto
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var producto = _context.Productos
                .Include(p => p.Categoria) // Cargar las categorías relacionadas
                .FirstOrDefault(p => p.Id == id);

            if (producto == null)
            {
                return NotFound();
            }

            ViewBag.Categorias = _context.Categorias.ToList();
            return View(producto);
        }

        [HttpPost]
        public IActionResult Edit(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Productos.Update(producto);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categorias = _context.Categorias.ToList();
            return View(producto);
        }

        // Eliminar un producto
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }

    // Extensiones para la sesión
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
        }
    }
}
