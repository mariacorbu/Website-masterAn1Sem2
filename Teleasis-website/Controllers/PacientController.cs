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
                nume_pacient = item.Object.nume_pacient,
                prenume_pacient = item.Object.prenume_pacient,
                CNP = item.Object.CNP,
                email_pacient = item.Object.email_pacient,
                id_medic = item.Object.id_medic,
                id_pacient = item.Key,
                judet_pacient = item.Object.judet_pacient,
                oras_pacient = item.Object.oras_pacient,
                strada_pacient = item.Object.strada_pacient,
                id_ingrijitor = item.Object.id_ingrijitor,
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
                id_ingrijitor = item.Object.id_ingrijitor,
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
                id_ingrijitor = item.Object.id_ingrijitor,
                strada_pacient = item.Object.strada_pacient,
                numar_telefon_pacient = item.Object.numar_telefon_pacient,
                profesie_pacient = item.Object.profesie_pacient,
                loc_de_munca_pacient = item.Object.loc_de_munca_pacient
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
                nume_pacient = item.Object.nume_pacient,
                prenume_pacient = item.Object.prenume_pacient,
                CNP = item.Object.CNP,
                email_pacient = item.Object.email_pacient,
                id_medic = item.Object.id_medic,
                id_pacient = item.Key,
                judet_pacient = item.Object.judet_pacient,
                oras_pacient = item.Object.oras_pacient,
                strada_pacient = item.Object.strada_pacient,
                id_ingrijitor = item.Object.id_ingrijitor,
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
                nume_pacient = item.Object.nume_pacient,
                prenume_pacient = item.Object.prenume_pacient,
                CNP = item.Object.CNP,
                email_pacient = item.Object.email_pacient,
                id_medic = item.Object.id_medic,
                id_pacient = item.Key,
                judet_pacient = item.Object.judet_pacient,
                oras_pacient = item.Object.oras_pacient,
                strada_pacient = item.Object.strada_pacient,
                id_ingrijitor = item.Object.id_ingrijitor,
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
                nume_pacient = item.Object.nume_pacient,
                prenume_pacient = item.Object.prenume_pacient,
                CNP = item.Object.CNP,
                email_pacient = item.Object.email_pacient,
                id_medic = item.Object.id_medic,
                id_pacient = item.Key,
                judet_pacient = item.Object.judet_pacient,
                oras_pacient = item.Object.oras_pacient,
                strada_pacient = item.Object.strada_pacient,
                id_ingrijitor = item.Object.id_ingrijitor,
                numar_telefon_pacient = item.Object.numar_telefon_pacient,
                profesie_pacient = item.Object.profesie_pacient,
                loc_de_munca_pacient = item.Object.loc_de_munca_pacient
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
                nume_pacient = item.Object.nume_pacient,
                prenume_pacient = item.Object.prenume_pacient,
                CNP = item.Object.CNP,
                email_pacient = item.Object.email_pacient,
                id_medic = item.Object.id_medic,
                id_pacient = item.Key,
                judet_pacient = item.Object.judet_pacient,
                oras_pacient = item.Object.oras_pacient,
                strada_pacient = item.Object.strada_pacient,
                id_ingrijitor = item.Object.id_ingrijitor,
                numar_telefon_pacient = item.Object.numar_telefon_pacient,
                profesie_pacient = item.Object.profesie_pacient,
                loc_de_munca_pacient = item.Object.loc_de_munca_pacient
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
                nume_pacient = item.Object.nume_pacient,
                prenume_pacient = item.Object.prenume_pacient,
                CNP = item.Object.CNP,
                email_pacient = item.Object.email_pacient,
                id_medic = item.Object.id_medic,
                id_pacient = item.Key,
                judet_pacient = item.Object.judet_pacient,
                oras_pacient = item.Object.oras_pacient,
                strada_pacient = item.Object.strada_pacient,
                id_ingrijitor = item.Object.id_ingrijitor,
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



            var valori_puls = await firebase.Child("Conturi/Pacienti/" + id_pacient + "/ValoriPuls").OrderByKey().OnceAsync<dynamic>();

            List<ValoriPuls> valoriPuls = new List<ValoriPuls>();
            List<string> listaValoriPuls = new List<string>();
            List<string> listaDatePuls = new List<string>();

            valoriPuls = valori_puls.Select(item => new ValoriPuls
            { 
                data = item.Object.data,
                value = item.Object.value
            }).ToList();

            foreach(ValoriPuls val in valoriPuls)
            {
                listaValoriPuls.Add(val.value);
                listaDatePuls.Add(val.data);
            }

            ViewBag.datePuls = listaDatePuls;
            ViewBag.valoriPuls = listaValoriPuls;

            return View();
        }
    }
}
