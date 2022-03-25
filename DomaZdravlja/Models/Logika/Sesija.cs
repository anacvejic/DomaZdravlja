using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomaZdravlja.Models.Logika
{
    public class Sesija
    {
        public bool GetSession(HttpContext httpContext, string idK)
        {
            var id = httpContext.Session.GetString(idK);
            if (!string.IsNullOrWhiteSpace(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CreateSession(HttpContext httpContext, string idK)
        {
            string sesija = idK;
            var value = httpContext.Session.GetString(sesija);
            if (string.IsNullOrWhiteSpace(value))
            {
                DateTime vreme = System.DateTime.Now;
                var sverVreme = JsonConvert.SerializeObject(vreme);
                httpContext.Session.SetString(sesija, sverVreme);
                //TempData["id"] = korisnik.IdKorisnik.ToString();
                return true;
            }
            else { return false; }
        }
    }
}
