using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomaZdravlja.Models
{
    public partial class VrstaIzvestaja
    {
        public VrstaIzvestaja()
        {
            Izvestaji = new HashSet<Izvestaji>();
        }
        [Key]
        public int IdVrsteIzvestaja { get; set; }

        [Required(ErrorMessage = "NazivVrsteIzvestaja")]
        [Display(Name = "Vrstaizvestaja")]
        [RegularExpression("[A-Za-z]{6,50}", ErrorMessage = "VrstaizvestajaRegex")]
        public string NazivVrsteIzvestaja { get; set; }
        public int IdKorisnik { get; set; }

        public Korisnik IdKorisnikNavigation { get; set; }
        public ICollection<Izvestaji> Izvestaji { get; set; }
    }
}
