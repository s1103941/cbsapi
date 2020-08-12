using System;
using System.Collections.Generic;

namespace cbstest.Models
{
    public partial class Periode
    {
        public Periode()
        {
            Opgave = new HashSet<Opgave>();
        }

        public decimal PeriodeId { get; set; }
        public string Periodetype { get; set; }
        public string Code { get; set; }
        public DateTime? Begindatum { get; set; }
        public DateTime? Einddatum { get; set; }

        public virtual ICollection<Opgave> Opgave { get; set; }
    }
}
