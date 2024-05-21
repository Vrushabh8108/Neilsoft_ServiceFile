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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service29" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service29.svc or Service29.svc.cs at the Solution Explorer and start debugging.
    public class Service29 : Projectlock
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        public int updateprojlock(string proj_guid, string proj_name,int f_proj_lock, string locked_by,string city, string country)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                  
                        SqlCommand cmd2 = new SqlCommand("update Project set f_locked = "+f_proj_lock+",locked_by = N'"+locked_by+"' where proj_guid ="+
                            @"N'"+proj_guid+"' and name = N'"+proj_name+"' and city_id =(select id from City where name = N'"+city+"' and"+
                            @" country_id in (select id from Country_Code where country = N'"+country+"'));", conn);
                        cmd2.ExecuteNonQuery();
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
        public  proj_lock Projlockdtl(string proj_guid, string proj_name, string city, string country)
        {
            proj_lock lock_dtl = new proj_lock();
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select f_locked,locked_by from Project where proj_guid =" +
                            @"N'" + proj_guid + "' and name = N'" + proj_name + "' and city_id =(select id from City where name = N'" + city + "' and" +
                            @" country_id in (select id from Country_Code where country = N'" + country + "'));", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                lock_dtl.f_locked = Convert.ToInt16(dt.Rows[0]["f_locked"]);
                lock_dtl.locked_by = dt.Rows[0]["locked_by"].ToString();
                conn.Close();
                return lock_dtl;
            }
            catch (System.Exception ex)

            {


                if (state == ConnectionState.Open)
                {
                    conn.Close();

                }
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
                return null;

            }
        }

    }
}
