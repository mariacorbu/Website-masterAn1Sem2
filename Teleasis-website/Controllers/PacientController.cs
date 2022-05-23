using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Mvc;
using Teleasis_website.Models;

namespace Teleasis_website.Controllers
{
    public class PacientController : Controller
    {
        FirebaseAuthProvider auth;
        FirebaseClient firebase;
        FirebaseApp defaultApp;
        private const String databaseUrl = "https://teleasisfirebase-default-rtdb.firebaseio.com/";
        private const String databaseSecret = "r6fXFNlgsApi1LwKa3qcVIwZYXE8UM1PjfmU7wDg";

        public PacientController()
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

        public async Task<IActionResult> AccesarePacient(string id_pacient)
        {
            var query_pacienti = await firebase.Child("Conturi/Pacienti").OrderByKey().OnceAsync<dynamic>();

            List<AdaugarePacientModel> pacientiLista = new List<AdaugarePacientModel>();

            pacientiLista = query_pacienti.Select(item => new AdaugarePacientModel
            {
                nume_pacient = item.Object.nume_pacient,
                prenume_pacient = item.Object.prenume_pacient,
                CNP = item.Object.CNP,
                email_pacient = item.Object.email_pacient,
                id_medic = item.Object.id_medic,
                id_pacient = item.Key,
                judet_pacient = item.Object.judet_pacient,
                oras_pacient = item.Object.oras_pacient,
                strada_pacient = item.Object.strada_pacient,
                numar_telefon_pacient = item.Object.numar_telefon_pacient,
                profesie_pacient = item.Object.profesie_pacient,
                loc_de_munca_pacient = item.Object.loc_de_munca_pacient
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

        public async Task<IActionResult> FisaPacient(string id_pacient)
        {
            var query_pacienti = await firebase.Child("Conturi/Pacienti").OrderByKey().OnceAsync<dynamic>();

            List<AdaugarePacientModel> pacientiLista = new List<AdaugarePacientModel>();

            pacientiLista = query_pacienti.Select(item => new AdaugarePacientModel
            {
                nume_pacient = item.Object.nume_pacient,
                prenume_pacient = item.Object.prenume_pacient,
                CNP = item.Object.CNP,
                email_pacient = item.Object.email_pacient,
                id_medic = item.Object.id_medic,
                id_pacient = item.Key,
                judet_pacient = item.Object.judet_pacient,
                oras_pacient = item.Object.oras_pacient,
                strada_pacient = item.Object.strada_pacient,
                numar_telefon_pacient = item.Object.numar_telefon_pacient,
                profesie_pacient = item.Object.profesie_pacient,
                loc_de_munca_pacient = item.Object.loc_de_munca_pacient
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

        [HttpPost]
        public async Task<IActionResult> FisaPacient(string CNP, string email_pacient, string id_medic, string judet_pacient,
            string loc_de_munca_pacient, string numar_telefon_pacient, string nume_pacient, string oras_pacient, string prenume_pacient,
            string profesie_pacient, string strada_pacient, string id_pacient)
        {
            AdaugarePacientModel pacient = new AdaugarePacientModel()
            {
                CNP = CNP,
                email_pacient = email_pacient,
                id_medic = id_medic,
                judet_pacient = judet_pacient,
                loc_de_munca_pacient = loc_de_munca_pacient,
                numar_telefon_pacient = numar_telefon_pacient,
                nume_pacient = nume_pacient,
                oras_pacient = oras_pacient,
                prenume_pacient = prenume_pacient,
                profesie_pacient = profesie_pacient,
                strada_pacient = strada_pacient,
                id_pacient = id_pacient
            };
            await firebase.Child("Conturi/Pacienti/" + pacient.id_pacient).PutAsync<AdaugarePacientModel>(pacient);

            return RedirectToAction("FisaPacient", new { id_pacient = id_pacient });

        }
    }

}
