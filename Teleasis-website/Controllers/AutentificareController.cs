using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Teleasis_website.Models;
using Firebase.Auth;

namespace Teleasis_website.Controllers
{
    public class AutentificareController : Controller
    {
        Firebase.Auth.FirebaseAuthProvider auth;
        public AutentificareController()
        {
            auth = new Firebase.Auth.FirebaseAuthProvider(
                         new FirebaseConfig("AIzaSyBSdoiZ3E-8azjmBo1DlbG_OUOOWzr4qBw"));
        }

        public IActionResult Autentificare()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Autentificare(string email, string password)
        {
            try
            {
                //log in the user
                var authLink = await auth
                                .SignInWithEmailAndPasswordAsync(email, password);
                string token = authLink.FirebaseToken;
                //saving the token in a session variable
                if (token != null)
                {
                    HttpContext.Session.SetString("_UserToken", token);

                    return RedirectToAction("AcasaAdministrator", "Acasa");
                }
                else
                {
                    ViewBag.Eroare = "Autentificarea a esuat! Verificati email-ul sau parola!";

                    return View();
                }
            }
            catch{
                ViewBag.Eroare = "Autentificarea a esuat! Verificati email-ul sau parola!";
                return View();
            }

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}