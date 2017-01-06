using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuinCommunity
{
    public class Planten
    {
        private String plantNaamValue;
        private String plantSoortValue;
        private String leverancierNaamValue;
        private String kleurValue;
        private Decimal kostprijsValue;

        public String PlantNaam
        {
            get { return plantNaamValue; }
        }
        public String PlantSoort
        {
            get { return plantSoortValue; }
        }
        public String LeverancierNaam
        {
            get { return leverancierNaamValue; }
        }

        public String Kleur
        {
            get { return kleurValue; }
        }
        public Decimal Kostprijs
        {
            get { return kostprijsValue; }
        }

        public Planten(String plantNaam, String plantSoort, String leverancierNaam, String kleur, Decimal kostprijs)
        {
            plantNaamValue = plantNaam;
            plantSoortValue = plantSoort;
            leverancierNaamValue = leverancierNaam;
            kleurValue = kleur;
            kostprijsValue = kostprijs;
        }

    }
}
