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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service27" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service27.svc or Service27.svc.cs at the Solution Explorer and start debugging.
    public class Service27 : AddJob
    {
        public int AddJobtypes(string name,string created_by,int f_virtual,[Optional]Int64? category_id,[Optional]Int64? prev_seq)
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
                Int64? seq = null;
                if (f_virtual == 1)
                {
                    if (prev_seq == null)
                    {
                        cmd = new SqlCommand(@"SELECT  coalesce(max(seq)+1,1) seq FROM Material where f_virtual = 1 ; ", conn);
                        sda = new SqlDataAdapter(cmd);
                        dt = new DataTable("mat");
                        sda.Fill(dt);
                        seq = Convert.ToInt64(dt.Rows[0]["seq"]);
                    }
                    else
                    {
                        seq = prev_seq + 1;
                        cmd_seq = new SqlCommand((@"update Material 
                        set seq = seq + 1 where seq > " + prev_seq + " and f_virtual = 1;"), conn);
                        cmd_seq.ExecuteNonQuery();
                    }
                }
                if (category_id == null)
                {
                     cmd1 = new SqlCommand((@"INSERT INTO Material 
                        (name,created_by,created_on,f_virtual,seq) 
           select N'" + name + "',N'" + created_by + "',current_timestamp," + f_virtual+","+seq), conn);
                }
                else
                {
                     cmd1 = new SqlCommand((@"INSERT INTO Material 
                        (name,created_by,created_on,f_virtual,category_id,seq) 
           select N'" + name + "',N'" + created_by + "',current_timestamp," + f_virtual + "," + category_id+","+seq), conn);
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
