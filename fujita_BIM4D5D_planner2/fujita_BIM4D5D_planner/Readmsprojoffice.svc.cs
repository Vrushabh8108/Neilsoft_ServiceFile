﻿using System;
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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service7" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service7.svc or Service7.svc.cs at the Solution Explorer and start debugging.
    public class Service7 : Readmsprojoffice
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        public Int64 showmaxversion (string proj_id, string proj_name, int proj_version)
        {
            SqlConnection conn;
            try
            {
                using (conn = new SqlConnection(connection_string))
                {
                    Int64 max_ver;
                    conn.Open();
                    string query_proj_maxver = "select max(version) from project_version where f_reject <> 1 and proj_ver_id in (select id from Revit_Project_Version where version = " + proj_version+" and project_id in (select id from project where proj_guid = '" + proj_id + "'  and name = N'" + proj_name + "'));";
                    using (SqlCommand command = new SqlCommand(query_proj_maxver, conn))
                    {
                        max_ver = (Int64)command.ExecuteScalar();
                    }

                    return max_ver;
                }
               
            }
            catch(System.Exception)
            {
                return 0;
            };
        }
        public msproj_ver_dtl showmsprojdetail(string proj_id, string proj_name,Int64 proj_ver,Int64 version   )
        {
            SqlConnection con = new SqlConnection(connection_string);
            ConnectionState state = con.State;
            try
            {


                SqlCommand cmd;
                SqlDataAdapter sda;
                DataTable dt;
                SqlCommand cmd1;
                SqlDataAdapter sda1;
                DataTable dt1;
                msproj_ver_dtl msproj_ver_dtl1 = new msproj_ver_dtl();
                List<msproj_dtl> msprojoffice_dtl = new List<msproj_dtl>();
                using (con = new SqlConnection(connection_string))
                {
                    cmd = new SqlCommand(@"select * from project_task_details where proj_ver_id in (select id from project_version where version = " + version + " and proj_ver_id in (select id from Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "  '  and name = N'" + proj_name + "') and version = "+proj_ver+"));", con);
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
                            plan_start_date = Convert.ToDateTime(dt.Rows[i]["plan_start_date"]),
                            plan_end_date = Convert.ToDateTime(dt.Rows[i]["plan_end_date"]),
                            plan_duration = string.IsNullOrEmpty(dt.Rows[i]["plan_duration"].ToString()) ? (Int32?)null : Convert.ToInt32(dt.Rows[i]["plan_duration"]),
                            start_date = Convert.ToDateTime(dt.Rows[i]["start_date"]),
                            end_date = Convert.ToDateTime(dt.Rows[i]["end_date"]),
                            duration = string.IsNullOrEmpty(dt.Rows[i]["duration"].ToString()) ? (Int32?)null : Convert.ToInt32(dt.Rows[i]["duration"]),
                            actual_dur = string.IsNullOrEmpty(dt.Rows[i]["act_duration"].ToString()) ? (Int32?)null : Convert.ToInt32(dt.Rows[i]["act_duration"]),
                            progress = string.IsNullOrEmpty(dt.Rows[i]["progress"].ToString()) ? (Int32?)null : Convert.ToInt32(dt.Rows[i]["progress"]),
                            cost = string.IsNullOrEmpty(dt.Rows[i]["cost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["cost"].ToString()),
                            successor_relation_type = Convert.ToString(dt.Rows[i]["successor_relation_type"]),
                            predecessor_relation_type = Convert.ToString(dt.Rows[i]["predecessor_relation_type"]),
                            Revit_elements = GetRevitelements(Convert.ToInt32(dt.Rows[i]["id"])),
                            predecessor = Getpredecessor(Convert.ToInt32(dt.Rows[i]["id"])),
                            successor = Getsuccessor(Convert.ToInt32(dt.Rows[i]["id"])),
                            resource = Getresource(Convert.ToInt32(dt.Rows[i]["id"]))
                        };
                        msprojoffice_dtl.Add(msprojoffice_dtl1);
                    }
                    cmd1 = new SqlCommand(@"select f_base_version,f_locked,f_baseversion_created,f_baseversion_updated from project_version where version = " + version + " and proj_ver_id in (select id from Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "  '  and name = N'" + proj_name + "') and version = " + proj_ver + ");", con);
                    sda1 = new SqlDataAdapter(cmd1);
                    dt1 = new DataTable("element1");
                    sda1.Fill(dt1);
                    msproj_ver_dtl1.f_base_version = Convert.ToInt64(dt1.Rows[0]["f_base_version"]);
                    msproj_ver_dtl1.f_locked = Convert.ToInt64(dt1.Rows[0]["f_locked"]);
                    msproj_ver_dtl1.f_baseversion_created = Convert.ToInt64(dt1.Rows[0]["f_baseversion_created"]);
                    msproj_ver_dtl1.f_baseversion_updated = Convert.ToInt64(dt1.Rows[0]["f_baseversion_updated"]);
                    msproj_ver_dtl1.msproj_dtl1 = msprojoffice_dtl;
                    return msproj_ver_dtl1;
                }
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
        public void projrevreject(string proj_id, string proj_name, Int64 version,Int16 version_status,Int64 proj_ver)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand((@"update project_version set f_reject= "+ version_status + " where " +
                 @"proj_ver_id = (select id from Revit_Project_Version where project_id in (select id from project where proj_guid = '" + proj_id + "'  and name = N'" + proj_name + "') and version = "+proj_ver+") and version = " + version), conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (System.Exception ex)

            {

                if (state == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        public Int64 Put(List<msproj_dtl> msproj_dtl11, string proj_id, string proj_name, bool? saveonly, Int64 proj_version,bool f_save)
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
                string query_proj_ver;
                int check_proj_approved;
                using (conn = new SqlConnection(connection_string))
                {
                    conn.Open();
                    string query_proj = "select isnull((select 1 from project where proj_guid=N'" + proj_id + @"' and name = N'" + proj_name + "'),0);";
                    using (SqlCommand command = new SqlCommand(query_proj, conn))
                    {
                        check_proj = (int)command.ExecuteScalar();
                    }

                    if (check_proj == 0)
                    {
                        SqlCommand cmd = new SqlCommand((@"INSERT INTO PROJECT 
                  (proj_guid,name) VALUES(N'" + proj_id + "',N'" + proj_name + "')"), conn);
                        cmd.ExecuteNonQuery();
                    }
                    // string query1 = "select isnull((select 1 from project_version where version = 1 and proj_id in (select id from project where proj_guid = N'" + proj_id + "')),0);";
                    // using (SqlCommand command1 = new SqlCommand(query1, conn))
                    // {
                    //    check_proj_ver = (int)command1.ExecuteScalar();
                    //}
                    //if(check_proj_ver == 0)
                    //{
                    string query1 = "select isnull(max(version),0) FROM project_version where proj_ver_id in (select id from Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "'  and name = N'" + proj_name + "') and version = " + proj_version + ") and f_reject <>1;";
                    using (SqlCommand command12 = new SqlCommand(query1, conn))
                    {
                        check_proj_ver = (Int64)command12.ExecuteScalar();
                    }
                    if (f_save == true)
                    {
                        if (saveonly == false)
                        {
                            SqlCommand cmd1 = new SqlCommand((@"INSERT INTO project_version (proj_ver_id,version) VALUES((select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ")," + (check_proj_ver + 1) + ")"), conn);
                            cmd1.ExecuteNonQuery();
                            SqlCommand cmd2 = new SqlCommand((@"UPDATE Project_Version SET f_base_version = (select f_base_version from project_version where proj_ver_id = (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ") and version = " + (check_proj_ver) + ") where proj_ver_id = (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ") and version = " + (check_proj_ver + 1)), conn);
                            cmd2.ExecuteNonQuery();
                            SqlCommand cmd3 = new SqlCommand((@"UPDATE Project_Version SET f_locked = (select f_locked from project_version where proj_ver_id = (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ") and version = " + (check_proj_ver) + ") where proj_ver_id = (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ") and version = " + (check_proj_ver + 1)), conn);
                            cmd3.ExecuteNonQuery();
                            SqlCommand cmd4 = new SqlCommand((@"UPDATE Project_Version SET f_baseversion_created = (select f_baseversion_created from project_version where proj_ver_id = (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ") and version = " + (check_proj_ver) + ") where proj_ver_id = (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ") and version = " + (check_proj_ver + 1)), conn);
                            cmd4.ExecuteNonQuery();
                            SqlCommand cmd5 = new SqlCommand((@"UPDATE Project_Version SET f_baseversion_updated = (select f_baseversion_updated from project_version where proj_ver_id = (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ") and version = " + (check_proj_ver) + ") where proj_ver_id = (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ") and version = " + (check_proj_ver + 1)), conn);
                            cmd5.ExecuteNonQuery();
                            query_proj_ver = "select id from project_version where version = " + (check_proj_ver + 1) + " and proj_ver_id in (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ");";
                        }
                        else if (saveonly == true)
                        {
                            query_proj_ver = "select id from project_version where version = " + check_proj_ver + " and proj_ver_id in (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "')and version = " + proj_version + ");";
                        }
                        else
                        {
                            SqlCommand cmd1 = new SqlCommand((@"INSERT INTO project_version (proj_ver_id,version) VALUES((select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + "),0)"), conn);
                            cmd1.ExecuteNonQuery();
                            query_proj_ver = "select id from project_version where version = 0 and proj_ver_id in (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ");";
                        }
                    }
                    else
                    {
                        if (saveonly == true)
                        {
                            string query_approved = "select isnull(count(version),0) FROM project_version where proj_ver_id in (select id from Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "'  and name = N'" + proj_name + "') and version = " + proj_version + ") and f_reject not in (1,2);";
                            using (SqlCommand command12 = new SqlCommand(query_approved, conn))
                            {
                                check_proj_approved = (int)command12.ExecuteScalar();
                            }
                            if (check_proj_approved < 1)
                            {
                                SqlCommand cmd1 = new SqlCommand((@"INSERT INTO project_version (proj_ver_id,version) VALUES((select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ")," + (check_proj_ver + 1) + ")"), conn);
                                cmd1.ExecuteNonQuery();
                                SqlCommand cmd2 = new SqlCommand((@"UPDATE Project_Version SET f_base_version = (select f_base_version from project_version where proj_ver_id = (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ") and version = " + (check_proj_ver) + ") where proj_ver_id = (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ") and version = " + (check_proj_ver + 1)), conn);
                                cmd2.ExecuteNonQuery();
                                SqlCommand cmd3 = new SqlCommand((@"UPDATE Project_Version SET f_locked = (select f_locked from project_version where proj_ver_id = (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ") and version = " + (check_proj_ver) + ") where proj_ver_id = (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ") and version = " + (check_proj_ver + 1)), conn);
                                cmd3.ExecuteNonQuery();
                                SqlCommand cmd4 = new SqlCommand((@"UPDATE Project_Version SET f_baseversion_created = (select f_baseversion_created from project_version where proj_ver_id = (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ") and version = " + (check_proj_ver) + ") where proj_ver_id = (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ") and version = " + (check_proj_ver + 1)), conn);
                                cmd4.ExecuteNonQuery();
                                SqlCommand cmd5 = new SqlCommand((@"UPDATE Project_Version SET f_baseversion_updated = (select f_baseversion_updated from project_version where proj_ver_id = (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ") and version = " + (check_proj_ver) + ") where proj_ver_id = (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ") and version = " + (check_proj_ver + 1)), conn);
                                cmd5.ExecuteNonQuery();

                                query_proj_ver = "select id from project_version where version = " + (check_proj_ver + 1) + " and proj_ver_id in (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ");";
                            }

                            else
                            {
                                query_proj_ver = "select id from project_version where version = " + check_proj_ver + " and proj_ver_id in (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ");";
                            }
                    }

                        else if (saveonly == false)
                        {
                            query_proj_ver = "select id from project_version where version = " + check_proj_ver + " and proj_ver_id in (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "')and version = " + proj_version + ");";
                        }
                        else
                        {
                            SqlCommand cmd1 = new SqlCommand((@"INSERT INTO project_version (proj_ver_id,version) VALUES((select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + "),0)"), conn);
                            cmd1.ExecuteNonQuery();
                            query_proj_ver = "select id from project_version where version = 0 and proj_ver_id in (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ");";
                        }

                    }
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
                        SqlCommand cmd12 = new SqlCommand("insert into project_task_details (proj_ver_id,Name,seq,level,plan_start_date,plan_end_date,plan_duration,start_date,end_date,duration,act_duration,progress,cost,successor_relation_type,predecessor_relation_type)" +
                                     @"values (" + proj_ver + ",@Name,@seq,@level,@plan_start_date,@plan_end_date,@plan_duration,@start_date,@end_date,@duration,@act_duration,@progress,@cost,@successor_relation_type,@predecessor_relation_type)", conn);
                        cmd12.Parameters.AddWithValue("@Name", msproj_dtl_1.name);
                        cmd12.Parameters.AddWithValue("@seq", msproj_dtl_1.seq);
                        cmd12.Parameters.AddWithValue("@level", msproj_dtl_1.level);
                        cmd12.Parameters.AddWithValue("@start_date", msproj_dtl_1.start_date);
                        cmd12.Parameters.AddWithValue("@end_date", msproj_dtl_1.end_date);
                        cmd12.Parameters.AddWithValue("@duration", ((object)msproj_dtl_1.duration) ?? DBNull.Value);
                        cmd12.Parameters.AddWithValue("@plan_start_date", msproj_dtl_1.plan_start_date);
                        cmd12.Parameters.AddWithValue("@plan_end_date", msproj_dtl_1.plan_end_date);
                        cmd12.Parameters.AddWithValue("@plan_duration", ((object)msproj_dtl_1.plan_duration) ?? DBNull.Value);
                        cmd12.Parameters.AddWithValue("@act_duration", ((object)msproj_dtl_1.actual_dur) ?? DBNull.Value);
                        cmd12.Parameters.AddWithValue("@progress", ((object)msproj_dtl_1.progress) ?? DBNull.Value);
                        cmd12.Parameters.AddWithValue("@cost", ((object)msproj_dtl_1.cost) ?? DBNull.Value);
                        cmd12.Parameters.AddWithValue("@successor_relation_type", ((object)msproj_dtl_1.successor_relation_type) ?? DBNull.Value);
                        cmd12.Parameters.AddWithValue("@predecessor_relation_type", ((object)msproj_dtl_1.predecessor_relation_type) ?? DBNull.Value);
                        cmd12.ExecuteNonQuery();

                        string query3 = "select id from project_task_details where proj_ver_id = " + proj_ver + " and seq = " + msproj_dtl_1.seq + "; ";
                        long? proj_task_det;

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

                    if (saveonly == false)
                    {
                        return (check_proj_ver + 1);
                    }
                    else
                    {
                        return check_proj_ver;
                    }
                    conn.Close();
                }
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

        //public Int64 Put(List<msproj_dtl> msproj_dtl11, string proj_id, string proj_name, bool? saveonly, Int64 proj_version, Int64 cur_version)
        //{
        //    SqlConnection conn = new SqlConnection(connection_string);
        //    ConnectionState state = conn.State;
        //    try
        //    {
        //        List<msproj_dtl> msproj_dtltemp = new List<msproj_dtl>();
        //        msproj_dtltemp = msproj_dtl11;
        //        Int64 proj_ver;
        //        Int64 check_proj_ver;
        //        int check1_1;
        //        int check_proj;
        //        string query1_1;
        //        string query_proj_ver;
        //        using (conn = new SqlConnection(connection_string))
        //        {
        //            conn.Open();
        //            string query_proj = "select isnull((select 1 from project where proj_guid=N'" + proj_id + @"' and name = N'" + proj_name + "'),0);";
        //            using (SqlCommand command = new SqlCommand(query_proj, conn))
        //            {
        //                check_proj = (int)command.ExecuteScalar();
        //            }

        //            if (check_proj == 0)
        //            {
        //                SqlCommand cmd = new SqlCommand((@"INSERT INTO PROJECT 
        //          (proj_guid,name) VALUES(N'" + proj_id + "',N'" + proj_name + "')"), conn);
        //                cmd.ExecuteNonQuery();
        //            }
        //            // string query1 = "select isnull((select 1 from project_version where version = 1 and proj_id in (select id from project where proj_guid = N'" + proj_id + "')),0);";
        //            // using (SqlCommand command1 = new SqlCommand(query1, conn))
        //            // {
        //            //    check_proj_ver = (int)command1.ExecuteScalar();
        //            //}
        //            //if(check_proj_ver == 0)
        //            //{
        //            string query1 = "select isnull(max(version),0) FROM project_version where proj_ver_id in (select id from Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "'  and name = N'" + proj_name + "') and version = " + proj_version + ") and f_reject <>1;";
        //            using (SqlCommand command12 = new SqlCommand(query1, conn))
        //            {
        //                check_proj_ver = (Int64)command12.ExecuteScalar();
        //            }
        //            if (saveonly == true)
        //            {
        //                SqlCommand cmd1 = new SqlCommand((@"INSERT INTO project_version (proj_ver_id,version) VALUES((select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ")," + (check_proj_ver + 1) + ")"), conn);
        //                cmd1.ExecuteNonQuery();
        //                SqlCommand cmd2 = new SqlCommand((@"UPDATE Project_Version SET f_base_version = (select f_base_version from project_version where proj_ver_id = (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ") and version = " + (check_proj_ver) + ") where proj_ver_id = (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ") and version = " + (check_proj_ver + 1)), conn);
        //                cmd2.ExecuteNonQuery();
        //                SqlCommand cmd3 = new SqlCommand((@"UPDATE Project_Version SET f_locked = (select f_locked from project_version where proj_ver_id = (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ") and version = " + (check_proj_ver) + ") where proj_ver_id = (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ") and version = " + (check_proj_ver + 1)), conn);
        //                cmd3.ExecuteNonQuery();
        //                SqlCommand cmd4 = new SqlCommand((@"UPDATE Project_Version SET f_baseversion_created = (select f_baseversion_created from project_version where proj_ver_id = (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ") and version = " + (check_proj_ver) + ") where proj_ver_id = (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ") and version = " + (check_proj_ver + 1)), conn);
        //                cmd4.ExecuteNonQuery();
        //                SqlCommand cmd5 = new SqlCommand((@"UPDATE Project_Version SET f_baseversion_updated = (select f_baseversion_updated from project_version where proj_ver_id = (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ") and version = " + (check_proj_ver) + ") where proj_ver_id = (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ") and version = " + (check_proj_ver + 1)), conn);
        //                cmd5.ExecuteNonQuery();
        //                query_proj_ver = "select id from project_version where version = " + (check_proj_ver + 1) + " and proj_ver_id in (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ");";
        //            }
        //            else if (saveonly == false)
        //            {
        //                query_proj_ver = "select id from project_version where version = " + check_proj_ver + 1 + " and proj_ver_id in (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "')and version = " + proj_version + ");";
        //            }
        //            else
        //            {
        //                SqlCommand cmd1 = new SqlCommand((@"INSERT INTO project_version (proj_ver_id,version) VALUES((select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + "),0)"), conn);
        //                cmd1.ExecuteNonQuery();
        //                query_proj_ver = "select id from project_version where version = 0 and proj_ver_id in (select id from  Revit_Project_Version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version = " + proj_version + ");";
        //            }
        //            using (SqlCommand command1 = new SqlCommand(query_proj_ver, conn))
        //            {
        //                proj_ver = (Int64)command1.ExecuteScalar();
        //            }
        //            query1_1 = "select isnull((select count(*) from project_task_details where proj_ver_id = " + proj_ver + "),0);";

        //            using (SqlCommand command10 = new SqlCommand(query1_1, conn))
        //            {
        //                check1_1 = (int)command10.ExecuteScalar();
        //            }
        //            if (check1_1 != 0)
        //            {
        //                SqlCommand cmd120 = new SqlCommand("delete from task_revit_map where task_id in (select id from project_task_details  where proj_ver_id = " + proj_ver + ")", conn);
        //                cmd120.ExecuteNonQuery();
        //                SqlCommand cmd121 = new SqlCommand("delete from task_resource_map where task_id in (select id from project_task_details  where proj_ver_id = " + proj_ver + ")", conn);
        //                cmd121.ExecuteNonQuery();
        //                SqlCommand cmd122 = new SqlCommand("delete from task_predecessor_map where task_id in (select id from project_task_details  where proj_ver_id = " + proj_ver + ")", conn);
        //                cmd122.ExecuteNonQuery();
        //                SqlCommand cmd123 = new SqlCommand("delete from task_successor_map where task_id in (select id from project_task_details  where proj_ver_id = " + proj_ver + ")", conn);
        //                cmd123.ExecuteNonQuery();
        //                SqlCommand cmd124 = new SqlCommand("DELETE FROM project_task_details WHERE proj_ver_id  = " + proj_ver, conn);
        //                cmd124.ExecuteNonQuery();
        //            }
        //            foreach (msproj_dtl msproj_dtl_1 in msproj_dtl11)
        //            {

        //                //Insert-Update project_task_details table
        //                SqlCommand cmd12 = new SqlCommand("insert into project_task_details (proj_ver_id,Name,seq,level,plan_start_date,plan_end_date,plan_duration,start_date,end_date,duration,act_duration,progress,cost,successor_relation_type,predecessor_relation_type)" +
        //                             @"values (" + proj_ver + ",@Name,@seq,@level,@plan_start_date,@plan_end_date,@plan_duration,@start_date,@end_date,@duration,@act_duration,@progress,@cost,@successor_relation_type,@predecessor_relation_type)", conn);
        //                cmd12.Parameters.AddWithValue("@Name", msproj_dtl_1.name);
        //                cmd12.Parameters.AddWithValue("@seq", msproj_dtl_1.seq);
        //                cmd12.Parameters.AddWithValue("@level", msproj_dtl_1.level);
        //                cmd12.Parameters.AddWithValue("@start_date", msproj_dtl_1.start_date);
        //                cmd12.Parameters.AddWithValue("@end_date", msproj_dtl_1.end_date);
        //                cmd12.Parameters.AddWithValue("@duration", ((object)msproj_dtl_1.duration) ?? DBNull.Value);
        //                cmd12.Parameters.AddWithValue("@plan_start_date", msproj_dtl_1.plan_start_date);
        //                cmd12.Parameters.AddWithValue("@plan_end_date", msproj_dtl_1.plan_end_date);
        //                cmd12.Parameters.AddWithValue("@plan_duration", ((object)msproj_dtl_1.plan_duration) ?? DBNull.Value);
        //                cmd12.Parameters.AddWithValue("@act_duration", ((object)msproj_dtl_1.actual_dur) ?? DBNull.Value);
        //                cmd12.Parameters.AddWithValue("@progress", ((object)msproj_dtl_1.progress) ?? DBNull.Value);
        //                cmd12.Parameters.AddWithValue("@cost", ((object)msproj_dtl_1.cost) ?? DBNull.Value);
        //                cmd12.Parameters.AddWithValue("@successor_relation_type", ((object)msproj_dtl_1.successor_relation_type) ?? DBNull.Value);
        //                cmd12.Parameters.AddWithValue("@predecessor_relation_type", ((object)msproj_dtl_1.predecessor_relation_type) ?? DBNull.Value);
        //                cmd12.ExecuteNonQuery();

        //                string query3 = "select id from project_task_details where proj_ver_id = " + proj_ver + " and seq = " + msproj_dtl_1.seq + "; ";
        //                long? proj_task_det;

        //                using (SqlCommand command1 = new SqlCommand(query3, conn))
        //                {
        //                    proj_task_det = (Int64)command1.ExecuteScalar();
        //                }

        //                //Insert-Update resource table

        //                List<string> resource_name = msproj_dtl_1.resource;
        //                for (int i12 = 0; i12 < resource_name.Count; i12++)
        //                {
        //                    string query2 = "select id from resource where name = N'" + resource_name[i12] + "';";
        //                    Int64 res_id;
        //                    using (SqlCommand command1 = new SqlCommand(query2, conn))
        //                    {
        //                        res_id = (Int64)command1.ExecuteScalar();
        //                    }

        //                    SqlCommand cmd121 = new SqlCommand(string.Format("insert into task_resource_map (task_id,resource_id)" +
        //                 @"values ({0},{1})", proj_task_det, res_id), conn);
        //                    cmd121.ExecuteNonQuery();
        //                }

        //                //Insert-Update task_revit_map

        //                List<int?> element_id = msproj_dtl_1.Revit_elements;
        //                for (int i21 = 0; i21 < element_id.Count; i21++)
        //                {
        //                    SqlCommand cmd211 = new SqlCommand(string.Format("insert into task_revit_map (task_id,element_id)" +
        //                @"values ({0},{1})", proj_task_det, element_id[i21]), conn);
        //                    cmd211.ExecuteNonQuery();
        //                }

        //                //Insert-Update task_predecessor_map

        //                List<int?> pred_map = msproj_dtl_1.predecessor;
        //                for (int i31 = 0; i31 < pred_map.Count; i31++)
        //                {
        //                    SqlCommand cmd311 = new SqlCommand(string.Format("insert into task_predecessor_map (task_id,predecessor_task_id)" +
        //                @"values ({0},{1})", proj_task_det, pred_map[i31]), conn);
        //                    cmd311.ExecuteNonQuery();
        //                }

        //                //Insert-Update task_successor_map

        //                List<int?> succ_map = msproj_dtl_1.successor;
        //                for (int i41 = 0; i41 < succ_map.Count; i41++)
        //                {
        //                    SqlCommand cmd411 = new SqlCommand(string.Format("insert into task_successor_map (task_id,successor_task_id)" +
        //                @"values ({0},{1})", proj_task_det, succ_map[i41]), conn);
        //                    cmd411.ExecuteNonQuery();
        //                }
        //            }

        //            if (saveonly == false)
        //            {
        //                return (check_proj_ver + 1);
        //            }
        //            else
        //            {
        //                return check_proj_ver;
        //            }
        //            conn.Close();
        //        }
        //    }
        //    catch (System.Exception ex)

        //    {
        //        if (state == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }

        //        return 0;
        //    }
        //}

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
        public Int16 projbaseversion(string proj_id, string proj_name, Int64 version, Int64 proj_ver,Int64 base_flag)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand((@"update project_version set f_base_version= " + base_flag + " where " +
                 @"proj_ver_id = (select id from Revit_Project_Version where project_id in (select id from project where proj_guid = '" + proj_id + "'  and name = N'" + proj_name + "') and version = " + proj_ver + ") and version = " + version), conn);
                cmd.ExecuteNonQuery();
                conn.Close();
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
        public Int16 projbasecreate(string proj_id, string proj_name, Int64 version, Int64 proj_ver, Int64 basecreate_flag)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand((@"update project_version set f_baseversion_created= " + basecreate_flag + " where " +
                 @"proj_ver_id = (select id from Revit_Project_Version where project_id in (select id from project where proj_guid = '" + proj_id + "'  and name = N'" + proj_name + "') and version = " + proj_ver + ") and version = " + version), conn);
                cmd.ExecuteNonQuery();
                conn.Close();
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
        public Int16 projbaseupd(string proj_id, string proj_name, Int64 version, Int64 proj_ver, Int64 baseupd_flag)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand((@"update project_version set f_baseversion_updated= " + baseupd_flag + " where " +
                 @"proj_ver_id = (select id from Revit_Project_Version where project_id in (select id from project where proj_guid = '" + proj_id + "'  and name = N'" + proj_name + "') and version = " + proj_ver + ") and version = " + version), conn);
                cmd.ExecuteNonQuery();
                conn.Close();
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
        public Int16 projlock(string proj_id, string proj_name, Int64 version, Int64 proj_ver, Int64 lock_flag)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand((@"update project_version set f_locked= " + lock_flag + " where " +
                 @"proj_ver_id = (select id from Revit_Project_Version where project_id in (select id from project where proj_guid = '" + proj_id + "'  and name = N'" + proj_name + "') and version = " + proj_ver + ") and version = " + version), conn);
                cmd.ExecuteNonQuery();
                conn.Close();
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