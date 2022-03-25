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
using Newtonsoft.Json;

namespace DomaZdravlja.Controllers
{
    public class LogovanjeController : Controller
    {
        string sessionID = "";
        public HttpContext Http { get; set; }
        //Dodaci za lokalizaciju
        public IStringLocalizer<Resource> localizer;
        public LogovanjeController(IStringLocalizer<Resource> localizer)
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

        //Metode kontrolera
        public IActionResult Pocetna()
        {
            return View();
        }

        //[ValidateAntiForgeryToken]
        public IActionResult LogIn()
        {
            return View();
        }

        public IActionResult RedirektujKorisnika(string Email, string Password)
        {
            
            if (ModelState.IsValid)
            {
                Korisnik korisnik = Logovanje.RedirekcijaKorisnika(Email, Password);
                
                if (korisnik != null && korisnik.IdRola == 1)
                {
                    Sesija s = new Sesija();
                    TempData["id"] = korisnik.IdKorisnik;
                    if (s.CreateSession(HttpContext, korisnik.IdKorisnik.ToString())) { 
                    
                        return RedirectToAction("Index", "Administrator");
                    }
                    else
                    {
                        return RedirectToAction("LogIn", "Logovanje");
                    }
                }
                else if(korisnik != null && korisnik.IdRola == 2)
                {
                    Sesija s = new Sesija();
                    TempData["id"] = korisnik.IdKorisnik;
                    if (s.CreateSession(HttpContext, korisnik.IdKorisnik.ToString()))
                    {
                        return RedirectToAction("Index", "MedicinskaSestra");
                    }
                    else
                    {
                        return RedirectToAction("LogIn", "Logovanje");
                    }
                }
                else if (korisnik != null && korisnik.IdRola == 3)
                {
                    Sesija s = new Sesija();
                    TempData["id"] = korisnik.IdKorisnik;
                    if (s.CreateSession(HttpContext, korisnik.IdKorisnik.ToString()))
                    {
                        return RedirectToAction("Index", "Lekar");
                    }
                    else
                    {
                        return RedirectToAction("LogIn", "Logovanje");
                    }
                }
                else
                {
                    ViewBag.Admin = localizer["RedirekcijaKorisnika"];
                    return View("LogIn");
                }

            }
            else
            {
                return View();
            }
        }
    }
}