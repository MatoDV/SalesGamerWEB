using Microsoft.AspNetCore.Mvc;
using SalesGamerWEB.Models;
using System.Threading.Tasks;

namespace SalesGamerWEB.Controllers
{
    public class ContactoController : Controller
    {
        private readonly IEmailSender _emailSender;

        public ContactoController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Enviar(string name, string email, string message)
        {
            var subject = "Gracias por contactarte con SalesGamer";
            var body = $"<p>Muchas gracias por contactarte con el equipo de ayuda de SalesGamer. En breve estaremos respondiendo tus dudas.</p>";

            try
            {
                // Envía el correo al email proporcionado
                await _emailSender.SendEmailAsync(email, subject, body);

                // Mensaje de éxito
                ViewBag.Message = "Tu mensaje ha sido enviado correctamente.";
            }
            catch (Exception ex)
            {
                // Manejo de errores
                ViewBag.Message = "Hubo un problema al enviar tu mensaje. Por favor, inténtalo de nuevo más tarde.";
            }

            // Redirige a la vista de contacto
            return View("Index");
        }
    }
}
