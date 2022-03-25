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
    public class LekarController : Controller
    {
        public static string idKorisnik = "";
        Lekari lekar = new Lekari();
        //Dodaci za lokalizaciju
        public IStringLocalizer<Resource> localizer;
        public LekarController(IStringLocalizer<Resource> localizer)
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

        public JsonResult LekarPacijenti()
        {
            List<PreglediPacijentaKarton> preglediPacijentaKarton = lekar.PreglediPacijenata(Convert.ToInt32(idKorisnik));
            return Json(preglediPacijentaKarton);
        }
        public JsonResult StornirajPregled(string id)
        {
            if (lekar.StorniranjePregleda(Convert.ToInt32(id)))
            {
                return Json("");
            }
            else
            {
                return Json("");
            }
        }
        public JsonResult SacuvajIzvestaj(string nazivVrsteIzvestaja, string opisIzvestaj,
            string nazivUstanove, string lekarSpecijalista, string imePacijenta, string prezimePacijenta)

        {
            RegexValidacija reg = new RegexValidacija();
            if (!string.IsNullOrWhiteSpace(nazivVrsteIzvestaja))
            {
                if (reg.RegexNazivVrsteIzvestaja(nazivVrsteIzvestaja))
                {
                    if (!string.IsNullOrEmpty(opisIzvestaj))
                    {
                        if (!string.IsNullOrWhiteSpace(imePacijenta))
                        {
                            if (reg.RegexIme(imePacijenta))
                            {
                                if (!string.IsNullOrWhiteSpace(prezimePacijenta))
                                {
                                    if (reg.RegexPrezime(prezimePacijenta))
                                    {
                                        if (lekar.UnesiIzvestaj(nazivVrsteIzvestaja, opisIzvestaj,
                                            lekarSpecijalista, imePacijenta, prezimePacijenta, idKorisnik))
                                        {
                                            return Json("9");
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