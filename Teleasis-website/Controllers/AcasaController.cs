using Microsoft.AspNetCore.Mvc;
using Teleasis_website.Models;
using Firebase.Auth;

namespace Teleasis_website.Controllers
{
    public class AcasaController : Controller
    {
        FirebaseAuthProvider auth;

        public AcasaController() {
            auth = new FirebaseAuthProvider(
                             new FirebaseConfig("AIzaSyBSdoiZ3E-8azjmBo1DlbG_OUOOWzr4qBw"));
        }
        public IActionResult AcasaAdministrator()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AcasaAdministrator(string tipuri, PacientModel pacientModel, MedicModel medicModel, IngrijitorModel ingrijitorModel, SupraveghetorModel supraveghetorModel)
        {

            switch (tipuri) {
                case "pacient":
                    await auth.CreateUserWithEmailAndPasswordAsync(pacientModel.email_pacient, pacientModel.parola_pacient);

                    break;
                case "medic":
                    await auth.CreateUserWithEmailAndPasswordAsync(medicModel.email_medic, medicModel.parola_medic);

                    break;
                case "ingrijitor":
                    await auth.CreateUserWithEmailAndPasswordAsync(ingrijitorModel.email_ingrijitor, ingrijitorModel.parola_ingrijitor);

                    break;
                case "supraveghetor":
                    await auth.CreateUserWithEmailAndPasswordAsync(supraveghetorModel.email_supraveghetor, supraveghetorModel.parola_supraveghetor);

                    break;
            }
            return View();
           
        }
    }
}
