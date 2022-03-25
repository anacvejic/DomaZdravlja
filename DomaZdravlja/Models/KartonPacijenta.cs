using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomaZdravlja.Models
{
    public partial class KartonPacijenta
    {
        public KartonPacijenta()
        {
            PreglediPacijenta = new HashSet<PreglediPacijenta>();
            ReceptKartonVeza = new HashSet<ReceptKartonVeza>();
        }
        [Key]
        public int IdKarton { get; set; }

        [Required(ErrorMessage = "BrojKnjizice")]
        [Display(Name = "BrojKnjizica")]
        [RegularExpression("[0-9]{6,15}", ErrorMessage = "BrojKnjiziceRegex")]
        public Int64 BrojKnjizice { get; set; }

        [Required(ErrorMessage = "ImeKartonPacijent")]
        [Display(Name = "ImePacijenta")]
        [RegularExpression("[a-zA-Z]{3,20}", ErrorMessage = "ImeRegex")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "PrezimePacijenta")]
        [Display(Name = "PrezimePacijent")]
        [RegularExpression("[a-zA-Z]{3,20}", ErrorMessage = "PrezimeRegex")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "DatumOtvaranjaKartona")]
        [Display(Name = "DatumKartona")]
        public DateTime DatumOtvaranjaKartona { get; set; }
        public int AktivanKarton { get; set; }
        public int IdKorisnik { get; set; }

        public Korisnik IdKorisnikNavigation { get; set; }
        public ICollection<PreglediPacijenta> PreglediPacijenta { get; set; }
        public ICollection<ReceptKartonVeza> ReceptKartonVeza { get; set; }
    }
}
