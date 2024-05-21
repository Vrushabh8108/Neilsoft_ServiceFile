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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service9" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service9.svc or Service9.svc.cs at the Solution Explorer and start debugging.
    public class Service9 : MeasurementUnit
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        public List<string> Findmaterialsub()
        {
            using (con = new SqlConnection(connection_string))
            {
                List<string> units = new List<string>();
                cmd = new SqlCommand(@"select distinct unit from unit_of_measurement", con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable("Unit");
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string unitinfo;
                        unitinfo = dt.Rows[i]["unit"].ToString();
                        units.Add(unitinfo);
                    }
                }
                return units;
            }

        }
    }
}
