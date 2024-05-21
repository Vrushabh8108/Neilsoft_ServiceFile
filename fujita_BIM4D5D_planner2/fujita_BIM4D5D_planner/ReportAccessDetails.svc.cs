using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service26" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service26.svc or Service26.svc.cs at the Solution Explorer and start debugging.
    public class Service26 : ReportAccessDetails
    {
        string connectionString = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();



        public void ins_ReportAccessDetail(string reportName, string accessBy, [Optional] DateTime accessTime)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            ConnectionState state = conn.State;
            try
            {


                conn.Open();
                SqlCommand cmd = new SqlCommand((@"INSERT INTO Report_Access_Details(report_name,accessby,access_time) VALUES (N'" + reportName + "',N'" + accessBy + "', PARSE('" + accessTime + "' as datetime using 'en-US'))"), conn);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (state == ConnectionState.Open)
                {
                    conn.Close();
                }

                Service17 exc = new Service17();
                exc.SendErrorToText(ex);
            }
        }
    

        

        public reportAccess_dtl retrieve_ReportAccessDetail()
        {
            SqlConnection con = new SqlConnection(connectionString);
            ConnectionState state = con.State;
            SqlDataAdapter sda;
            DataTable dt;

            reportAccess_dtl report = new reportAccess_dtl();

            try
            {
                using (con)
                {
                    state = con.State;
                    SqlCommand cmd1 = new SqlCommand(@"select * from Report_Access_Details", con);
                    sda = new SqlDataAdapter(cmd1);
                    dt = new DataTable("AccessReport");
                    sda.Fill(dt);
                    report.access_dtl = dt;

                    return report;
                }

            }
            catch (Exception ex)
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
