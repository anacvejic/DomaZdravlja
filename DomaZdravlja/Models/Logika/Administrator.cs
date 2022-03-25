using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomaZdravlja.Models
{
    public class Administrator
    {
        
        public List<Korisnik> GetKorisnici(int str, int brPrikaz)
        {
           using(var db = new DomZdravljaContext())
            {
                try
                {
                    List<Korisnik> korisnici = (from m in db.Korisnik
                                                where m.IdRola == 3 && m.AktivanKorisnik == 1
                                                select m).Skip((str - 1) * brPrikaz).Take(brPrikaz).ToList();
                    return korisnici;
                }
                catch (Exception) { throw; }
            }
        }

        public bool DodajDoktora(string ime, string prezime, string email, string password, string specijalnost)
        {
            using (var db = new DomZdravljaContext())
            {
                if(ime != null && prezime != null && email!= null && password != null && specijalnost != null)
                {
                    try
                    {
                        Korisnik korisnik = new Korisnik();
                        korisnik.Ime = ime;
                        korisnik.Prezime = prezime;
                        korisnik.Email = email;
                        korisnik.Password = Security.Encrypt(password);
                        korisnik.Specijalnost = specijalnost;
                        korisnik.AktivanKorisnik = 1;
                        korisnik.IdRola = 3;
                        db.Korisnik.Add(korisnik);
                        db.SaveChanges();
                        return true;
                    }
                    catch (Exception) { return false; }
                }
                else
                {
                    return false;
                }
            }
        }
        public bool BrisanjeDoktora(int id)
        {
            using (var db = new DomZdravljaContext())
            {
                if (id != null)
                {
                    Korisnik korisnik = db.Korisnik.Find(id);
                    try
                    {
                        if(korisnik != null)
                        {
                            korisnik.AktivanKorisnik = 0;
                            db.SaveChanges();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception) { return false; }
                }
                else { return false; }
            }
        }

        //public List<Korisnik> GetMedicinskeSestre()
        //{
        //    using (var db = new DomZdravljaContext())
        //    {
        //        try
        //        {
        //            List<Korisnik> korisnici = (from m in db.Korisnik
        //                                        where m.IdRola == 2 && m.AktivanKorisnik == 1
        //                                        select m).ToList();
        //            return korisnici;
        //        }
        //        catch (Exception) { throw; }
        //    }
        //}


        public List<Korisnik> GetMedicinskeSestre(int str, int brPrikaz)
        {
            using (var db = new DomZdravljaContext())
            {
                try
                {
                    List<Korisnik> korisnici = (from m in db.Korisnik
                                                where m.IdRola == 2 && m.AktivanKorisnik == 1
                                                select m).Skip((str - 1) * brPrikaz).Take(brPrikaz).ToList();
                    return korisnici;
                }
                catch (Exception) { throw; }
            }
        }

        public bool DodajMedicinskuSestru(string ime, string prezime, string email, string password, string specijalnost)
        {
            using (var db = new DomZdravljaContext())
            {
                if (ime != null && prezime != null && email != null && password != null && specijalnost != null)
                {
                    try
                    {
                        Korisnik korisnik = new Korisnik();
                        korisnik.Ime = ime;
                        korisnik.Prezime = prezime;
                        korisnik.Email = email;
                        korisnik.Password = Security.Encrypt(password);
                        korisnik.Specijalnost = specijalnost;
                        korisnik.AktivanKorisnik = 1;
                        korisnik.IdRola = 2;
                        db.Korisnik.Add(korisnik);
                        db.SaveChanges();
                        return true;
                    }
                    catch (Exception) { return false; }
                }
                else
                {
                    return false;
                }
            }
        }

        public bool BrisanjeMedicinskeSestre(int id)
        {
            using (var db = new DomZdravljaContext())
            {
                if (id != null)
                {
                    Korisnik korisnik = db.Korisnik.Find(id);
                    try
                    {
                        if (korisnik != null)
                        {
                            korisnik.AktivanKorisnik = 0;
                            db.SaveChanges();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception) { return false; }
                }
                else { return false; }
            }
        }

        public List<PacijentKorisnika> GetPacijenti()
        {
            using (var db = new DomZdravljaContext())
            {
                try
                {
                    var pacijenti = (from k in db.KartonPacijenta
                                     join kor in db.Korisnik
                                     on k.IdKorisnik equals kor.IdKorisnik
                                     where k.AktivanKarton == 0 
                                     select new PacijentKorisnika()
                                     {
                                         IdKarton = k.IdKarton,
                                         BrojKnjizice = k.BrojKnjizice,
                                         Ime = k.Ime,
                                         Prezime = k.Prezime,
                                         DatumOtvaranjaKartona = k.DatumOtvaranjaKartona,
                                         AktivanKarton = k.AktivanKarton,
                                         IdKorisnik = k.IdKorisnik,
                                         ImeK = kor.Ime,
                                         PrezimeK = kor.Prezime,
                                         Specijalnost = kor.Specijalnost
                                     }).ToList();
                    return pacijenti;

                }
                catch (Exception) { throw; }
            }
        }
        public bool BrisanjePacijentaObavljenpregled(int id)
        {
            using (var db = new DomZdravljaContext())
            {
                if(id != null)
                {
                    try
                    {
                        PreglediPacijenta pregled = db.PreglediPacijenta.Where(x => x.IdKarton == id).SingleOrDefault();
                        var kartonPacijenta = db.KartonPacijenta.Find(id);

                        if(pregled != null)
                        {
                            db.PreglediPacijenta.Remove(pregled);
                            db.SaveChanges();
                            db.KartonPacijenta.Remove(kartonPacijenta);
                            db.SaveChanges();
                            return true;
                        }
                        else
                        {
                            db.KartonPacijenta.Remove(kartonPacijenta);
                            db.SaveChanges();
                            return true;
                        }
                    }
                    catch (Exception) { return false; }
                }
                else { return false; }
            }
        }
    }
}
