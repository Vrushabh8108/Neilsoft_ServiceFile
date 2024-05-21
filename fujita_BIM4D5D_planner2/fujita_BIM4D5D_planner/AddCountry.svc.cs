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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service18" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service18.svc or Service18.svc.cs at the Solution Explorer and start debugging.
    public class Service18 : Addcountry
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        public List<country_dtl> FindCountry()
        {
            List<country_dtl> country = new List<country_dtl>();
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select * from country_code order by country asc;", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        country_dtl country1 = new country_dtl
                        {
                            country = dt.Rows[i]["country"].ToString(),
                            code = dt.Rows[i]["code"].ToString(),
                            currency = dt.Rows[i]["currency"].ToString()
                        };
                        country.Add(country1);
                    }
                }
                conn.Close();
                return country;
            }
            catch (System.Exception ex)

            {


                if (state == ConnectionState.Open)
                {
                    conn.Close();

                }
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
                return country;

            }
        }
        public int ins_country(country_dtl country_dtl,string to_city,string from_country,string from_city)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            string code;
            int check;
            try
            {
                conn.Open();
              SqlCommand cmd = new SqlCommand((@"INSERT INTO Country_code 
                        (country,currency,code,currency_exchange) 
            VALUES      (N'" + country_dtl.country + @"',N'"+country_dtl.currency+"',N'"+ country_dtl.country + "',N'"+country_dtl.currency + "')"), conn);
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand((@"INSERT INTO currency_exchange_rate 
                        (country_id) 
            VALUES      ((select id from Country_Code where country = N'" + country_dtl.country + @"'))"), conn);
                cmd1.ExecuteNonQuery();
                SqlCommand cmd3 = new SqlCommand((@"INSERT INTO City 
                        (name,country_id) 
            VALUES      (N'"+to_city+"',(select id from Country_Code where country = N'" + country_dtl.country + @"'))"), conn);
                cmd3.ExecuteNonQuery();
                // to add the design rule
                SqlCommand cmd_dr = new SqlCommand((@"insert into Design_Rule_Country (design_rule_id,city_id,value,formula) 
                select design_rule_id,(select id from city where name = N'" + to_city + "' and country_id = (select id from Country_code where country = N'" + country_dtl.country + @"')),
                value,formula from design_rule_country where city_id = (select id from city where name = N'"+from_city+"' and country_id in (select id from country_code where country = N'"+from_country+"'))"), conn);
                cmd_dr.ExecuteNonQuery();

                SqlCommand cmd_subdiv = new SqlCommand("INSERT INTO Material_Variance_Subdivision (material_variance_id,name,created_by,created_on,unit_of_measurement,f_del,category_id,seq,specification,city_id) select material_variance_id,name,created_by,created_on,unit_of_measurement,f_del,category_id,seq,specification,(select id from City where name = N'" + to_city + "' and country_id = (select id from Country_Code where country = N'" + country_dtl.country + "')) from Material_Variance_Subdivision where city_id = (select id from City where name = N'" + from_city + "' and country_id = (select id from Country_Code where country = '" + from_country + "'))", conn);
                cmd_subdiv.ExecuteNonQuery();


                Service19 client = new Service19();
                List < currency_dtl > currency= client.Getcurrency();
                if (from_country == null)
                {  }
                else
                {
                    decimal exchange_rate;
                    string query = "select ((select exchange_rate from Currency_Exchange_Rate where country_id= (select id from country_code where country = N'"+ country_dtl.country+"' ))/"+
                                            @"(select exchange_rate from Currency_Exchange_Rate where country_id = (select id from country_code where country = N'" + from_country + "' ))) ; ";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                         exchange_rate = (decimal)command.ExecuteScalar();
                    }


                    SqlCommand cmd_City = new SqlCommand("select material_variance_subdiv_id,eff_date,eff_date_end,cost,modified_by,modified_on,material_option_id,(select id from city where country_id in (select id from Country_code where country = N'" + country_dtl.country + @"') 
and name =N'" + to_city + @"') as city_id
from cost_master where city_id =(select id from city where country_id in 
(select id from Country_code where country = N'" + from_country + @"')
and name =N'" + from_city + "') and current_timestamp between eff_date and eff_date_end;;", conn);
                    SqlDataAdapter sda_city = new SqlDataAdapter(cmd_City);
                    DataTable dt_city = new DataTable();
                    sda_city.Fill(dt_city);

                    for (int c = 0; c < dt_city.Rows.Count; c++)
                    {
                        SqlCommand cmd_updcity = new SqlCommand("select id from Material_Variance_Subdivision where name = (select name from Material_Variance_Subdivision where id = " + dt_city.Rows[c]["material_variance_subdiv_id"] + ") and city_id = (select id from City where name = N'" + to_city + "' and country_id = (select id from Country_Code where country =N'" + country_dtl.country + "'));", conn);
                        long? id = Convert.ToInt64(cmd_updcity.ExecuteScalar());

                        if (id != null)
                        {
                            try
                            {
                                var Cost = dt_city.Rows[c]["cost"] == null ? 0 : Convert.ToInt64(dt_city.Rows[c]["cost"]) * exchange_rate;
                                SqlCommand cmd_ins = new SqlCommand("INSERT INTO Cost_master (material_variance_subdiv_id,eff_date,eff_date_end,cost,modified_by,modified_on,material_option_id,city_id) VALUES (" + id + @",
                            '" + dt_city.Rows[c]["eff_date"] + "','" + dt_city.Rows[c]["eff_date_end"] + "'," + Cost + ",N'" + dt_city.Rows[c]["modified_by"] + "','" + dt_city.Rows[c]["modified_on"] + "'," + dt_city.Rows[c]["material_option_id"] + "," + dt_city.Rows[c]["city_id"] + ")", conn);
                                int result = cmd_ins.ExecuteNonQuery();
                                if (result == 0)
                                {

                                }
                            }
                            catch (Exception ex)
                            {
                                Service17 exception1 = new Service17();
                                exception1.SendErrorToText(ex);
                            }
                        }
                        else
                        {

                        }
                    }


//                    SqlCommand cmd2 = new SqlCommand((@"INSERT INTO Cost_master 
//                        (material_variance_subdiv_id,eff_date,eff_date_end,cost,modified_by,modified_on,material_option_id,city_id) 
//           select material_variance_subdiv_id,eff_date,eff_date_end,cost*"+exchange_rate+",modified_by,modified_on,material_option_id,(select id from city where country_id in (select id from Country_code where country = N'" + country_dtl.country + "') and name =N'" + to_city + "') from cost_master where city_id =(select id from city where country_id in (select id from Country_code where country = N'" + from_country + "') and name =N'" + from_city + "') and current_timestamp between eff_date and eff_date_end;"), conn);
//                    cmd2.ExecuteNonQuery();
                }
                conn.Close();
                return 1;
            }
            catch (System.Exception ex)

            {


                if (state == ConnectionState.Open)
                {
                    conn.Close();

                }
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
                return 0;

            }

        }
        public List<country_dtl_lst> FindCountryList()
        {
            List<country_dtl_lst> country = new List<country_dtl_lst>();
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select name,currency from Country_List where name not in (select country from country_code) order by name asc;", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        country_dtl_lst country1 = new country_dtl_lst
                        {
                            country = dt.Rows[i]["name"].ToString(),
                            currency = dt.Rows[i]["currency"].ToString()
                        };
                        country.Add(country1);
                    }
                }
                conn.Close();
                return country;
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
