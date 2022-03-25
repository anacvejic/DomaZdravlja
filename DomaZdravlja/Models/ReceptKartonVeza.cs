using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomaZdravlja.Models
{
    public partial class ReceptKartonVeza
    {
        public int IdRecepta { get; set; }
        public int IdKarton { get; set; }

        [Required(ErrorMessage = "DatumIzdavanja")]
        [Display(Name = "DatumIzdavanjaR")]
        public DateTime DatumIzadavanja { get; set; }
        public int IdVeze { get; set; }

        public KartonPacijenta IdKartonNavigation { get; set; }
        public Recept IdReceptaNavigation { get; set; }
    }
}
