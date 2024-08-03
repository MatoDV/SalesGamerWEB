using Microsoft.AspNetCore.Mvc;
using SalesGamerWEB.Models;

namespace SalesGamerWEB.Controllers
{
    public class RegisterController : Controller
    {
        // GET: /Register
        public IActionResult Index()
        {
            Usuario usuario = new Usuario(); // Crear una instancia de Usuario
            return View(usuario); // Pasar el usuario a la vista
        }

        // POST: /Register/Create
        [HttpPost]
        public IActionResult Create(Usuario usr, string passConfirm)
        {
            // Verificar que el modelo es válido
            if (ModelState.IsValid)
            {
                // Validar que las contraseñas coincidan
                if (usr.Contraseña != passConfirm)
                {
                    ModelState.AddModelError(string.Empty, "Las contraseñas no coinciden.");
                    return View("Index", usr);
                }

                try
                {
                    // Intentar crear el usuario
                    bool created = Usuario_Controller.CrearUsuario(usr);
                    if (created)
                    {
                        // Redirigir a la página de inicio de sesión o a otra página de destino
                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Error al crear el usuario.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error: " + ex.Message);
                }
            }

            // Mostrar errores de validación si existen
            return View("Index", usr);
        }
    }
}
