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
		public IActionResult Authenticate(string email, string pass)
		{
			try
			{
				// Llamada al método de autenticación en el controlador de Usuario
				bool isAuthenticated = Usuario_Controller.Autenticar(email, pass);

				if (isAuthenticated)
				{
					// Autenticación exitosa, redirige a la página principal
					return RedirectToAction("Index", "Home");
				}
				else
				{
					// Autenticación fallida, mostrar un mensaje de error en la vista de login
					ModelState.AddModelError(string.Empty, "La contraseña o el nombre de usuario son incorrectos");
					return View("Index");
				}
			}
			catch (Exception ex)
			{
				// Manejar la excepción si ocurre alguna durante la autenticación
				ModelState.AddModelError(string.Empty, "Se produjo un error durante la autenticación. Por favor, inténtalo de nuevo más tarde.");
				return View("Index");
			}
		}
	}
}
