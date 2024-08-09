using Microsoft.AspNetCore.Mvc;
using SalesGamerWEB.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace SalesGamerWEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Producto> productos = obtenerProductos();
            return View(productos);
        }

        public static List<Producto> obtenerProductos()
        {
            List<Producto> list = new List<Producto>();
            string query = "SELECT * FROM dbo.Producto;";

            using (SqlCommand cmd = new SqlCommand(query, DB_Controller.Connection))
            {
                try
                {
                    if (DB_Controller.Connection.State != System.Data.ConnectionState.Open)
                    {
                        DB_Controller.Connection.Open();
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Producto producto = new Producto(
                                id: reader.GetInt32(reader.GetOrdinal("Id")),
                                nombre: reader.GetString(reader.GetOrdinal("Nombre_producto")),
                                desc: reader.GetString(reader.GetOrdinal("Descripcion")),
                                precio: reader.GetInt32(reader.GetOrdinal("Precio"))
                            );

                            list.Add(producto);
                            Trace.WriteLine("Producto encontrado, nombre: " + producto.Nombre_producto);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception details here
                    throw new Exception("Hay un error en la query: " + ex.Message);
                }
                finally
                {
                    if (DB_Controller.Connection.State == System.Data.ConnectionState.Open)
                    {
                        DB_Controller.Connection.Close();
                    }
                }
            }

            return list;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
