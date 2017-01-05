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
        public Boolean LeverancierToevoegen(String naam, String adres, String postcode, String plaats)
        {
            var dbManager = new TuinDBManager();
            using (var conTuin = dbManager.GetConnection())
            {
                using (var comLeverancierToevoegen = conTuin.CreateCommand())
                {
                    comLeverancierToevoegen.CommandType = CommandType.Text;
                    comLeverancierToevoegen.CommandText = "insert into Leveranciers (Naam,Adres,PostNr,Woonplaats) values(@naam, @adres, @postcode, @plaats)";

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
                    return comLeverancierToevoegen.ExecuteNonQuery() != 0;
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
    }
}
