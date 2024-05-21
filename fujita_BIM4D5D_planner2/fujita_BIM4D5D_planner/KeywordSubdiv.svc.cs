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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service31" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service31.svc or Service31.svc.cs at the Solution Explorer and start debugging.
    public class Service31 : SubdivisionKeyword
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();

        public List<subdiv_keyword_details> GetAllKeyword(bool isJPN)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;

            List<subdiv_keyword_details> lst_all_keyword = new List<subdiv_keyword_details>();
            try
            {
                conn.Open();
                SqlCommand cmd;
                if (isJPN)
                {
                    cmd = new SqlCommand("Select keyword_index,prop_name_eng,prop_name_jpn,specification_jpn from Subdivision_Keyword;", conn);
                }
                else
                {
                    cmd = new SqlCommand("Select keyword_index,prop_name_eng,prop_name_jpn,specification from Subdivision_Keyword;", conn);
                }
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    subdiv_keyword_details obj = new subdiv_keyword_details();
                    obj.keyword_index = Convert.ToInt64(dt.Rows[i]["keyword_index"]);
                    obj.propname_eng = dt.Rows[i]["prop_name_eng"].ToString();
                    obj.propname_jpn = dt.Rows[i]["prop_name_jpn"].ToString();
                    if (isJPN)
                    {
                        obj.specification = dt.Rows[i]["specification_jpn"].ToString();
                    }
                    else
                    {
                        obj.specification = dt.Rows[i]["specification"].ToString();
                    }
                    
                    lst_all_keyword.Add(obj);
                }

                conn.Close();
                return lst_all_keyword;
            }
            catch (Exception ex)
            {
                if (state == ConnectionState.Open)
                {
                    conn.Close();

                }
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
                return lst_all_keyword;
            }
        }

        public subdiv_keyword_details GetSpecificKeyword(Int64 key_index)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            subdiv_keyword_details obj = new subdiv_keyword_details();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select prop_name_eng,prop_name_jpn from Subdivision_Keyword where keyword_index = " + key_index + ";", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    obj = new subdiv_keyword_details();
                    obj.keyword_index = Convert.ToInt64(dt.Rows[i]["keyword_index"]);
                    obj.propname_eng = dt.Rows[i]["prop_name_eng"].ToString();
                    obj.propname_jpn = dt.Rows[i]["prop_name_jpn"].ToString();
                    obj.specification = dt.Rows[i]["specification"].ToString();

                }

                conn.Close();
                return obj;
            }
            catch (Exception ex)
            {
                if (state == ConnectionState.Open)
                {
                    conn.Close();

                }
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
                return obj;
            }
        }

        public string UpdateKeyword(Int64 key_index, string keyword_eng, string keyword_jpn,string user)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select COUNT(*) from Subdivision_Keyword where keyword_index = " + key_index + ";", conn);
                Int64 count = Convert.ToInt64(cmd.ExecuteScalar());
                if (count == 0)
                {
                    conn.Close();
                    return "No Index Found";
                }

                else
                {
                    cmd = new SqlCommand("Update Subdivision_Keyword set prop_name_eng = N'" + keyword_eng + "', prop_name_jpn = N'" + keyword_jpn + "', modified_by = N'" + user + "', modified_on = CURRENT_TIMESTAMP where keyword_index = " + key_index + ";", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return "Success";
                }
            }
            catch (Exception ex)
            {
                if (state == ConnectionState.Open)
                {
                    conn.Close();

                }
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
                return "Error";
            }
        }
    }
}
