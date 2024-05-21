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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service16" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service16.svc or Service16.svc.cs at the Solution Explorer and start debugging.
    public class Service16 : AddCost
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        public addcost_dtl readcostdtlprojelement(string proj_id, string proj_name, List<Int64> element_id,string country_code, string city, int proj_version)
        {
            SqlConnection con = new SqlConnection(connection_string);
            ConnectionState state = con.State;
            try
            {
                SqlCommand cmd;
                SqlDataAdapter sda;
                DataTable dt;
                List<long> elem_id = element_id;
                addcost_dtl cost_dtl1 = new addcost_dtl();
                using (con = new SqlConnection(connection_string))
                {
                    //for (int i = 0; i < elem_id.Count; i++)
                    //{
                        string list_element = string.Join(",", elem_id);
                        cmd = new SqlCommand(@"select element_id,element_type_id,elemnt_dtl from Revit_project_model where proj_version_id in (select id from Revit_Project_version where project_id in(select id from Project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "' and city_id in (select id from city where name = N'" + city + "' and country_id in (select id from country_code where country = N'" + country_code + "')))and version=" + proj_version + ") and element_id in (" + list_element + ");", con);
                        sda = new SqlDataAdapter(cmd);
                        dt = new DataTable("cost");
                        sda.Fill(dt);
                        cost_dtl1.addcost_detail = dt;
                   // }
                }
                return cost_dtl1;
            }
            catch (System.Exception ex)
            {
                if (state == ConnectionState.Open)
                {
                    con.Close();
                }
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);

                return null;
            }
        }
    }
}
