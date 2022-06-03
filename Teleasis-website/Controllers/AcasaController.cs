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
        public string IdMedic;
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

            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "FirebaseApp/teleasisfirebase-firebase-adminsdk-dgw38-2c95afeecc.json");

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
                nume_pacient = item.Object.DateDemografice.nume_pacient,
                prenume_pacient = item.Object.DateDemografice.prenume_pacient,
                CNP = item.Object.DateDemografice.CNP,
                email_pacient = item.Object.DateDemografice.email_pacient,
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

            var query_cereri = await firebase.Child("Conturi/Administrator/Cereri/").OrderByKey().OnceAsync<dynamic>();
            var cereriLista = query_cereri.Select(item => new AdaugarePacientModel
            {
                nume_pacient = item.Object.nume_pacient,
                prenume_pacient = item.Object.prenume_pacient,
                email_pacient = item.Object.email_pacient,
                CNP = item.Object.CNP,
                judet_pacient = item.Object.judet_pacient,
                oras_pacient = item.Object.oras_pacient,
                strada_pacient = item.Object.strada_pacient,
                id_supraveghetor = item.Object.id_supraveghetor,
                id_ingrijitor = item.Object.id_ingrijitor,
                numar_telefon_pacient = item.Object.numar_telefon_pacient,
                profesie_pacient = item.Object.profesie_pacient,
                loc_de_munca_pacient = item.Object.loc_de_munca_pacient,
                id_medic = item.Object.id_medic
            }).ToList();

            ViewBag.cereriLista = cereriLista;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AcasaAdministrator(string tipuri, MedicModel medicModel, IngrijitorModel ingrijitorModel, SupraveghetorModel supraveghetorModel)
        {

            switch (tipuri)
            {
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
                nume_pacient = item.Object.DateDemografice.nume_pacient,
                prenume_pacient = item.Object.DateDemografice.prenume_pacient,
                CNP = item.Object.DateDemografice.CNP,
                email_pacient = item.Object.DateDemografice.email_pacient,
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

            var query_cereri = await firebase.Child("Conturi/Administrator/Cereri/").OrderByKey().OnceAsync<dynamic>();
            var cereriLista = query_cereri.Select(item => new AdaugarePacientModel
            {
                nume_pacient = item.Object.nume_pacient,
                prenume_pacient = item.Object.prenume_pacient,
                email_pacient = item.Object.email_pacient,
                CNP = item.Object.CNP,
                judet_pacient = item.Object.judet_pacient,
                oras_pacient = item.Object.oras_pacient,
                id_ingrijitor = item.Object.id_ingrijitor,
                id_supraveghetor = item.Object.id_supraveghetor,
                strada_pacient = item.Object.strada_pacient,
                numar_telefon_pacient = item.Object.numar_telefon_pacient,
                profesie_pacient = item.Object.profesie_pacient,
                loc_de_munca_pacient = item.Object.loc_de_munca_pacient,
                id_medic = item.Object.id_medic
            }).ToList();

            ViewBag.cereriLista = cereriLista;

            TempData["Mesaj"] = "Cont creat.";

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
                email_pacient = item.Object.DateDemografice.email,
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

            TempData["Mesaj"] = "Cont sters.";

            return Redirect("Acasa/AcasaAdministrator");


        }


        [HttpPost]
        public async Task<ActionResult> CreatePacientAccount(string date_pacient, string parola_cerere_pacient)
        {
            AdaugarePacientModel pacient = new AdaugarePacientModel();
            var date = date_pacient.Split("#");
            pacient.id_medic = date[0];
            pacient.CNP = date[1];
            pacient.prenume_pacient = date[2];
            pacient.nume_pacient = date[3];
            pacient.email_pacient = date[4];
            pacient.judet_pacient = date[5];
            pacient.oras_pacient = date[6].Replace("&", " ");
            pacient.strada_pacient = date[7].Replace("&", " ");
            pacient.numar_telefon_pacient = date[8];
            pacient.profesie_pacient = date[9].Replace("&", " ");
            pacient.loc_de_munca_pacient = date[10].Replace("&", " ");
            pacient.id_ingrijitor = date[11];
            pacient.id_supraveghetor = date[12];
            var result = await auth.CreateUserWithEmailAndPasswordAsync(pacient.email_pacient, parola_cerere_pacient);

            await firebase.Child("Conturi/Pacienti/" + result.User.LocalId + "/DateDemografice").PutAsync<AdaugarePacientModel>(pacient);

            await firebase.Child("Conturi/Administrator/Cereri/" + pacient.CNP).DeleteAsync();

            TempData["Mesaj"] = "Cont creat.";

            return Redirect("AcasaAdministrator");
        }

        [HttpPost]
        public async Task<ActionResult> Deconectare()
        {
            HttpContext.Session.Remove("_UserToken");
            return RedirectToAction("Autentificare", "Autentificare");
        }

        public async Task<IActionResult> AdaugarePacient(string id_medic)
        {

            ViewBag.IdMedic = id_medic;

            var query_ingrijitori = await firebase.Child("Conturi/Ingrijitori").OrderByKey().OnceAsync<dynamic>();
            var query_supraveghetori = await firebase.Child("Conturi/Supraveghetori").OrderByKey().OnceAsync<dynamic>();

            List<IngrijitorModel> ingrijitoriLista = new List<IngrijitorModel>();
            List<SupraveghetorModel> supraveghetoriLista = new List<SupraveghetorModel>();

            ingrijitoriLista = query_ingrijitori.Select(item => new IngrijitorModel
            {
                nume_ingrijitor = item.Object.nume_ingrijitor,
                prenume_ingrijitor = item.Object.prenume_ingrijitor,
                email_ingrijitor = item.Object.email_ingrijitor,
                id_ingrijitor = item.Key
            }).ToList();

            supraveghetoriLista = query_supraveghetori.Select(item => new SupraveghetorModel
            {
                nume_supraveghetor = item.Object.nume_supraveghetor,
                prenume_supraveghetor = item.Object.prenume_supraveghetor,
                email_supraveghetor = item.Object.email_supraveghetor,
                id_supraveghetor = item.Key
            }).ToList();

            ViewBag.ingrijitoriLista = ingrijitoriLista;
            ViewBag.supraveghetoriLista = supraveghetoriLista;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdaugarePacient(AdaugarePacientModel pacient)
        {
            pacient.strada_pacient = pacient.strada_pacient.Replace(" ", "&");
            pacient.oras_pacient = pacient.oras_pacient.Replace(" ", "&");
            pacient.profesie_pacient = pacient.profesie_pacient.Replace(" ", "&");
            pacient.loc_de_munca_pacient = pacient.loc_de_munca_pacient.Replace(" ", "&");

            await firebase.Child("Conturi/Administrator/Cereri/" + pacient.CNP).PutAsync<AdaugarePacientModel>(pacient);

            TempData["Mesaj"] = "Cerere inregistrata.";

            return RedirectToAction("AcasaMedic", "Acasa", new { medic_id = pacient.id_medic });
        }

        public async Task<IActionResult> AcasaMedic(string medic_id)
        {
            ViewBag.id_medic = medic_id;
            var query_pacienti = await firebase.Child("Conturi/Pacienti").OrderByKey().OnceAsync<dynamic>();
            List<AdaugarePacientModel> pacientiLista = new List<AdaugarePacientModel>();
            List<AdaugarePacientModel> pacientiListaView = new List<AdaugarePacientModel>();

            pacientiLista = query_pacienti.Select(item => new AdaugarePacientModel
            {
                nume_pacient = item.Object.DateDemografice.nume_pacient,
                prenume_pacient = item.Object.DateDemografice.prenume_pacient,
                CNP = item.Object.DateDemografice.CNP,
                email_pacient = item.Object.DateDemografice.email_pacient,
                id_medic = item.Object.DateDemografice.id_medic,
                id_pacient = item.Key
            }).ToList();

            foreach (AdaugarePacientModel pacient in pacientiLista)
            {
                if (pacient.id_medic.Equals(medic_id))
                {
                    pacientiListaView.Add(pacient);

                }
            }
            ViewBag.pacientiLista = pacientiListaView;
            return View();
        }

        public async Task<IActionResult> AcasaSupraveghetor(string id_supraveghetor)
        {
            ViewBag.id_supraveghetor = id_supraveghetor;
            var query_pacienti = await firebase.Child("Conturi/Pacienti").OrderByKey().OnceAsync<dynamic>();
            List<AdaugarePacientModel> pacientiLista = new List<AdaugarePacientModel>();
            List<AdaugarePacientModel> pacientiListaView = new List<AdaugarePacientModel>();

            pacientiLista = query_pacienti.Select(item => new AdaugarePacientModel
            {
                nume_pacient = item.Object.DateDemografice.nume_pacient,
                prenume_pacient = item.Object.DateDemografice.prenume_pacient,
                CNP = item.Object.DateDemografice.CNP,
                email_pacient = item.Object.DateDemografice.email_pacient,
                id_medic = item.Object.DateDemografice.id_medic,
                id_supraveghetor = item.Object.DateDemografice.id_supraveghetor,
                id_pacient = item.Key
            }).ToList();

            foreach (AdaugarePacientModel pacient in pacientiLista)
            {
                if (pacient.id_supraveghetor.Equals(id_supraveghetor))
                {
                    pacientiListaView.Add(pacient);

                }
            }
            ViewBag.pacientiLista = pacientiListaView;
            return View();
        }

        public async Task<IActionResult> ArhivaPacienti(string id_medic)
        {
            ViewBag.id_medic = id_medic;
            var query_pacienti = await firebase.Child("Conturi/Medici/" + id_medic + "/PacientiArhivati/").OrderByKey().OnceAsync<dynamic>();
            List<AdaugarePacientModel> pacientiLista = new List<AdaugarePacientModel>();

            pacientiLista = query_pacienti.Select(item => new AdaugarePacientModel
            {
                nume_pacient = item.Object.nume_pacient,
                prenume_pacient = item.Object.prenume_pacient,
                CNP = item.Object.CNP,
                email_pacient = item.Object.email_pacient,
                id_medic = item.Object.id_medic,
                id_pacient = item.Key
            }).ToList();

            ViewBag.pacientiLista = pacientiLista;

            return View();

        }


        public async Task<IActionResult> AcasaPacient(string id_pacient)
        {
            var query_pacienti = await firebase.Child("Conturi/Pacienti").OrderByKey().OnceAsync<dynamic>();

            List<AdaugarePacientModel> pacientiLista = new List<AdaugarePacientModel>();

            pacientiLista = query_pacienti.Select(item => new AdaugarePacientModel
            {
                nume_pacient = item.Object.DateDemografice.nume_pacient,
                prenume_pacient = item.Object.DateDemografice.prenume_pacient,
                CNP = item.Object.DateDemografice.CNP,
                email_pacient = item.Object.DateDemografice.email_pacient,
                id_medic = item.Object.DateDemografice.id_medic,
                id_pacient = item.Key,
                judet_pacient = item.Object.DateDemografice.judet_pacient,
                oras_pacient = item.Object.DateDemografice.oras_pacient,
                strada_pacient = item.Object.DateDemografice.strada_pacient,
                id_ingrijitor = item.Object.DateDemografice.id_ingrijitor,
                id_supraveghetor = item.Object.DateDemografice.id_supraveghetor,
                numar_telefon_pacient = item.Object.DateDemografice.numar_telefon_pacient,
                profesie_pacient = item.Object.DateDemografice.profesie_pacient,
                loc_de_munca_pacient = item.Object.DateDemografice.loc_de_munca_pacient
            }).ToList();

            foreach (AdaugarePacientModel pacient in pacientiLista)
            {
                if (pacient.id_pacient.Equals(id_pacient))
                {
                    ViewBag.pacient = pacient;
                }
            }
           
            return View();
        }
    }
}
