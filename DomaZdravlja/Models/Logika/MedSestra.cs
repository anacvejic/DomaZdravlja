using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomaZdravlja.Models.Logika
{
    public class MedSestra

    {
        public List<Korisnik> GetLekari()
        {
            using (var db = new DomZdravljaContext())
            {
                try
                {
                    List<Korisnik> korisnici = (from k in db.Korisnik
                                                where k.IdRola == 3 && k.AktivanKorisnik==1
                                                select k).ToList();
                    return korisnici;
                }
                catch (Exception) { throw; }
            }
        }
        public bool DodajNovKarton(KartonPacijenta kartonPacijenta)
        {
            using (var db = new DomZdravljaContext())
            {
                if (kartonPacijenta != null)
                {
                    try
                    {
                        kartonPacijenta.AktivanKarton = 1;
                        db.KartonPacijenta.Add(kartonPacijenta);
                        db.SaveChanges();
                        return true;
                    }
                    catch (Exception) { throw; }

                }
                else { return false; }
            }
        }
        public bool UnosNovogPregledaPacijenta(PreglediPacijenta preglediPacijenta)
        {
            using (var db = new DomZdravljaContext())
            {
                if(preglediPacijenta != null)

                {
                    try
                    {
                        preglediPacijenta.AktivanPregled = 1;
                        db.PreglediPacijenta.Add(preglediPacijenta);
                        db.SaveChanges();
                        return true;
                    }
                    catch (Exception) { throw; }
                }
                else
                {
                    return false;
                }
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
                                     where k.AktivanKarton == 1
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

        //public List<PacijentKorisnika> GetPacijenti(int str, int brPrikaz)
        //{
        //    using (var db = new DomZdravljaContext())
        //    {
        //        try
        //        {
        //            var pacijenti = (from k in db.KartonPacijenta
        //                             join kor in db.Korisnik
        //                             on k.IdKorisnik equals kor.IdKorisnik
        //                             where k.AktivanKarton==1
        //                             select new PacijentKorisnika()
        //                             {
        //                                 IdKarton = k.IdKarton,
        //                                 BrojKnjizice = k.BrojKnjizice,
        //                                 Ime = k.Ime,
        //                                 Prezime = k.Prezime,
        //                                 DatumOtvaranjaKartona = k.DatumOtvaranjaKartona,
        //                                 AktivanKarton = k.AktivanKarton,
        //                                 IdKorisnik = k.IdKorisnik,
        //                                 ImeK = kor.Ime,
        //                                 PrezimeK = kor.Prezime,
        //                                 Specijalnost = kor.Specijalnost
        //                             }).Skip((str - 1) * brPrikaz).Take(brPrikaz).ToList();
        //            return pacijenti;

        //        }
        //        catch (Exception) { throw; }
        //    }
        //}
        public List<PreglediPacijentaKarton> PreglediPacijenata()
        {
            using (var db = new DomZdravljaContext())
            {
                try
                {
                    var preglediPacijenata = (from k in db.Korisnik
                                              join karton in db.KartonPacijenta
                                              on k.IdKorisnik equals karton.IdKorisnik
                                              join p in db.PreglediPacijenta
                                              on karton.IdKarton equals p.IdKarton
                                              where karton.AktivanKarton == 1
                                              orderby p.DatumIvremePregleda descending
                                              select new PreglediPacijentaKarton()
                                              {
                                                  IdPregleda = p.IdPregleda,
                                                  DatumIvremePregleda = p.DatumIvremePregleda,
                                                  AktivanPregled = p.AktivanPregled,
                                                  IdKarton = karton.IdKarton,
                                                  BrojKnjizice = karton.BrojKnjizice,
                                                  Ime = karton.Ime,
                                                  Prezime = karton.Prezime,
                                                  DatumOtvaranjaKartona = karton.DatumOtvaranjaKartona,
                                                  AktivanKarton = karton.AktivanKarton,
                                                  IdKorisnik = k.IdKorisnik,
                                                  ImeKorisnik = k.Ime,
                                                  PrezimeKorisnik = k.Prezime,
                                                  Specijalnost = k.Specijalnost
                                              }).ToList();
                    return preglediPacijenata;
                }
                catch (Exception) { throw; }
            }
        }

        public bool BrisnjePacijenta(int id)
        {
            using (var db = new DomZdravljaContext())
            {
                if(id != null)
                {
                    KartonPacijenta pacijent = db.KartonPacijenta.Find(id);
                    //PacijentKorisnika pacijent = db.PacijentKorisnika.Find(id);
                    try
                    {
                        if(pacijent != null)
                        {
                            pacijent.AktivanKarton = 0;
                            db.SaveChanges();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception) { throw; }
                }
                else
                {
                    return false;
                }
            }
        }
        public bool IzmeniKartonPacijenta(string idKarton, string brojKnjizice, string prezime, string idKorisnik)
        {
            using (var db = new DomZdravljaContext())
            {
                KartonPacijenta kartonPacijenta = db.KartonPacijenta.Find(Convert.ToInt32(idKarton));
                try
                {
                    if (kartonPacijenta != null)
                    {
                        kartonPacijenta.BrojKnjizice = Convert.ToInt64(brojKnjizice);
                        kartonPacijenta.Prezime = prezime;
                        kartonPacijenta.IdKorisnik = Convert.ToInt32(idKorisnik);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception) { throw; }

            }
        }
    }
}
