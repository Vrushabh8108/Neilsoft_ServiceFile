using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Runtime.InteropServices;
using System.Data;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : InsertRevitModelData
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        #region Copy
        //        public void ins_revit_model(string Project_id, string proj_name, [Optional] int? element_type_id, [Optional] int? element_id, string elemnt_dtl, string usr, [Optional] int f_virtual, [Optional] string virtual_element_name,string country,string city, int proj_version, [Optional] int? isQuantitySaved, [Optional] int? isInputCostSaved)
//        {
//            SqlConnection conn = new SqlConnection(connection_string);
//            ConnectionState state = conn.State;
//            try
//            {
//                int check;
//                string query1;
//                conn.Open();
//                string query = "select isnull((select 1 from project where proj_guid=N'" + Project_id + @"' and name = N'" + proj_name +"'),0);";
//                using (SqlCommand command = new SqlCommand(query, conn))
//                {
//                    check = (int)command.ExecuteScalar();
//                }

//                if (check == 0)
//                {
//                    SqlCommand cmd = new SqlCommand((@"INSERT INTO PROJECT 
//                   (proj_guid,name,city_id) VALUES(N'" + Project_id + "',N'"+ proj_name +"',(select id from city where name = N'"+city+ "' and country_id in (select id from Country_Code where country = N'"+country+"')))"), conn);
//                    cmd.ExecuteNonQuery();
//                }
//                string query11 = "select isnull((select 1 from revit_project_version where project_id=(select id from project where proj_guid=N'" + Project_id + @"' and name=N'" + proj_name + "')and version=" + proj_version + "),0);";
//                using (SqlCommand cd = new SqlCommand(query11, conn))
//                {
//                    int check_version = (int)cd.ExecuteScalar();
//                    if (check_version == 0)
//                    {

//                            SqlCommand cd1 = new SqlCommand((@"INSERT INTO revit_project_version(project_id,version)values((select id from project where proj_guid=N'" + Project_id + @"' and name=N'" + proj_name + "')," + proj_version + ") "), conn);
//                            cd1.ExecuteNonQuery();
                        
//                    }
//                }
//                if (f_virtual == 1)
//                {
//                    string query2 = "select isnull((select count(*) from virtual_element_type where name = N'" + virtual_element_name + @"'),0);";
//                    using (SqlCommand command = new SqlCommand(query2, conn))
//                    {
//                        int check_v = (int)command.ExecuteScalar();
//                        if (check_v == 0)
//                        {
//                            SqlCommand cmd_v = new SqlCommand((@"INSERT INTO virtual_element_type 
//                          (name, created_by) VALUES(N'" + virtual_element_name + "', N'" + usr + @"')"), conn);
//                            cmd_v.ExecuteNonQuery();
//                        }
//                    }
//                    query1 = "select isnull((select count(*) from Revit_project_model where  proj_version_id = (select id from revit_project_version where project_id=(select id from project where proj_guid =N'" + Project_id + "' and name = N'" + proj_name + "') and version=" + proj_version + ") and element_type_id = (select id from virtual_element_type where name = N'" + virtual_element_name + "')),0);";

//                }
//                else
//                {
//                    query1 = "select isnull((select count(*) from Revit_project_model where proj_version_id = (select id from revit_project_version where project_id=(select id from project where proj_guid =N'" + Project_id + "' and name = N'" + proj_name + "') and version=" + proj_version + ") and element_type_id = " + element_type_id + " and element_id = " + element_id + "),0);";
//                }
//                using (SqlCommand command1 = new SqlCommand(query1, conn))
//                {
//                    check = (int)command1.ExecuteScalar();
//                }

