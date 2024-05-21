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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service28" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service28.svc or Service28.svc.cs at the Solution Explorer and start debugging.
    public class Service28 : AddJobVariance
    {
        public int AddJobVartypes(string name,string mat_name, string created_by, [Optional]Int64? category_id, [Optional]Int64? prev_seq)
        {
            string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                SqlCommand cmd;
                SqlCommand cmd1;
                SqlCommand cmd_seq;
                DataTable dt;
                SqlDataAdapter sda;
                Int64? seq;
                if (prev_seq == null)
                {
                    cmd = new SqlCommand(@"SELECT coalesce(max(seq)+1,1) seq FROM Material_Variance where material_id in  (select id from Material where name=N'" + mat_name + "') ; ", conn);
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable("mat");
                    sda.Fill(dt);
                    seq = Convert.ToInt64(dt.Rows[0]["seq"]);
                }
                else
                {
                    seq = prev_seq + 1;
                    cmd_seq = new SqlCommand((@"update Material_Variance 
                        set seq = seq + 1 where material_id in (select id from Material where name=N'" + mat_name + "') and seq > " + prev_seq), conn);
                    cmd_seq.ExecuteNonQuery();
                }
                if (category_id == null)
                {
                    cmd1 = new SqlCommand((@"INSERT INTO Material_Variance 
                        (material_id,variance,created_by,created_on,seq) 
           select (select id from Material where name=N'" + mat_name + "'),N'" + name + "',N'" + created_by + "',current_timestamp,"+seq ), conn);
                }
                else
                {
                    cmd1 = new SqlCommand((@"INSERT INTO Material_Variance 
                        (material_id,variance,created_by,created_on,category_id,seq) 
           select (select id from Material where name=N'" + mat_name + "'),N'" + name + "',N'" + created_by + "',current_timestamp,"+category_id+","+ seq), conn);
                }
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
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
                return 0;

            }
        }
    }
}
