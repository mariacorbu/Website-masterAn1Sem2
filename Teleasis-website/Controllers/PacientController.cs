using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        public async Task<IActionResult> FisaPacient(string id_pacient)
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

            var query_consultatii = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/Consultatii").OrderByKey().StartAt("1").OnceAsync<dynamic>();

            List<ConsultatieModel> listaConsultatii = new List<ConsultatieModel>();

            listaConsultatii = query_consultatii.Select(item => new ConsultatieModel
            {
                id_consultatie = item.Key,
                motiv_prezentare_consultatie = item.Object.motiv_prezentare_consultatie,
                simptome_consultatie = item.Object.simptome_consultatie,
                diagnostic_consultatie = item.Object.diagnostic_consultatie,
                data_consultatie = item.Object.data_consultatie,
                trimitere_consultatie = item.Object.trimitere_consultatie,
                retete_generate_consultatie = item.Object.retete_generate_consultatie
            }).ToList();
            ViewBag.listaConsultatii = listaConsultatii;

            return View();
        }

        public async Task<IActionResult> FisaPacientPacient(string id_pacient)
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

            var query_consultatii = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/Consultatii").OrderByKey().StartAt("1").OnceAsync<dynamic>();

            List<ConsultatieModel> listaConsultatii = new List<ConsultatieModel>();

            listaConsultatii = query_consultatii.Select(item => new ConsultatieModel
            {
                id_consultatie = item.Key,
                motiv_prezentare_consultatie = item.Object.motiv_prezentare_consultatie,
                simptome_consultatie = item.Object.simptome_consultatie,
                diagnostic_consultatie = item.Object.diagnostic_consultatie,
                data_consultatie = item.Object.data_consultatie,
                trimitere_consultatie = item.Object.trimitere_consultatie,
                retete_generate_consultatie = item.Object.retete_generate_consultatie
            }).ToList();
            ViewBag.listaConsultatii = listaConsultatii;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> FisaPacient(string CNP, string email_pacient, string id_medic, string judet_pacient,
            string loc_de_munca_pacient, string numar_telefon_pacient, string nume_pacient, string oras_pacient, string prenume_pacient,
            string profesie_pacient, string strada_pacient, string id_pacient, string id_ingrijitor, string id_supraveghetor)
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
                id_pacient = id_pacient,
                id_ingrijitor = id_ingrijitor,
                id_supraveghetor = id_supraveghetor
            };
            await firebase.Child("Conturi/Pacienti/" + pacient.id_pacient + "/DateDemografice").PutAsync<AdaugarePacientModel>(pacient);


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

            foreach (AdaugarePacientModel p in pacientiLista)
            {
                if (p.id_pacient.Equals(id_pacient))
                {
                    ViewBag.pacient = p;
                }
            }

            var query_consultatii = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/Consultatii").OrderByKey().StartAt("1").OnceAsync<dynamic>();

            List<ConsultatieModel> listaConsultatii = new List<ConsultatieModel>();

            listaConsultatii = query_consultatii.Select(item => new ConsultatieModel
            {
                id_consultatie = item.Key,
                motiv_prezentare_consultatie = item.Object.motiv_prezentare_consultatie,
                simptome_consultatie = item.Object.simptome_consultatie,
                diagnostic_consultatie = item.Object.diagnostic_consultatie,
                data_consultatie = item.Object.data_consultatie,
                trimitere_consultatie = item.Object.trimitere_consultatie,
                retete_generate_consultatie = item.Object.retete_generate_consultatie
            }).ToList();
            ViewBag.listaConsultatii = listaConsultatii;

            TempData["Mesaj"] = "Modificari salvate.";

            return RedirectToAction("FisaPacient", new { id_pacient = id_pacient });

        }

        [HttpPost]
        public async Task<IActionResult> AdaugareConsultatie(ConsultatieModel consultatie, string id_pacient)
        {
            string cale = "Conturi/Pacienti/" + id_pacient + "/Consultatii";

            var consultatii = await firebase.Child(cale).OrderByKey().StartAt("1").OnceAsync<dynamic>();

            if (!consultatii.Any())
            {
                //daca ii gol
                consultatie.id_consultatie = "1";

                await firebase.Child("Conturi/Pacienti/" + id_pacient + "/Consultatii/1").PutAsync<ConsultatieModel>(consultatie);

            }
            else
            {
                List<ConsultatieModel> listaConsultatii = new List<ConsultatieModel>();

                listaConsultatii = consultatii.Select(item => new ConsultatieModel
                {
                    id_consultatie = item.Key,
                    motiv_prezentare_consultatie = item.Object.motiv_prezentare_consultatie,
                    simptome_consultatie = item.Object.simptome_consultatie,
                    diagnostic_consultatie = item.Object.diagnostic_consultatie,
                    trimitere_consultatie = item.Object.trimitere_consultatie,
                    retete_generate_consultatie = item.Object.retete_generate_consultatie,
                    data_consultatie = item.Object.data_consultatie
                }).ToList();

                int id = int.Parse(listaConsultatii[listaConsultatii.Count - 1].id_consultatie) + 1;
                consultatie.id_consultatie = id.ToString();
                await firebase.Child("Conturi/Pacienti/" + id_pacient + "/Consultatii/" + id + "/").PutAsync<ConsultatieModel>(consultatie);

            }

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

            var query_consultatii = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/Consultatii").OrderByKey().StartAt("1").OnceAsync<dynamic>();

            List<ConsultatieModel> listaConsultatii2 = new List<ConsultatieModel>();

            listaConsultatii2 = query_consultatii.Select(item => new ConsultatieModel
            {
                id_consultatie = item.Key,
                motiv_prezentare_consultatie = item.Object.motiv_prezentare_consultatie,
                simptome_consultatie = item.Object.simptome_consultatie,
                diagnostic_consultatie = item.Object.diagnostic_consultatie,
                data_consultatie = item.Object.data_consultatie,
                trimitere_consultatie = item.Object.trimitere_consultatie,
                retete_generate_consultatie = item.Object.retete_generate_consultatie
            }).ToList();
            ViewBag.listaConsultatii = listaConsultatii2;

            TempData["Mesaj"] = "Consultatie adaugata.";

            return RedirectToAction("FisaPacient", new { id_pacient = id_pacient });

        }

        [HttpPost("~/Pacient/ArhiveazaConsultatie/{id_uri}")]
        public async Task<IActionResult> ArhiveazaConsultatie(string id_uri)
        {
            var splitul = id_uri.Split("#");
            string id_cons = splitul[0];
            string id_pacient = splitul[1];
            Random rnd = new Random();


            ConsultatieModel c = new ConsultatieModel();

            var consultatii = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/Consultatii").OrderByKey().StartAt("1").OnceAsync<dynamic>();
            List<ConsultatieModel> listaConsultatii = new List<ConsultatieModel>();

            listaConsultatii = consultatii.Select(item => new ConsultatieModel
            {
                id_consultatie = item.Key,
                motiv_prezentare_consultatie = item.Object.motiv_prezentare_consultatie,
                simptome_consultatie = item.Object.simptome_consultatie,
                diagnostic_consultatie = item.Object.diagnostic_consultatie,
                trimitere_consultatie = item.Object.trimitere_consultatie,
                retete_generate_consultatie = item.Object.retete_generate_consultatie,
                data_consultatie = item.Object.data_consultatie
            }).ToList();

            foreach (ConsultatieModel consultatie in listaConsultatii)
            {
                if (consultatie.id_consultatie.Equals(id_cons))
                {
                    c = consultatie;
                    await firebase.Child("Conturi/Pacienti/" + id_pacient + "/ConsultatiiArhivate/" + rnd.Next(99999999) + "/").PutAsync<ConsultatieModel>(c);

                }
            }


            await firebase.Child("Conturi/Pacienti/" + id_pacient + "/Consultatii/" + id_cons).DeleteAsync();



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

            var query_consultatii = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/Consultatii").OrderByKey().StartAt("1").OnceAsync<dynamic>();

            List<ConsultatieModel> listaConsultatii2 = new List<ConsultatieModel>();

            listaConsultatii2 = query_consultatii.Select(item => new ConsultatieModel
            {
                id_consultatie = item.Key,
                motiv_prezentare_consultatie = item.Object.motiv_prezentare_consultatie,
                simptome_consultatie = item.Object.simptome_consultatie,
                diagnostic_consultatie = item.Object.diagnostic_consultatie,
                data_consultatie = item.Object.data_consultatie,
                trimitere_consultatie = item.Object.trimitere_consultatie,
                retete_generate_consultatie = item.Object.retete_generate_consultatie
            }).ToList();
            ViewBag.listaConsultatii = listaConsultatii2;

            TempData["Mesaj"] = "Consultatie arhivata.";

            return RedirectToAction("FisaPacient", new { id_pacient = id_pacient });
        }

        public async Task<IActionResult> ArhivaConsultatii(string id_pacient, string nume_pacient, string prenume_pacient, string id_medic)
        {
            var query_consultatii = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/ConsultatiiArhivate").OrderByKey().OnceAsync<dynamic>();

            List<ConsultatieModel> listaConsultatii = new List<ConsultatieModel>();

            listaConsultatii = query_consultatii.Select(item => new ConsultatieModel
            {
                id_consultatie = item.Key,
                motiv_prezentare_consultatie = item.Object.motiv_prezentare_consultatie,
                simptome_consultatie = item.Object.simptome_consultatie,
                diagnostic_consultatie = item.Object.diagnostic_consultatie,
                data_consultatie = item.Object.data_consultatie,
                trimitere_consultatie = item.Object.trimitere_consultatie,
                retete_generate_consultatie = item.Object.retete_generate_consultatie
            }).ToList();
            ViewBag.listaConsultatii = listaConsultatii;
            ViewBag.prenume_pacient = prenume_pacient;
            ViewBag.nume_pacient = nume_pacient;
            ViewBag.id_medic = id_medic;


            return View();
        }

        [HttpPost("~/Pacient/ArhivarePacient/{id_uri}")]
        public async Task<IActionResult> ArhivarePacient(string id_uri)
        {
            var splitul = id_uri.Split("#");
            string id_pacient = splitul[0];
            string id_medic = splitul[1];

            List<AdaugarePacientModel> pacient = new List<AdaugarePacientModel>();

            var pacienti = await firebase.Child("Conturi/Pacienti/").OrderByKey().OnceAsync<dynamic>();

            pacient = pacienti.Select(item => new AdaugarePacientModel
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

            foreach (AdaugarePacientModel p in pacient)
            {
                if (p.id_pacient.Equals(id_pacient))
                {
                    await firebase.Child("Conturi/Medici/" + id_medic + "/PacientiArhivati/" + id_pacient + "/").PutAsync<AdaugarePacientModel>(p);
                }
            }


            await firebase.Child("Conturi/Pacienti/" + id_pacient).DeleteAsync();

            TempData["Mesaj"] = "Pacient arhivat.";

            return RedirectToAction("AcasaMedic", "Acasa", new { medic_id = id_medic });
        }

        public async Task<IActionResult> RecomandariSiInterventii(string id_pacient, string id_ingrijitor)
        {
            List<RecomandareModel> lista_recomandari = new List<RecomandareModel>();
            List<InterventieModel> lista_interventii = new List<InterventieModel>();

            var recomandare = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/Recomandari").OrderByKey().OnceAsync<dynamic>();
            var interventie = await firebase.Child("Conturi/Ingrijitori/" + id_ingrijitor + "/Interventii").OrderByKey().OnceAsync<dynamic>();

            lista_recomandari = recomandare.Select(item => new RecomandareModel
            {
                alte_indicatii = item.Object.alte_indicatii,
                durata = item.Object.durata,
                id_recomandare = item.Key,
                id_pacient = item.Object.id_pacient,
                tipul_recomandarii = item.Object.tipul_recomandarii
            }).ToList();

            lista_interventii = interventie.Select(item => new InterventieModel
            {
                descriere = item.Object.descriere,
                id_interventie = item.Key,
                id_ingrijitor = item.Object.id_ingrijitor,
                id_pacient = item.Object.id_pacient,
                tip = item.Object.tip,
                stadiu = item.Object.stadiu
            }).ToList();

            ViewBag.recomandariLista = lista_recomandari;
            ViewBag.interventiiLista = lista_interventii;


            ViewBag.id_pacient = id_pacient;
            ViewBag.id_ingrijitor = id_ingrijitor;
            return View();
        }


        public async Task<IActionResult> RecomandariPacient(string id_pacient)
        {
            List<RecomandareModel> lista_recomandari = new List<RecomandareModel>();

            var recomandare = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/Recomandari").OrderByKey().OnceAsync<dynamic>();

            lista_recomandari = recomandare.Select(item => new RecomandareModel
            {
                alte_indicatii = item.Object.alte_indicatii,
                durata = item.Object.durata,
                id_recomandare = item.Key,
                id_pacient = item.Object.id_pacient,
                tipul_recomandarii = item.Object.tipul_recomandarii
            }).ToList();



            ViewBag.recomandariLista = lista_recomandari;


            ViewBag.id_pacient = id_pacient;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RecomandariSiInterventii(string tipuri, InterventieModel interventie, RecomandareModel recomandare)
        {
            Random rnd = new Random();
            recomandare.id_recomandare = rnd.Next(99999).ToString();
            interventie.id_interventie = rnd.Next(99999).ToString();
            interventie.stadiu = "Necompletat";
            switch (tipuri)
            {
                case "recomandare":
                    await firebase.Child("Conturi/Pacienti/" + recomandare.id_pacient + "/Recomandari/" + recomandare.id_recomandare).PutAsync<RecomandareModel>(recomandare);
                    TempData["Mesaj"] = "Recomandarea s-a adaugat cu succes!";

                    break;
                case "interventie":
                    await firebase.Child("Conturi/Ingrijitori/" + interventie.id_ingrijitor + "/Interventii/" + interventie.id_interventie).PutAsync<InterventieModel>(interventie);
                    TempData["Mesaj"] = "Interventia s-a adaugat cu succes!";

                    break;
            }

            List<RecomandareModel> lista_recomandari = new List<RecomandareModel>();
            List<InterventieModel> lista_interventii = new List<InterventieModel>();

            var query_recomandare = await firebase.Child("Conturi/Pacienti/" + recomandare.id_pacient + "/Recomandari").OrderByKey().OnceAsync<dynamic>();
            var query_interventie = await firebase.Child("Conturi/Ingrijitori/" + interventie.id_ingrijitor + "/Interventii").OrderByKey().OnceAsync<dynamic>();

            lista_recomandari = query_recomandare.Select(item => new RecomandareModel
            {
                alte_indicatii = item.Object.alte_indicatii,
                durata = item.Object.durata,
                id_recomandare = item.Key,
                id_pacient = item.Object.id_pacient,
                tipul_recomandarii = item.Object.tipul_recomandarii
            }).ToList();

            lista_interventii = query_interventie.Select(item => new InterventieModel
            {
                descriere = item.Object.descriere,
                id_interventie = item.Key,
                id_ingrijitor = item.Object.id_ingrijitor,
                id_pacient = item.Object.id_pacient,
                tip = item.Object.tip,
                stadiu = item.Object.stadiu
            }).ToList();

            ViewBag.recomandariLista = lista_recomandari;
            ViewBag.interventiiLista = lista_interventii;
            ViewBag.id_pacient = recomandare.id_pacient;
            ViewBag.id_ingrijitor = interventie.id_ingrijitor;

            return View();
        }

        [HttpPost("~/Pacient/StergereRecomandare/{id_uriRecomandari}")]
        public async Task<IActionResult> StergereRecomandare(string id_uriRecomandari)
        {
            var splitul = id_uriRecomandari.Split("#");
            string id_recomandare = splitul[0];
            string id_pacient = splitul[1];
            string id_ingrijitor = "";
            await firebase.Child("Conturi/Pacienti/" + id_pacient + "/Recomandari/" + id_recomandare).DeleteAsync();


            List<RecomandareModel> lista_recomandari = new List<RecomandareModel>();
            var recomandari = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/Recomandari").OrderByKey().OnceAsync<dynamic>();

            lista_recomandari = recomandari.Select(item => new RecomandareModel
            {
                alte_indicatii = item.Object.alte_indicatii,
                durata = item.Object.durata,
                id_recomandare = item.Key,
                id_pacient = item.Object.id_pacient,
                tipul_recomandarii = item.Object.tipul_recomandarii
            }).ToList();


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
                    id_ingrijitor = pacient.id_ingrijitor;
                }
            }

            List<InterventieModel> lista_interventii = new List<InterventieModel>();
            var query_interventie = await firebase.Child("Conturi/Ingrijitori/" + id_ingrijitor + "/Interventii").OrderByKey().OnceAsync<dynamic>();

            lista_interventii = query_interventie.Select(item => new InterventieModel
            {
                descriere = item.Object.descriere,
                id_interventie = item.Key,
                id_ingrijitor = item.Object.id_ingrijitor,
                id_pacient = item.Object.id_pacient,
                tip = item.Object.tip,
                stadiu = item.Object.stadiu
            }).ToList();

            ViewBag.recomandariLista = lista_recomandari;
            ViewBag.interventiiLista = lista_interventii;

            ViewBag.id_pacient = id_pacient;
            ViewBag.id_ingrijitor = id_ingrijitor;
            TempData["Mesaj"] = "Recomandarea a fost stearsa.";

            return RedirectToAction("RecomandariSiInterventii", "Pacient", new { id_pacient = id_pacient, id_ingrijitor = id_ingrijitor });
        }

        [HttpPost("~/Pacient/StergereInterventie/{id_uriInterventii}")]
        public async Task<IActionResult> StergereInterventie(string id_uriInterventii)
        {
            var splitul = id_uriInterventii.Split("#");
            string id_interventie = splitul[0];
            string id_ingrijitor = splitul[1];
            string id_pacient = splitul[2];

            await firebase.Child("Conturi/Ingrijitori/" + id_ingrijitor + "/Interventii/" + id_interventie).DeleteAsync();


            List<RecomandareModel> lista_recomandari = new List<RecomandareModel>();
            var recomandari = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/Recomandari").OrderByKey().OnceAsync<dynamic>();

            lista_recomandari = recomandari.Select(item => new RecomandareModel
            {
                alte_indicatii = item.Object.alte_indicatii,
                durata = item.Object.durata,
                id_recomandare = item.Key,
                id_pacient = item.Object.id_pacient,
                tipul_recomandarii = item.Object.tipul_recomandarii
            }).ToList();

            List<InterventieModel> lista_interventii = new List<InterventieModel>();
            var query_interventie = await firebase.Child("Conturi/Ingrijitori/" + id_ingrijitor + "/Interventii").OrderByKey().OnceAsync<dynamic>();

            lista_interventii = query_interventie.Select(item => new InterventieModel
            {
                descriere = item.Object.descriere,
                id_interventie = item.Key,
                id_ingrijitor = item.Object.id_ingrijitor,
                id_pacient = item.Object.id_pacient,
                tip = item.Object.tip,
                stadiu = item.Object.stadiu
            }).ToList();

            ViewBag.recomandariLista = lista_recomandari;
            ViewBag.interventiiLista = lista_interventii;

            ViewBag.id_pacient = id_pacient;
            ViewBag.id_ingrijitor = id_ingrijitor;

            TempData["Mesaj"] = "Interventia a fost stearsa.";

            return RedirectToAction("RecomandariSiInterventii", "Pacient", new { id_pacient = id_pacient, id_ingrijitor = id_ingrijitor });
        }

        public async Task<IActionResult> GraficeRapoarte(string id_pacient)
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



            var valori_puls = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/ValoriPuls").OrderByKey().OnceAsync<dynamic>();

            List<ValoriPuls> valoriPuls = new List<ValoriPuls>();
            List<string> listaValoriPuls = new List<string>();
            List<string> listaDatePuls = new List<string>();
            List<string> listaLipitePuls = new List<string>();

            valoriPuls = valori_puls.Select(item => new ValoriPuls
            {
                data = item.Object.data,
                value = item.Object.value
            }).ToList();

            foreach (ValoriPuls val in valoriPuls)
            {
                listaValoriPuls.Add(val.value);
                listaDatePuls.Add(val.data);
                listaLipitePuls.Add(val.data + "#" + val.value);
            }

            var valori_mediu = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/ValoriMediu").OrderByKey().OnceAsync<dynamic>();

            List<ValoriMediu> valoriMediu = new List<ValoriMediu>();
            List<string> listaValoriGaz = new List<string>();
            List<string> listaValoriTemperatura = new List<string>();
            List<string> listaValoriPrezenta = new List<string>();
            List<string> listaValoriUmiditate = new List<string>();
            List<string> listaDateMediu = new List<string>();
            List<string> listaLipiteMediu = new List<string>();


            valoriMediu = valori_mediu.Select(item => new ValoriMediu
            {
                data = item.Object.data,
                value_gaz = item.Object.value_gaz,
                value_prezenta = item.Object.value_prezenta,
                value_temperatura = item.Object.value_temperatura,
                value_umiditate = item.Object.value_umiditate,
            }).ToList();

            foreach (ValoriMediu val in valoriMediu)
            {
                listaValoriGaz.Add(val.value_gaz);
                listaValoriTemperatura.Add(val.value_temperatura);
                if (val.value_prezenta == "Detectata")
                    listaValoriPrezenta.Add("1");
                else
                    listaValoriPrezenta.Add("0");
                listaValoriUmiditate.Add(val.value_umiditate);
                listaDateMediu.Add(val.data);
                listaLipiteMediu.Add(val.data + "#" + val.value_gaz + "#" + val.value_temperatura + "#" + val.value_prezenta + "#" + val.value_umiditate);
            }


            ViewBag.datePuls = JsonConvert.SerializeObject(listaDatePuls);
            ViewBag.valoriPuls = JsonConvert.SerializeObject(listaValoriPuls);
            ViewBag.listaLipitePuls = JsonConvert.SerializeObject(listaLipitePuls);

            ViewBag.dateMediu = JsonConvert.SerializeObject(listaDateMediu);
            ViewBag.valoriGaz = JsonConvert.SerializeObject(listaValoriGaz);
            ViewBag.valoriUmiditate = JsonConvert.SerializeObject(listaValoriUmiditate);
            ViewBag.valoriPrezenta = JsonConvert.SerializeObject(listaValoriPrezenta);
            ViewBag.valoriTemperatura = JsonConvert.SerializeObject(listaValoriTemperatura);
            ViewBag.listaLipiteMediu = JsonConvert.SerializeObject(listaLipiteMediu);

            return View();
        }

        public async Task<IActionResult> GraficeRapoartePacient(string id_pacient)
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



            var valori_puls = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/ValoriPuls").OrderByKey().OnceAsync<dynamic>();

            List<ValoriPuls> valoriPuls = new List<ValoriPuls>();
            List<string> listaValoriPuls = new List<string>();
            List<string> listaDatePuls = new List<string>();
            List<string> listaLipitePuls = new List<string>();

            valoriPuls = valori_puls.Select(item => new ValoriPuls
            {
                data = item.Object.data,
                value = item.Object.value
            }).ToList();

            foreach (ValoriPuls val in valoriPuls)
            {
                listaValoriPuls.Add(val.value);
                listaDatePuls.Add(val.data);
                listaLipitePuls.Add(val.data + "#" + val.value);
            }

            var valori_mediu = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/ValoriMediu").OrderByKey().OnceAsync<dynamic>();

            List<ValoriMediu> valoriMediu = new List<ValoriMediu>();
            List<string> listaValoriGaz = new List<string>();
            List<string> listaValoriTemperatura = new List<string>();
            List<string> listaValoriPrezenta = new List<string>();
            List<string> listaValoriUmiditate = new List<string>();
            List<string> listaDateMediu = new List<string>();
            List<string> listaLipiteMediu = new List<string>();


            valoriMediu = valori_mediu.Select(item => new ValoriMediu
            {
                data = item.Object.data,
                value_gaz = item.Object.value_gaz,
                value_prezenta = item.Object.value_prezenta,
                value_temperatura = item.Object.value_temperatura,
                value_umiditate = item.Object.value_umiditate,
            }).ToList();

            foreach (ValoriMediu val in valoriMediu)
            {
                listaValoriGaz.Add(val.value_gaz);
                listaValoriTemperatura.Add(val.value_temperatura);
                if (val.value_prezenta == "Detectata")
                    listaValoriPrezenta.Add("1");
                else
                    listaValoriPrezenta.Add("0");
                listaValoriUmiditate.Add(val.value_umiditate);
                listaDateMediu.Add(val.data);
                listaLipiteMediu.Add(val.data + "#" + val.value_gaz + "#" + val.value_temperatura + "#" + val.value_prezenta + "#" + val.value_umiditate);
            }


            ViewBag.datePuls = JsonConvert.SerializeObject(listaDatePuls);
            ViewBag.valoriPuls = JsonConvert.SerializeObject(listaValoriPuls);
            ViewBag.listaLipitePuls = JsonConvert.SerializeObject(listaLipitePuls);

            ViewBag.dateMediu = JsonConvert.SerializeObject(listaDateMediu);
            ViewBag.valoriGaz = JsonConvert.SerializeObject(listaValoriGaz);
            ViewBag.valoriUmiditate = JsonConvert.SerializeObject(listaValoriUmiditate);
            ViewBag.valoriPrezenta = JsonConvert.SerializeObject(listaValoriPrezenta);
            ViewBag.valoriTemperatura = JsonConvert.SerializeObject(listaValoriTemperatura);
            ViewBag.listaLipiteMediu = JsonConvert.SerializeObject(listaLipiteMediu);

            return View();
        }
        public async Task<IActionResult> VizualizareDateNormale(string id_pacient, string id_supraveghetor)
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

            var query_valori_normale = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/ValoriNormale").OrderByKey().OnceAsync<dynamic>();

            List<ValoareNormala> listaValoriNormale = new List<ValoareNormala>();

            listaValoriNormale = query_valori_normale.Select(item => new ValoareNormala
            {
                valoare = item.Object.valoare,
                tip = item.Key
            }).ToList();

            ViewBag.listaValoriNormale = listaValoriNormale;
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> AdaugareValoareNormala(ValoareNormala valoareNormala, string id_pacient, string id_supraveghetor)
        {

            await firebase.Child("Conturi/Pacienti/" + id_pacient + "/ValoriNormale/" + valoareNormala.tip).PutAsync<ValoareNormala>(valoareNormala);

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

            var query_valori_normale = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/ValoriNormale").OrderByKey().OnceAsync<dynamic>();

            List<ValoareNormala> listaValoriNormale = new List<ValoareNormala>();

            listaValoriNormale = query_valori_normale.Select(item => new ValoareNormala
            {
                valoare = item.Object.valoare,
                tip = item.Key
            }).ToList();

            TempData["Mesaj"] = "Adaugare s-a realizat cu succes.";

            ViewBag.listaValoriNormale = listaValoriNormale;
            return RedirectToAction("VizualizareDateNormale", "Pacient", new { id_pacient = id_pacient, id_supraveghetor = id_supraveghetor });

        }

        [HttpPost("~/Pacient/StergereValoareNormala/{id_uri}")]
        public async Task<IActionResult> StergereValoareNormala(string id_uri)
        {
            var splitul = id_uri.Split("#");
            string tip = splitul[0];
            string valoare = splitul[1];
            string id_pacient = splitul[2];
            string id_supraveghetor = splitul[3];
            await firebase.Child("Conturi/Pacienti/" + id_pacient + "/ValoriNormale/" + tip).DeleteAsync();

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

            var query_valori_normale = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/ValoriNormale").OrderByKey().OnceAsync<dynamic>();

            List<ValoareNormala> listaValoriNormale = new List<ValoareNormala>();

            listaValoriNormale = query_valori_normale.Select(item => new ValoareNormala
            {
                valoare = item.Object.valoare,
                tip = item.Key
            }).ToList();

            TempData["Mesaj"] = "Valoarea s-a sters cu succes.";

            ViewBag.listaValoriNormale = listaValoriNormale;
            return RedirectToAction("VizualizareDateNormale", "Pacient", new { id_pacient = id_pacient, id_supraveghetor = id_supraveghetor });

        }

        public async Task<IActionResult> VizualizareAlarme(string id_pacient, string id_supraveghetor)
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

            var query_alarme = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/AlarmeMedic").OrderByKey().OnceAsync<dynamic>();

            List<ValoareNormala> listaAlarme = new List<ValoareNormala>();

            listaAlarme = query_alarme.Select(item => new ValoareNormala
            {
                valoare = item.Object.valoare,
                tip = item.Key
            }).ToList();

            ViewBag.listaAlarme = listaAlarme;
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> AdaugareAlarma(ValoareNormala alarma, string id_pacient, string id_supraveghetor)
        {
            await firebase.Child("Conturi/Pacienti/" + id_pacient + "/AlarmeMedic/" + alarma.tip).PutAsync<ValoareNormala>(alarma);

            var valori_puls = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/ValoriPuls").OrderByKey().OnceAsync<dynamic>();

            List<ValoriPuls> valoriPuls = new List<ValoriPuls>();

            valoriPuls = valori_puls.Select(item => new ValoriPuls
            {
                data = item.Object.data,
                value = item.Object.value
            }).ToList();

            var valori_mediu = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/ValoriMediu").OrderByKey().OnceAsync<dynamic>();

            List<ValoriMediu> valoriMediu = new List<ValoriMediu>();

            valoriMediu = valori_mediu.Select(item => new ValoriMediu
            {
                data = item.Object.data,
                value_gaz = item.Object.value_gaz,
                value_prezenta = item.Object.value_prezenta,
                value_temperatura = item.Object.value_temperatura,
                value_umiditate = item.Object.value_umiditate,
            }).ToList();

            List<ValoareNormala> listaAlarmeSupraveghetor = new List<ValoareNormala>();
            Random rnd = new Random();

            var alarme_existente = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/AlarmeSupraveghetor").OrderByKey().OnceAsync<dynamic>();

            List<ValoareNormala> alarmeExistente = new List<ValoareNormala>();

            alarmeExistente = alarme_existente.Select(item => new ValoareNormala
            {
                tip = item.Object.tip,
                valoare = item.Object.valoare,
                id = item.Key,
                status = item.Object.status,
                observatii = item.Object.observatii

            }).ToList();


            if (alarma.tip == "puls")
            {
                foreach (ValoriPuls puls in valoriPuls)
                {
                    if (float.Parse(puls.value) >= float.Parse(alarma.valoare))
                    {
                        ValoareNormala obiect = new ValoareNormala
                        {
                            valoare = puls.value,
                            tip = alarma.tip,
                            observatii = alarma.observatii,
                            id = rnd.Next(9999999).ToString()
                        };

                        obiect.status = "Necompletat";
                        await firebase.Child("Conturi/Pacienti/" + id_pacient + "/AlarmeSupraveghetor/" + obiect.id).PutAsync<ValoareNormala>(obiect);
                        listaAlarmeSupraveghetor.Add(obiect);

                    }
                }
            }
            else if (alarma.tip == "temperatura")
            {
                foreach (ValoriMediu vm in valoriMediu)
                {
                    if (float.Parse(vm.value_temperatura) >= float.Parse(alarma.valoare))
                    {
                        ValoareNormala obiect = new ValoareNormala
                        {
                            valoare = vm.value_temperatura,
                            tip = alarma.tip,
                            observatii = alarma.observatii,
                            id = rnd.Next(9999999).ToString()
                        };

                        obiect.status = "Necompletat";
                        await firebase.Child("Conturi/Pacienti/" + id_pacient + "/AlarmeSupraveghetor/" + obiect.id).PutAsync<ValoareNormala>(obiect);
                        listaAlarmeSupraveghetor.Add(obiect);

                    }
                }

            }
            else if (alarma.tip == "gaz")
            {
                foreach (ValoriMediu vm in valoriMediu)
                {
                    if (float.Parse(vm.value_gaz) >= float.Parse(alarma.valoare))
                    {
                        ValoareNormala obiect = new ValoareNormala
                        {
                            valoare = vm.value_gaz,
                            tip = alarma.tip,
                            observatii = alarma.observatii,
                            id = rnd.Next(9999999).ToString()
                        };

                        obiect.status = "Necompletat";
                        await firebase.Child("Conturi/Pacienti/" + id_pacient + "/AlarmeSupraveghetor/" + obiect.id).PutAsync<ValoareNormala>(obiect);
                        listaAlarmeSupraveghetor.Add(obiect);

                    }
                }

            }
            else if (alarma.tip == "umiditate")
            {
                foreach (ValoriMediu vm in valoriMediu)
                {
                    if (float.Parse(vm.value_umiditate) >= float.Parse(alarma.valoare))
                    {
                        ValoareNormala obiect = new ValoareNormala
                        {
                            valoare = vm.value_umiditate,
                            tip = alarma.tip,
                            observatii = alarma.observatii,
                            id = rnd.Next(9999999).ToString()
                        };

                        obiect.status = "Necompletat";
                        await firebase.Child("Conturi/Pacienti/" + id_pacient + "/AlarmeSupraveghetor/" + obiect.id).PutAsync<ValoareNormala>(obiect);
                        listaAlarmeSupraveghetor.Add(obiect);

                    }
                }


            }



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

            var query_alarme = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/AlarmeMedic").OrderByKey().OnceAsync<dynamic>();

            List<ValoareNormala> listaAlarme = new List<ValoareNormala>();

            listaAlarme = query_alarme.Select(item => new ValoareNormala
            {
                valoare = item.Object.valoare,
                tip = item.Key
            }).ToList();

            ViewBag.listaAlarme = listaAlarme;
            TempData["Mesaj"] = "Valoarea s-a adaugat cu succes.";

            return RedirectToAction("VizualizareAlarme", "Pacient", new { id_pacient = id_pacient, id_supraveghetor = id_supraveghetor });


        }

        [HttpPost("~/Pacient/StergereAlarma/{id_uri}")]
        public async Task<IActionResult> StergereAlarma(string id_uri)
        {
            var splitul = id_uri.Split("#");
            string tip = splitul[0];
            string valoare = splitul[1];
            string id_pacient = splitul[2];
            string id_supraveghetor = splitul[3];
            await firebase.Child("Conturi/Pacienti/" + id_pacient + "/AlarmeMedic/" + tip).DeleteAsync();

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

            var query_alarme = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/AlarmeMedic").OrderByKey().OnceAsync<dynamic>();

            List<ValoareNormala> listaAlarme = new List<ValoareNormala>();

            listaAlarme = query_alarme.Select(item => new ValoareNormala
            {
                valoare = item.Object.valoare,
                tip = item.Key
            }).ToList();

            ViewBag.listaAlarme = listaAlarme;
            TempData["Mesaj"] = "Valoarea s-a sters cu succes.";


            return RedirectToAction("VizualizareAlarme", "Pacient", new { id_pacient = id_pacient, id_supraveghetor = id_supraveghetor });


        }


        public async Task<IActionResult> AccesarePacientSupraveghetor(string id_pacient)
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


        public async Task<IActionResult> FisaPacientSupraveghetor(string id_pacient)
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

            var query_consultatii = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/Consultatii").OrderByKey().StartAt("1").OnceAsync<dynamic>();

            List<ConsultatieModel> listaConsultatii = new List<ConsultatieModel>();

            listaConsultatii = query_consultatii.Select(item => new ConsultatieModel
            {
                id_consultatie = item.Key,
                motiv_prezentare_consultatie = item.Object.motiv_prezentare_consultatie,
                simptome_consultatie = item.Object.simptome_consultatie,
                diagnostic_consultatie = item.Object.diagnostic_consultatie,
                data_consultatie = item.Object.data_consultatie,
                trimitere_consultatie = item.Object.trimitere_consultatie,
                retete_generate_consultatie = item.Object.retete_generate_consultatie
            }).ToList();
            ViewBag.listaConsultatii = listaConsultatii;

            return View();
        }

        public async Task<IActionResult> GraficeRapoarteSupraveghetor(string id_pacient)
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



            var valori_puls = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/ValoriPuls").OrderByKey().OnceAsync<dynamic>();

            List<ValoriPuls> valoriPuls = new List<ValoriPuls>();
            List<string> listaValoriPuls = new List<string>();
            List<string> listaDatePuls = new List<string>();
            List<string> listaLipitePuls = new List<string>();

            valoriPuls = valori_puls.Select(item => new ValoriPuls
            {
                data = item.Object.data,
                value = item.Object.value
            }).ToList();

            foreach (ValoriPuls val in valoriPuls)
            {
                listaValoriPuls.Add(val.value);
                listaDatePuls.Add(val.data);
                listaLipitePuls.Add(val.data + "#" + val.value);
            }

            var valori_mediu = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/ValoriMediu").OrderByKey().OnceAsync<dynamic>();

            List<ValoriMediu> valoriMediu = new List<ValoriMediu>();
            List<string> listaValoriGaz = new List<string>();
            List<string> listaValoriTemperatura = new List<string>();
            List<string> listaValoriPrezenta = new List<string>();
            List<string> listaValoriUmiditate = new List<string>();
            List<string> listaDateMediu = new List<string>();
            List<string> listaLipiteMediu = new List<string>();


            valoriMediu = valori_mediu.Select(item => new ValoriMediu
            {
                data = item.Object.data,
                value_gaz = item.Object.value_gaz,
                value_prezenta = item.Object.value_prezenta,
                value_temperatura = item.Object.value_temperatura,
                value_umiditate = item.Object.value_umiditate,
            }).ToList();

            foreach (ValoriMediu val in valoriMediu)
            {
                listaValoriGaz.Add(val.value_gaz);
                listaValoriTemperatura.Add(val.value_temperatura);
                if (val.value_prezenta == "Detectata")
                    listaValoriPrezenta.Add("1");
                else
                    listaValoriPrezenta.Add("0");
                listaValoriUmiditate.Add(val.value_umiditate);
                listaDateMediu.Add(val.data);
                listaLipiteMediu.Add(val.data + "#" + val.value_gaz + "#" + val.value_temperatura + "#" + val.value_prezenta + "#" + val.value_umiditate);
            }


            ViewBag.datePuls = JsonConvert.SerializeObject(listaDatePuls);
            ViewBag.valoriPuls = JsonConvert.SerializeObject(listaValoriPuls);
            ViewBag.listaLipitePuls = JsonConvert.SerializeObject(listaLipitePuls);

            ViewBag.dateMediu = JsonConvert.SerializeObject(listaDateMediu);
            ViewBag.valoriGaz = JsonConvert.SerializeObject(listaValoriGaz);
            ViewBag.valoriUmiditate = JsonConvert.SerializeObject(listaValoriUmiditate);
            ViewBag.valoriPrezenta = JsonConvert.SerializeObject(listaValoriPrezenta);
            ViewBag.valoriTemperatura = JsonConvert.SerializeObject(listaValoriTemperatura);
            ViewBag.listaLipiteMediu = JsonConvert.SerializeObject(listaLipiteMediu);

            return View();
        }


        public async Task<IActionResult> VizualizareAlarmeSupraveghetor(string id_pacient, string id_supraveghetor)
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

            var query_alarmeSupraveghetor = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/AlarmeSupraveghetor").OrderByKey().OnceAsync<dynamic>();

            List<ValoareNormala> listaAlarmeSupraveghetor = new List<ValoareNormala>();

            listaAlarmeSupraveghetor = query_alarmeSupraveghetor.Select(item => new ValoareNormala
            {
                valoare = item.Object.valoare,
                tip = item.Object.tip,
                status = item.Object.status,
                observatii = item.Object.observatii,
                id = item.Key
            }).ToList();



            ViewBag.listaAlarmeSupraveghetor = listaAlarmeSupraveghetor.OrderByDescending(o => o.status).ToList();
            return View();

        }

        public async Task<IActionResult> VizualizareAlarmePacient(string id_pacient, string id_supraveghetor)
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

            var query_alarmeSupraveghetor = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/AlarmeSupraveghetor").OrderByKey().OnceAsync<dynamic>();

            List<ValoareNormala> listaAlarmeSupraveghetor = new List<ValoareNormala>();

            listaAlarmeSupraveghetor = query_alarmeSupraveghetor.Select(item => new ValoareNormala
            {
                valoare = item.Object.valoare,
                tip = item.Object.tip,
                status = item.Object.status,
                observatii = item.Object.observatii,
                id = item.Key
            }).ToList();



            ViewBag.listaAlarmeSupraveghetor = listaAlarmeSupraveghetor.OrderByDescending(o => o.status).ToList();
            return View();

        }


        [HttpPost("~/Pacient/RezolvareAlarma/{id_uri}")]
        public async Task<IActionResult> RezolvareAlarma(string id_uri, string observatii)
        {
            var splitul = id_uri.Split("#");
            string id_pacient = splitul[0];
            string id_supraveghetor = splitul[1];
            string id_alarma = splitul[2];

            DateTime dateTime = DateTime.UtcNow.Date;
            string dataCurenta = dateTime.ToString("dd/MM/yyyy").ToString();

            await firebase.Child("Conturi/Pacienti/" + id_pacient + "/AlarmeSupraveghetor/" + id_alarma + "/status").PutAsync<string>("Completat în data de " + dataCurenta);
            await firebase.Child("Conturi/Pacienti/" + id_pacient + "/AlarmeSupraveghetor/" + id_alarma + "/observatii").PutAsync<string>(observatii);

            return RedirectToAction("VizualizareAlarmeSupraveghetor", new { id_pacient = id_pacient, id_supraveghetor = id_supraveghetor });
        }


        public async Task<IActionResult> IstoricValoriPacient(string id_pacient)
        {
            var valori_puls = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/ValoriPuls").OrderByKey().OnceAsync<dynamic>();

            List<ValoriPuls> valoriPuls = new List<ValoriPuls>();

            valoriPuls = valori_puls.Select(item => new ValoriPuls
            {
                data = item.Object.data,
                value = item.Object.value
            }).ToList();


            var valori_mediu = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/ValoriMediu").OrderByKey().OnceAsync<dynamic>();

            List<ValoriMediu> valoriMediu = new List<ValoriMediu>();


            valoriMediu = valori_mediu.Select(item => new ValoriMediu
            {
                data = item.Object.data,
                value_gaz = item.Object.value_gaz,
                value_prezenta = item.Object.value_prezenta,
                value_temperatura = item.Object.value_temperatura,
                value_umiditate = item.Object.value_umiditate,
            }).ToList();

            ViewBag.mediuLista = valoriMediu;
            ViewBag.pulsLista = valoriPuls;
            return View();
        }
    }
}
