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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service11" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service11.svc or Service11.svc.cs at the Solution Explorer and start debugging.
    public class Service11 : InsertUnitofmeasurement
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        public void ins_unit_of_measurement(string unit_of_measurement)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand((@"INSERT INTO unit_of_measurement 
                        (unit) 
            VALUES      (N'" + @unit_of_measurement + @"')"), conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (System.Exception ex)

            {


                if (state == ConnectionState.Open)
                {
                    conn.Close();

                }
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);

            }

        }
    }
}
