using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomaZdravlja.Models
{
    public partial class Recept
    {
        public Recept()
        {
            ReceptKartonVeza = new HashSet<ReceptKartonVeza>();
        }

        public int IdRecepta { get; set; }
        public int? AktivanRecept { get; set; }

        [Required(ErrorMessage = "Nazivleka")]
        [Display(Name = "Lek")]
        [RegularExpression("[a-zA-Z]{3,50}", ErrorMessage = "NazivLekaRegex")]
        public string NazivLeka { get; set; }

        [Required(ErrorMessage = "KolicinaLeka")]
        [Display(Name = "Kolicina")]
        [RegularExpression("[0-9]{2}", ErrorMessage = "NazivLekaRegex")]
        public int Kolicina { get; set; }

        public ICollection<ReceptKartonVeza> ReceptKartonVeza { get; set; }
    }
}
