using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Teleasis_website.Models;

namespace Teleasis_website.Controllers
{
    public class AutentificareController : Controller
    {
        private readonly ILogger<AutentificareController> _logger;

        public AutentificareController(ILogger<AutentificareController> logger)
        {
            _logger = logger;
        }

        public IActionResult Autentificare()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}