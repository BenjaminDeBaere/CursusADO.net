using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace TuinCommunity
{
    public class TuinCentrumManager
    {
        public Int64 LeverancierToevoegen(String naam, String adres, String postcode, String plaats)
        {
            var dbManager = new TuinDBManager();
            using (var conTuin = dbManager.GetConnection())
            {
                using (var comLeverancierToevoegen = conTuin.CreateCommand())
                {
                    comLeverancierToevoegen.CommandType = CommandType.StoredProcedure;
                    comLeverancierToevoegen.CommandText = "LeverancierToevoegen";

                    DbParameter parNaam = comLeverancierToevoegen.CreateParameter();
                    parNaam.ParameterName = "@naam";
                    parNaam.Value = naam;
                    comLeverancierToevoegen.Parameters.Add(parNaam);

                    DbParameter parAdres = comLeverancierToevoegen.CreateParameter();
                    parAdres.ParameterName = "@adres";
                    parAdres.Value = adres;
                    comLeverancierToevoegen.Parameters.Add(parAdres);

                    DbParameter parPostcode = comLeverancierToevoegen.CreateParameter();
                    parPostcode.ParameterName = "@postcode";
                    parPostcode.Value = postcode;
                    comLeverancierToevoegen.Parameters.Add(parPostcode);

                    DbParameter parPlaats = comLeverancierToevoegen.CreateParameter();
                    parPlaats.ParameterName = "@plaats";
                    parPlaats.Value = plaats;
                    comLeverancierToevoegen.Parameters.Add(parPlaats);

                    conTuin.Open();
                    Int64 LeverancierNummer = Convert.ToInt64(comLeverancierToevoegen.ExecuteScalar());
                    return LeverancierNummer;
                }
            }
        }

        public Boolean EindejaarsKorting()
        {
            TuinDBManager dbManager = new TuinDBManager();
            using (var conTuin = dbManager.GetConnection())
            {
                using (var comEindejaarskorting = conTuin.CreateCommand())
                {
                    comEindejaarskorting.CommandType = CommandType.StoredProcedure;
                    comEindejaarskorting.CommandText = "Eindejaarskorting";

                    conTuin.Open();
                    return comEindejaarskorting.ExecuteNonQuery() != 0;
                }
            }
        }

        public void LeverancierVervangen(int OudLevNr, int NieuwLevNr)
        {
            var dbManager = new TuinDBManager();
            using (var conTuin = dbManager.GetConnection())
            {
                conTuin.Open();
                using (var traLeverancierVervangen = conTuin.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    using (var comLevVeranderen = conTuin.CreateCommand())
                    {
                        comLevVeranderen.Transaction = traLeverancierVervangen;
                        comLevVeranderen.CommandType = CommandType.StoredProcedure;
                        comLevVeranderen.CommandText = "LevVeranderen";

                        DbParameter parOudLevNr = comLevVeranderen.CreateParameter();
                        parOudLevNr.ParameterName = "@OudLevNr";
                        parOudLevNr.Value = OudLevNr;
                        comLevVeranderen.Parameters.Add(parOudLevNr);

                        DbParameter parNieuwLevNr = comLevVeranderen.CreateParameter();
                        parNieuwLevNr.ParameterName = "@NieuwLevNr";
                        parNieuwLevNr.Value = NieuwLevNr;                     
                        comLevVeranderen.Parameters.Add(parNieuwLevNr);

                        if(comLevVeranderen.ExecuteNonQuery()==0)
                        {
                            traLeverancierVervangen.Rollback();
                            throw new Exception("Fout Bij het veranderen van leveranciers.");
                        }
                                    
                    }

                    using (var comLevVerwijderen = conTuin.CreateCommand())
                    {
                        comLevVerwijderen.Transaction = traLeverancierVervangen;
                        comLevVerwijderen.CommandType = CommandType.StoredProcedure;
                        comLevVerwijderen.CommandText = "LevVerwijderen";

                        DbParameter parOudLevNr = comLevVerwijderen.CreateParameter();
                        parOudLevNr.ParameterName = "@OudLevNr";
                        parOudLevNr.Value = OudLevNr;
                        comLevVerwijderen.Parameters.Add(parOudLevNr);

                        if(comLevVerwijderen.ExecuteNonQuery() ==0)
                        {
                            traLeverancierVervangen.Rollback();
                            throw new Exception("Fout bij het verwijderen van de leverancier");
                        }
                    }
                }
            }
            
        }
        public Decimal GemiddeldePrijsBerekenen(String soort)
        {
            var dbManager = new TuinDBManager();
            using (var conTuin = dbManager.GetConnection())
            {
                using (var comGemiddeldePrijs = conTuin.CreateCommand())
                {
                    comGemiddeldePrijs.CommandType = CommandType.StoredProcedure;
                    comGemiddeldePrijs.CommandText = "berekenGemiddeldePrijs";

                    var parSoort = comGemiddeldePrijs.CreateParameter();
                    parSoort.ParameterName = "@Soort";
                    parSoort.Value = soort;
                    comGemiddeldePrijs.Parameters.Add(parSoort);

                    conTuin.Open();
                    Object resultaat = comGemiddeldePrijs.ExecuteScalar();
                    if (resultaat == null)
                    {
                        throw new Exception("Soort bestaat niet");
                    }
                    else
                    {
                        return Convert.ToDecimal(resultaat);
                    }


                }
            }
            
        }

     
        public Planten plantInfoRaadplegen(int plantNr)
        {
            var dbManager = new TuinDBManager();
            using (var conTuin = dbManager.GetConnection())
            {
                using (var comPlantInfo = conTuin.CreateCommand())
                {
                    comPlantInfo.CommandType = CommandType.StoredProcedure;
                    comPlantInfo.CommandText = "PlantGegevens";

                    var parPlantNr = comPlantInfo.CreateParameter();
                    parPlantNr.ParameterName = "@plantNr";
                    parPlantNr.Value = plantNr;
                    comPlantInfo.Parameters.Add(parPlantNr);

                    var parPlantNaam = comPlantInfo.CreateParameter();
                    parPlantNaam.ParameterName = "@plantNaam";
                    parPlantNaam.DbType = DbType.String;
                    parPlantNaam.Size = 30;
                    parPlantNaam.Direction = ParameterDirection.Output;
                    comPlantInfo.Parameters.Add(parPlantNaam);

                    var parPlantSoort = comPlantInfo.CreateParameter();
                    parPlantSoort.ParameterName = "@soortNr";
                    parPlantSoort.DbType = DbType.String;
                    parPlantSoort.Size = 30;
                    parPlantSoort.Direction = ParameterDirection.Output;
                    comPlantInfo.Parameters.Add(parPlantSoort);

                    var parLeveranciersNaam = comPlantInfo.CreateParameter();
                    parLeveranciersNaam.ParameterName = "@leveranciersNaam";
                    parLeveranciersNaam.DbType = DbType.String;
                    parLeveranciersNaam.Size = 30;
                    parLeveranciersNaam.Direction = ParameterDirection.Output;
                    comPlantInfo.Parameters.Add(parLeveranciersNaam);

                    var parPlantKleur = comPlantInfo.CreateParameter();
                    parPlantKleur.ParameterName = "@kleur";
                    parPlantKleur.DbType = DbType.String;
                    parPlantKleur.Size = 10;
                    parPlantKleur.Direction = ParameterDirection.Output;
                    comPlantInfo.Parameters.Add(parPlantKleur);

                    var parPlantVerkoopsPrijs = comPlantInfo.CreateParameter();
                    parPlantVerkoopsPrijs.ParameterName = "@VerkoopPrijs";
                    parPlantVerkoopsPrijs.DbType = DbType.Currency;
                    parPlantVerkoopsPrijs.Direction = ParameterDirection.Output;
                    comPlantInfo.Parameters.Add(parPlantVerkoopsPrijs);

                    conTuin.Open();
                    comPlantInfo.ExecuteNonQuery();
                    if(parPlantNr.Value.Equals(DBNull.Value))
                    {
                        throw new Exception("PlantNummer bestaat niet");
                    }
                    else
                    {
                        return new Planten(
                            parPlantNaam.Value.ToString(),
                            parPlantSoort.Value.ToString(),
                            parLeveranciersNaam.Value.ToString(),                            
                            parPlantKleur.Value.ToString(), (Decimal)parPlantVerkoopsPrijs.Value);
                    }
                }
            }
        }
        public List<Soorten> GetSoorten()
        {
            var soorten = new List<Soorten>();
            var manager = new TuinDBManager();
            using (var conTuin = manager.GetConnection())
            {
                using (var comSoorten = conTuin.CreateCommand())
                {
                    comSoorten.CommandType = CommandType.Text;
                    comSoorten.CommandText =
                    "select SoortNr, Soort from Soorten order by Soort";
                    conTuin.Open();
                    using (var rdrSoorten = comSoorten.ExecuteReader())
                    {
                        var soortPos = rdrSoorten.GetOrdinal("soort");
                        var soortnrPos = rdrSoorten.GetOrdinal("soortnr");
                        while (rdrSoorten.Read())
                        {
                            soorten.Add(new Soorten(
                            rdrSoorten.GetString(soortPos), rdrSoorten.GetInt32(soortnrPos)));
                        }
                    }
                }
            }
            return soorten;
        }
        public List<Plant> GetPlantNaamPerSoort(int soortNr)
        {
            List<Plant> planten = new List<Plant>();
            var manager = new TuinDBManager();
            using (var conTuin = manager.GetConnection())
            {
                using (var comPlantNaamPerSoort = conTuin.CreateCommand())
                {
                    comPlantNaamPerSoort.CommandType = CommandType.Text;
                    if (soortNr != 0)
                    {
                        comPlantNaamPerSoort.CommandText =
                            "select * from planten where SoortNr = @plantSoort";

                        var parPlantSoort = comPlantNaamPerSoort.CreateParameter();
                        parPlantSoort.ParameterName = "@plantSoort";
                        parPlantSoort.Value = soortNr;
                        comPlantNaamPerSoort.Parameters.Add(parPlantSoort);
                    }
                    else
                    {
                        comPlantNaamPerSoort.CommandText = "Select * from Planten";

                    }
                    conTuin.Open();
                    using (var rdrPlantNaamPerSoort = comPlantNaamPerSoort.ExecuteReader())
                    {
                        var plantNaamPos = rdrPlantNaamPerSoort.GetOrdinal("Naam");
                        var plantNrPos = rdrPlantNaamPerSoort.GetOrdinal("plantNr");
                        var LevNrPos = rdrPlantNaamPerSoort.GetOrdinal("levnr");
                        var KleurPos = rdrPlantNaamPerSoort.GetOrdinal("kleur");
                        var VerkoopPrijsPos = rdrPlantNaamPerSoort.GetOrdinal("verkoopprijs");
                        var SoortPos = rdrPlantNaamPerSoort.GetOrdinal("soortnr");

                        while(rdrPlantNaamPerSoort.Read())
                        {
                            var plant = new Plant(
                                rdrPlantNaamPerSoort.GetString(plantNaamPos),
                                rdrPlantNaamPerSoort.GetInt32(plantNrPos),
                                rdrPlantNaamPerSoort.GetInt32(LevNrPos),
                                rdrPlantNaamPerSoort.GetString(KleurPos),
                                rdrPlantNaamPerSoort.GetDecimal(VerkoopPrijsPos));
                            planten.Add(plant);
                        }
                    }
                }
             
            }
            return planten;
        }
        public List<Plant> VeranderPlanten(List<Plant> planten , int soortNr)
        {
            List<Plant> nietVeranderdePlanten = new List<Plant>();
            var manager = new TuinDBManager();
            using (var conTuin = manager.GetConnection())
            {
                using (var comVerander = conTuin.CreateCommand())
                {
                    comVerander.CommandType = CommandType.Text;
                    comVerander.CommandText = "update planten set kleur = @plantKleur, VerkoopPrijs = @prijs where soortNr = @soortNr";

                    var parPlantKleur = comVerander.CreateParameter();
                    parPlantKleur.ParameterName = "@plantKleur";
                    comVerander.Parameters.Add(parPlantKleur);

                    var parPrijs = comVerander.CreateParameter();
                    parPrijs.ParameterName = "@prijs";
                    comVerander.Parameters.Add(parPrijs);

                    var parsoortNr = comVerander.CreateParameter();
                    parsoortNr.ParameterName = "@soortNr";
                    parsoortNr.Value = soortNr;
                    comVerander.Parameters.Add(parsoortNr);

                    conTuin.Open();
                    foreach(Plant plant in planten)
                    {
                        try
                        {
                            
                            parPlantKleur.Value = plant.PlantKleur;
                            parPrijs.Value = plant.VerkoopPrijs;

                            if (comVerander.ExecuteNonQuery() == 0)
                                nietVeranderdePlanten.Add(plant);
                        }
                        catch
                        {
                            nietVeranderdePlanten.Add(plant);
                        }
                    }


                }
                return nietVeranderdePlanten;
            }
        }
       
    }
}
