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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service21" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service21.svc or Service21.svc.cs at the Solution Explorer and start debugging.
    public class Service21 : GetCurrency
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();

        public List<currency_dtl> Get()
        {

            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select exchange_rate,(select country from Country_code where id = country_id) country,(select currency from Country_code where id = country_id) currency from Currency_Exchange_Rate order by country", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                List<currency_dtl> currency_dtl = new List<currency_dtl>();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        currency_dtl currency_dtl11 = new fujita_BIM4D5D_planner.currency_dtl
                        {
                            basecurrency = "USD",
                            country = dt.Rows[i]["country"].ToString(),
                            currency = dt.Rows[i]["currency"].ToString(),
                            rate = string.IsNullOrEmpty(dt.Rows[i]["exchange_rate"].ToString()) ? (Decimal?)null : Convert.ToDecimal(dt.Rows[i]["exchange_rate"])
                        };
                        currency_dtl.Add(currency_dtl11);
                    }
                }
                conn.Close();
                return currency_dtl;
            }
            catch (System.Exception ex)

            {


                if (state == ConnectionState.Open)
                {
                    conn.Close();

                }
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
                return null;

            }
        }
    

    }
}
