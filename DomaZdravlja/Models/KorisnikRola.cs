using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DomaZdravlja.Models
{
    public class KorisnikRola
    {
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
        [RegularExpression("^[a-z0-9\\.a-z0-9\\-?]{1,64}\\@[a-z]{2,6}\\.[a-z]{2,3}$", ErrorMessage = "EmailRegex")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PasswordKorisnika")]
        [Display(Name = "Password")]
        [RegularExpression("[a-zA-Z\\d]{6,15}", ErrorMessage = "PasswordRegex")]
        public string Password { get; set; }

        [Required(ErrorMessage = "SpecijalnostKorisnika")]
        [Display(Name = "Specijalnost")]
        [RegularExpression("[A-Za-z\\d\\W]{6,50}", ErrorMessage = "SpecijalnostRegex")]
        public string Specijalnost { get; set; }
        public int? AktivanKorisnik { get; set; }

        public int IdRola { get; set; }

        [Required(ErrorMessage = "NazivRola")]
        [Display(Name = "NazivR")]
        [RegularExpression("[a-z]{4,50}", ErrorMessage = "RegexRola")]
        public string NazivRola { get; set; }
    }
}
