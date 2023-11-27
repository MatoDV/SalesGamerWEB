using Microsoft.AspNetCore.Mvc;
using System;


namespace SalesGamerWEB.Controllers
{
    public class DB_Connection : Controller
    {
        public IActionResult Index()
        {
            // Inicialización de la conexión a la base de datos
            DB_Controller.Initialize();

            if (validateConnection())
            {
                // Si la conexión es exitosa, puedes continuar con la acción requerida
                // Por ejemplo, devolver la vista Index
                return View();
            }
            else
            {
                // Si hay un error en la conexión, puedes manejarlo adecuadamente
                // Por ejemplo, redirigir a una página de error o mostrar un mensaje
                return RedirectToAction("Error");
            }
        }

        public bool validateConnection()
        {
            try
            {
                DB_Controller.connection.Open();
                DB_Controller.connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                // Manejo de errores de conexión a la base de datos
                // Por ejemplo, registrar el error o devolver false
                // Puedes adaptar este bloque para manejar el error según tus necesidades
                return false;
            }
        }
    }
}
