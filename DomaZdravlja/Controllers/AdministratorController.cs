using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomaZdravlja.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Session;
using DomaZdravlja.Models.Logika;

namespace DomaZdravlja.Controllers
{
    public class AdministratorController : Controller
    {
        public static string idKorisnik = "";
        public HttpContext Http { get; set; }
        //Dodaci za lokalizaciju
        public IStringLocalizer<Resource> localizer;
        public AdministratorController(IStringLocalizer<Resource> localizer)
        {
            this.localizer = localizer;
        }
        public IActionResult SetCulture(string culture, string sourceUrl)
        {
            Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            //new CookieOptions { Expires = DateTimeOffset.UtcNow.AddSeconds(30) });
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            return Redirect(sourceUrl);
        }

        private Administrator admin = new Administrator();
        public IActionResult Index()
        {
            Sesija s = new Sesija();
            try
            {
                idKorisnik = TempData["id"].ToString();
            }
            catch (Exception) { }
            if (s.GetSession(HttpContext, idKorisnik))
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "Logovanje");
            }
        }
        public JsonResult GetDoktori(int str, int brPrikaz)
        {
            List<Korisnik> korisnici = admin.GetKorisnici(str, brPrikaz);
            return Json(korisnici);
        }
        public double BrojStrana()
        {
            using (var db = new DomZdravljaContext())
            {
                //int brojStrana = db.Korisnik.ToList().Count;
                int brojStrana = db.Korisnik.Where(m=> m.IdRola == 3 && m.AktivanKorisnik == 1).ToList().Count;
                double broj = brojStrana / 3;
                return broj;
            }
        }
        public JsonResult DodajDoktora(string ime, string prezime, string email, string password, string specijalnost)
      {
            RegexValidacija reg = new RegexValidacija();

            if (!string.IsNullOrWhiteSpace(ime))
            {
                if (reg.RegexIme(ime))
                {
                    if (!string.IsNullOrWhiteSpace(prezime))
                    {
                        if (reg.RegexPrezime(prezime))
                        {
                            if (!string.IsNullOrWhiteSpace(email))
                            {
                                if (reg.RegexEmail(email))
                                {
                                    if (!string.IsNullOrWhiteSpace(password))
                                    {
                                        if (reg.RegexPassword(password))
                                        {
                                            if (!string.IsNullOrWhiteSpace(specijalnost))
                                            {
                                                if (reg.RegexSpecijalnost(specijalnost))
                                                {
                                                    if (admin.DodajDoktora(ime, prezime, email, password, specijalnost))
                                                    {
                                                        return Json("12");
                                                    }
                                                    else
                                                    {
                                                        return Json("11");
                                                    }
                                                }
                                                else
                                                {
                                                    return Json("10");
                                                }
                                            }
                                            else
                                            {
                                                return Json("9");
                                            }
                                        }
                                        else
                                        {
                                            return Json("8");
                                        }
                                    }
                                    else
                                    {
                                        return Json("7");
                                    }
                                }
                                else
                                {
                                    return Json("6");
                                }
                            }
                            else
                            {
                                return Json("5");
                            }
                        }
                        else
                        {
                            return Json("4");
                        }
                    }
                    else
                    {
                        return Json("3");
                    }
                }
                else
                {
                    return Json("2");
                }
            }
            else
            {
                return Json("1");
            }
        }

        public JsonResult BrisanjeDoktora(string id)
        {
            if (admin.BrisanjeDoktora(Convert.ToInt32(id)))
            {
                return Json("Izabrani lekar više nije aktivan!");
            }
            else
            {
                return Json("Došlo je do greške, korisnik ne može biti deaktiviran!");
            }
        }

        public IActionResult MedicinskeSestre()
        {
            Sesija s = new Sesija();
            try
            {
                idKorisnik = TempData["id"].ToString();
            }
            catch (Exception) { }
            if (s.GetSession(HttpContext, idKorisnik))
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "Logovanje");
            }
        }
        public JsonResult GetMedicinskeSestre(int str, int brPrikaz)
        {
            List<Korisnik> korisnici = admin.GetMedicinskeSestre(str, brPrikaz);
            return Json(korisnici);
        }

        public double BrojStranaMedicinskeSestre()
        {
            using (var db = new DomZdravljaContext())
            {
                //int brojStrana = db.Korisnik.ToList().Count;
                int brojStrana = db.Korisnik.Where(m => m.IdRola == 2 && m.AktivanKorisnik == 1).ToList().Count;
                double broj = brojStrana / 3;
                return broj;
            }
        }

        public JsonResult DodajMedicinskuSestru(string ime, string prezime, string email, string password, string specijalnost)
        {
            RegexValidacija reg = new RegexValidacija();

            if (!string.IsNullOrWhiteSpace(ime))
            {
                if (reg.RegexIme(ime))
                {
                    if (!string.IsNullOrWhiteSpace(prezime))
                    {
                        if (reg.RegexPrezime(prezime))
                        {
                            if (!string.IsNullOrWhiteSpace(email))
                            {
                                if (reg.RegexEmail(email))
                                {
                                    if (!string.IsNullOrWhiteSpace(password))
                                    {
                                        if (reg.RegexPassword(password))
                                        {
                                            if (!string.IsNullOrWhiteSpace(specijalnost))
                                            {
                                                if (reg.RegexSpecijalnost(specijalnost))
                                                {
                                                    if (admin.DodajMedicinskuSestru(ime, prezime, email, password, specijalnost))
                                                    {
                                                        return Json("12");
                                                    }
                                                    else
                                                    {
                                                        return Json("11");
                                                    }
                                                }
                                                else
                                                {
                                                    return Json("10");
                                                }
                                            }
                                            else
                                            {
                                                return Json("9");
                                            }
                                        }
                                        else
                                        {
                                            return Json("8");
                                        }
                                    }
                                    else
                                    {
                                        return Json("7");
                                    }
                                }
                                else
                                {
                                    return Json("6");
                                }
                            }
                            else
                            {
                                return Json("5");
                            }
                        }
                        else
                        {
                            return Json("4");
                        }
                    }
                    else
                    {
                        return Json("3");
                    }
                }
                else
                {
                    return Json("2");
                }
            }
            else
            {
                return Json("1");
            }
        }

        public JsonResult BrisanjeMedicinskeSestre(string id)
        {
            if (admin.BrisanjeMedicinskeSestre(Convert.ToInt32(id)))
            {
                return Json("Izabrani lekar više nije aktivan!");
            }
            else
            {
                return Json("Došlo je do greške, korisnik ne može biti deaktiviran!");
            }
        }

        public IActionResult IzlogujSE()
        {
            Sesija s = new Sesija();
            try
            {
                idKorisnik = TempData["id"].ToString();
            }
            catch (Exception) { }
            if (s.GetSession(HttpContext, idKorisnik))
            {
                HttpContext.Session.SetString(idKorisnik, "");
                return RedirectToAction("LogIn", "Logovanje");
            }
            else
            {
                return RedirectToAction("LogIn", "Logovanje");
            }
        }

        public IActionResult Pacijenti()
        {
            Sesija s = new Sesija();
            try
            {
                idKorisnik = TempData["id"].ToString();
            }
            catch (Exception) { }
            if (s.GetSession(HttpContext, idKorisnik))
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "Logovanje");
            }
        }

        public JsonResult GetPacijenti()
        {
            List<PacijentKorisnika> pacijenti = admin.GetPacijenti();
            return Json(pacijenti);
        }

        public JsonResult BrisanjePacijenta(string id)
        {
            using (var db = new DomZdravljaContext())
            {
                PreglediPacijenta pregled = db.PreglediPacijenta.Where(x => x.IdKarton == Convert.ToInt32(id) && x.AktivanPregled==1).SingleOrDefault();
                if (pregled != null)
                {
                    return Json("1");
                }
                else
                {
                    if (admin.BrisanjePacijentaObavljenpregled(Convert.ToInt32(id)))
                    {
                        return Json("2");
                    }
                    else
                    {
                        return Json("3");
                    }
                }
            }
        }
    }
}