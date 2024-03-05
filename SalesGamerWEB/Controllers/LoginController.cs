using Microsoft.AspNetCore.Mvc;
using SalesGamerWEB.Models;

namespace SalesGamerWEB.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Authenticate(string usr, string pass)
        {
            try
            {
                // Llamada al método de autenticación en el controlador de Usuario
                bool isAuthenticated = Usuario_Controller.autenticar(usr, pass, hasheado: false); // Suponiendo que la contraseña no está hasheada

                if (isAuthenticated)
                {
                    // Autenticación exitosa, redirige a la página principal u otra página de destino
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Autenticación fallida, mostrar un mensaje de error en la vista de login
                    ModelState.AddModelError(string.Empty, "La contraseña o el nombre de usuario son incorrectos");
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción si ocurre alguna durante la autenticación
                ModelState.AddModelError(string.Empty, "Se produjo un error durante la autenticación. Por favor, inténtalo de nuevo más tarde.");
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
