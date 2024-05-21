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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service13" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service13.svc or Service13.svc.cs at the Solution Explorer and start debugging.
    public class Service13 : GlobalSettings
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        public global_setting_dtl showglobalsetting()
        {
            SqlConnection conn = new SqlConnection(connection_string);
            SqlDataAdapter sda;
            DataTable dt;
            global_setting_dtl global = new global_setting_dtl();
            ConnectionState state = conn.State;
            try
            {

                using (conn = new SqlConnection(connection_string))
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"select * from global_settings; ", conn);
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable("global");
                    sda.Fill(dt);
                    global.global_setting_detail = dt;
                    conn.Close();
                    return global;
                }
            }
            catch (System.Exception ex)

            {


                if (state == ConnectionState.Open)
                {
                    conn.Close();
                }
                return null;
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
            }
        }
        public void ins_glb_detail(string web_server_name, string site_manager_name, string site_manager_email, string designer_name, string designer_email,string database_server_path)
        {
            int check;
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                string query1 = "select isnull((select 1 from global_settings),0);";
                using (SqlCommand command1 = new SqlCommand(query1, conn))
                {
                    check = (int)command1.ExecuteScalar();
                }

                if (check == 0)
                {
                    SqlCommand cmd1 = new SqlCommand((@"INSERT INTO global_settings 
                  ( web_server_name,site_manager_name, site_manager_email, designer_name, designer_email,database_server_path) VALUES
                  (N'" + web_server_name + "',N'" + site_manager_name + "', N'" + site_manager_email + "', " + "N'" + designer_name + "', N'" + designer_email + @"',N'"+ database_server_path +"')"), conn);
                    cmd1.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand((@"update global_settings set web_server_name = N'" + web_server_name + "',site_manager_name = N'" + site_manager_name + "',site_manager_email = N'" + site_manager_email + "',designer_name = N'" + designer_name + "',designer_email = N'" + designer_email + "',database_server_path = N'"+ database_server_path + "'"), conn);
                    cmd2.ExecuteNonQuery();
                }
                conn.Close();

            }
            catch (System.Exception ex)

            {

                if (state == ConnectionState.Open)
                {
                    conn.Close();
                }
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
            }

        }
    }
}
