using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BIM4D5D_service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service10" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service10.svc or Service10.svc.cs at the Solution Explorer and start debugging.
    public class Service10 : SignUp
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        public void sign_up(String usrname,string pwd,string first_name,string last_name,string email,long phone,long emp_id)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            int check;
            using (conn = new SqlConnection(connection_string))
            {
                conn.Open();
                string query = "select isnull((select 1 from username_password where username=N'" + usrname + @"'),0);";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    check = (int)command.ExecuteScalar();
                }

                if (check == 0)
                {

                    SqlCommand cmd = new SqlCommand((@"INSERT INTO username_password 
                                                    (username, 
                                                     password,first_name,last_name,email,phone,emp_id) 
                                        VALUES      (N'" + usrname + @"','" + pwd + @"','"+first_name+"','"+last_name+"','"+email+"',"+phone+","+emp_id+")"), conn);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}
