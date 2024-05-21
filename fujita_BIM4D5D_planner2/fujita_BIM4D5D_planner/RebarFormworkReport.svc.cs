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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service15" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service15.svc or Service15.svc.cs at the Solution Explorer and start debugging.
    public class Service15 : RebarFormworkReport
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        rep_dtl rep = new rep_dtl();
        public rep_dtl showallrepdetail(string proj_id, string proj_name, int f_show_rebar)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {

                using (con = new SqlConnection(connection_string))
                {
                    if (f_show_rebar == 1)
                    {
                        cmd = new SqlCommand(@"select element_id,element_name,weight,no_of_mainbar,no_of_stirrups from Rebar_Formwork_Report rfr,Rebar_Parameters rp where rp.rep_id = rfr.id and proj_id in (select id from project where proj_guid = '" + proj_id + "' and name = N'" + proj_name + "');", con);

                    }
                    else
                    {
                        cmd = new SqlCommand(@"select element_id,element_name,area,cost,total_cost
                                                ,(select stuff(( select (','+ concat(length,'x',width)) as 'data()' from Formwork_Parameters_lb lb where lb.fw_id = rp.id for xml path('')),1,1,'')) length_width
                                                from Rebar_Formwork_Report rfr,Formwork_Parameters rp
                                                where rp.rep_id = rfr.id 
                                                and proj_id  in (select id from project where proj_guid = '" + proj_id + "' and name = N'" + proj_name + "');", con);
                    }
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable("projdtl");
                    sda.Fill(dt);
                    rep.rep_detail = dt;
                    return rep;
                }
            }
            catch (System.Exception ex)

            {


                if (state == ConnectionState.Open)
                {
                    conn.Close();
                }
                return null;

            }
        }
        public Int64 Put(List<rebar_dtl> rebar_dtl1,List<fw_dtl> fw_dtl1, string proj_id, string proj_name)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            Int64 check_proj_id;
            Int64 check_rep_id;
            Int64 fw_id;
            try
            {
                List<rebar_dtl> rebar_dtltemp = new List<rebar_dtl>();
                rebar_dtltemp = rebar_dtl1;
                using (conn = new SqlConnection(connection_string))
                {
                    conn.Open();
                    foreach (rebar_dtl rebar_dtltemp1 in rebar_dtltemp)
                    {
                        string query1 = "select isnull(count(id),0) FROM Rebar_Formwork_Report where proj_id in (select id from project where proj_guid = N'" + proj_id + "'  and name = N'" + proj_name + "') and element_name = N'" + rebar_dtltemp1.element_name + "' and element_id = " + rebar_dtltemp1.element_id+ ";";
                        using (SqlCommand command12 = new SqlCommand(query1, conn))
                        {
                            check_proj_id = (int)command12.ExecuteScalar();
                        }
                        if (check_proj_id == 0)
                        {
                            //Insert-Update Rebar_Formwork_Report table
                            SqlCommand cmd12 = new SqlCommand("insert into Rebar_Formwork_Report (proj_id,element_id,element_name)" +
                                         @"values ((select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "'),@element_id,@element_name)", conn);
                            cmd12.Parameters.AddWithValue("@element_id", ((object)rebar_dtltemp1.element_id) ?? DBNull.Value);
                            cmd12.Parameters.AddWithValue("@element_name", ((object)rebar_dtltemp1.element_name) ?? DBNull.Value);
                            cmd12.ExecuteNonQuery();
                        }
                        string query11 = "select isnull(id,0) FROM Rebar_Formwork_Report where proj_id in (select id from project where proj_guid = N'" + proj_id + "'  and name = N'" + proj_name + "') and element_name = N'" + rebar_dtltemp1.element_name + "' and element_id = " + rebar_dtltemp1.element_id + ";";
                        using (SqlCommand command12 = new SqlCommand(query11, conn))
                        {
                            check_rep_id = (Int64)command12.ExecuteScalar();
                        }
                        SqlCommand cmd120 = new SqlCommand("delete from Rebar_Parameters where rep_id = " + check_rep_id + ";", conn);
                        cmd120.ExecuteNonQuery();
                        SqlCommand cmd13 = new SqlCommand("insert into Rebar_Parameters (rep_id,weight,no_of_mainbar,no_of_stirrups)" +
                                    @"values (@rep_id,@weight,@no_of_mainbar,@no_of_stirrups)", conn);
                        cmd13.Parameters.AddWithValue("@rep_id", check_rep_id);
                        cmd13.Parameters.AddWithValue("@weight", ((object)rebar_dtltemp1.weight) ?? DBNull.Value);
                        cmd13.Parameters.AddWithValue("@no_of_mainbar", ((object)rebar_dtltemp1.no_of_mainbar) ?? DBNull.Value);
                        cmd13.Parameters.AddWithValue("@no_of_stirrups", ((object)rebar_dtltemp1.no_of_stirrups) ?? DBNull.Value);
                        cmd13.ExecuteNonQuery();
                       

                    }
                    conn.Close();

                }
                List<fw_dtl> fw_dtltemp = new List<fw_dtl>();
                fw_dtltemp = fw_dtl1;
                using (conn = new SqlConnection(connection_string))
                {
                    conn.Open();
                    foreach (fw_dtl fw_dtltemp1 in fw_dtltemp)
                    {
                        string query1 = "select isnull(count(id),0) FROM Rebar_Formwork_Report where proj_id in (select id from project where proj_guid = N'" + proj_id + "'  and name = N'" + proj_name + "') and element_name = N'" + fw_dtltemp1.element_name + "' and element_id = " + fw_dtltemp1.element_id + ";";
                        using (SqlCommand command12 = new SqlCommand(query1, conn))
                        {
                            check_proj_id = (int)command12.ExecuteScalar();
                        }
                        if (check_proj_id == 0)
                        {
                            //Insert-Update Rebar_Formwork_Report table
                            SqlCommand cmd12 = new SqlCommand("insert into Rebar_Formwork_Report (proj_id,element_id,element_name)" +
                                         @"values ((select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "'),@element_id,@element_name)", conn);
                            cmd12.Parameters.AddWithValue("@element_id", ((object)fw_dtltemp1.element_id) ?? DBNull.Value);
                            cmd12.Parameters.AddWithValue("@element_name", ((object)fw_dtltemp1.element_name) ?? DBNull.Value);
                            cmd12.ExecuteNonQuery();
                        }
                        string query11 = "select isnull(id,0) FROM Rebar_Formwork_Report where proj_id in (select id from project where proj_guid = N'" + proj_id + "'  and name = N'" + proj_name + "') and element_name = N'" + fw_dtltemp1.element_name + "' and element_id = " + fw_dtltemp1.element_id + ";";
                        using (SqlCommand command12 = new SqlCommand(query11, conn))
                        {
                            check_rep_id = (Int64)command12.ExecuteScalar();
                        }
                        SqlCommand cmd1201 = new SqlCommand("delete from Formwork_Parameters_lb where fw_id in (select id from Formwork_parameters where rep_id = " + check_rep_id + ");", conn);
                        cmd1201.ExecuteNonQuery();
                        SqlCommand cmd1202 = new SqlCommand("delete from Formwork_parameters where rep_id = " + check_rep_id + ";", conn);
                        cmd1202.ExecuteNonQuery();
                        SqlCommand cmd13 = new SqlCommand("insert into Formwork_parameters (rep_id,area,cost,total_cost)" +
                                    @"values (@rep_id,@area,@cost,@total_cost)", conn);
                        cmd13.Parameters.AddWithValue("@rep_id", check_rep_id);
                        cmd13.Parameters.AddWithValue("@area", ((object)fw_dtltemp1.area) ?? DBNull.Value);
                        cmd13.Parameters.AddWithValue("@cost", ((object)fw_dtltemp1.cost) ?? DBNull.Value);
                        cmd13.Parameters.AddWithValue("@total_cost", ((object)fw_dtltemp1.total_cost) ?? DBNull.Value);
                        cmd13.ExecuteNonQuery();
                        string query112 = "select isnull(id,0) FROM Formwork_parameters where rep_id = " + check_rep_id + ";";
                        using (SqlCommand command121 = new SqlCommand(query112, conn))
                        {
                            fw_id = (Int64)command121.ExecuteScalar();
                        }
                        List<string> lb1 = fw_dtltemp1.lb.Split(',').ToList();
                        for (int i =0; i < lb1.Count; i++)
                        {
                            List<string> lb_tmp = lb1[i].Split('x').ToList();
                            SqlCommand cmd14 = new SqlCommand("insert into Formwork_Parameters_lb (fw_id,length,width)" +
                                    @"values (@fw_id,@length,@width)", conn);
                            cmd14.Parameters.AddWithValue("@fw_id", fw_id);
                            cmd14.Parameters.AddWithValue("@length", decimal.Parse(lb_tmp[0]));
                            cmd14.Parameters.AddWithValue("@width", decimal.Parse(lb_tmp[1]));
                            cmd14.ExecuteNonQuery();
                        }
                       

                    }
                    conn.Close();

                }
                return 1;
            }
            catch (System.Exception ex)

            {
                if (state == ConnectionState.Open)
                {
                    conn.Close();
                }

                return 0;
            }
        }

            }
}
