using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DomaZdravlja.Models
{
    public class PacijentKorisnika
    {
        [Key]
        public int IdKarton { get; set; }

        [Required(ErrorMessage = "BrojKnjizice")]
        [Display(Name = "BrojKnjizica")]
        [RegularExpression("[0-9]{11}", ErrorMessage = "BrojKnjiziceRegex")]
        public Int64 BrojKnjizice { get; set; }

        [Required(ErrorMessage = "ImeKartonPacijent")]
        [Display(Name = "ImePacijenta")]
        [RegularExpression("[a-zA-Z]{3,50}", ErrorMessage = "ImePacijentaRegex")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "PrezimePacijenta")]
        [Display(Name = "PrezimePacijent")]
        [RegularExpression("[a-zA-Z]{4,50}", ErrorMessage = "PrezimePacijentaRegex")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "DatumOtvaranjaKartona")]
        [Display(Name = "DatumKartona")]
        public DateTime DatumOtvaranjaKartona { get; set; }
        public int AktivanKarton { get; set; }
        public int IdKorisnik { get; set; }
        [Required(ErrorMessage = "ImeKorisnika")]
        [Display(Name = "Ime")]
        [RegularExpression("[a-zA-Z]{3,20}", ErrorMessage = "ImeRegex")]
        public string ImeK { get; set; }

        [Required(ErrorMessage = "PrezimeKorisnika")]
        [Display(Name = "Prezime")]
        [RegularExpression("[a-zA-Z]{3,20}", ErrorMessage = "PrezimeRegex")]
        public string PrezimeK { get; set; }

        [Required(ErrorMessage = "SpecijalnostKorisnika")]
        [Display(Name = "Specijalnost")]
        [RegularExpression("[A-Za-z\\d\\W]{6,50}", ErrorMessage = "SpecijalnostRegex")]
        public string Specijalnost { get; set; }
    }
}
