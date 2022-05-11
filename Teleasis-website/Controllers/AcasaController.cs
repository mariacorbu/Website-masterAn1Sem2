using Microsoft.AspNetCore.Mvc;
using Teleasis_website.Models;
using Firebase.Database;
using Firebase.Database.Query;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using FirebaseAdmin.Auth;
using Firebase.Auth;

namespace Teleasis_website.Controllers
{
    public class AcasaController : Controller
    {
        FirebaseAuthProvider auth;
        FirebaseClient firebase;
        FirebaseApp defaultApp;
        private const String databaseUrl = "https://teleasisfirebase-default-rtdb.firebaseio.com/";
        private const String databaseSecret = "r6fXFNlgsApi1LwKa3qcVIwZYXE8UM1PjfmU7wDg";

        public AcasaController()
        {
            auth = new FirebaseAuthProvider(
                             new FirebaseConfig("AIzaSyBSdoiZ3E-8azjmBo1DlbG_OUOOWzr4qBw"));

            firebase = new FirebaseClient(
                databaseUrl,
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(databaseSecret)
                });

            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "C:\\Users\\Asus\\Desktop\\teleasisfirebase-firebase-adminsdk-dgw38-2c95afeecc.json");

            if (FirebaseApp.DefaultInstance == null)
            {
                defaultApp = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.GetApplicationDefault(),
                });
            }

        }


        public async Task<IActionResult> AcasaAdministrator()
        {
            var query_pacienti = await firebase.Child("Conturi/Pacienti").OrderByKey().OnceAsync<dynamic>();
            var pacientiLista = query_pacienti.Select(item => new PacientModel
            {
                nume_pacient = "",
                prenume_pacient = "",
                CNP = "",
                email_pacient = item.Object.email,
                id_pacient = item.Key
            }).ToList();

            ViewBag.pacientiLista = pacientiLista;

            var query_medici = await firebase.Child("Conturi/Medici").OrderByKey().OnceAsync<dynamic>();
            var mediciLista = query_medici.Select(item => new MedicModel
            {
                nume_medic = item.Object.nume_medic,
                prenume_medic = item.Object.prenume_medic,
                cod_parafa = item.Object.cod_parafa,
                email_medic = item.Object.email_medic,
                id_medic = item.Key
            }).ToList();

            ViewBag.mediciLista = mediciLista;

            var query_supraveghetori = await firebase.Child("Conturi/Supraveghetori").OrderByKey().OnceAsync<dynamic>();
            var supraveghetoriLista = query_supraveghetori.Select(item => new SupraveghetorModel
            {
                nume_supraveghetor = item.Object.nume_supraveghetor,
                prenume_supraveghetor = item.Object.prenume_supraveghetor,
                email_supraveghetor = item.Object.email_supraveghetor,
                id_supraveghetor = item.Key
            }).ToList();

            ViewBag.supraveghetoriLista = supraveghetoriLista;

            var query_ingrijitori = await firebase.Child("Conturi/Ingrijitori").OrderByKey().OnceAsync<dynamic>();
            var ingrijitoriLista = query_ingrijitori.Select(item => new IngrijitorModel
            {
                nume_ingrijitor = item.Object.nume_ingrijitor,
                prenume_ingrijitor = item.Object.prenume_ingrijitor,
                email_ingrijitor = item.Object.email_ingrijitor,
                id_ingrijitor = item.Key
            }).ToList();

            ViewBag.ingrijitoriLista = ingrijitoriLista;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AcasaAdministrator(string tipuri, PacientModel pacientModel, MedicModel medicModel, IngrijitorModel ingrijitorModel, SupraveghetorModel supraveghetorModel)
        {

            switch (tipuri)
            {
                case "pacient":
                    var result = await auth.CreateUserWithEmailAndPasswordAsync(pacientModel.email_pacient, pacientModel.parola_pacient);

                    await firebase.Child("Conturi/Pacienti/" + result.User.LocalId + "/email").PutAsync<string>(pacientModel.email_pacient);

                    break;
                case "medic":
                    var medic = new
                    {
                        nume_medic = medicModel.nume_medic,
                        prenume_medic = medicModel.prenume_medic,
                        cod_parafa = medicModel.cod_parafa,
                        email_medic = medicModel.email_medic
                    };
                    var result2 = await auth.CreateUserWithEmailAndPasswordAsync(medicModel.email_medic, medicModel.parola_medic);
                    await firebase.Child("Conturi/Medici/" + result2.User.LocalId).PutAsync<dynamic>(medic);
                    break;
                case "ingrijitor":
                    var ingrijitor = new
                    {
                        nume_ingrijitor = ingrijitorModel.nume_ingrijitor,
                        prenume_ingrijitor = ingrijitorModel.prenume_ingrijitor,
                        email_ingrijitor = ingrijitorModel.email_ingrijitor
                    };
                    var result3 = await auth.CreateUserWithEmailAndPasswordAsync(ingrijitorModel.email_ingrijitor, ingrijitorModel.parola_ingrijitor);
                    await firebase.Child("Conturi/Ingrijitori/" + result3.User.LocalId).PutAsync<dynamic>(ingrijitor);
                    break;
                case "supraveghetor":
                    var supraveghetor = new
                    {
                        nume_supraveghetor = supraveghetorModel.nume_supraveghetor,
                        prenume_supraveghetor = supraveghetorModel.prenume_supraveghetor,
                        email_supraveghetor = supraveghetorModel.email_supraveghetor
                    };
                    var result4 = await auth.CreateUserWithEmailAndPasswordAsync(supraveghetorModel.email_supraveghetor, supraveghetorModel.parola_supraveghetor);
                    await firebase.Child("Conturi/Supraveghetori/" + result4.User.LocalId).PutAsync<dynamic>(supraveghetor);
                    break;
            }

            var query_pacienti = await firebase.Child("Conturi/Pacienti").OrderByKey().OnceAsync<dynamic>();
            var pacientiLista = query_pacienti.Select(item => new PacientModel
            {
                nume_pacient = "",
                prenume_pacient = "",
                CNP = "",
                email_pacient = item.Object.email,
                id_pacient = item.Key
            }).ToList();

            ViewBag.pacientiLista = pacientiLista;

            var query_medici = await firebase.Child("Conturi/Medici").OrderByKey().OnceAsync<dynamic>();
            var mediciLista = query_medici.Select(item => new MedicModel
            {
                nume_medic = item.Object.nume_medic,
                prenume_medic = item.Object.prenume_medic,
                cod_parafa = item.Object.cod_parafa,
                email_medic = item.Object.email_medic,
                id_medic = item.Key
            }).ToList();

            ViewBag.mediciLista = mediciLista;

            var query_supraveghetori = await firebase.Child("Conturi/Supraveghetori").OrderByKey().OnceAsync<dynamic>();
            var supraveghetoriLista = query_supraveghetori.Select(item => new SupraveghetorModel
            {
                nume_supraveghetor = item.Object.nume_supraveghetor,
                prenume_supraveghetor = item.Object.prenume_supraveghetor,
                email_supraveghetor = item.Object.email_supraveghetor,
                id_supraveghetor = item.Key
            }).ToList();

            ViewBag.supraveghetoriLista = supraveghetoriLista;

            var query_ingrijitori = await firebase.Child("Conturi/Ingrijitori").OrderByKey().OnceAsync<dynamic>();
            var ingrijitoriLista = query_ingrijitori.Select(item => new IngrijitorModel
            {
                nume_ingrijitor = item.Object.nume_ingrijitor,
                prenume_ingrijitor = item.Object.prenume_ingrijitor,
                email_ingrijitor = item.Object.email_ingrijitor,
                id_ingrijitor = item.Key
            }).ToList();

            ViewBag.ingrijitoriLista = ingrijitoriLista;

            return View();

        }

        [HttpPost("{id_cont}")]
        public async Task<ActionResult> DeleteAccount(string id_cont)
        {
            var noduriPacienti = await firebase.Child("Conturi/Pacienti").OrderByKey().OnceAsync<dynamic>();
            foreach (var pacient in noduriPacienti)
            {
                if (pacient.Key.Equals(id_cont))
                {
                    await firebase.Child("Conturi/Pacienti/" + id_cont).DeleteAsync();
                }
            }
            //var conturi = await firebase.Child("Conturi/").OrderByKey().OnceAsync<dynamic>();
            //foreach (dynamic cont in conturi)
            //{
            //    var persoane = await firebase.Child("Conturi/" + cont.Key).OrderByKey().OnceAsync<dynamic>();
            //    foreach (dynamic persoana in persoane)
            //    {
            //        if(persoana.Key.Equals(id_pacient))
            //        {
            //            await firebase.Child("Conturi/" + cont.Key + "/" + id_pacient).DeleteAsync();
            //        }
            //    }
            //}            

            var noduriMedici = await firebase.Child("Conturi/Medici").OrderByKey().OnceAsync<dynamic>();
            foreach (var medic in noduriMedici)
            {
                if (medic.Key.Equals(id_cont))
                {
                    await firebase.Child("Conturi/Medici/" + id_cont).DeleteAsync();
                }
            }
            var noduriIngrijitori = await firebase.Child("Conturi/Ingrijitori").OrderByKey().OnceAsync<dynamic>();
            foreach (var ingrijitor in noduriIngrijitori)
            {
                if (ingrijitor.Key.Equals(id_cont))
                {
                    await firebase.Child("Conturi/Ingrijitori/" + id_cont).DeleteAsync();
                }
            }
            var noduriSupraveghetori = await firebase.Child("Conturi/Supraveghetori").OrderByKey().OnceAsync<dynamic>();
            foreach (var supraveghetor in noduriSupraveghetori)
            {
                if (supraveghetor.Key.Equals(id_cont))
                {
                    await firebase.Child("Conturi/Supraveghetori/" + id_cont).DeleteAsync();
                }
            }
            await FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance.DeleteUserAsync(id_cont);


            var query_pacienti = await firebase.Child("Conturi/Pacienti").OrderByKey().OnceAsync<dynamic>();
            var pacientiLista = query_pacienti.Select(item => new PacientModel
            {
                nume_pacient = "",
                prenume_pacient = "",
                CNP = "",
                email_pacient = item.Object.email,
                id_pacient = item.Key
            }).ToList();

            ViewBag.pacientiLista = pacientiLista;

            var query_medici = await firebase.Child("Conturi/Medici").OrderByKey().OnceAsync<dynamic>();
            var mediciLista = query_medici.Select(item => new MedicModel
            {
                nume_medic = item.Object.nume_medic,
                prenume_medic = item.Object.prenume_medic,
                cod_parafa = item.Object.cod_parafa,
                email_medic = item.Object.email_medic,
                id_medic = item.Key
            }).ToList();

            ViewBag.mediciLista = mediciLista;

            var query_supraveghetori = await firebase.Child("Conturi/Supraveghetori").OrderByKey().OnceAsync<dynamic>();
            var supraveghetoriLista = query_supraveghetori.Select(item => new SupraveghetorModel
            {
                nume_supraveghetor = item.Object.nume_supraveghetor,
                prenume_supraveghetor = item.Object.prenume_supraveghetor,
                email_supraveghetor = item.Object.email_supraveghetor,
                id_supraveghetor = item.Key
            }).ToList();

            ViewBag.supraveghetoriLista = supraveghetoriLista;

            var query_ingrijitori = await firebase.Child("Conturi/Ingrijitori").OrderByKey().OnceAsync<dynamic>();
            var ingrijitoriLista = query_ingrijitori.Select(item => new IngrijitorModel
            {
                nume_ingrijitor = item.Object.nume_ingrijitor,
                prenume_ingrijitor = item.Object.prenume_ingrijitor,
                email_ingrijitor = item.Object.email_ingrijitor,
                id_ingrijitor = item.Key
            }).ToList();

            ViewBag.ingrijitoriLista = ingrijitoriLista;

            return Redirect("Acasa/AcasaAdministrator");


        }
    }
}
