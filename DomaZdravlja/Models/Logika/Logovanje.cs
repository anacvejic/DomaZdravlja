using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomaZdravlja.Models
{
    public class Logovanje
    {
        public static Korisnik RedirekcijaKorisnika(string Email, string Password)
        {
          using(var db = new DomZdravljaContext())
            {
                try
                {
                    Korisnik korisnik = (from f in db.Korisnik
                                         where f.Email == Email && f.Password == Security.Encrypt(Password)
                                         select f).SingleOrDefault();
                    return korisnik;
                }
                catch (Exception) { throw; }
            }
        }
    }
}
