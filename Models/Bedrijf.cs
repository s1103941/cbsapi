using System;
using System.Collections.Generic;

namespace cbstest.Models
{
    public partial class Bedrijf
    {
        public Bedrijf()
        {
            Opgave = new HashSet<Opgave>();
        }

        public decimal BeId { get; set; }
        public string Naam { get; set; }

        public virtual ICollection<Opgave> Opgave { get; set; }
    }
}
