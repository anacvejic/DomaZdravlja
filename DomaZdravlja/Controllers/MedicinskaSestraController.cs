using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomaZdravlja.Models;
using DomaZdravlja.Models.Logika;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace DomaZdravlja.Controllers
{
    public class MedicinskaSestraController : Controller
    {
        public static string idKorisnik = "";
        private MedSestra md = new MedSestra();
        //Dodaci za lokalizaciju
        public IStringLocalizer<Resource> localizer;
        public MedicinskaSestraController(IStringLocalizer<Resource> localizer)
        {
            this.localizer = localizer;
        }
        public IActionResult SetCulture(string culture, string sourceUrl)
        {
            Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            return Redirect(sourceUrl);
        }
        public IActionResult Index(KartonPacijenta kartonPacijenta)
        {
            Sesija s = new Sesija();
            try
            {
                idKorisnik = TempData["id"].ToString();
            }
            catch (Exception) { }
            if (s.GetSession(HttpContext, idKorisnik))
            {
                if (ModelState.IsValid)
                {
                    if (md.DodajNovKarton(kartonPacijenta))
                    {
                        ViewBag.UspehDodavanjaKorisnika = localizer["UspehDodavanjaKartona"];
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.NeuspehDodavanjaKorisnika = localizer["NeuspexDodavanjaKartona"];
                        return View("Index");
                    }
                }
                else
                {
                    return View("Index");
                }
            }
            else
            {
                return RedirectToAction("LogIn", "Logovanje");
            }
        }

        public JsonResult GetDoktori()
        {
            return Json(md.GetLekari());
        }
       public IActionResult GetPacijenti()
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
        public JsonResult Pacijenti()
        {
            List<PacijentKorisnika> pacijenti = md.GetPacijenti();
            return Json(pacijenti);
        }

        //public JsonResult Pacijenti(int str, int brPrikaz)
        //{
        //    List<PacijentKorisnika> pacijenti = md.GetPacijenti(str, brPrikaz);
        //    return Json(pacijenti);
        //}
        //public double BrojStranaPacijenti()
        //{
        //    using (var db = new DomZdravljaContext())
        //    {
        //        //int brojStrana = db.Korisnik.ToList().Count;
        //        int brojStrana = db.KartonPacijenta.Where(m => m.AktivanKarton == 1).ToList().Count;
        //        double broj = brojStrana / 3;
        //        return broj;
        //    }
        //}

        public JsonResult BrisanjePacijenta(string id)
        {
            if (md.BrisnjePacijenta(Convert.ToInt32(id)))
            {
                return Json("Uspešno ste obrisali pacijenta");
            }
            else
            {
                return Json("Došlo je do greške!");
            }
        }

        public JsonResult Getlekari()
        {
            List<Korisnik> k = md.GetLekari();
            return Json(k);
        }
        public IActionResult Pregledipacijenata()
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
        public JsonResult PreglediPacijenataKarton()
        {
            List<PreglediPacijentaKarton> preglediPacijenata = md.PreglediPacijenata();
            return Json(preglediPacijenata);
        }
        public JsonResult IzmeniKartonPacijenta(string idKarton, string brojKnjizice, string prezime, string idKorisnik)
        {
            
            RegexValidacija reg = new RegexValidacija();
            Int64 vrednost;
            try
            {
                Int64 brKnjizica = Convert.ToInt64(brojKnjizice);
                if (Int64.TryParse(brojKnjizice, out vrednost) || brojKnjizice.Count() >= 15 || brojKnjizice.Count() <= 6)
                {
                    if (Convert.ToInt64(brojKnjizice) != 0)
                    {
                        if (!string.IsNullOrWhiteSpace(prezime))
                        {
                            if (reg.RegexPrezime(prezime))
                            {
                                if (md.IzmeniKartonPacijenta(idKarton, brojKnjizice, prezime, idKorisnik))
                                {
                                    return Json("5");
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
                else
                {
                    return Json("6");
                }
            }
            catch (Exception) { return Json("7"); }
        }

        public JsonResult ZakazivanjePregleda(PreglediPacijenta preglediPacijenta)
        {
            
            int a = preglediPacijenta.DatumIvremePregleda.Year;
            using (var db = new DomZdravljaContext())
            {
                //if (a != "1.1.0001. 00:00:00")
                if (a != 1)
                {
                    if (preglediPacijenta.IdKarton != 0)
                    {
                      //if(db.KartonPacijenta.Where(x=>x.IdKarton == preglediPacijenta.IdKarton).Count() > 1)
                      if(db.KartonPacijenta.Any(x=>x.IdKarton==preglediPacijenta.IdKarton))
                        {
                            if (md.UnosNovogPregledaPacijenta(preglediPacijenta))
                            {
                                return Json("4");
                            }
                            else
                            {
                                return Json("5");
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
    }
}