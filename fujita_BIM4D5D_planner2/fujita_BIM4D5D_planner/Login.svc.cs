using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BIM4D5D_service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service7" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service7.svc or Service7.svc.cs at the Solution Explorer and start debugging.
    public class Service7 : Login
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        SqlConnection con;


        public country_access_dtl checklogin(string email, string pwd)
        {

            int check;
            int check12 = 0;
                       
            using (con = new SqlConnection(connection_string))
            {
                con.Open();

                string query = "select isnull((select 1 from username_password where email=N'" + email + @"' and password='" + pwd + @"' and f_active=1),0);";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    check = (int)command.ExecuteScalar();
                }
                
                if (check == 1)
                {
                    string query12 = "select isnull((select f_admin from username_password where email=N'" + email + @"' and password='" + pwd + @"' and f_active =1),0);";
                    using (SqlCommand command = new SqlCommand(query12, con))
                    {
                        check12 = (int)command.ExecuteScalar();
                    }
                    string query1 = "select isnull((select 1 from username_password where email=N'" + email + @"' and password='" + pwd + @"' and f_active =1),0);";
                    using (SqlCommand command = new SqlCommand(query1, con))
                    {
                        check = (int)command.ExecuteScalar();
                    }

                }
                SqlCommand cmd;
                SqlDataAdapter sda;
                DataTable dt;
                
                List<country_access> country_access = new List<country_access>();
                using (con = new SqlConnection(connection_string))
                {
                    cmd = new SqlCommand(@"select (select code from Country_Code where id = countryid) country_code,accessid access_dtl from user_country_map where userid = (select id from Username_Password where email = N'" + email + "' and password = N'" + pwd + "' and f_active = 1) order by country_code asc;", con);
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable("country_access");
                    sda.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        country_access country_access1 = new country_access
                        {
                            country_code = Convert.ToString(dt.Rows[i]["country_code"]),
                            access_dtl = Convert.ToInt16(dt.Rows[i]["access_dtl"])

                        };
                        country_access.Add(country_access1);
                    }


                    country_access_dtl country_access_dtl = new country_access_dtl
                    {
                        auth = check,
                        f_admin= check12,
                        access_dtl = country_access
                    };
                    
return country_access_dtl;

                }
            }

        }

        public string UpdateCityDatainCostMaster()
        {
            using (con = new SqlConnection(connection_string))
            {
                try
                {
                    con.Open();

                    SqlCommand cmd_City = new SqlCommand("select id from City where name NOT In ('Delhi');", con);
                    SqlDataAdapter sda_city = new SqlDataAdapter(cmd_City);
                    DataTable dt_city = new DataTable();
                    sda_city.Fill(dt_city);

                    for (int c = 0; c < dt_city.Rows.Count; c++)
                    {
                        SqlCommand cmd_cm = new SqlCommand("select id,material_variance_subdiv_id,eff_date,material_option_id from Cost_Master where city_id = " + dt_city.Rows[c]["id"] + ";", con);
                        SqlDataAdapter sda_cm = new SqlDataAdapter(cmd_cm);
                        DataTable dt_cm = new DataTable();
                        sda_cm.Fill(dt_cm);
                                                
                        for (int cm = 0; cm < dt_cm.Rows.Count; cm++)
                        {
                            if (dt_cm.Rows[cm]["material_variance_subdiv_id"].ToString() == "4" && dt_cm.Rows[cm]["id"].ToString() == "35176")
                            {

                            }

                            SqlCommand cmd_d = new SqlCommand("Select name, material_variance_id from Material_Variance_Subdivision where id = " + dt_cm.Rows[cm]["material_variance_subdiv_id"] + ";", con);
                            SqlDataAdapter sda_d = new SqlDataAdapter(cmd_d);
                            DataTable dt_d = new DataTable();
                            sda_d.Fill(dt_d);

                            for (int u = 0; u < dt_d.Rows.Count; u++)
                            {
                                SqlCommand cmd_u = new SqlCommand("select id from Material_Variance_Subdivision where city_id = " + dt_city.Rows[c]["id"] + " and name = N'" + dt_d.Rows[u]["name"] + @"'
                             and material_variance_id =" + dt_d.Rows[u]["material_variance_id"]+";", con);
                                SqlDataAdapter sda_u = new SqlDataAdapter(cmd_u);
                                DataTable dt_u = new DataTable();
                                sda_u.Fill(dt_u);

                                try
                                {
                                    string mat_id = dt_u.Rows[u]["id"].ToString();
                                    string opt_id = dt_cm.Rows[cm]["material_option_id"].ToString();
                                    string id = dt_cm.Rows[cm]["id"].ToString();
                                }
                                catch (Exception ex)
                                {
                                    continue;
                                }

                                SqlCommand cmd_upd = new SqlCommand("update Cost_Master set material_variance_subdiv_id = " + dt_u.Rows[u]["id"] + " where material_option_id = " + dt_cm.Rows[cm]["material_option_id"] + " and id = " + dt_cm.Rows[cm]["id"] + ";", con);
                                int ifEff = cmd_upd.ExecuteNonQuery();
                                if (ifEff == 0)
                                {

                                }

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }

            }

            return "Success";

        }

    }
}