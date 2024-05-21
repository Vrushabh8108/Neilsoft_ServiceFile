using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service19" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service19.svc or Service19.svc.cs at the Solution Explorer and start debugging.
    public class Service19 : Currency
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        public List<currency_dtl> Getcurrency()
        {
            List<currency_dtl> currency_dtl = new List<currency_dtl>();
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;

            //currency api
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://openexchangerates.org/api/latest.json?app_id=ece15baa53f6451f86833a3d16a7c8e7");
                    //client.BaseAddress = new Uri("https://api.exchangeratesapi.io/latest?base=USD");
                    //HTTP GET
                    var responseTask = client.GetAsync("");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var currency = readTask.Result;
                    //var rate = JsonConvert.DeserializeObject(currency
                    var rate = JObject.Parse(currency);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select cc.id id,country,currency,currency_exchange,'USD' basecurrency,exchange_rate from Country_Code cc , Currency_Exchange_Rate ce where cc.id = ce.country_id;", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
             
                            decimal rate13 = rate["rates"][dt.Rows[i]["currency_exchange"]].Value<decimal>();
                            int id1 = Convert.ToInt32(dt.Rows[i]["id"]);
                            SqlCommand cmd1 = new SqlCommand((@"update Currency_Exchange_Rate set exchange_rate = " + rate13 + " where country_id = " + id1), conn);
                            cmd1.ExecuteNonQuery();
                            currency_dtl currency_dtl1 = new currency_dtl
                            {
                                country = dt.Rows[i]["country"].ToString(),
                                currency = dt.Rows[i]["currency"].ToString(),
                                basecurrency = dt.Rows[i]["basecurrency"].ToString(),
                                rate = string.IsNullOrEmpty(rate13.ToString()) ? (Decimal?)null : Convert.ToDecimal(dt.Rows[i]["exchange_rate"])
                            };
                            currency_dtl.Add(currency_dtl1);
                        }
                    }
                    conn.Close();
                    return currency_dtl;
                }
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
        public string currenyconvert(decimal amount, string fromCurrency, string toCurrency)
        {
            WebClient web = new WebClient();
            string url = string.Format("http://www.google.com/ig/calculator?hl=en&q={2}{0}%3D%3F{1}", fromCurrency.ToUpper(), toCurrency.ToUpper(), amount);
            string response = web.DownloadString(url);
            Regex regex = new Regex(@":(?<rhs>.+?),");
            string[] arrDigits = regex.Split(response);
            string rate = arrDigits[3];
            return rate;
        }
    }
}
