using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class LoginController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Authenticate(string email, string pass)
    {
        try
        {
            bool isAuthenticated = Usuario_Controller.Autenticar(email, pass);

            if (isAuthenticated)
            {
                // Crear los claims del usuario
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, email)
                };

                // Crear la identidad y el principal del usuario
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Iniciar la sesión del usuario
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                // Redirigir a la página principal
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Autenticación fallida, mostrar un mensaje de error en la vista de login
                ViewBag.Message = "La contraseña o el nombre de usuario son incorrectos";
                return View("Index");
            }
        }
        catch (Exception ex)
        {
            // Manejar la excepción si ocurre alguna durante la autenticación
            ViewBag.Message = "Se produjo un error durante la autenticación. Por favor, inténtalo de nuevo más tarde.";
            return View("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        // Cerrar la sesión del usuario
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}
