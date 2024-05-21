using fujita_BIM4D5D_planner;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BIM4D5D_service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service6" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service6.svc or Service6.svc.cs at the Solution Explorer and start debugging.
    public class Service6 : costplanner5D
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        SqlConnection con;
        public List<string> FindCostComponent(string category)
        {
            List<String> list_component = new List<String>();
            using (con = new SqlConnection(connection_string))
            {
                ConnectionState state = con.State;
                try
                {
                    con.Open();
                    string query = "select cost_component from BIM_5DPlanner where category = '" + @category + "';";
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list_component.Add(reader.GetString(0));
                            }
                            return list_component;
                        }
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
        }
    }
}