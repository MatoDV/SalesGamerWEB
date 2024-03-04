using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;

namespace SalesGamerWEB.Controllers
{
    public class DB_Connection : Controller
    {
        public IActionResult Index()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DB_Controller.connectionString))
                {
                    connection.Open();
                    return View();
                }
            }
            catch (SqlException ex)
            {
                // Manejar la excepción de conexión a la base de datos
                // Por ejemplo, redirigir a una página de error o mostrar un mensaje
                return RedirectToAction("Error");
            }
        }
    }
}
