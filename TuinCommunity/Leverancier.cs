using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TuinCommunity
{
    public class Leverancier
    {
        public int LeverancierNr { get; set; }
        public String Naam { get; set; }
        public String Adres { get; set; }
        public String Postcode { get; set; }
        public String Woonplaats { get; set; }

        public Leverancier(int leverancierNr, string naam, string adres, string postcode, string woonplaats)
        {
            this.LeverancierNr = leverancierNr;
            this.Naam = naam;
            this.Adres = adres;
            this.Postcode = postcode;
            this.Woonplaats = woonplaats;
        }
    }
}
