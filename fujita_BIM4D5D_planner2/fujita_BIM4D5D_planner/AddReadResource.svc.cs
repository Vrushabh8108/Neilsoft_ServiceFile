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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service8" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service8.svc or Service8.svc.cs at the Solution Explorer and start debugging.
    public class Service8 : AddReadResource
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        List<string> resource_name = new List<string>();
        string resource;
        public List<string> showallresource()
        {
            SqlConnection con = new SqlConnection(connection_string);
            ConnectionState state = con.State;
            try
            { 
            using (con = new SqlConnection(connection_string))
            {
                    cmd = new SqlCommand(@"select name from resource;", con);
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable("resorce");
                    sda.Fill(dt);
                for(int i=0; i < dt.Rows.Count; i++)
                {
                        resource = dt.Rows[i]["name"].ToString();
                        resource_name.Add(resource);
                }
                return resource_name;
            }
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
        public int addnewresource(List<string> resource_name)
        {
            SqlConnection con = new SqlConnection(connection_string);
            ConnectionState state = con.State;
            try
            {
                int check;
                using (con = new SqlConnection(connection_string))
                {
                    con.Open();
                    for (int i1 = 0; i1 < resource_name.Count; i1++)
                    {
                        string query = "select isnull((select 1 from resource where name=N'" + resource_name[i1] + "'),0);";
                        using (SqlCommand command = new SqlCommand(query, con))
                        {
                            check = (int)command.ExecuteScalar();
                        }

                        if (check == 0)
                        {
                            SqlCommand cmd = new SqlCommand((@"INSERT INTO resource 
                  (name) VALUES(N'" + resource_name[i1] + "')"), con);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    con.Close();
                }
                return 1;
            }
            catch (System.Exception ex)

            {


                if (state == ConnectionState.Open)
                {
                    con.Close();
                  
                }
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
                return 0;

            }
        }
    }
}
