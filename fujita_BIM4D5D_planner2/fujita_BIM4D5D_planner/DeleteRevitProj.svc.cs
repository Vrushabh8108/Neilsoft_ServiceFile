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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service2" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service2.svc or Service2.svc.cs at the Solution Explorer and start debugging.
    public class Service2 : DeleteRevitProj
    {

        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        public void del_revit_model(string Project_id, Int64 element_id, string usr)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand((@"update Revit_project_model set f_del=1,modified_by = '" + usr + "',modified_on = current_timestamp where " +
                 @"Project_id = '" + Project_id + "' and element_id = " + element_id), conn);
                cmd.ExecuteNonQuery();
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
        public int del_revit_project(string Project_id, string name, string country,string city)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand((@"update Project set f_active=0 where " +
                 @"Proj_guid = N'" + Project_id + "' and name = N'" + name +@"'and city_id in (select id from city where name = N'"+city+ "' and country_id in (select id from Country_Code where country = N'" + country+"'))"), conn);
                cmd.ExecuteNonQuery();
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