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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service14" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service14.svc or Service14.svc.cs at the Solution Explorer and start debugging.
    public class Service14 : MsprojofficeDtl
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        public Int16 ins_msprojoffice_diff(string Project_guid, string proj_name, string diff_guid, Int64 first_ver,Int64 second_ver,Int64 proj_ver)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();

                SqlCommand cmd1 = new SqlCommand((@"INSERT INTO msprojoffice_diff 
                  (proj_ver_id,comparison_guid,first_version,second_version) VALUES
                  ((select id from revit_project_version where version = "+proj_ver+" and project_id in (select id from project where proj_guid = N'" + Project_guid + "' and name = N'" + proj_name + "')),N'" + diff_guid + "',"+ first_ver + "," + second_ver + ")"), conn);
                    cmd1.ExecuteNonQuery();

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
        public int showpending(string proj_id, string proj_name,Int64 proj_ver)
        {
            SqlConnection conn;
            try
            {
                using (conn = new SqlConnection(connection_string))
                {
                    int f_available;
                    conn.Open();
                    string query_proj_maxver = "select top 1 1 from msprojoffice_diff where f_approved = 0 and proj_ver_id in (select id from Revit_Project_Version where version = "+proj_ver+" and project_id in (select id from project where proj_guid = '" + proj_id + "'  and name = N'" + proj_name + "'));";
                    using (SqlCommand command = new SqlCommand(query_proj_maxver, conn))
                    {
                        f_available = (int)command.ExecuteScalar();
                    }

                    return f_available;
                }

            }
            catch (System.Exception ex)
            {
                return 0;
            };
        }
        public diff_dtl readdiffdtl(string comparison_id)
        {
            SqlConnection con = new SqlConnection(connection_string);
            ConnectionState state = con.State;
            try
            {
                SqlCommand cmd;
                diff_dtl diff_dtl11 = new diff_dtl();
                SqlDataAdapter sda;
                DataTable dt;
                using (con = new SqlConnection(connection_string))
                {
                        cmd = new SqlCommand(@"select pj.proj_guid,pj.name,first_version,second_version,f_approved,ver.version from msprojoffice_diff ms,project pj,Revit_Project_Version ver where ms.proj_ver_id=ver.id and pj.id =ver.project_id and comparison_guid = N'" + comparison_id + @"';", con);
                        sda = new SqlDataAdapter(cmd);
                        dt = new DataTable("diff");
                        sda.Fill(dt);
                        int cnt = dt.Rows.Count;
                        if (cnt > 0)
                        {
                        diff_dtl11 = new diff_dtl
                        {
                            proj_guid = Convert.ToString(dt.Rows[0]["proj_guid"]),
                            proj_name = Convert.ToString(dt.Rows[0]["name"]),
                            from_ver = Convert.ToInt64(dt.Rows[0]["first_version"]),
                            to_ver = Convert.ToInt64(dt.Rows[0]["second_version"]),
                            f_approved = Convert.ToInt64(dt.Rows[0]["f_approved"]),
                            version = Convert.ToInt64(dt.Rows[0]["version"])
                        };
                        }
                }
                return diff_dtl11;
            }
            catch (System.Exception ex)
            {
                if (state == ConnectionState.Open)
                {
                    con.Close();
                }

                return null;
            }
        }
public Int16 upd_msprojoffice_diff(string Project_guid, string proj_name, string diff_guid,Int16 f_approved,Int64 proj_ver)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();

                SqlCommand cmd1 = new SqlCommand((@"update msprojoffice_diff 
                  set f_approved = "+ f_approved +
                  " where comparison_guid = N'"+ diff_guid + "' AND proj_ver_id in (select id from Revit_Project_Version where version = " + proj_ver+" and project_id in (select id from project where proj_guid = N'" + Project_guid + "' and name = N'" + proj_name + "'))"), conn);
                cmd1.ExecuteNonQuery();

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
