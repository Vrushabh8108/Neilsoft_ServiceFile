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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service10" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service10.svc or Service10.svc.cs at the Solution Explorer and start debugging.
    public class Service10 : oldmsprojoffice
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        public List<msproj_dtl> showmsprojdetail(string proj_id)
        {
            SqlConnection con = new SqlConnection(connection_string);
            ConnectionState state = con.State;
            try
            {


                SqlCommand cmd;
                SqlDataAdapter sda;
                DataTable dt;
                List<msproj_dtl> msprojoffice_dtl = new List<msproj_dtl>();
                using (con = new SqlConnection(connection_string))
                {
                    cmd = new SqlCommand(@"select * from project_task_details where proj_ver_id in (select id from project_version where version = 1 and proj_id in (select id from project where proj_guid = N'" + proj_id + "'));", con);
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable("element");
                    sda.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        msproj_dtl msprojoffice_dtl1 = new msproj_dtl
                        {
                            name = Convert.ToString(dt.Rows[i]["name"]),
                            seq = string.IsNullOrEmpty(dt.Rows[i]["seq"].ToString()) ? (Int32?)null : Convert.ToInt32(dt.Rows[i]["seq"]),
                            level = Convert.ToString(dt.Rows[i]["level"]),
                            start_date = Convert.ToDateTime(dt.Rows[i]["start_date"]),
                            end_date = Convert.ToDateTime(dt.Rows[i]["end_date"]),
                            duration = string.IsNullOrEmpty(dt.Rows[i]["duration"].ToString()) ? (Int32?)null : Convert.ToInt32(dt.Rows[i]["duration"]),
                            actual_dur = string.IsNullOrEmpty(dt.Rows[i]["act_duration"].ToString()) ? (Int32?)null : Convert.ToInt32(dt.Rows[i]["act_duration"]),
                            progress = string.IsNullOrEmpty(dt.Rows[i]["progress"].ToString()) ? (Int32?)null : Convert.ToInt32(dt.Rows[i]["progress"]),
                            cost = string.IsNullOrEmpty(dt.Rows[i]["cost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["cost"].ToString()),
                            Revit_elements = GetRevitelements(Convert.ToInt32(dt.Rows[i]["id"])),
                            predecessor = Getpredecessor(Convert.ToInt32(dt.Rows[i]["id"])),
                            successor = Getsuccessor(Convert.ToInt32(dt.Rows[i]["id"])),
                            resource = Getresource(Convert.ToInt32(dt.Rows[i]["id"]))
                        };
                        msprojoffice_dtl.Add(msprojoffice_dtl1);
                    }

                    return msprojoffice_dtl;
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
             public Int64 Put(List<msproj_dtl> msproj_dtl11, string proj_id)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                List<msproj_dtl> msproj_dtltemp = new List<msproj_dtl>();
                msproj_dtltemp = msproj_dtl11;
                Int64 proj_ver;
                Int64 check_proj_ver;
                int check1_1;
                int check_proj;
                string query1_1;
                using (conn = new SqlConnection(connection_string))
                {
                    conn.Open();
                    string query_proj = "select isnull((select 1 from project where proj_guid=N'" + proj_id + @"'),0);";
                    using (SqlCommand command = new SqlCommand(query_proj, conn))
                    {
                        check_proj = (int)command.ExecuteScalar();
                    }

                    if (check_proj == 0)
                    {
                        SqlCommand cmd = new SqlCommand((@"INSERT INTO PROJECT 
                  (proj_guid) VALUES(N'" + proj_id + "')"), conn);
                        cmd.ExecuteNonQuery();
                    }
                     string query1 = "select isnull((select 1 from project_version where version = 1 and proj_id in (select id from project where proj_guid = N'" + proj_id + "')),0);";
                     using (SqlCommand command1 = new SqlCommand(query1, conn))
                     {
                        check_proj_ver = (int)command1.ExecuteScalar();
                    }
                    if(check_proj_ver == 0)
                    {
                    SqlCommand cmd1 = new SqlCommand((@"INSERT INTO project_version (proj_id,version) VALUES((select id from project where proj_guid = N'" + proj_id + "'),1)"), conn);
                    cmd1.ExecuteNonQuery();
                     }
                    string query_proj_ver = "select id from project_version where version = 1 and proj_id in (select id from project where proj_guid = N'" + proj_id + "');";
                    using (SqlCommand command1 = new SqlCommand(query_proj_ver, conn))
                    {
                        proj_ver = (Int64)command1.ExecuteScalar();
                    }
                    query1_1 = "select isnull((select count(*) from project_task_details where proj_ver_id = " + proj_ver + "),0);";

                    using (SqlCommand command10 = new SqlCommand(query1_1, conn))
                    {
                        check1_1 = (int)command10.ExecuteScalar();
                    }
                    if (check1_1 != 0)
                    {
                        SqlCommand cmd120 = new SqlCommand("delete from task_revit_map where task_id in (select id from project_task_details  where proj_ver_id = " + proj_ver + ")", conn);
                        cmd120.ExecuteNonQuery();
                        SqlCommand cmd121 = new SqlCommand("delete from task_resource_map where task_id in (select id from project_task_details  where proj_ver_id = " + proj_ver + ")", conn);
                        cmd121.ExecuteNonQuery();
                        SqlCommand cmd122 = new SqlCommand("delete from task_predecessor_map where task_id in (select id from project_task_details  where proj_ver_id = " + proj_ver + ")", conn);
                        cmd122.ExecuteNonQuery();
                        SqlCommand cmd123 = new SqlCommand("delete from task_successor_map where task_id in (select id from project_task_details  where proj_ver_id = " + proj_ver + ")", conn);
                        cmd123.ExecuteNonQuery();
                        SqlCommand cmd124 = new SqlCommand("DELETE FROM project_task_details WHERE proj_ver_id  = " + proj_ver, conn);
                        cmd124.ExecuteNonQuery();
                    }
                    foreach (msproj_dtl msproj_dtl_1 in msproj_dtl11)
                    {

                        //Insert-Update project_task_details table
                        SqlCommand cmd12 = new SqlCommand("insert into project_task_details (proj_ver_id,Name,seq,level,start_date,end_date,duration,act_duration,progress,cost)" +
                                     @"values (" + proj_ver + ",@Name,@seq,@level,@start_date,@end_date,@duration,@act_duration,@progress,@cost)", conn);
                        cmd12.Parameters.AddWithValue("@Name", msproj_dtl_1.name);
                        cmd12.Parameters.AddWithValue("@seq", msproj_dtl_1.seq);
                        cmd12.Parameters.AddWithValue("@level", msproj_dtl_1.level);
                        cmd12.Parameters.AddWithValue("@start_date", msproj_dtl_1.start_date);
                        cmd12.Parameters.AddWithValue("@end_date", msproj_dtl_1.end_date);
                        cmd12.Parameters.AddWithValue("@duration", ((object)msproj_dtl_1.duration) ?? DBNull.Value);
                        cmd12.Parameters.AddWithValue("@act_duration", ((object)msproj_dtl_1.actual_dur) ?? DBNull.Value);
                        cmd12.Parameters.AddWithValue("@progress", ((object)msproj_dtl_1.progress) ?? DBNull.Value);
                        cmd12.Parameters.AddWithValue("@cost", ((object)msproj_dtl_1.cost) ?? DBNull.Value);
                        cmd12.ExecuteNonQuery();

                        string query3 = "select id from project_task_details where proj_ver_id = " + proj_ver + " and name = N'" + msproj_dtl_1.name + "'; ";
                        Nullable<Int64> proj_task_det;

                        using (SqlCommand command1 = new SqlCommand(query3, conn))
                        {
                            proj_task_det = (Int64)command1.ExecuteScalar();
                        }

                        //Insert-Update resource table

                        List<string> resource_name = msproj_dtl_1.resource;
                        for (int i12 = 0; i12 < resource_name.Count; i12++)
                        {
                            string query2 = "select id from resource where name = N'" + resource_name[i12] + "';";
                            Int64 res_id;
                            using (SqlCommand command1 = new SqlCommand(query2, conn))
                            {
                                res_id = (Int64)command1.ExecuteScalar();
                            }

                            SqlCommand cmd121 = new SqlCommand(string.Format("insert into task_resource_map (task_id,resource_id)" +
                         @"values ({0},{1})", proj_task_det, res_id), conn);
                            cmd121.ExecuteNonQuery();
                        }

                        //Insert-Update task_revit_map

                        List<int?> element_id = msproj_dtl_1.Revit_elements;
                        for (int i21 = 0; i21 < element_id.Count; i21++)
                        {
                            SqlCommand cmd211 = new SqlCommand(string.Format("insert into task_revit_map (task_id,element_id)" +
                        @"values ({0},{1})", proj_task_det, element_id[i21]), conn);
                            cmd211.ExecuteNonQuery();
                        }

                        //Insert-Update task_predecessor_map

                        List<int?> pred_map = msproj_dtl_1.predecessor;
                        for (int i31 = 0; i31 < pred_map.Count; i31++)
                        {
                            SqlCommand cmd311 = new SqlCommand(string.Format("insert into task_predecessor_map (task_id,predecessor_task_id)" +
                        @"values ({0},{1})", proj_task_det, pred_map[i31]), conn);
                            cmd311.ExecuteNonQuery();
                        }

                        //Insert-Update task_successor_map

                        List<int?> succ_map = msproj_dtl_1.successor;
                        for (int i41 = 0; i41 < succ_map.Count; i41++)
                        {
                            SqlCommand cmd411 = new SqlCommand(string.Format("insert into task_successor_map (task_id,successor_task_id)" +
                        @"values ({0},{1})", proj_task_det, succ_map[i41]), conn);
                            cmd411.ExecuteNonQuery();
                        }
                    }

                    return 1;
                    conn.Close();
                }
            }
            catch (Exception ex)

            {
                if (state == ConnectionState.Open)
                {
                    conn.Close();
                }

                return 0;
            }
        }

        public List<int?> GetRevitelements(int task_id)
        {
            List<int?> rev_elm = new List<int?>();
            SqlConnection conn = new SqlConnection(connection_string);
            conn.Open();
            SqlCommand cmd1 = new SqlCommand(string.Format("select element_id from task_revit_map where task_id ='{0}'", task_id), conn);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            for (int i1 = 0; i1 < dt1.Rows.Count; i1++)
            {
                int rev_elm1;
                rev_elm1 = Convert.ToInt32(dt1.Rows[i1]["element_id"]);
                rev_elm.Add(rev_elm1);
            }
            conn.Close();
            return rev_elm;
        }
        public List<int?> Getpredecessor(int task_id)
        {
            List<int?> pred = new List<int?>();
            SqlConnection conn = new SqlConnection(connection_string);
            conn.Open();
            SqlCommand cmd2 = new SqlCommand(string.Format("select predecessor_task_id from task_predecessor_map where task_id ='{0}'", task_id), conn);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            for (int i2 = 0; i2 < dt2.Rows.Count; i2++)
            {
                int pred1;
                pred1 = Convert.ToInt32(dt2.Rows[i2]["predecessor_task_id"]);
                pred.Add(pred1);
            }
            conn.Close();
            return pred;
        }
        public List<int?> Getsuccessor(int task_id)
        {
            List<int?> suc = new List<int?>();
            SqlConnection conn = new SqlConnection(connection_string);
            conn.Open();
            SqlCommand cmd3 = new SqlCommand(string.Format("select successor_task_id from task_successor_map where task_id ='{0}'", task_id), conn);
            SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);
            for (int i3 = 0; i3 < dt3.Rows.Count; i3++)
            {
                int suc1;
                suc1 = Convert.ToInt32(dt3.Rows[i3]["successor_task_id"]);
                suc.Add(suc1);
            }
            conn.Close();
            return suc;
        }
        public List<string> Getresource(int task_id)
        {
            List<string> res = new List<string>();
            SqlConnection conn = new SqlConnection(connection_string);
            conn.Open();
            SqlCommand cmd4 = new SqlCommand(string.Format("select name from resource where id in (select resource_id from task_resource_map where task_id in (select id from project_task_details where id ='{0}'))", task_id), conn);
            SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
            DataTable dt4 = new DataTable();
            da4.Fill(dt4);
            for (int i4 = 0; i4 < dt4.Rows.Count; i4++)
            {
                string res1;
                res1 = Convert.ToString(dt4.Rows[i4]["name"]);
                res.Add(res1);
            }
            conn.Close();
            return res;
        }
    }
    }
