using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace SalesGamerWEB.Controllers
{
    public class DB_Connection
    {
        public IActionResult CheckConnection()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DB_Controller.connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Conexión establecida correctamente.");
                    return new OkResult(); // Devuelve un resultado HTTP 200 Ok
                }
            }
            catch (SqlException ex)
            {
                // Manejar la excepción de conexión a la base de datos
                // Por ejemplo, redirigir a una página de error o mostrar un mensaje
                Console.WriteLine("Error al establecer la conexión: " + ex.Message);
                return new StatusCodeResult(500); // Devuelve un resultado HTTP 500 Error interno del servidor
            }
        }
    }
}
