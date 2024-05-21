using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service30" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service30.svc or Service30.svc.cs at the Solution Explorer and start debugging.
    public class Service30 : GetExchangedValue
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();

        public decimal GetExchangedCostValue(string toCountry, string fromCountry)
        {
            decimal exchangedCost = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connection_string))
                {
                    con.Open();
                    string query = "select ((select exchange_rate from Currency_Exchange_Rate where country_id= (select id from country_code where country = N'" + toCountry + "' ))/" +
                                                @"(select exchange_rate from Currency_Exchange_Rate where country_id = (select id from country_code where country = N'" + fromCountry + "' ))) ; ";
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        exchangedCost = (decimal)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
            }

            return Math.Round(exchangedCost,2);
        }
    }
}
