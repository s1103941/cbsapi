using System;
using System.Collections.Generic;

namespace cbstest.Models
{
    public partial class Opgave
    {
        public decimal OpgaveId { get; set; }
        public decimal BeId { get; set; }
        public decimal PeriodeId { get; set; }
        public decimal OmzetInclusiefbtw { get; set; }
        public decimal Btw { get; set; }

        public virtual Bedrijf Be { get; set; }
        public virtual Periode Periode { get; set; }
    }
}
