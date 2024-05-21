using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service3" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service3.svc or Service3.svc.cs at the Solution Explorer and start debugging.
    public class Service3 : ModifyRevitProj
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        public void mod_revit_model(string elmt_dtl,string Project_id, Int64 element_id, Int64 element_no, string usr)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            conn.Open();
            SqlCommand cmd = new SqlCommand((@"update Revit_project_model set elemnt_dtl=N'"+elmt_dtl+"',modified_by = '" + usr + "',modified_on = current_timestamp where " +
                @"Project_id = '" + Project_id + "' and element_id = " + element_id + "and element_no = " + element_no), conn);
            cmd.ExecuteNonQuery();
            conn.Close();

        }
    }
}
