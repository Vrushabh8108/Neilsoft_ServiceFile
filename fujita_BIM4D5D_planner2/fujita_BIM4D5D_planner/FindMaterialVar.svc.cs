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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FindMaterialOption" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select FindMaterialOption.svc or FindMaterialOption.svc.cs at the Solution Explorer and start debugging.
    public class FindMaterialOption : IFindMaterialOption
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        public List<string> FindMaterialVar(String Mat)
        {
            List<string> MaterialOption = new List<string>();
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select variance from material_variance where material_id =(select id from material where name='" + Mat + "') order by seq;", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string MaterialInfo;
                        MaterialInfo = dt.Rows[i]["variance"].ToString();
                        MaterialOption.Add(MaterialInfo);
                    }
                }
                conn.Close();
                return MaterialOption;
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