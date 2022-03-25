using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomaZdravlja.Models
{
    public partial class PreglediPacijenta
    {
        public int IdPregleda { get; set; }


        [Required(ErrorMessage = "DatumPregleda")]
        [Display(Name = "DatumPregledaP")]
        public DateTime DatumIvremePregleda { get; set; }
        public int? AktivanPregled { get; set; }
        public int IdKarton { get; set; }

        public KartonPacijenta IdKartonNavigation { get; set; }
    }
}
