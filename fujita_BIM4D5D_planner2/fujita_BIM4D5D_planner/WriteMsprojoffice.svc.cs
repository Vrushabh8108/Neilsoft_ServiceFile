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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service3" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service3.svc or Service3.svc.cs at the Solution Explorer and start debugging.
    public class Service3 : WriteMsprojoffice
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        public Int64 ins_msprojoffice(string Project_guid, string msprojoffice_file, string user)
        {
            int check;
            Int64 ver;
            Int64 current_ver;
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            { 
            conn.Open();
            string query = "select isnull((select 1 from project where proj_guid=N'" + Project_guid + @"'),0);";
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                check = (int)command.ExecuteScalar();
            }

            if (check == 0)
            {
                    SqlCommand cmd = new SqlCommand((@"INSERT INTO PROJECT 
                  (proj_guid) VALUES(N '" + Project_guid + "')"), conn);
                cmd.ExecuteNonQuery();
            }
            string query1 = "select isnull((select max(version) from revit_msprojoffice where proj_id=(select id from project where proj_guid = N'" + Project_guid + "')),0);"; using (SqlCommand command = new SqlCommand(query1, conn))
            {
                ver = (Int64)command.ExecuteScalar();
            }
            current_ver = ver + 1; SqlCommand cmd1 = new SqlCommand((@"INSERT INTO revit_msprojoffice 
              (proj_id, msprojoffice_file, modified_by, version) VALUES((select id from project where proj_guid = N '" + Project_guid + "'), '" + msprojoffice_file + "', '" + user + "', "  + current_ver+ ")"), conn);
              cmd1.ExecuteNonQuery();

            conn.Close();
            return current_ver;
            }
            catch (System.Exception ex)

            {


                if (state == ConnectionState.Open)
                {
                    conn.Close();
                }
                return 0;

            }

        }
    }
}