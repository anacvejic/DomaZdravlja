using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomaZdravlja.Models
{
    public partial class Izvestaji
    {
        public int IdIzvestaj { get; set; }
        
        [Required(ErrorMessage = "DatumIzvestaj")]
        [Display(Name = "Datum")]
        public DateTime DatumIzvestaj { get; set; }

        [Required(ErrorMessage = "OpisIzvestaj")]
        [Display(Name = "Opis")]
        [RegularExpression("[A-Za-z\\d\\W]{6,50}", ErrorMessage = "OpisIzvestajRegex")]
        public string OpisIzvestaj { get; set; }

        [Required(ErrorMessage = "NazivUstanove")]
        [Display(Name = "NazivUst")]
        [RegularExpression("[A-Za-z]{6,50}", ErrorMessage = "NazivUstanoveRegex")]
        public string NazivUstanove { get; set; }

        [Required(ErrorMessage = "LekarSpecijalista")]
        [Display(Name = "Specijalista")]
        [RegularExpression("[A-Za-z]{6,50}", ErrorMessage = "SpecijalistaRegex")]
        public string LekarSpecijalista { get; set; }
        public int? AktivanIzvestaj { get; set; }
        public int IdVrsteIzvestaja { get; set; }
        public string ImePacijenta { get; set; }
        public string PrezimePacijenta { get; set; }

        public VrstaIzvestaja IdVrsteIzvestajaNavigation { get; set; }
    }
}
