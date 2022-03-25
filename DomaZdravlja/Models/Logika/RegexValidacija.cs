using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DomaZdravlja.Models.Logika
{
    public class RegexValidacija
    {
        public bool RegexBrojKnjizice(string brojknjizice)
        {
            //Puca na broju brojeva
           string provera = @"^\d{11}$";
            if (Regex.IsMatch(brojknjizice, provera))
            {
                return true;
            }
            else { return false; }
        }
        public bool RegexIme(string ime)
        {
            string provera = @"^[a-zA-Z]{3,20}$";
            if (Regex.IsMatch(ime, provera))
            {
                return true;
            }
            else { return false; }
        }
        public bool RegexPrezime(string prezime)
        {
            string provera = @"^[a-zA-Z]{3,20}$";
            if (Regex.IsMatch(prezime, provera))
            {
                return true;
            }
            else { return false; }
        }
        public bool RegexEmail(string email)
        {
            string provera = @"^[a-z0-9\.a-z0-9\-?]{1,11}\@[a-z]{2,6}\.[a-z]{2,3}$";
            if (Regex.IsMatch(email, provera))
            {
                return true;
            }
            else { return false; }
        }

        public bool RegexPassword(string password)
        {
            string provera = @"^[A-Z]+[a-z]+\d+$";
            if (Regex.IsMatch(password, provera))
            {
                return true;
            }
            else { return false; }
        }
        public bool RegexSpecijalnost(string specijalnost)
        {
            string provera = @"^[A-Z]+[a-z\s]+$";
            if (Regex.IsMatch(specijalnost, provera))
            {
                return true;
            }
            else { return false; }
        }

        public bool RegexNazivVrsteIzvestaja(string nazivVrsteIzvestaja)
        {
            string provera = @"^[A-Z]+[a-z\s]+$";
            if (Regex.IsMatch(nazivVrsteIzvestaja, provera))
            {
                return true;
            }
            else { return false; }
        }
    }
}
