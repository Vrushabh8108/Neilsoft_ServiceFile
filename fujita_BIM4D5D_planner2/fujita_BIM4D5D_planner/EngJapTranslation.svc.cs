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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service24" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service24.svc or Service24.svc.cs at the Solution Explorer and start debugging.
    public class Service24 : EngJapTranslation
    {
        public List<Eng_jap_translation> Gettranslation(string lang)
        {
            string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
            SqlConnection con = new SqlConnection(connection_string);
            ConnectionState state = con.State;
            try
            {


                SqlCommand cmd;
                SqlDataAdapter sda;
                DataTable dt;
                List<Eng_jap_translation> Eng_jap_translation = new List<Eng_jap_translation>();
                using (con = new SqlConnection(connection_string))
                {
                    if (lang == "en")
                    {
                        cmd = new SqlCommand(@"select col_name,english_text from Eng_Jap_Translation;", con);
                        sda = new SqlDataAdapter(cmd);
                        dt = new DataTable("trans");
                        sda.Fill(dt);
                        
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Eng_jap_translation Eng_jap_translation1 = new Eng_jap_translation
                            {
                                col_name = Convert.ToString(dt.Rows[i]["col_name"]),
                                text = Convert.ToString(dt.Rows[i]["english_text"])
                               
                            };
                            Eng_jap_translation.Add(Eng_jap_translation1);
                        }
                    }
                    else
                    {
                        cmd = new SqlCommand(@"select col_name,japanese_text from Eng_Jap_Translation;", con);
                        sda = new SqlDataAdapter(cmd);
                        dt = new DataTable("trans");
                        sda.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Eng_jap_translation Eng_jap_translation1 = new Eng_jap_translation
                            {
                                col_name = Convert.ToString(dt.Rows[i]["col_name"]),
                                text = Convert.ToString(dt.Rows[i]["japanese_text"])

                            };
                            Eng_jap_translation.Add(Eng_jap_translation1);
                        }

                    }
                }
                return Eng_jap_translation;
            }
            catch (System.Exception ex)

            {

                if (state == ConnectionState.Open)
                {
                    con.Close();
                }

                return null;
            }
        }
        }
}
