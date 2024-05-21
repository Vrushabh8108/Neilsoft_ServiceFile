using fujita_BIM4D5D_planner;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace BIM4D5D_service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service13" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service13.svc or Service13.svc.cs at the Solution Explorer and start debugging.
    public class Service13 : ActivateDeactivateUsers
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        public void activate_deactivate(String email,int flag,List<country_access> country_access,[Optional]int? f_admin)
        {
            try
            {
                List<string> MaterialDetails = new List<string>();
                Int64 id;
                SqlConnection conn = new SqlConnection(connection_string);
                conn.Open();
                SqlCommand cmd = new SqlCommand((@"update username_password set f_active = " + flag + " where email = '" + email + "';"), conn);
                cmd.ExecuteNonQuery();
                foreach(country_access country_access1 in country_access)
                {
                    string query1 = "select isnull((select id from user_country_map where userid=(select id from username_password where email = '" + email + "') and countryid in (select id from country_code where code = '" +
                        country_access1.country_code + "')),0);";
                    using (SqlCommand command = new SqlCommand(query1, conn))
                    {
                        id = (Int64)command.ExecuteScalar();
                    }
                    if (id != 0)
                    {
                        SqlCommand cmd1_upd = new SqlCommand((@"UPDATE user_country_map SET accessid = " + country_access1.access_dtl + " where id = " + id), conn);
                        cmd1_upd.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmd1 = new SqlCommand((@"Insert into user_country_map(userid,countryid,accessid) values ((select id from username_password where email = '" + email + "'),(select id from country_code where code = '" +
                            country_access1.country_code + "')," + country_access1.access_dtl + ");"), conn);
                        cmd1.ExecuteNonQuery();
                    }
                }
                if(f_admin!=null )
                {
                    SqlCommand cmd_admin = new SqlCommand((@"update username_password set f_admin = " + f_admin + " where email = '" + email + "';"), conn);
                    cmd_admin.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch(System.Exception ex)
            {
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
            }

        }
    }
}
