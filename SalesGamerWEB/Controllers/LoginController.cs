using Microsoft.AspNetCore.Mvc;

namespace SalesGamerWEB.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View("Login");
        }
    }
}
