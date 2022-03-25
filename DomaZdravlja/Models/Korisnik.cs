using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomaZdravlja.Models
{
    public partial class Korisnik
    {
        public Korisnik()
        {
            KartonPacijenta = new HashSet<KartonPacijenta>();
            VrstaIzvestaja = new HashSet<VrstaIzvestaja>();
        }
        [Key]
        public int IdKorisnik { get; set; }

        [Required(ErrorMessage = "ImeKorisnika")]
        [Display(Name = "Ime")]
        [RegularExpression("[a-zA-Z]{3,20}", ErrorMessage = "ImeRegex")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "PrezimeKorisnika")]
        [Display(Name = "Prezime")]
        [RegularExpression("[a-zA-Z]{3,20}", ErrorMessage = "PrezimeRegex")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "EmailKorisnika")]
        [Display(Name = "Email")]
        [RegularExpression("^[a-z0-9\\.a-z0-9\\-?]{1,11}\\@[a-z]{2,6}\\.[a-z]{2,3}$", ErrorMessage = "EmailRegex")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PasswordKorisnika")]
        [Display(Name = "Password")]
        [RegularExpression("[A-Z]+[a-z]+\\d+", ErrorMessage = "PasswordRegex")]
        public string Password { get; set; }

        [Required(ErrorMessage = "SpecijalnostKorisnika")]
        [Display(Name = "Specijalnost")]
        [RegularExpression("[a-zA-Z\\s]{6,50}", ErrorMessage = "SpecijalnostRegex")]
        public string Specijalnost { get; set; }
        public int? AktivanKorisnik { get; set; }
        public int IdRola { get; set; }

        public Rola IdRolaNavigation { get; set; }
        public ICollection<KartonPacijenta> KartonPacijenta { get; set; }
        public ICollection<VrstaIzvestaja> VrstaIzvestaja { get; set; }
    }
}
