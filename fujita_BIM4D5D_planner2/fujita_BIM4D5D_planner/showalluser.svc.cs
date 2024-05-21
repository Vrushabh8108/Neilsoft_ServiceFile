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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service12" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service12.svc or Service12.svc.cs at the Solution Explorer and start debugging.
    public class Service12 : showalluser
    {
       string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        SqlCommand cmd1;
        SqlDataAdapter sda1;
        DataTable dt1;
        public List<User_dtl> showallusr(int flag)
        {

            using (con = new SqlConnection(connection_string))
            {
                cmd = new SqlCommand(@"SELECT  id,username,first_name,last_name,email,phone,emp_id,f_admin from username_password where f_active = " + flag, con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable("username");
                sda.Fill(dt);
                //user.user_detail = dt;
                List<User_dtl> User_list1 = new List<User_dtl>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmd1 = new SqlCommand(@"  select (select code from country_code where id = countryid) country_code,accessid from user_country_map where userid = "+dt.Rows[i]["id"]+" order by country_code asc", con);

                    sda1 = new SqlDataAdapter(cmd1);
                    dt1 = new DataTable("cost");
                    sda1.Fill(dt1);
                    List<country_access> country_access1 = new List<country_access>();
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        country_access country_access2 = new country_access
                        {
                            country_code = Convert.ToString(dt1.Rows[j]["country_code"]),
                            access_dtl= Convert.ToInt16(dt1.Rows[j]["accessid"])
                          };
                        country_access1.Add(country_access2);
                    }
                    User_dtl user_dtl1 = new User_dtl
                    {
                        username = Convert.ToString(dt.Rows[i]["username"]),
                        first_name = Convert.ToString(dt.Rows[i]["first_name"]),
                        last_name = Convert.ToString(dt.Rows[i]["last_name"]),
                        email = Convert.ToString(dt.Rows[i]["email"]),
                        phone = Convert.ToString(dt.Rows[i]["phone"]),
                        emp_id = Convert.ToString(dt.Rows[i]["emp_id"]),
                        f_admin = Convert.ToInt16(dt.Rows[i]["f_admin"]),
                        country_access = country_access1
                    };
                    User_list1.Add(user_dtl1);
                } 

                return User_list1;
            }
        }
    }
}
