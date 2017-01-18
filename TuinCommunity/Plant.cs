using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuinCommunity
{
    public class Plant
    {
        public bool HasChanged { get; set; }
        public string PlantNaam { get; set; }
        public int PlantNr { get; set; }
        public int LeverancierNr { get; set; }
        private string plantKleurValue;
        public string PlantKleur
        {
            get {return plantKleurValue; }
            set
            {
                plantKleurValue=value;
                HasChanged = true;
            }
        }

        private decimal VerkoopPrijsValue;
        public decimal VerkoopPrijs
        {
            get { return VerkoopPrijsValue; }   
            set
            {
               VerkoopPrijsValue=value;
                HasChanged = true;
            }
        }

        public Plant(string plantNaam,int plantNr, int leverancierNr, string plantKleur, decimal verkoopPrijs )
        {
            this.PlantNaam = plantNaam;
            this.PlantNr = plantNr;
            this.LeverancierNr = leverancierNr;
            this.PlantKleur = plantKleur;
            this.VerkoopPrijs = verkoopPrijs;
            HasChanged = false;
        }
        public Plant() { }

    }
}
