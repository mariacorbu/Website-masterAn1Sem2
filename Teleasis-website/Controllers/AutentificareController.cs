using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Teleasis_website.Models;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;

namespace Teleasis_website.Controllers
{
    public class AutentificareController : Controller
    {
        Firebase.Auth.FirebaseAuthProvider auth;
        string uid_administrator = "dwPFK7J5tHWksQlkZgAsBPv8xmG2";
        FirebaseClient firebase;
        private const String databaseUrl = "https://teleasisfirebase-default-rtdb.firebaseio.com/";
        private const String databaseSecret = "r6fXFNlgsApi1LwKa3qcVIwZYXE8UM1PjfmU7wDg";

        public AutentificareController()
        {
            auth = new Firebase.Auth.FirebaseAuthProvider(
                         new FirebaseConfig("AIzaSyBSdoiZ3E-8azjmBo1DlbG_OUOOWzr4qBw"));
            firebase = new FirebaseClient(
               databaseUrl,
               new FirebaseOptions
               {
                   AuthTokenAsyncFactory = () => Task.FromResult(databaseSecret)
               });
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

                    var noduriPacienti = await firebase.Child("Conturi/Pacienti").OrderByKey().OnceAsync<dynamic>();
                    foreach (var pacient in noduriPacienti)
                    {
                        if (pacient.Key.Equals(authLink.User.LocalId))
                        {
                            return RedirectToAction("AcasaPacient", "Acasa");
                        }
                    }
                    var noduriMedici = await firebase.Child("Conturi/Medici").OrderByKey().OnceAsync<dynamic>();
                    foreach (var medic in noduriMedici)
                    {
                        if (medic.Key.Equals(authLink.User.LocalId))
                        {
                            return RedirectToAction("AcasaMedic", "Acasa");

                        }
                    }
                    var noduriIngrijitori = await firebase.Child("Conturi/Ingrijitori").OrderByKey().OnceAsync<dynamic>();
                    foreach (var ingrijitor in noduriIngrijitori)
                    {
                        if (ingrijitor.Key.Equals(authLink.User.LocalId))
                        {
                            return RedirectToAction("AcasaIngrijitori", "Acasa");

                        }
                    }
                    var noduriSupraveghetori = await firebase.Child("Conturi/Supraveghetori").OrderByKey().OnceAsync<dynamic>();
                    foreach (var supraveghetor in noduriSupraveghetori)
                    {
                        if (supraveghetor.Key.Equals(authLink.User.LocalId))
                        {
                            return RedirectToAction("AcasaSupraveghetori", "Acasa");

                        }
                    }

                    if (authLink.User.LocalId == uid_administrator) 
                    {
                        return RedirectToAction("AcasaAdministrator", "Acasa");

                    }

                    return View();
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