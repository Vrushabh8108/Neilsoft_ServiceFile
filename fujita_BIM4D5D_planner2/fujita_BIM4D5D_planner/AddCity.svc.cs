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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service20" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service20.svc or Service20.svc.cs at the Solution Explorer and start debugging.
    public class Service20 : AddCity
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        public List<string> Findcity(string country)
        {
          
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                List<string> city = new List<string>();
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select * from City where country_id in (select id from Country_code where country = N'" + country + "') order by name asc;", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string citytmp = dt.Rows[i]["name"].ToString();
                        city.Add(citytmp);
                    }
                }
                conn.Close();
                return city;
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
        public int ins_city(string city,string country,string from_city)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            string code;
            int check;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand((@"INSERT INTO City 
                        (name,country_id) 
            VALUES      (N'" + city + @"',(select id from country_code where country = N'"+country+"'))"), conn);
                cmd.ExecuteNonQuery();

                SqlCommand cmd_subdiv = new SqlCommand("INSERT INTO Material_Variance_Subdivision (material_variance_id,name,created_by,created_on,unit_of_measurement,f_del,category_id,seq,specification,city_id) select material_variance_id,name,created_by,created_on,unit_of_measurement,f_del,category_id,seq,specification,(select id from City where name = N'" + city + "' and country_id = (select id from Country_Code where country = N'" + country + "')) from Material_Variance_Subdivision where city_id = (select id from City where name = N'" + from_city + "' and country_id = (select id from Country_Code where country = '" + country + "'))", conn);
                cmd_subdiv.ExecuteNonQuery();

                SqlCommand cmd_City = new SqlCommand("select material_variance_subdiv_id,eff_date,eff_date_end,cost,modified_by,modified_on,material_option_id from cost_master where material_variance_subdiv_id in (select id from Material_Variance_Subdivision where city_id = (select id from city where country_id in (select id from Country_code where country = N'" + country + @"')
and name =N'" + from_city + "')) and current_timestamp between eff_date and eff_date_end;", conn);
                    SqlDataAdapter sda_city = new SqlDataAdapter(cmd_City);
                    DataTable dt_city = new DataTable();
                    sda_city.Fill(dt_city);

                    for (int c = 0; c < dt_city.Rows.Count; c++)
                    {
                        SqlCommand cmd_updcity = new SqlCommand("select id from Material_Variance_Subdivision where name = (select name from Material_Variance_Subdivision where id = " + dt_city.Rows[c]["material_variance_subdiv_id"] + ") and city_id = (select id from City where name = N'"+city+"' and country_id = (select id from Country_Code where country =N'"+country+"'));", conn);
                        long? id = Convert.ToInt64(cmd_updcity.ExecuteScalar());

                        if (id != null)
                        {
                            try
                            {
                                SqlCommand cmd_ins = new SqlCommand("INSERT INTO Cost_master (material_variance_subdiv_id,eff_date,eff_date_end,cost,modified_by,modified_on,material_option_id) VALUES (" + id + @",
                            '" + dt_city.Rows[c]["eff_date"] + "','" + dt_city.Rows[c]["eff_date_end"] + "'," + dt_city.Rows[c]["cost"] + ",N'" + dt_city.Rows[c]["modified_by"] + "','" + dt_city.Rows[c]["modified_on"] + "'," + dt_city.Rows[c]["material_option_id"] + ")", conn);
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
                

                SqlCommand cmd_dr = new SqlCommand((@"insert into Design_Rule_Country (design_rule_id,city_id,value,formula) 
                select design_rule_id,(select id from city where name = N'" + city + "' and country_id = (select id from Country_code where country = N'" + country + @"')),
                value,formula from design_rule_country where city_id = (select id from city where name = N'" + from_city + "' and country_id in (select id from country_code where country = N'" + country + "'))"), conn);
                cmd_dr.ExecuteNonQuery();
                           

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
    }
}
