using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuinCommunity
{
    public class Soorten
    {
        public string SoortNaam { get; set; }
        public int SoortNr { get; set; }

        public Soorten(String nSoortNaam, int nSoortNr)
        {
            SoortNaam = nSoortNaam;
            SoortNr = nSoortNr;
        }

        public override string ToString()
        {
            return SoortNaam;
        }
    }
}
