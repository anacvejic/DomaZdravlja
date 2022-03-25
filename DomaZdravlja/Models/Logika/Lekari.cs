using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomaZdravlja.Models.Logika
{
    public class Lekari
    {
        public List<PreglediPacijentaKarton> PreglediPacijenata(int idKor)
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
                                              where karton.AktivanKarton == 1 && k.IdKorisnik == idKor
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

        public bool StorniranjePregleda(int id)
        {
            using (var db = new DomZdravljaContext())
            {
                if (id != null)
                {
                    PreglediPacijenta pregledi = db.PreglediPacijenta.Find(id);
                    try
                    {
                        if (pregledi != null)
                        {
                            pregledi.AktivanPregled = 0;
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
        public bool UnesiIzvestaj(string nazivVrsteIzvestaja, string opisIzvestaj,
             string lekarSpecijalista, string imePacijenta, string prezimePacijenta, string idKor)
        {
            using (var db = new DomZdravljaContext())
            {
                if (nazivVrsteIzvestaja != null && opisIzvestaj != null && lekarSpecijalista != null && imePacijenta != null && prezimePacijenta != null)
                {
                    try
                    {
                        VrstaIzvestaja vrsta = new VrstaIzvestaja();
                        vrsta.NazivVrsteIzvestaja = nazivVrsteIzvestaja;
                        vrsta.IdKorisnik = Convert.ToInt32(idKor);
                        db.VrstaIzvestaja.Add(vrsta);
                        db.SaveChanges();
                        var idVrsteIz = db.VrstaIzvestaja.OrderByDescending(x=>x.IdVrsteIzvestaja).First();
                        Korisnik korisnik = (from k in db.Korisnik
                                             where k.IdKorisnik == Convert.ToInt32(idKor)
                                             select k).SingleOrDefault();
                        Izvestaji izvestaj = new Izvestaji();
                        izvestaj.DatumIzvestaj = System.DateTime.Now;
                        izvestaj.OpisIzvestaj = opisIzvestaj;
                        izvestaj.NazivUstanove = "Dom zdravlja Vracar";
                        izvestaj.LekarSpecijalista = korisnik.Ime + ' ' + korisnik.Prezime;
                        izvestaj.AktivanIzvestaj = 1;
                        izvestaj.IdVrsteIzvestaja = idVrsteIz.IdVrsteIzvestaja;
                        izvestaj.ImePacijenta = imePacijenta;
                        izvestaj.PrezimePacijenta = prezimePacijenta;
                        db.Izvestaji.Add(izvestaj);
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
    }
}
