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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service10" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service10.svc or Service10.svc.cs at the Solution Explorer and start debugging.
    public class Service10 : ProjectFilter
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        SqlConnection con;
        SqlCommand cmd;
        string filter_dtl;
        public string showfilter(string proj_id, string filter_name)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {

                using (conn = new SqlConnection(connection_string))
                {

                    conn.Open();
                    cmd = new SqlCommand(@"select filter_dtl from Project_filter where proj_id in (select id from project where proj_guid = '" + proj_id + "') and filter_name = N'" + filter_name + "'; ", conn);
                    filter_dtl = (string)cmd.ExecuteScalar();
                    conn.Close();
                    return filter_dtl;
                }
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
        public List<filter_dtl> showallfilter(string proj_id,string proj_name,[Optional] Int64?version, [Optional] Int64? proj_version)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            SqlDataAdapter sda;
            DataTable dt;
            List<filter_dtl> filter = new List<filter_dtl>();
            ConnectionState state = conn.State;
            try
            {

                using (conn = new SqlConnection(connection_string))
                {

                    conn.Open();
                    if (version == null)
                    {
                        cmd = new SqlCommand(@"select filter_name,id from Project_filter where proj_id in (select id from project where proj_guid = '" + proj_id + "' and name = N'" + proj_name + "') and proj_ver_id is null; ", conn);
                    }
                    else
                    {
                        cmd = new SqlCommand(@"select filter_name,id from Project_filter where proj_id in (select id from project where proj_guid = '" + proj_id + "' and name = N'" + proj_name + "') and proj_ver_id in (select id from project_version where version = " + version + " and proj_ver_id in (select id from Revit_Project_Version where version = " + proj_version + " and project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "'))); ", conn);
                    }
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable("element");
                    sda.Fill(dt);
                    
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        filter_dtl filter_dtl1 = new filter_dtl
                        {
                            filter_name = Convert.ToString(dt.Rows[i]["filter_name"]),
                            filter_element_dtl = Get_dtl(Convert.ToInt64(dt.Rows[i]["id"]))
                        };
                        filter.Add(filter_dtl1);
                    }
                    conn.Close();
                    return filter;
                }
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
        public List<filter_element> Get_dtl(Int64 id)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            SqlDataAdapter sda;
            ConnectionState state = conn.State;
            List<filter_element> filter_elem = new List<filter_element>();
            try
            {

                using (conn = new SqlConnection(connection_string))
                {

                    conn.Open();
                    cmd = new SqlCommand(@"select filter_category,filter_sign,filter_value from project_filter_dtl where filter_id = " + id + ";", conn);
                    sda = new SqlDataAdapter(cmd);
                    DataTable dt11 = new DataTable("filter");
                    sda.Fill(dt11);
                    for (int i1 = 0; i1 < dt11.Rows.Count; i1++)
                    {
                        filter_element filter_element1 = new filter_element
                        {
                            filter_category = Convert.ToString(dt11.Rows[i1]["filter_category"]),
                            filter_sign = Convert.ToString(dt11.Rows[i1]["filter_sign"]),
                            filter_value = Convert.ToString(dt11.Rows[i1]["filter_value"])
                        };
                        filter_elem.Add(filter_element1);
                    }
                    return filter_elem;
                }
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

        public Int16 ins_proj_filter(string Project_guid,string proj_name,[Optional] Int64? Version, string filter_name, List<filter_element> filter_dtl, [Optional] Int64? proj_version)
        {
            int check;
            Int64 filter_id;
            int proj_filter_check;
            string query1;
            string query21;
            string query2;
            List<filter_element> filter_element1 = new List<filter_element>();
            filter_element1 = filter_dtl;
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
                  (proj_guid,name) VALUES(N'" + Project_guid + "',N'" + proj_name + "')"), conn);
                    cmd.ExecuteNonQuery();
                    SqlCommand cmd1 = new SqlCommand((@"INSERT INTO Revit_Project_Version 
                  (project_id,version,created_on) VALUES((select id from project where proj_guid=N'" + Project_guid + @"' and name = N'" + proj_name + "'),1,current_timestamp)"), conn);
                    cmd1.ExecuteNonQuery();
                }
                if (Version == null)
                        {
                     query1 = "select isnull((select 1 from Project_filter where proj_id = (select id from project where proj_guid =N'" + Project_guid + @"' and name = N'" + proj_name + "') and proj_ver_id is null and filter_name = N'" + filter_name + "'),0);";
                        
                        }
                else
                {
                  query1 = "select isnull((select 1 from Project_filter where proj_id = (select id from project where proj_guid =N'" + Project_guid + @"' and name = N'" + proj_name + "') and proj_ver_id in (select id from project_version where version = " + Version + " and proj_id in (select id from project where proj_guid = N'" + Project_guid + "' and name = N'" + proj_name + "')) and filter_name = N'" + filter_name + "'),0);";
                }
                 using (SqlCommand command1 = new SqlCommand(query1, conn))
                {
                        proj_filter_check = (int)command1.ExecuteScalar();
                    }

                if (proj_filter_check == 1)
                {
                    if (Version == null)
                    {
                         query21 = "select id from Project_filter where proj_id = (select id from project where proj_guid =N'" + Project_guid + @"' and name = N'" + proj_name + "') and proj_ver_id is null and filter_name = N'" + filter_name + "'";
                    }
                    else
                    {
                         query21 = "select id from Project_filter where proj_id = (select id from project where proj_guid =N'" + Project_guid + @"' and name = N'" + proj_name + "') and proj_ver_id in (select id from project_version where version = " + Version + " and proj_ver_id in (select id from Revit_Project_Version where version = " + proj_version + " and project_id in (select id from project where proj_guid = N'" + Project_guid + "' and name = N'" + proj_name + "'))) and filter_name = N'" + filter_name + "'";
                    }
                    using (SqlCommand command11 = new SqlCommand(query21, conn))
                    {
                        filter_id = (Int64)command11.ExecuteScalar();
                    }
                    SqlCommand del_cmd = new SqlCommand((@"DELETE FROM project_filter_dtl WHERE filter_id = " + filter_id + ";"), conn);
                    del_cmd.ExecuteNonQuery();
                    SqlCommand del_cmd1 = new SqlCommand((@"DELETE FROM Project_filter WHERE ID = " + filter_id + ";"), conn);
                    del_cmd1.ExecuteNonQuery();
                }
                if (Version == null)
                {
                    SqlCommand cmd1 = new SqlCommand((@"INSERT INTO Project_filter 
                  (proj_id,filter_name) VALUES
                  ((select id from project where proj_guid = N'" + Project_guid + "' and name = N'" + proj_name + "'),N'" + filter_name + "')"), conn);
                    cmd1.ExecuteNonQuery();
                     query2 = "select id from Project_filter where proj_id = (select id from project where proj_guid =N'" + Project_guid + @"' and name = N'" + proj_name + "') and proj_ver_id is null and filter_name = N'" + filter_name + "'";
                }
                else
                {
                    SqlCommand cmd1 = new SqlCommand((@"INSERT INTO Project_filter 
                  (proj_id,filter_name,proj_ver_id) VALUES
                  ((select id from project where proj_guid = N'" + Project_guid + "' and name = N'" + proj_name + "'),N'" + filter_name + "',(select id from project_version where version = " + Version + " and proj_ver_id in (select id from Revit_Project_Version where version = " + proj_version + " and project_id in (select id from project where proj_guid = N'" + Project_guid + "' and name = N'" + proj_name + "'))))"), conn);
                    cmd1.ExecuteNonQuery();
                     query2 = "select id from Project_filter where proj_id = (select id from project where proj_guid =N'" + Project_guid + @"' and name = N'" + proj_name + "') and proj_ver_id in (select id from project_version where version = " + Version + " and proj_ver_id in (select id from Revit_Project_Version where version = " + proj_version + " and project_id in (select id from project where proj_guid = N'" + Project_guid + "' and name = N'" + proj_name + "'))) and filter_name = N'" + filter_name + "'";
                }
                    using (SqlCommand command1 = new SqlCommand(query2, conn))
                    {
                        filter_id = (Int64)command1.ExecuteScalar();
                    }
                    for (int j = 0; j < filter_element1.Count; j++)
                    {
                    SqlCommand cmd2 = new SqlCommand((@"INSERT INTO project_filter_dtl (filter_id,filter_category,filter_sign,filter_value) VALUES(" + filter_id + ",N'" + filter_element1[j].filter_category+"',N'"+ filter_element1[j].filter_sign+"',N'"+ filter_element1[j].filter_value+"')"), conn);
                        cmd2.ExecuteNonQuery();
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

