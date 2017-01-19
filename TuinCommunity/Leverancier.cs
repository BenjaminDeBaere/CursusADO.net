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
        public bool HasChanged { get; protected set; }
        private int LeverancierNrvalue;
        public int LeverancierNr
        {
            get { return LeverancierNrvalue; }
            set
            {
                LeverancierNrvalue = value;
                HasChanged = true;
            }
        }
        private String NaamValue;

        public String Naam
        {
            get { return NaamValue; }
            set
            {
                NaamValue = value;
                HasChanged = true;
            }

        }
        private String AdresValue;

        public String Adres
        {
            get { return AdresValue; }
            set
            {
                AdresValue = value;
                HasChanged = true;
            }
        }
        private String PostcodeValue;

        public String Postcode
        {
            get { return PostcodeValue; }
            set
            {
                PostcodeValue = value;
                HasChanged = true;
            }
        }
        private String WoonplaatsValue;

        public String Woonplaats
        {
            get { return WoonplaatsValue; }
            set
            {
                WoonplaatsValue = value;
                HasChanged = true;
            }
        }
        

        public Leverancier(int leverancierNr, string naam, string adres, string postcode, string woonplaats)
        {
            this.LeverancierNr = leverancierNr;
            this.Naam = naam;
            this.Adres = adres;
            this.Postcode = postcode;
            this.Woonplaats = woonplaats;
            this.HasChanged = false;
        }

        public Leverancier() { }
    }
}
