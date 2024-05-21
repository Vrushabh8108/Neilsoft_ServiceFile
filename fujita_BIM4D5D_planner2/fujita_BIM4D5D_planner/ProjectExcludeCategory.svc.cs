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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service12" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service12.svc or Service12.svc.cs at the Solution Explorer and start debugging.
    public class Service12 : ProjectExcludeCategory
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        public Int16 ins_excl_category(string Project_guid,string proj_name, [Optional] Int64? version, string exclude_category, [Optional] Int64? proj_version)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                if (version == null)
                {
                    SqlCommand cmd1 = new SqlCommand((@"INSERT INTO project_exclude_category 
                  (proj_id,category) VALUES
                  ((select id from project where proj_guid = N'" + Project_guid + "' and name = N'" + proj_name + "'),N'" + exclude_category + "')"), conn);
                    cmd1.ExecuteNonQuery();

                }
                else
                {
                    SqlCommand cmd1 = new SqlCommand((@"INSERT INTO project_exclude_category 
                  (proj_id,proj_ver_id,category) VALUES
                  ((select id from project where proj_guid = N'" + Project_guid + "' and name = N'" + proj_name + "'),(select id from project_version where version = " + version + " and proj_ver_id in (select id from Revit_Project_Version where version = " + proj_version + " and project_id in (select id from project where proj_guid = N'" + Project_guid + "' and name = N'" + proj_name + "'))),N'" + exclude_category + "')"), conn);
                    cmd1.ExecuteNonQuery();
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
                return 0;

            }

        }
        public List<string> get_excl_category(string Project_guid,string proj_name,[Optional] Int64? version, [Optional] Int64? proj_version)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            List<string> proj_exclude_category = new List<string>();
            string query;
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                if (version == null)
                {
                     query = "select category from project_exclude_category where proj_id in" +
                      @"(select id from project where proj_guid = N'" + Project_guid + "' and name = N'" + proj_name + "') and proj_ver_id is null";
                }
                else
                {
                     query = "select category from project_exclude_category where proj_id in" +
                      @"(select id from project where proj_guid = N'" + Project_guid + "' and name = N'" + proj_name + "') and proj_ver_id in (select id from project_version where version = " + version + " and proj_ver_id in (select id from Revit_Project_Version where version = " + proj_version + " and project_id in (select id from project where proj_guid = N'" + Project_guid + "' and name = N'" + proj_name + "')))";
                }
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            proj_exclude_category.Add(reader.GetString(0));
                        }

                    }
                }
                    conn.Close();
                return proj_exclude_category;

            }
            catch (System.Exception ex)

            {

                if (state == ConnectionState.Open)
                {

                    conn.Close();
                }
                return null;

            }

        }
        public Int16 del_excl_category(string Project_guid,string proj_name, [Optional] Int64? version, List<string> exclude_category, [Optional] Int64? proj_version)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            List<string> proj_exclude_category = exclude_category;
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                if (version == null)
                {
                    for (int i = 0; i < proj_exclude_category.Count; i++)
                    {
                        SqlCommand cmd1 = new SqlCommand((@"delete from project_exclude_category where proj_id in" +
                          @"(select id from project where proj_guid = N'" + Project_guid + "' and name = N'" + proj_name + "') and proj_ver_id is null and category = N'" + proj_exclude_category[i] + "';"), conn);
                        cmd1.ExecuteNonQuery();
                    }
                }
                else
                {
                    for (int i = 0; i < proj_exclude_category.Count; i++)
                    {
                        SqlCommand cmd1 = new SqlCommand((@"delete from project_exclude_category where proj_id in" +
                          @"(select id from project where proj_guid = N'" + Project_guid + "' and name = N'" + proj_name + "') and proj_ver_id in (select id from project_version where version = " + version + " and proj_ver_id in (select id from Revit_Project_Version where version = " + proj_version + " and project_id in (select id from project where proj_guid = N'" + Project_guid + "' and name = N'" + proj_name + "'))) and category = N'" + proj_exclude_category[i] + "';"), conn);
                        cmd1.ExecuteNonQuery();
                    }
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
                return 0;

            }

        }
    }
}
