using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service6" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service6.svc or Service6.svc.cs at the Solution Explorer and start debugging.
    public class Service6 : InsertProjDetails
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        public void ins_proj_dtl(string Project_guid,string proj_name, string revit_project_file, string ms_project_file, string site_manager_name, string site_manager_email, string designer_name, string designer_email,string country,string city,int f_create_base,[Optional] string construction_type,[Optional] DateTime construction_start_date,[Optional] string ms_proj_file_path, string revit_version)
        {
            int check;
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                string query = "select isnull((select 1 from project where proj_guid=N'" + Project_guid + @"' and name = N'" + proj_name + "'),0);";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    check = (int)command.ExecuteScalar();
                }

                if (check == 0)
                {
                    SqlCommand cmd = new SqlCommand((@"INSERT INTO PROJECT 
                  (proj_guid,name,city_id) VALUES(N'" + Project_guid + "',N'"+ proj_name +"',(select id from City where name = N'"+city+ "' and country_id in (select id from country_code where  country = N'" + country +"')))"), conn);
                    cmd.ExecuteNonQuery();
                    SqlCommand cmd1 = new SqlCommand((@"INSERT INTO Revit_Project_Version 
                  (project_id,version,created_on) VALUES((select id from project where proj_guid=N'" + Project_guid + @"' and name = N'" + proj_name + "'),1,current_timestamp)"), conn);
                    cmd1.ExecuteNonQuery();
                }
                string query1 = "select isnull((select 1 from project_details where project_id = (select id from project where proj_guid =N'" + Project_guid + @"' and name = N'" + proj_name +"')),0);"; using (SqlCommand command1 = new SqlCommand(query1, conn))
                {
                    check = (int)command1.ExecuteScalar();
                }

                if (check == 0)
                {
                    SqlCommand cmd1 = new SqlCommand((@"INSERT INTO project_details 
                  (project_id,revit_project_file, ms_project_file, site_manager_name, site_manager_email, designer_name, designer_email,f_create_base,construction_type,construction_start_date,ms_proj_file_path,revit_version) VALUES
                  ((select id from project where proj_guid = N'" + Project_guid + "' and name = N'" + proj_name + "'),N'" + revit_project_file + "', N'" + ms_project_file + "', " + "N'" + site_manager_name + "', N'" + site_manager_email + @"', N'" + designer_name + "', N'" + designer_email + "'," + f_create_base + ",N'" + construction_type + "','" + construction_start_date + "',N'" + ms_proj_file_path + "',N'" + revit_version + "')"), conn);
                    cmd1.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand((@"update project_details set revit_project_file = N'" + revit_project_file + "',ms_project_file = N'" + ms_project_file + "',site_manager_name = N'" + site_manager_name + "',site_manager_email = N'" + site_manager_email + "',designer_name = N'" + designer_name + "',designer_email = N'" + designer_email +
                     "',f_create_base=" + f_create_base + ",construction_type=N'" + construction_type + "',construction_start_date='" + construction_start_date + "',ms_proj_file_path = N'" + ms_proj_file_path + "',revit_version = N'" + revit_version + "' where project_id =(select id from project where proj_guid = N'" + Project_guid + "' and name = N'" + proj_name + "')"), conn);
                    cmd2.ExecuteNonQuery();
                }
                conn.Close();

            }
            catch (System.Exception ex)

            {

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);

            }

        }
    }
}