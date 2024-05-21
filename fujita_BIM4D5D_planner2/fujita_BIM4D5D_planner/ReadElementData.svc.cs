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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service4" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service4.svc or Service4.svc.cs at the Solution Explorer and start debugging.
    public class Service4 : ReadElementData
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        element_dtl element = new element_dtl();
        public element_dtl showallelm(string proj_id,string proj_name, int proj_version)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            { 
            using (con = new SqlConnection(connection_string))
            {
                    cmd = new SqlCommand(@"select * from Revit_project_model where proj_version_id in(select id from revit_project_version where project_id=(select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version=" + proj_version + ");", con);
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable("element");
                    sda.Fill(dt);
                    element.element_detail = dt;
                return element;
            }
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
    }
}