//                if (check == 0)
//                {
//                    if (f_virtual == 1)
//                    {
//                        SqlCommand cmd1 = new SqlCommand((@"INSERT INTO Revit_project_model 
//                      (proj_version_id, element_type_id, elemnt_dtl, modified_by, f_del, f_virtual,is_quantitysaved,is_inputcostsaved) VALUES
//                      ((select id from revit_project_version where project_id=(select id from project where proj_guid = N'" + Project_id + "' and name = N'" + proj_name + "') and version=" + proj_version + "), (select id from virtual_element_type where name = N'" + virtual_element_name + "'), " + "N'" + elemnt_dtl + "', '" + usr + "', 0, " + f_virtual + ","+isQuantitySaved+","+isInputCostSaved+")"), conn);
//                        cmd1.ExecuteNonQuery();
//                    }
//                    else
//                    {
//                        SqlCommand cmd1 = new SqlCommand((@"INSERT INTO Revit_project_model 
//                      (proj_version_id, element_type_id, element_id, elemnt_dtl, modified_by, f_del, f_virtual,is_quantitysaved,is_inputcostsaved) VALUES
//                      ((select id from revit_project_version where project_id=(select id from project where proj_guid = N'" + Project_id + "' and name = N'" + proj_name + "') and version=" + proj_version + "), " + element_type_id + ", " + element_id + ", " + "N'" + elemnt_dtl + "', '" + usr + "', 0, " + f_virtual + "," + isQuantitySaved + "," + isInputCostSaved + ")"), conn);
//                        cmd1.ExecuteNonQuery();
//                    }
//                }
//                else
//                {
//                    if (f_virtual == 1)
//                    {
//                        SqlCommand cmd2 = new SqlCommand((@"update Revit_project_model set elemnt_dtl = N'" + elemnt_dtl +
//                    "',is_quantitysaved="+isQuantitySaved+ ",is_inputcostsaved="+isInputCostSaved+" where proj_version_id =(select id from revit_project_version where project_id=(select id from project where proj_guid = N'" + Project_id + "' and name = N'" + proj_name + "')and version=" + proj_version + ") and element_type_id = (select id from virtual_element_type where name = N'" + virtual_element_name + "') and f_virtual = 1"), conn);
//                        cmd2.ExecuteNonQuery();
//                    }
//                    else
//                    {
//                        SqlCommand cmd2 = new SqlCommand((@"update Revit_project_model set elemnt_dtl = N'" + elemnt_dtl +
//                     "',is_quantitysaved=" + isQuantitySaved + ",is_inputcostsaved=" + isInputCostSaved + " where proj_version_id=(select id from revit_project_version where project_id =(select id from project where proj_guid = N'" + Project_id + "' and name = N'" + proj_name + "') and version=" + proj_version + " ) and element_type_id = " + element_type_id + "and element_id = " + element_id), conn);
//                        cmd2.ExecuteNonQuery();
//                    }
//                }
//                conn.Close();

//            }

//            catch (System.Exception ex)

//            {


//                if (state == ConnectionState.Open)
//                {
//                    conn.Close();
//                }
//                Service17 exception1 = new Service17();
//                exception1.SendErrorToText(ex);

