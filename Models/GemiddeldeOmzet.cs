namespace cbstest.Models
{
    public class GemiddeldeOmzet
    {
        public decimal periodeID { get; set; }
        public decimal gemiddeldeOmzet { get; set; }

        public GemiddeldeOmzet(decimal periodeID, decimal gemiddeldeOmzet)
        {
            this.periodeID = periodeID;
            this.gemiddeldeOmzet = gemiddeldeOmzet;
        }
    }
}
