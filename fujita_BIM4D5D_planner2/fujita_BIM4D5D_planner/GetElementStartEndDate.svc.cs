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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service11" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service11.svc or Service11.svc.cs at the Solution Explorer and start debugging.
    public class Service11 : GetElementStartEndDate
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        public List<Rev_element_dtl> readelementdtl(string proj_id,string proj_name,Int64 ver,List<Int64> element_id,Int64 Proj_ver)
        {
            SqlConnection con = new SqlConnection(connection_string);
            ConnectionState state = con.State;
            try
            {
                SqlCommand cmd;
                SqlDataAdapter sda;
                DataTable dt;
                List<long> elem_id = element_id;
                List<Rev_element_dtl> rev_elem = new List<Rev_element_dtl>();
                using (con = new SqlConnection(connection_string))
                {
                    for (int i = 0; i < elem_id.Count; i++)
                    {
                        cmd = new SqlCommand(@"select element_id,cost,start_date,end_date
                                                from task_revit_map tmap,project_task_details dtl
                                                where tmap.task_id=dtl.id
                                                and tmap.element_id=" + elem_id[i]
                                                + @"and proj_ver_id in (select id from project_version where version="+ ver + @"and proj_ver_id  in
                                                 (select id from Revit_Project_Version where version = "+Proj_ver +" and project_id in (select id from project where proj_guid = N'" + proj_id + "' and name =N'"+ proj_name +"')));", con);
                        sda = new SqlDataAdapter(cmd);
                        dt = new DataTable("element");
                        sda.Fill(dt);
                        int cnt = dt.Rows.Count;
                        if (cnt > 0)
                        {
                            Rev_element_dtl rev_dtl11 = new Rev_element_dtl
                            {
                                revit_element_id = string.IsNullOrEmpty(dt.Rows[0]["element_id"].ToString()) ? (Int64?)null : Convert.ToInt64(dt.Rows[0]["element_id"]),
                                cost = string.IsNullOrEmpty(dt.Rows[0]["cost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[0]["cost"].ToString()),
                                start_date = Convert.ToDateTime(dt.Rows[0]["start_date"]),
                                end_date = Convert.ToDateTime(dt.Rows[0]["end_date"]),
                            };
                            rev_elem.Add(rev_dtl11);
                        }
                    }
                }
                return rev_elem;
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
