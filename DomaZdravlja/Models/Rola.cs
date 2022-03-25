using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomaZdravlja.Models
{
    public partial class Rola
    {
        public Rola()
        {
            Korisnik = new HashSet<Korisnik>();
        }
        [Key]
        public int IdRola { get; set; }

        [Required(ErrorMessage ="NazivRola")]
        [Display(Name ="NazivR")]
        [RegularExpression("[a-z]{4,50}",ErrorMessage ="RegexRola")]
        public string NazivRola { get; set; }

        public ICollection<Korisnik> Korisnik { get; set; }
    }
}