//            }

        //        }
        #endregion
        public void ins_revit_model(string Project_id, string proj_name, [Optional] int? element_type_id, [Optional] int? element_id, string elemnt_dtl, string usr, [Optional] int f_virtual, [Optional] string virtual_element_name, string country, string city, int proj_version, [Optional] int? isQuantitySaved, [Optional] int? isInputCostSaved)
        {
            SqlConnection conn;
            try
            {
                int check;
                string query1;
                if (virtual_element_name.Contains("'") || elemnt_dtl.Contains("'"))
                {
                    virtual_element_name = virtual_element_name.Replace("'", "''");
                    elemnt_dtl = elemnt_dtl.Replace("'", "''");
                }

                //using (conn = new SqlConnection(connection_string))
                //{

                     //Check  Wether project Exists or Not 

//                    try
//                    {
//                        conn.Open();
//                        string query = "select isnull((select 1 from project where proj_guid=N'" + Project_id + @"' and name = N'" + proj_name + "'),0);";
//                        using (SqlCommand command = new SqlCommand(query, conn))
//                        {
//                            check = (int)command.ExecuteScalar();
//                        }

//                        if (check == 0)
//                        {
//                            SqlCommand cmd = new SqlCommand((@"INSERT INTO PROJECT 
//                   (proj_guid,name,city_id) VALUES(N'" + Project_id + "',N'" + proj_name + "',(select id from city where name = N'" + city + "' and country_id in (select id from Country_Code where country = N'" + country + "')))"), conn);
//                            cmd.ExecuteNonQuery();
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        if (conn.State == ConnectionState.Open)
//                        {
//                            conn.Close();
//                        }
//                        Service17 exception1 = new Service17();
//                        exception1.SendErrorToText(ex);
//                    }
                    
//                }
                using (conn = new SqlConnection(connection_string))
                {
                    try
                    {
                        conn.Open();
                    //    string query11 = "select isnull((select 1 from revit_project_version where project_id=(select id from project where proj_guid=N'" + Project_id + @"' and name=N'" + proj_name + "')and version=" + proj_version + "),0);";
                    //   using (SqlCommand cd = new SqlCommand(query11, conn))
                        //{
                        //    int check_version = (int)cd.ExecuteScalar();
                        //    if (check_version == 0)
                        //    {

                        //        SqlCommand cd1 = new SqlCommand((@"INSERT INTO revit_project_version(project_id,version)values((select id from project where proj_guid=N'" + Project_id + @"' and name=N'" + proj_name + "')," + proj_version + ") "), conn);
                        //        cd1.ExecuteNonQuery();

                        //    }
                        //}
                        if (f_virtual == 1)
                        {
                            string query2 = "select isnull((select count(*) from virtual_element_type where name = N'" + virtual_element_name + @"'),0);";
                            using (SqlCommand command = new SqlCommand(query2, conn))
                            {
                                int check_v = (int)command.ExecuteScalar();
                                if (check_v == 0)
                                {
                                    SqlCommand cmd_v = new SqlCommand((@"INSERT INTO virtual_element_type 
                          (name, created_by) VALUES(N'" + virtual_element_name + "', N'" + usr + @"')"), conn);
                                    cmd_v.ExecuteNonQuery();
                                }
                            }
                            query1 = "select isnull((select count(*) from Revit_project_model where  proj_version_id = (select id from revit_project_version where project_id=(select id from project where proj_guid =N'" + Project_id + "' and name = N'" + proj_name + "') and version=" + proj_version + ") and element_type_id = (select id from virtual_element_type where name = N'" + virtual_element_name + "')),0);";

                        }

                        else
                        {
                            query1 = "select isnull((select count(*) from Revit_project_model where proj_version_id = (select id from revit_project_version where project_id=(select id from project where proj_guid =N'" + Project_id + "' and name = N'" + proj_name + "') and version=" + proj_version + ") and element_type_id = " + element_type_id + " and element_id = " + element_id + "),0);";
                        }
                        using (SqlCommand command1 = new SqlCommand(query1, conn))
                        {
                            check = (int)command1.ExecuteScalar();
                        }

                        if (check == 0)
                        {
                            if (f_virtual == 1)
                            {
                                SqlCommand cmd1 = new SqlCommand((@"INSERT INTO Revit_project_model 
                      (proj_version_id, element_type_id, elemnt_dtl, modified_by, f_del, f_virtual,is_quantitysaved,is_inputcostsaved) VALUES
                      ((select id from revit_project_version where project_id=(select id from project where proj_guid = N'" + Project_id + "' and name = N'" + proj_name + "') and version=" + proj_version + "), (select id from virtual_element_type where name = N'" + virtual_element_name + "'), " + "N'" + elemnt_dtl + "', '" + usr + "', 0, " + f_virtual + "," + isQuantitySaved + "," + isInputCostSaved + ")"), conn);

                                cmd1.ExecuteNonQuery();
                            }
                            else
                            {
                                SqlCommand cmd1 = new SqlCommand((@"INSERT INTO Revit_project_model 
                      (proj_version_id, element_type_id, element_id, elemnt_dtl, modified_by, f_del, f_virtual,is_quantitysaved,is_inputcostsaved) VALUES
                      ((select id from revit_project_version where project_id=(select id from project where proj_guid = N'" + Project_id + "' and name = N'" + proj_name + "') and version=" + proj_version + "), " + element_type_id + ", " + element_id + ", " + "N'" + elemnt_dtl + "', '" + usr + "', 0, " + f_virtual + "," + isQuantitySaved + "," + isInputCostSaved + ")"), conn);
                                cmd1.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            if (f_virtual == 1)
                            {
                                SqlCommand cmd2 = new SqlCommand((@"update Revit_project_model set elemnt_dtl = N'" + elemnt_dtl +
                            "',is_quantitysaved=" + isQuantitySaved + ",is_inputcostsaved=" + isInputCostSaved + " where proj_version_id =(select id from revit_project_version where project_id=(select id from project where proj_guid = N'" + Project_id + "' and name = N'" + proj_name + "')and version=" + proj_version + ") and element_type_id = (select id from virtual_element_type where name = N'" + virtual_element_name + "') and f_virtual = 1"), conn);
                                cmd2.ExecuteNonQuery();
                            }
                            else
                            {
                                SqlCommand cmd2 = new SqlCommand((@"update Revit_project_model set elemnt_dtl = N'" + elemnt_dtl +
                             "',is_quantitysaved=" + isQuantitySaved + ",is_inputcostsaved=" + isInputCostSaved + " where proj_version_id=(select id from revit_project_version where project_id =(select id from project where proj_guid = N'" + Project_id + "' and name = N'" + proj_name + "') and version=" + proj_version + " ) and element_type_id = " + element_type_id + "and element_id = " + element_id), conn);
                                cmd2.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                        Service17 exception1 = new Service17();
                        exception1.SendErrorToText(ex);
                    }
                    
                }
            }

            catch (System.Exception ex)
            {
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
            }

        }
  
        public Int64? maxprojversion(string Project_id, string proj_name)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                Int64 version;
                string query1;
                conn.Open();
                string query = "select isnull((select max(version) from Revit_Project_Version where project_id in (select id from project where proj_guid=N'" + Project_id + @"' and name = N'" + proj_name + "')),0);";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    version = (Int64)command.ExecuteScalar();
                }

                
                    conn.Close();
                return version;
                
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
        public List<version_dtl> showallrevision(string Project_id, string proj_name)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            SqlDataAdapter sda;
            DataTable dt;
            SqlCommand cmd;
            ConnectionState state = conn.State;
            try
            {
                List<version_dtl> version = new List<version_dtl>();
                conn.Open();
                using (conn = new SqlConnection(connection_string))
                {
                    cmd = new SqlCommand(@"select version,created_on from Revit_Project_Version where project_id in (select id from project where proj_guid=N'" + Project_id + @"' and name = N'" + proj_name + "');", conn);
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable("Paging");
                    sda.Fill(dt);
                    for (int i = 0;i <dt.Rows.Count;i++)
                    {
                        version_dtl version1 = new version_dtl
                        {
                            version = Convert.ToInt64(dt.Rows[i]["version"]),
                            created_on = Convert.ToDateTime(dt.Rows[i]["created_on"])
                        };
                        version.Add(version1);
                    }

                }

                    conn.Close();
                return version;

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
        public Int16 revit_model_data_copy(string Project_id, string proj_name, string country, string city, int proj_version)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                int check;
                string query1;
                conn.Open();
                string query = "select isnull((select 1 from project where proj_guid=N'" + Project_id + @"' and name = N'" + proj_name + "'),0);";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    check = (int)command.ExecuteScalar();
                }

                if (check == 0)
                {
                    SqlCommand cmd = new SqlCommand((@"INSERT INTO PROJECT 
                   (proj_guid,name,city_id) VALUES(N'" + Project_id + "',N'" + proj_name + "',(select id from city where name = N'" + city + "' and country_id in (select id from Country_Code where country = N'" + country + "')))"), conn);
                    cmd.ExecuteNonQuery();
                }
                string query11 = "select isnull((select 1 from revit_project_version where project_id=(select id from project where proj_guid=N'" + Project_id + @"' and name=N'" + proj_name + "')and version=" + proj_version + "),0);";
                using (SqlCommand cd = new SqlCommand(query11, conn))
                {
                    int check_version = (int)cd.ExecuteScalar();
                    if (check_version == 0)
                    {

                        SqlCommand cd1 = new SqlCommand((@"INSERT INTO revit_project_version(project_id,version)values((select id from project where proj_guid=N'" + Project_id + @"' and name=N'" + proj_name + "')," + proj_version + ") "), conn);
                        cd1.ExecuteNonQuery();

                    }
                }
                int old_ver = proj_version - 1;
                Int64 proj_ver_id_old;
                Int64 proj_ver_id_new;
                int task_detail_cnt;
                Int64? proj_ver_id_maxversion;
                int filter_id;
                Int64 proj_task_list;
                Int64 task_detail_id;
                Int64 task_detail_id_old;
                Int64 filter_id_loop;
                Int64 filter_dtl_cnt, filter_dtl_loop, filter_dtl_loop_new;
                       SqlCommand cmd1 = new SqlCommand((@"INSERT INTO Revit_project_model 
                      (proj_version_id, element_type_id, element_id,elemnt_dtl, modified_by, f_del, f_virtual,is_quantitysaved,is_inputcostsaved) 
                      select (select id from revit_project_version where project_id=(select id from project where proj_guid = N'" + Project_id + "' and name = N'" + proj_name + "') and version=" + proj_version + "), element_type_id, element_id,elemnt_dtl, modified_by, f_del, f_virtual,is_quantitysaved,is_inputcostsaved from Revit_project_model where proj_version_id in (select id from revit_project_version where project_id=(select id from project where proj_guid = N'" + Project_id + "' and name = N'" + proj_name + "') and version=" + old_ver + ")"), conn);
                        cmd1.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand((@"INSERT INTO Project_Version
                       (version,created_by,created_on,approved_by,approved_on,f_reject,proj_ver_id,f_base_version,f_locked,f_baseversion_created,f_baseversion_updated)
                        select version,created_by,created_on,approved_by,approved_on,f_reject,(
                        select id from revit_project_version where version = " + proj_version + " and project_id in (select id from project where proj_guid = N'" + Project_id + "' and name = N'" + proj_name + @"')),f_base_version,f_locked,f_baseversion_created,f_baseversion_updated from Project_Version where proj_ver_id in (select id from revit_project_version where version ="
                  + old_ver + "and project_id in (select id from project where proj_guid = N'" + Project_id + "' and name = N'" + proj_name + "'))"),conn);
                cmd2.ExecuteNonQuery();
                string query_proj_ver_id_old = "Select id from Revit_Project_Version where version = " + old_ver + @" and project_id in (select id from project where proj_guid = N'" + Project_id + "' and name = N'" + proj_name + @"');";
                string query_proj_ver_id_new = "Select id from Revit_Project_Version where version = " + proj_version + @" and project_id in (select id from project where proj_guid = N'" + Project_id + "' and name = N'" + proj_name + @"');";
                using (SqlCommand command = new SqlCommand(query_proj_ver_id_old, conn))
                {
                    proj_ver_id_old = (Int64)command.ExecuteScalar();
                }
                using (SqlCommand command = new SqlCommand(query_proj_ver_id_new, conn))
                {
                    proj_ver_id_new = (Int64)command.ExecuteScalar();
                }
                string query_proj_ver_id_maxversion = "Select isnull(max(version),0) from Project_Version where proj_ver_id in (select id from Revit_Project_Version where version = " + proj_version + @" and project_id in (select id from project where proj_guid = N'" + Project_id + "' and name = N'" + proj_name + @"'));";
                using (SqlCommand command = new SqlCommand(query_proj_ver_id_maxversion, conn))
                {
                    proj_ver_id_maxversion = (Int64?)command.ExecuteScalar();
                }
                // Execution for each version of the task list of 4D
                for (int i = 0; i <= proj_ver_id_maxversion; i++)
                {

                    // Insert into Project_Filter
                   // string query_filter_id = "Select count(*) from Project_Filter where proj_ver_id in (select id from Project_Version where proj_ver_id in (select id from Revit_Project_Version where version = " + old_ver + @" and project_id in (select id from project where proj_guid = N'" + Project_id + "' and name = N'" + proj_name + @"')) and version = "+i+");";
                   // using (SqlCommand command = new SqlCommand(query_filter_id, conn))
                   // {
                     //   filter_id = (int)command.ExecuteScalar();
                  //  }
                   //for (int i13 = 0; i13 <= filter_id; i13++)
                   // {
                        SqlCommand cmd3 = new SqlCommand((@"INSERT INTO Project_Filter(proj_id,filter_name,proj_ver_id)
                                                    select proj_id,filter_name,(select id from Project_Version where version = " + i + @" and  proj_ver_id = " + proj_ver_id_new + @" ) from Project_Filter where proj_ver_id in
                                                    (select id from Project_Version where  version = " + i + @" and proj_ver_id = " + proj_ver_id_old + ")"), conn);
                        cmd3.ExecuteNonQuery();
                    //}
                    // -------check-----Project dtl needs to be added here
                    string query_filter_id = "Select count(*) from Project_Filter where proj_ver_id in (select id from Project_Version where proj_ver_id in (select id from Revit_Project_Version where version = " + old_ver + @" and project_id in (select id from project where proj_guid = N'" + Project_id + "' and name = N'" + proj_name + @"')));";
                    using (SqlCommand command = new SqlCommand(query_filter_id, conn))
                    {
                        filter_id = (int)command.ExecuteScalar();
                    }
                    for(int i13 = 0;i13 < filter_id;i13++)
                    {
                        string query_filter_id_loop = "select id from (Select  id,proj_id,filter_name,proj_ver_id,ROW_NUMBER() over(order by id) row_num "+
                                                                        "from Project_Filter where proj_ver_id in (select id from Project_Version where proj_ver_id in "+
                                                                        "(select id from Revit_Project_Version where version = " + old_ver + @" and project_id in 
                                                                        (select id from project where proj_guid = N'" + Project_id + "' and name = N'" + proj_name + @"')))) T where row_num = "+i13+";";
                        using (SqlCommand command = new SqlCommand(query_filter_id_loop, conn))
                        {
                            filter_id_loop = (Int64)command.ExecuteScalar();
                        }
                        
                          
                            string query_filter_dtl_loop_new = "select id from Project_filter where proj_ver_id in (select id from Project_Version where proj_ver_id in " +
                                                                        "(select id from Revit_Project_Version where version = " + proj_version + @" and project_id in 
                                                                        (select id from project where proj_guid = N'" + Project_id + "' and name = N'" + proj_name + @"'))) and filter_name =(select filter_name from Project_filter where id = " + filter_id_loop +") ;";
                            using (SqlCommand command = new SqlCommand(query_filter_dtl_loop_new, conn))
                            {
                                filter_dtl_loop_new = (Int64)command.ExecuteScalar();
                            }

                            SqlCommand sql_filterdtl = new SqlCommand((@"INSERT INTO Project_Filter_dtl(filter_id,filter_category,filter_sign,filter_value) 
                                                                    select "+filter_dtl_loop_new +@",filter_category,filter_sign,filter_value from Project_Filter_dtl where 
                                                                       filter_id = " + filter_id_loop), conn);
                            sql_filterdtl.ExecuteNonQuery();
                        

                    }
                    // Insert into Project_Exclude_Category
                    SqlCommand cmd4 = new SqlCommand((@"INSERT INTO Project_Exclude_Category(proj_id,category,proj_ver_id)
                                                    select proj_id,category,(select id from Project_Version where version = " + i + @" and  proj_ver_id = " + proj_ver_id_new + @" ) from Project_Exclude_Category where proj_ver_id in
                                                    (select id from Project_Version where  version = " + i + @" and proj_ver_id = " + proj_ver_id_old + ")"), conn);
                    cmd4.ExecuteNonQuery();
                    // Insert into Project_Task_Details
                    SqlCommand cmd5 = new SqlCommand((@"INSERT INTO Project_Task_Details(proj_ver_id,name,seq,level,start_date,end_date,duration,act_duration,progress,cost,plan_start_date,plan_end_date,plan_duration,successor_relation_type,predecessor_relation_type)
                                                    select (select id from Project_Version where version = " + i + @" and  proj_ver_id = " + proj_ver_id_new + @" ),name,seq,level,start_date,end_date,duration,act_duration,progress,cost,plan_start_date,plan_end_date,plan_duration,successor_relation_type,predecessor_relation_type from Project_Task_Details where proj_ver_id in
                                                    (select id from Project_Version where  version = " + i + @" and proj_ver_id = " + proj_ver_id_old + ")"), conn);
                    cmd5.ExecuteNonQuery();
                    //Insert all task details
                    string query_task_list = "Select isnull(max(seq),0) from Project_Task_Details where proj_ver_id in (select id from Project_Version where version = " + i + @" and  proj_ver_id = " + proj_ver_id_new+") ; ";
                    using (SqlCommand command = new SqlCommand(query_task_list, conn))
                    {
                        proj_task_list = (Int64)command.ExecuteScalar();
                    }
                    for (int i12 = 1; i12 <= proj_task_list; i12++)
                    {
                        string query_task_cnt = "Select count(*) from Project_Task_Details where proj_ver_id in (select id from Project_Version where version = " + i + @" and  proj_ver_id = " + proj_ver_id_new + ") and seq = " + i12 + " ; ";
                        string query_task_detail_id = "Select id from Project_Task_Details where proj_ver_id in (select id from Project_Version where version = " + i + @" and  proj_ver_id = " + proj_ver_id_new + ") and seq = " + i12 + " ; ";
                        string query_task_detail_id_old = "Select id from Project_Task_Details where proj_ver_id in (select id from Project_Version where version = " + i + @" and  proj_ver_id = " + proj_ver_id_old + ") and seq = " + i12 + " ; ";
                        using (SqlCommand command = new SqlCommand(query_task_cnt, conn))
                        {
                            task_detail_cnt = (int)command.ExecuteScalar();
                        }
                        if (task_detail_cnt > 0)
                        {
                            using (SqlCommand command = new SqlCommand(query_task_detail_id, conn))
                            {
                                task_detail_id = (Int64)command.ExecuteScalar();
                            }
                            using (SqlCommand command = new SqlCommand(query_task_detail_id_old, conn))
                            {
                                task_detail_id_old = (Int64)command.ExecuteScalar();
                            }
                            SqlCommand cmd6 = new SqlCommand((@"INSERT INTO task_resource_map(task_id,resource_id)
                                                    select " + task_detail_id + ",resource_id from task_resource_map where task_id = " + task_detail_id_old), conn);
                            cmd6.ExecuteNonQuery();
                            SqlCommand cmd7 = new SqlCommand((@"INSERT INTO Task_Revit_Map(task_id,element_id)
                                                    select " + task_detail_id + ",element_id from Task_Revit_Map where task_id = " + task_detail_id_old), conn);
                            cmd7.ExecuteNonQuery();
                            SqlCommand cmd8 = new SqlCommand((@"INSERT INTO Task_Predecessor_Map(task_id,predecessor_task_id)
                                                    select " + task_detail_id + ",predecessor_task_id from Task_Predecessor_Map where task_id = " + task_detail_id_old), conn);
                            cmd8.ExecuteNonQuery();
                            SqlCommand cmd9 = new SqlCommand((@"INSERT INTO Task_Successor_Map(task_id,successor_task_id)
                                                    select " + task_detail_id + ",successor_task_id from Task_Successor_Map where task_id = " + task_detail_id_old), conn);
                            cmd9.ExecuteNonQuery();

                        }

                    }

                }
                SqlCommand cmd10 = new SqlCommand((@"INSERT INTO Msprojoffice_Diff(comparison_guid,first_version,second_version,f_approved,proj_ver_id)
                                                    select comparison_guid,first_version,second_version,f_approved,"+proj_ver_id_new+" from Msprojoffice_Diff where proj_ver_id="+ proj_ver_id_old), conn);

                cmd10.ExecuteNonQuery();
                conn.Close();
                return 1;
            }

            catch (System.Exception ex)

            {


                if (state == ConnectionState.Open)
                {
                    conn.Close();
                }
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
                return 0;

            }

        }

        public save_dtl readsavedtl(string Project_id, string proj_name, int proj_version)
        {
            SqlConnection con = new SqlConnection(connection_string);
            ConnectionState state = con.State;
            try
            {
                SqlCommand cmd;
                SqlDataAdapter sda;
                DataTable dt;
                save_dtl save_dtl1 = new save_dtl();
                using (con = new SqlConnection(connection_string))
                {
                    cmd = new SqlCommand(@"select is_quantitysaved,is_inputcostsaved from Revit_project_model where f_virtual=1 AND proj_version_id = (select id from revit_project_version where project_id=(select id from project where proj_guid =N'" + Project_id + "' and name = N'" + proj_name + "') and version=" + proj_version + ")", con);
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable("cost");
                    sda.Fill(dt);
                    save_dtl1.isInputCostSaved = string.IsNullOrEmpty(dt.Rows[0]["is_inputcostsaved"].ToString()) ? (Int16?)null : Convert.ToInt16(dt.Rows[0]["is_inputcostsaved"]);
                    save_dtl1.isQuantitySaved = string.IsNullOrEmpty(dt.Rows[0]["is_quantitysaved"].ToString()) ? (Int16?)null : Convert.ToInt16(dt.Rows[0]["is_quantitysaved"]);
                    return save_dtl1;
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

        public void ins_revit_model_Test(List<DBElementdetails> lstElementDetails)
        {
            SqlConnection conn;
            try
            {
                int check;
                string query1;

                foreach (DBElementdetails ele in lstElementDetails)
                {
                    if (ele.VirtualElemetName.Contains("'") || ele.Elementdetails.Contains("'"))
                    {
                        ele.VirtualElemetName = ele.VirtualElemetName.Replace("'", "''");
                        ele.Elementdetails = ele.Elementdetails.Replace("'", "''");
                    }

                    using (conn = new SqlConnection(connection_string))
                    {
                        try
                        {
                            conn.Open();

                            string query = "select isnull((select 1 from project where proj_guid=N'" + ele.ProjGUID + @"' and name = N'" + ele.ProjName + "'),0);";
                            using (SqlCommand command = new SqlCommand(query, conn))
                            {
                                check = (int)command.ExecuteScalar();
                            }

                            if (check == 0)
                            {
                                SqlCommand cmd = new SqlCommand((@"INSERT INTO PROJECT 
                   (proj_guid,name,city_id) VALUES(N'" + ele.ProjGUID + "',N'" + ele.ProjName + "',(select id from city where name = N'" + ele.ProjectCity + "' and country_id in (select id from Country_Code where country = N'" + ele.ProjectCountry + "')))"), conn);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        catch (Exception ex)
                        {
                            if (conn.State == ConnectionState.Open)
                            {
                                conn.Close();
                            }
                            Service17 exception1 = new Service17();
                            exception1.SendErrorToText(ex);
                        }

                    }
                    using (conn = new SqlConnection(connection_string))
                    {
                        try
                        {
                            conn.Open();
                            string query11 = "select isnull((select 1 from revit_project_version where project_id=(select id from project where proj_guid=N'" + ele.ProjGUID + @"' and name=N'" + ele.ProjName + "')and version=" + ele.ProjectVersion + "),0);";
                            using (SqlCommand cd = new SqlCommand(query11, conn))
                            {
                                int check_version = (int)cd.ExecuteScalar();
                                if (check_version == 0)
                                {

                                    SqlCommand cd1 = new SqlCommand((@"INSERT INTO revit_project_version(project_id,version)values((select id from project where proj_guid=N'" + ele.ProjGUID + @"' and name=N'" + ele.ProjName + "')," + ele.ProjectVersion + ") "), conn);
                                    cd1.ExecuteNonQuery();

                                }
                            }
                            if (ele.IsVirtualElement == 1)
                            {
                                string query2 = "select isnull((select count(*) from virtual_element_type where name = N'" + ele.VirtualElemetName + @"'),0);";
                                using (SqlCommand command = new SqlCommand(query2, conn))
                                {
                                    int check_v = (int)command.ExecuteScalar();
                                    if (check_v == 0)
                                    {
                                        SqlCommand cmd_v = new SqlCommand((@"INSERT INTO virtual_element_type 
                          (name, created_by) VALUES(N'" + ele.VirtualElemetName + "', N'" + ele.User + @"')"), conn);
                                        cmd_v.ExecuteNonQuery();
                                    }
                                }
                                query1 = "select isnull((select count(*) from Revit_project_model where  proj_version_id = (select id from revit_project_version where project_id=(select id from project where proj_guid =N'" + ele.ProjGUID + "' and name = N'" + ele.ProjName + "') and version=" + ele.ProjectVersion + ") and element_type_id = (select id from virtual_element_type where name = N'" + ele.VirtualElemetName + "')),0);";

                            }

                            else
                            {
                                query1 = "select isnull((select count(*) from Revit_project_model where proj_version_id = (select id from revit_project_version where project_id=(select id from project where proj_guid =N'" + ele.ProjGUID + "' and name = N'" + ele.ProjName + "') and version=" + ele.ProjectVersion + ") and element_type_id = " + ele.TypeID + " and element_id = " + ele.ElementID + "),0);";
                            }
                            using (SqlCommand command1 = new SqlCommand(query1, conn))
                            {
                                check = (int)command1.ExecuteScalar();
                            }

                            if (check == 0)
                            {
                                if (ele.IsVirtualElement == 1)
                                {
                                    SqlCommand cmd1 = new SqlCommand((@"INSERT INTO Revit_project_model 
                      (proj_version_id, element_type_id, elemnt_dtl, modified_by, f_del, f_virtual,is_quantitysaved,is_inputcostsaved) VALUES
                      ((select id from revit_project_version where project_id=(select id from project where proj_guid = N'" + ele.ProjGUID + "' and name = N'" + ele.ProjName + "') and version=" + ele.ProjectVersion + "), (select id from virtual_element_type where name = N'" + ele.VirtualElemetName + "'), " + "N'" + ele.Elementdetails + "', '" + ele.User + "', 0, " + ele.IsVirtualElement + "," + ele.IsProjectQuantitySaved + "," + ele.IsProjectInputCostSaved + ")"), conn);

                                    cmd1.ExecuteNonQuery();
                                }
                                else
                                {
                                    SqlCommand cmd1 = new SqlCommand((@"INSERT INTO Revit_project_model 
                      (proj_version_id, element_type_id, element_id, elemnt_dtl, modified_by, f_del, f_virtual,is_quantitysaved,is_inputcostsaved) VALUES
                      ((select id from revit_project_version where project_id=(select id from project where proj_guid = N'" + ele.ProjGUID + "' and name = N'" + ele.ProjName + "') and version=" + ele.ProjectVersion + "), " + ele.TypeID + ", " + ele.ElementID + ", " + "N'" + ele.Elementdetails + "', '" + ele.User + "', 0, " + ele.IsVirtualElement + "," + ele.IsProjectQuantitySaved + "," + ele.IsProjectInputCostSaved + ")"), conn);
                                    cmd1.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                if (ele.IsVirtualElement == 1)
                                {
                                    SqlCommand cmd2 = new SqlCommand((@"update Revit_project_model set elemnt_dtl = N'" + ele.Elementdetails +
                                "',is_quantitysaved=" + ele.IsProjectQuantitySaved + ",is_inputcostsaved=" + ele.IsProjectInputCostSaved + " where proj_version_id =(select id from revit_project_version where project_id=(select id from project where proj_guid = N'" + ele.ProjGUID + "' and name = N'" + ele.ProjName + "') and version=" + ele.ProjectVersion + ") and element_type_id = (select id from virtual_element_type where name = N'" + ele.VirtualElemetName + "') and f_virtual = 1"), conn);
                                    cmd2.ExecuteNonQuery();
                                }
                                else
                                {
                                    SqlCommand cmd2 = new SqlCommand((@"update Revit_project_model set elemnt_dtl = N'" + ele.Elementdetails +
                                 "',is_quantitysaved=" + ele.IsProjectQuantitySaved + ",is_inputcostsaved=" + ele.IsProjectInputCostSaved + " where proj_version_id=(select id from revit_project_version where project_id =(select id from project where proj_guid = N'" + ele.ProjGUID + "' and name = N'" + ele.ProjName + "') and version=" + ele.ProjectVersion + " ) and element_type_id = " + ele.TypeID + "and element_id = " + ele.ElementID), conn);
                                    cmd2.ExecuteNonQuery();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            if (conn.State == ConnectionState.Open)
                            {
                                conn.Close();
                            }
                            Service17 exception1 = new Service17();
                            exception1.SendErrorToText(ex);
                        }

                    }
                }              
            }

            catch (System.Exception ex)
            {
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
            }

        }
    }
}