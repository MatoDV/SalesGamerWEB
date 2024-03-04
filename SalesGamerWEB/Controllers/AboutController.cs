using Microsoft.AspNetCore.Mvc;

namespace SalesGamerWEB.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
