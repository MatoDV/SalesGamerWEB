using Microsoft.AspNetCore.Mvc;
using SalesGamerWEB.Models;
using System;

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
        public IActionResult Create(Usuario usr)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Intentar crear el usuario
                    bool created = Usuario_Controller.crearUsuario(usr);
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
            // Si hay errores de validación, volver a mostrar el formulario con los mensajes de error
            return View("Index", usr);
        }
    }
}
