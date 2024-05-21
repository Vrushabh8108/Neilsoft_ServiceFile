using fujita_BIM4D5D_planner;
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

namespace BIM4D5D_service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service8" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service8.svc or Service8.svc.cs at the Solution Explorer and start debugging.
    public class Service8 : Delete_Subdiv
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();

        //        public void delete_subdiv(String mat_name, String mat_var_name, String mat_var_sub)
        //        {
        //            SqlConnection conn = new SqlConnection(connection_string);
        //            ConnectionState state = conn.State;
        //            SqlCommand cmd1;
        //            SqlCommand cmd_seq;
        //            DataTable dt;
        //            SqlDataAdapter sda;
        //            Int64? seq;
        //            try
        //            {
        //                conn.Open();
        //                cmd1 = new SqlCommand(@"SELECT max(seq) seq FROM Material_Variance_Subdivision where material_variance_id in  (
        //                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
        //                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and name = N'" + mat_var_sub + "' ; ", conn);
        //                sda = new SqlDataAdapter(cmd1);
        //                dt = new DataTable("mat");
        //                sda.Fill(dt);
        //                seq = Convert.ToInt64(dt.Rows[0]["seq"]);
        //                cmd_seq = new SqlCommand((@"update Material_Variance_Subdivision 
        //                        set seq = seq - 1 where material_variance_id in  (
        //                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
        //                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0  and seq > 1 and seq > " + seq), conn);
        //                cmd_seq.ExecuteNonQuery();
        //                SqlCommand cmd = new SqlCommand(@"Update Material_Variance_subdivision set f_del=1,seq = null WHERE Name=N'" + mat_var_sub + @"' and material_variance_id =(select id from material_variance where variance=N'" + mat_var_name + @"' and material_id = (select id from material where name=N'" + mat_name + @"'))", conn);
        //                cmd.ExecuteNonQuery();
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

//        public void delete_subdiv(String mat_name, String mat_var_name, String mat_var_sub, string user)
//        {
//            SqlConnection conM = new SqlConnection(connection_string);
//            ConnectionState state = conM.State;
//            SqlCommand cmd1;
//            SqlCommand cmd_seq;
//            SqlCommand cmd_country;
//            DataTable dt_country;
//            SqlDataAdapter sda_country;
//            SqlCommand cmd_city;
//            DataTable dt_city;
//            SqlDataAdapter sda_city;
//            SqlCommand cmd_project;
//            DataTable dt_project;
//            SqlDataAdapter sda_project;
//            SqlCommand cmd_matOpt;
//            DataTable dt_matOpt;
//            SqlDataAdapter sda_matOpt;
//            DataTable dt;
//            SqlDataAdapter sda;
//            Int64? seq;
//            int inCostMaster = 0;
//            int inCostMasterProjectMaster = 0;
//            int inCMPProject = 0;
//            int alreadyexistsinproject = 0;
//            int alreadydeletedProj = 0;
//            SqlCommand cmd_insert;
//            SqlCommand cmd_MasterDtl;
//            SqlDataAdapter sda_MasterDtl;
//            DataTable dt_MasterDtl;
//            string revit_category = string.Empty;
//            string family_type = string.Empty;
//            string property = string.Empty;
//            string design_rule_factor = string.Empty;
//            string keywords = string.Empty;
//            string quantity_extraction_formula = string.Empty;
//            Int64 originalId;
//            Int64? id = null;

//            try
//            {
//                conM.Open();

//                cmd_MasterDtl = new SqlCommand(@"select revit_category,family_type,property,design_rule_factor,keywords,quantity_extraction_formula from Material_Variance_Subdivision_dtl where material_var_subdiv_id = (select id from Material_Variance_Subdivision where name =N'" + mat_var_sub + "' and material_variance_id = (select id from Material_Variance where variance =N'" + mat_var_name + "' and material_id = (select id from Material where name = N'" + mat_name + "')));", conM);
//                sda_MasterDtl = new SqlDataAdapter(cmd_MasterDtl);
//                dt_MasterDtl = new DataTable("dtl");
//                sda_MasterDtl.Fill(dt_MasterDtl);
//                if (dt_MasterDtl.Rows.Count > 0)
//                {
//                    revit_category = dt_MasterDtl.Rows[0]["revit_category"].ToString();
//                    family_type = dt_MasterDtl.Rows[0]["family_type"].ToString();
//                    property = dt_MasterDtl.Rows[0]["property"].ToString();
//                    design_rule_factor = dt_MasterDtl.Rows[0]["design_rule_factor"].ToString();
//                    keywords = dt_MasterDtl.Rows[0]["keywords"].ToString();
//                    quantity_extraction_formula = dt_MasterDtl.Rows[0]["quantity_extraction_formula"].ToString();
//                }

//                cmd1 = new SqlCommand(@"SELECT max(seq) seq FROM Material_Variance_Subdivision where material_variance_id in  (
//                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
//                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and name = N'" + mat_var_sub + "' ; ", conM);
//                sda = new SqlDataAdapter(cmd1);
//                dt = new DataTable("mat");
//                sda.Fill(dt);
//                seq = Convert.ToInt64(dt.Rows[0]["seq"]);
//                cmd_seq = new SqlCommand((@"update Material_Variance_Subdivision 
//                        set seq = seq - 1 where material_variance_id in  (
//                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
//                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0  and seq > 1 and seq > " + seq), conM);
//                cmd_seq.ExecuteNonQuery();
//                SqlCommand cmd = new SqlCommand(@"Update Material_Variance_subdivision set f_del=1,seq = null WHERE Name=N'" + mat_var_sub + @"' and material_variance_id =(select id from material_variance where variance=N'" + mat_var_name + @"' and material_id = (select id from material where name=N'" + mat_name + @"'))", conM);
//                cmd.ExecuteNonQuery();

//                cmd = new SqlCommand(@"select id from Material_Variance_subdivision where f_del=1 and Name=N'" + mat_var_sub + @"' and material_variance_id =(select id from material_variance where variance=N'" + mat_var_name + @"' and material_id = (select id from material where name=N'" + mat_name + @"'))", conM);
//                SqlDataAdapter sdaID = new SqlDataAdapter(cmd);
//                DataTable dtID = new DataTable("ID");
//                sdaID.Fill(dtID);
//                originalId = Convert.ToInt64(dtID.Rows[0]["id"]);

//                cmd_country = new SqlCommand(@"select id,code,currency from Country_Code", conM);
//                sda_country = new SqlDataAdapter(cmd_country);
//                dt_country = new DataTable("country");
//                sda_country.Fill(dt_country);
//                for (int c = 0; c < dt_country.Rows.Count; c++)
//                {
//                    cmd_city = new SqlCommand(@"select id,name from City where country_id =" + dt_country.Rows[c]["id"] + "", conM);
//                    sda_city = new SqlDataAdapter(cmd_city);
//                    dt_city = new DataTable("city");
//                    sda_city.Fill(dt_city);

//                    for (int cty = 0; cty < dt_city.Rows.Count; cty++)
//                    {
//                        cmd_project = new SqlCommand(@"select proj_guid,name from project where created_on >='2022-09-23' and f_active = 1 and city_id = " + dt_city.Rows[cty]["id"] + "", conM);
//                        sda_project = new SqlDataAdapter(cmd_project);
//                        dt_project = new DataTable("project");
//                        sda_project.Fill(dt_project);
//                        for (int p = 0; p < dt_project.Rows.Count; p++)
//                        {
//                            //Check in Material_Variance_Subdivision_Project first then update 
//                            using (SqlConnection conn = new SqlConnection(connection_string))
//                            {
//                                conn.Open();
//                                using (SqlCommand cmdGetProjectCount = new SqlCommand(@"SELECT COUNT(*) FROM Material_Variance_Subdivision_Project where material_variance_id in  (
//                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
//                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and project_id = (select id from Project where proj_guid =N'" + dt_project.Rows[p]["proj_guid"] + @"' and name= N'" + dt_project.Rows[p]["name"] + @"') and name = N'" + mat_var_sub + @"';", conn))
//                                {
//                                    alreadyexistsinproject = (int)cmdGetProjectCount.ExecuteScalar();
//                                }

//                                if (alreadyexistsinproject == 0)
//                                {
//                                    using (SqlCommand cmdGetProjectCount = new SqlCommand(@"SELECT COUNT(*) FROM Material_Variance_Subdivision_Project where material_variance_id in  (
//                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
//                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 1 and project_id = (select id from Project where proj_guid =N'" + dt_project.Rows[p]["proj_guid"] + @"' and name= N'" + dt_project.Rows[p]["name"] + @"') and name = N'" + mat_var_sub + @"';", conn))
//                                    {
//                                        alreadydeletedProj = (int)cmdGetProjectCount.ExecuteScalar();
//                                    }
//                                }
//                                if (alreadyexistsinproject == 0 && alreadydeletedProj == 0)
//                                {
//                                    int countProject = 0;
//                                    using (SqlCommand cmdGetProjectCount = new SqlCommand("(select COUNT(id) from Project where proj_guid =N'" + dt_project.Rows[p]["proj_guid"] + @"' and name= N'" + dt_project.Rows[p]["name"] + @"'and created_on >= 
// (select created_on from Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and material_variance_id = 
// (select id from Material_Variance where variance = N'" + mat_var_name + @"' and material_id = (select id from Material where name = N'" + mat_name + @"'))))", conn))
//                                    {
//                                        countProject = (int)cmdGetProjectCount.ExecuteScalar();
//                                    }
//                                    if (countProject == 1)
//                                    {

//                                        cmd_insert = new SqlCommand(@"INSERT into Material_Variance_Subdivision_Project(material_variance_id,name,created_by,created_on,unit_of_measurement,f_del,seq,specification,project_id)		
//VALUES((select id from Material_Variance where variance = N'" + mat_var_name + @"' and material_id = (select id from Material where name = N'" + mat_name + @"')),
//N'" + mat_var_sub + @"',N'" + user + @"',CURRENT_TIMESTAMP,(select Unit_Of_Measurement from Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and material_variance_id =(select id from Material_Variance where variance = N'" + mat_var_name + @"' and
// material_id = (select id from Material where name = N'" + mat_name + @"'))),
//0," + seq + ",(select specification from Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and material_variance_id =(select id from Material_Variance where variance = N'" + mat_var_name + @"' and
// material_id = (select id from Material where name = N'" + mat_name + @"'))),(select id from Project where proj_guid =N'" + dt_project.Rows[p]["proj_guid"] + @"' and name= N'" + dt_project.Rows[p]["name"] + @"'and created_on >= (select created_on from Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and material_variance_id = (select id from Material_Variance where variance = N'" + mat_var_name + @"' and material_id = (select id from Material where name = N'" + mat_name + @"')))))", conn);

//                                        cmd_insert.ExecuteNonQuery();

//                                        SqlCommand cmd_id = new SqlCommand(@"SELECT id FROM Material_Variance_Subdivision_Project where name = N'" + @mat_var_sub + @"' and material_variance_id in  (
//                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
//                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and project_id = (select id from Project where proj_guid =N'" + dt_project.Rows[p]["proj_guid"] + "' and name= N'" + dt_project.Rows[p]["name"] + "') ; ", conn);
//                                        SqlDataAdapter sda_id = new SqlDataAdapter(cmd_id);
//                                        DataTable dt_id = new DataTable("mat");
//                                        sda_id.Fill(dt_id);
//                                        id = Convert.ToInt64(dt_id.Rows[0]["id"]);

//                                        if (!string.IsNullOrEmpty(revit_category) || !string.IsNullOrEmpty(family_type) || !string.IsNullOrEmpty(property) || !string.IsNullOrEmpty(design_rule_factor) || !string.IsNullOrEmpty(keywords) || !string.IsNullOrEmpty(quantity_extraction_formula))
//                                        {
//                                            SqlCommand cmd11 = new SqlCommand((@"INSERT INTO Material_Variance_Subdivision_dtl_Project 
//                        (material_var_subdiv_proj_id, 
//                         revit_category, 
//                         family_type, 
//                         property, 
//                         design_rule_factor,
//                         keywords,
//                         quantity_extraction_formula,project_id) 
//            VALUES    (" + id + ",N'" + revit_category + "',N'" + family_type + "',N'" + property + "',N'" + design_rule_factor + "',N'" + keywords + "',N'" + quantity_extraction_formula + "',(select id from Project where proj_guid =N'" + dt_project.Rows[p]["proj_guid"] + @"' and name= N'" + dt_project.Rows[p]["name"] + @"'))"), conn);
//                                            cmd11.ExecuteNonQuery();
//                                        }

//                                        cmd_matOpt = new SqlCommand(@"select id,name from Material_Option", conn);
//                                        sda_matOpt = new SqlDataAdapter(cmd_matOpt);
//                                        dt_matOpt = new DataTable("matOpt");
//                                        sda_matOpt.Fill(dt_matOpt);
//                                        for (int mo = 0; mo < dt_matOpt.Rows.Count; mo++)
//                                        {

//                                            //if (inCostMastertemp != null)
//                                            //{
//                                            cmd_insert = new SqlCommand(@" SELECT COUNT(*) 
//                        FROM   cost_master_Project 
//                        WHERE  material_option_id = (SELECT id 
//                                                     FROM   material_option 
//                                                     WHERE  NAME = N'" + dt_matOpt.Rows[mo]["name"] + @"') and project_id in (select id from project where proj_guid = N'" + dt_project.Rows[p]["proj_guid"] + "' and name = N'" + dt_project.Rows[p]["name"] + @"')
//                               AND material_variance_subdiv_id = 
//                                   (SELECT id 
//                                    FROM 
//                                   material_variance_subdivision 
//                                                                  WHERE f_del=1 and NAME = 
//                                   N'" + @mat_var_sub + @"' 
//                                                                         AND 
//                                       material_variance_id = (SELECT id 
//                                                               FROM   material_variance 
//                                                               WHERE 
//                                       variance = N'" + @mat_var_name + @"' 
//                                       AND material_id = (SELECT id 
//                                                          FROM   material 
//                                                          WHERE  NAME = 
//                                           N'" + @mat_name + @"'))) 
//                               AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end  and city_id in (select id from city where country_id in (select id from  country_code where code = N'" + dt_country.Rows[c]["code"] + @"') and name = N'" + dt_city.Rows[cty]["name"] + @"');
//
//
//               ", conn);
//                                            inCostMasterProjectMaster = (int)cmd_insert.ExecuteScalar();
//                                            if (inCostMasterProjectMaster == 1 && id != null)
//                                            {
//                                                cmd_insert = new SqlCommand(@"UPDATE Cost_master_project 
//                              SET material_variance_subdiv_id_proj = " + id + @" 
//                              WHERE  material_option_id = (SELECT id 
//                                                           FROM   material_option 
//                                                           WHERE  NAME = N'" + dt_matOpt.Rows[mo]["name"] + @"') 
//                                     and project_id in (select id from project where proj_guid = N'" + dt_project.Rows[p]["proj_guid"] + "' and name = N'" + dt_project.Rows[p]["name"] + @"')
//                                     AND material_variance_subdiv_id = 
//                                         (SELECT id 
//                                          FROM 
//                                         material_variance_subdivision 
//                                                                        WHERE f_del=1 and NAME = 
//                                         N'" + @mat_var_sub + @"' 
//                                                                               AND 
//                                             material_variance_id = (SELECT id 
//                                                                     FROM   material_variance 
//                                                                     WHERE 
//                                             variance = N'" + @mat_var_name + @"' 
//                                             AND material_id = (SELECT id 
//                                                                FROM   material 
//                                                                WHERE  NAME = 
//                                                 N'" + @mat_name + @"'))) 
//                                     AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end and city_id in (select id from city where country_id in (select id from country_code where code = N'" + dt_country.Rows[c]["code"] + @"') and name = N'" + dt_city.Rows[cty]["name"] + @"');", conn);

//                                                cmd_insert.ExecuteNonQuery();

//                                                cmd_insert = new SqlCommand(@"UPDATE Cost_master_project SET material_variance_subdiv_id = null 
//WHERE  material_option_id = (SELECT id FROM   material_option 
//WHERE  NAME = N'" + dt_matOpt.Rows[mo]["name"] + @"') 
//and project_id in (select id from project where proj_guid = N'" + dt_project.Rows[p]["proj_guid"] + "' and name = N'" + dt_project.Rows[p]["name"] + @"')
//AND material_variance_subdiv_id_proj = (SELECT id FROM Material_Variance_Subdivision_Project WHERE f_del=0 and project_id in (select id from project where proj_guid = N'" + dt_project.Rows[p]["proj_guid"] + "' and name = N'" + dt_project.Rows[p]["name"] + @"') and NAME = N'" + @mat_var_sub + @"' AND material_variance_id = (SELECT id FROM   material_variance WHERE variance = N'" + @mat_var_name + @"'  AND material_id = (SELECT id FROM material WHERE  NAME =  N'" + @mat_name + @"'))) AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end and  city_id in (select id from city where country_id in (select id from country_code where  code = N'" + dt_country.Rows[c]["code"] + @"') and name = N'" + dt_city.Rows[cty]["name"] + @"');", conn);

//                                                cmd_insert.ExecuteNonQuery();
//                                            }
//                                            //}
//                                        }
//                                    }
//                                }

//                            }
//                        }
//                    }
//                }
//                conM.Close();
//            }
//            catch (System.Exception ex)
//            {
//                if (conM.State == ConnectionState.Open)
//                {
//                    conM.Close();
//                }
//                Service17 exception1 = new Service17();
//                exception1.SendErrorToText(ex);

//            }
//        }


        public void delete_subdiv(String mat_name, String mat_var_name, String mat_var_sub, string user, string city_name, string country_name, bool f_allCountry, bool f_allCity, bool f_singleCity)
        {
            SqlConnection conn = new SqlConnection(connection_string);

            try
            {
                conn.Open();

                if (f_allCountry)
                {
                    SqlCommand cmd_country = new SqlCommand("select id,country from Country_Code order by country;", conn);
                    SqlDataAdapter sda_country = new SqlDataAdapter(cmd_country);
                    DataTable dt_country = new DataTable("ctry");
                    sda_country.Fill(dt_country);

                    for (int c = 0; c < dt_country.Rows.Count; c++)
                    {
                        SqlCommand cmd_city = new SqlCommand("select id,name from City where country_id = (select id from Country_Code where country = N'" + dt_country.Rows[c]["country"] + "')", conn);
                        SqlDataAdapter sda_city = new SqlDataAdapter(cmd_city);
                        DataTable dt_city = new DataTable("cty");
                        sda_city.Fill(dt_city);
                        for (int cc = 0; cc < dt_city.Rows.Count; cc++)
                        {
                            deleteSubvariant(mat_name, mat_var_name, mat_var_sub, user, dt_city.Rows[cc]["name"].ToString(), dt_country.Rows[c]["country"].ToString());
                        }

                    }

                   
                }
                else if (f_allCity)
                {
                    SqlCommand cmd_country = new SqlCommand("select id,country from Country_Code where country = N'" + country_name + "'", conn);
                    SqlDataAdapter sda_country = new SqlDataAdapter(cmd_country);
                    DataTable dt_country = new DataTable("ctry");
                    sda_country.Fill(dt_country);

                    for (int c = 0; c < dt_country.Rows.Count; c++)
                    {
                        SqlCommand cmd_city = new SqlCommand("select id,name from City where country_id = (select id from Country_Code where country = N'" + dt_country.Rows[c]["country"] + "')", conn);
                        SqlDataAdapter sda_city = new SqlDataAdapter(cmd_city);
                        DataTable dt_city = new DataTable("cty");
                        sda_city.Fill(dt_city);
                        for (int cc = 0; cc < dt_city.Rows.Count; cc++)
                        {
                            deleteSubvariant(mat_name, mat_var_name, mat_var_sub, user, Convert.ToString(dt_city.Rows[cc]["name"]), Convert.ToString(dt_country.Rows[c]["country"]));

                        }
                    }

                }
                else if (f_singleCity)
                {
                    deleteSubvariant(mat_name, mat_var_name, mat_var_sub, user, city_name, country_name);
                }
                else
                {

                }

                conn.Close();
            }
            catch (System.Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);

            }
        }

        private string deleteSubvariant(String mat_name, String mat_var_name, String mat_var_sub, string user, string city_name, string country_name)
        {
            SqlConnection conM = new SqlConnection(connection_string);

            try
            {
                SqlCommand cmd1;
                SqlCommand cmd_seq;
                SqlCommand cmd_country;
                DataTable dt_country;
                SqlDataAdapter sda_country;
                SqlCommand cmd_city;
                DataTable dt_city;
                SqlDataAdapter sda_city;
                SqlCommand cmd_project;
                DataTable dt_project;
                SqlDataAdapter sda_project;
                SqlCommand cmd_matOpt;
                DataTable dt_matOpt;
                SqlDataAdapter sda_matOpt;
                DataTable dt;
                SqlDataAdapter sda;
                Int64? seq;
                int inCostMaster = 0;
                int inCostMasterProjectMaster = 0;
                int inCMPProject = 0;
                int alreadyexistsinproject = 0;
                int alreadydeletedProj = 0;
                SqlCommand cmd_insert;
                SqlCommand cmd_MasterDtl;
                SqlDataAdapter sda_MasterDtl;
                DataTable dt_MasterDtl;
                string revit_category = string.Empty;
                string family_type = string.Empty;
                string property = string.Empty;
                string design_rule_factor = string.Empty;
                string keywords = string.Empty;
                string quantity_extraction_formula = string.Empty;
                int f_all_elements = 0;
                string element_filter = string.Empty;
                Int64 originalId;
                Int64? id = null;

                conM.Open();

                cmd_MasterDtl = new SqlCommand(@"select revit_category,family_type,property,design_rule_factor,keywords,quantity_extraction_formula,f_all_elements,element_filter from 
Material_Variance_Subdivision_dtl where material_var_subdiv_id = (select id from Material_Variance_Subdivision 
where name =N'" + mat_var_sub + "' and city_id = (select id from City where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_name + "')) and material_variance_id = (select id from Material_Variance where variance =N'" + mat_var_name + @"' 
and material_id = (select id from Material where name = N'" + mat_name + "')));", conM);

                sda_MasterDtl = new SqlDataAdapter(cmd_MasterDtl);
                dt_MasterDtl = new DataTable("dtl");
                sda_MasterDtl.Fill(dt_MasterDtl);
                if (dt_MasterDtl.Rows.Count > 0)
                {
                    revit_category = dt_MasterDtl.Rows[0]["revit_category"].ToString();
                    family_type = dt_MasterDtl.Rows[0]["family_type"].ToString();
                    property = dt_MasterDtl.Rows[0]["property"].ToString();
                    design_rule_factor = dt_MasterDtl.Rows[0]["design_rule_factor"].ToString();
                    keywords = dt_MasterDtl.Rows[0]["keywords"].ToString();
                    quantity_extraction_formula = dt_MasterDtl.Rows[0]["quantity_extraction_formula"].ToString();
                    f_all_elements = dt_MasterDtl.Rows[0]["f_all_elements"] == null ? 0 : Convert.ToInt16(dt_MasterDtl.Rows[0]["f_all_elements"]);
                    element_filter = dt_MasterDtl.Rows[0]["element_filter"].ToString();
                }

                cmd1 = new SqlCommand(@"SELECT max(seq) seq FROM Material_Variance_Subdivision where material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and name = N'" + mat_var_sub + @"'
                                                                                           and city_id =
													(select id from City where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_name + "')); ", conM);
                sda = new SqlDataAdapter(cmd1);
                dt = new DataTable("mat");
                sda.Fill(dt);

                seq = 1;
                if (dt == null || dt.Rows[0]["seq"] == null)
                {
                    seq = 1;
                }
                else
                {
                    try
                    {
                        seq = Convert.ToInt64(dt.Rows[0]["seq"]);
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
                cmd_seq = new SqlCommand((@"update Material_Variance_Subdivision 
                        set seq = seq - 1 where material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and city_id =
													(select id from City where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_name + "'))  and seq > 1 and seq > " + seq), conM);
                cmd_seq.ExecuteNonQuery();
                SqlCommand cmd = new SqlCommand(@"Update Material_Variance_subdivision set f_del=1,seq = null WHERE Name=N'" + mat_var_sub + @"' and city_id =
													(select id from City where name = N'" + city_name + @"' and country_id = (select id from Country_Code where 
													country = N'" + country_name + "')) and material_variance_id =(select id from material_variance where variance=N'" + mat_var_name + @"' and material_id = (select id from material where name=N'" + mat_name + @"'))", conM);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand(@"select id from Material_Variance_subdivision where f_del=1 and Name=N'" + mat_var_sub + @"' and city_id =
													(select id from City where name = N'" + city_name + @"' and country_id = (select id from Country_Code where 
													country = N'" + country_name + "')) and material_variance_id =(select id from material_variance where variance=N'" + mat_var_name + @"' and material_id = (select id from material where name=N'" + mat_name + @"'))", conM);
                SqlDataAdapter sdaID = new SqlDataAdapter(cmd);
                DataTable dtID = new DataTable("ID");
                sdaID.Fill(dtID);
                originalId = Convert.ToInt64(dtID.Rows[0]["id"]);

                //cmd_country = new SqlCommand(@"select id,code,currency from Country_Code where country = N'" + country_name + "'", conM);
                //sda_country = new SqlDataAdapter(cmd_country);
                //dt_country = new DataTable("country");
                //sda_country.Fill(dt_country);
                //for (int c = 0; c < dt_country.Rows.Count; c++)
                //{
                //    cmd_city = new SqlCommand(@"select id,name from City where country_id =" + dt_country.Rows[c]["id"] + "", conM);
                //    sda_city = new SqlDataAdapter(cmd_city);
                //    dt_city = new DataTable("city");
                //    sda_city.Fill(dt_city);

                //    for (int cty = 0; cty < dt_city.Rows.Count; cty++)
                //    {
                cmd_project = new SqlCommand(@"select proj_guid,name from project where created_on >='2022-09-23' and f_active = 1 and city_id = (select id from City where name = N'" + city_name + @"' and country_id = (select id from Country_Code where 
													country = N'" + country_name + "'))", conM);
                        sda_project = new SqlDataAdapter(cmd_project);
                        dt_project = new DataTable("project");
                        sda_project.Fill(dt_project);
                        for (int p = 0; p < dt_project.Rows.Count; p++)
                        {
                            //Check in Material_Variance_Subdivision_Project first then update 
                            using (SqlConnection conn = new SqlConnection(connection_string))
                            {
                                conn.Open();
                                using (SqlCommand cmdGetProjectCount = new SqlCommand(@"SELECT COUNT(*) FROM Material_Variance_Subdivision_Project where material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and project_id = (select id from Project where proj_guid =N'" + dt_project.Rows[p]["proj_guid"] + @"' and name= N'" + dt_project.Rows[p]["name"] + @"') and name = N'" + mat_var_sub + @"';", conn))
                                {
                                    alreadyexistsinproject = (int)cmdGetProjectCount.ExecuteScalar();
                                }

                                if (alreadyexistsinproject == 0)
                                {
                                    using (SqlCommand cmdGetProjectCount = new SqlCommand(@"SELECT COUNT(*) FROM Material_Variance_Subdivision_Project where material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 1 and project_id = (select id from Project where proj_guid =N'" + dt_project.Rows[p]["proj_guid"] + @"' and name= N'" + dt_project.Rows[p]["name"] + @"') and name = N'" + mat_var_sub + @"';", conn))
                                    {
                                        alreadydeletedProj = (int)cmdGetProjectCount.ExecuteScalar();
                                    }
                                }
                                if (alreadyexistsinproject == 0 && alreadydeletedProj == 0)
                                {
                                    int countProject = 0;
                                    using (SqlCommand cmdGetProjectCount = new SqlCommand("(select COUNT(id) from Project where proj_guid =N'" + dt_project.Rows[p]["proj_guid"] + @"' and name= N'" + dt_project.Rows[p]["name"] + @"'and created_on >= 
 (select created_on from Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and city_id = (select id from City where name = N'" + city_name + @"' and country_id = (select id from Country_Code where 
													country = N'" + country_name + @"'))  and material_variance_id = 
 (select id from Material_Variance where variance = N'" + mat_var_name + @"' and material_id = (select id from Material where name = N'" + mat_name + @"'))))", conn))
                                    {
                                        countProject = (int)cmdGetProjectCount.ExecuteScalar();
                                    }
                                    if (countProject == 1)
                                    {

                                        cmd_insert = new SqlCommand(@"INSERT into Material_Variance_Subdivision_Project(material_variance_id,name,created_by,created_on,unit_of_measurement,f_del,seq,specification,project_id)		
VALUES((select id from Material_Variance where variance = N'" + mat_var_name + @"' and material_id = (select id from Material where name = N'" + mat_name + @"')),
N'" + mat_var_sub + @"',N'" + user + @"',CURRENT_TIMESTAMP,(select Unit_Of_Measurement from Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and city_id = (select id from City where name = N'" + city_name + @"' and country_id = (select id from Country_Code where 
													country = N'" + country_name + @"')) and material_variance_id =(select id from Material_Variance where variance = N'" + mat_var_name + @"' and
 material_id = (select id from Material where name = N'" + mat_name + @"'))),
0," + seq + ",(select specification from Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and city_id = (select id from City where name = N'" + city_name + @"' and country_id = (select id from Country_Code where 
													country = N'" + country_name + @"')) and material_variance_id =(select id from Material_Variance where variance = N'" + mat_var_name + @"' and
 material_id = (select id from Material where name = N'" + mat_name + @"'))),(select id from Project where proj_guid =N'" + dt_project.Rows[p]["proj_guid"] + @"' and name= N'" + dt_project.Rows[p]["name"] + @"'and created_on >= (select created_on from Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and city_id = (select id from City where name = N'" + city_name + @"' and country_id = (select id from Country_Code where 
													country = N'" + country_name + @"')) and material_variance_id = (select id from Material_Variance where variance = N'" + mat_var_name + @"' and material_id = (select id from Material where name = N'" + mat_name + @"')))))", conn);

                                        cmd_insert.ExecuteNonQuery();

                                        SqlCommand cmd_id = new SqlCommand(@"SELECT id FROM Material_Variance_Subdivision_Project where name = N'" + @mat_var_sub + @"' and material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and project_id = (select id from Project where proj_guid =N'" + dt_project.Rows[p]["proj_guid"] + "' and name= N'" + dt_project.Rows[p]["name"] + "') ; ", conn);
                                        SqlDataAdapter sda_id = new SqlDataAdapter(cmd_id);
                                        DataTable dt_id = new DataTable("mat");
                                        sda_id.Fill(dt_id);
                                        id = Convert.ToInt64(dt_id.Rows[0]["id"]);

                                        if (!string.IsNullOrEmpty(revit_category) || !string.IsNullOrEmpty(family_type) || !string.IsNullOrEmpty(property) || !string.IsNullOrEmpty(design_rule_factor) || !string.IsNullOrEmpty(keywords) || !string.IsNullOrEmpty(quantity_extraction_formula))
                                        {
                                            SqlCommand cmd11 = new SqlCommand((@"INSERT INTO Material_Variance_Subdivision_dtl_Project 
                        (material_var_subdiv_proj_id, 
                         revit_category, 
                         family_type, 
                         property, 
                         design_rule_factor,
                         keywords,
                         quantity_extraction_formula,f_all_elements,element_filter,updated_by,project_id) 
            VALUES    (" + id + ",N'" + revit_category + "',N'" + family_type + "',N'" + property + "',N'" + design_rule_factor + "',N'" + keywords + "',N'" + quantity_extraction_formula + "'," + f_all_elements + ",N'" + element_filter + "',N'" + user + "',(select id from Project where proj_guid =N'" + dt_project.Rows[p]["proj_guid"] + @"' and name= N'" + dt_project.Rows[p]["name"] + @"'))"), conn);
                                            cmd11.ExecuteNonQuery();
                                        }

                                        cmd_matOpt = new SqlCommand(@"select id,name from Material_Option", conn);
                                        sda_matOpt = new SqlDataAdapter(cmd_matOpt);
                                        dt_matOpt = new DataTable("matOpt");
                                        sda_matOpt.Fill(dt_matOpt);
                                        for (int mo = 0; mo < dt_matOpt.Rows.Count; mo++)
                                        {

                                            //if (inCostMastertemp != null)
                                            //{
                                            cmd_insert = new SqlCommand(@" SELECT COUNT(*) 
                        FROM   cost_master_Project 
                        WHERE  material_option_id = (SELECT id 
                                                     FROM   material_option 
                                                     WHERE  NAME = N'" + dt_matOpt.Rows[mo]["name"] + @"') and project_id in (select id from project where proj_guid = N'" + dt_project.Rows[p]["proj_guid"] + "' and name = N'" + dt_project.Rows[p]["name"] + @"')
                               AND material_variance_subdiv_id = 
                                   (SELECT id 
                                    FROM 
                                   material_variance_subdivision 
                                                                  WHERE f_del=1 and NAME = 
                                   N'" + @mat_var_sub + @"' and city_id = (select id from City where name = N'" + city_name + @"' and country_id = (select id from Country_Code where 
													country = N'" + country_name + @"')) 
                                                                         AND 
                                       material_variance_id = (SELECT id 
                                                               FROM   material_variance 
                                                               WHERE 
                                       variance = N'" + @mat_var_name + @"' 
                                       AND material_id = (SELECT id 
                                                          FROM   material 
                                                          WHERE  NAME = 
                                           N'" + @mat_name + @"'))) 
                               AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end  and city_id in (select id from city where country_id in (select id from  country_code where code = N'" + country_name + @"') and name = N'" + city_name + @"');


               ", conn);
                                            inCostMasterProjectMaster = (int)cmd_insert.ExecuteScalar();
                                            if (inCostMasterProjectMaster == 1 && id != null)
                                            {
                                                cmd_insert = new SqlCommand(@"UPDATE Cost_master_project 
                              SET material_variance_subdiv_id_proj = " + id + @" 
                              WHERE  material_option_id = (SELECT id 
                                                           FROM   material_option 
                                                           WHERE  NAME = N'" + dt_matOpt.Rows[mo]["name"] + @"') 
                                     and project_id in (select id from project where proj_guid = N'" + dt_project.Rows[p]["proj_guid"] + "' and name = N'" + dt_project.Rows[p]["name"] + @"')
                                     AND material_variance_subdiv_id = 
                                         (SELECT id 
                                          FROM 
                                         material_variance_subdivision 
                                                                        WHERE f_del=1 and NAME = 
                                         N'" + @mat_var_sub + @"' and city_id = (select id from City where name = N'" + city_name + @"' and country_id = (select id from Country_Code where 
													country = N'" + country_name + @"')) 
                                                                               AND 
                                             material_variance_id = (SELECT id 
                                                                     FROM   material_variance 
                                                                     WHERE 
                                             variance = N'" + @mat_var_name + @"' 
                                             AND material_id = (SELECT id 
                                                                FROM   material 
                                                                WHERE  NAME = 
                                                 N'" + @mat_name + @"'))) 
                                     AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end and city_id in (select id from city where country_id in (select id from country_code where code = N'" + country_name + @"') and name = N'" + city_name + "');", conn);

                                                cmd_insert.ExecuteNonQuery();

                                                cmd_insert = new SqlCommand(@"UPDATE Cost_master_project SET material_variance_subdiv_id = null 
WHERE  material_option_id = (SELECT id FROM   material_option 
WHERE  NAME = N'" + dt_matOpt.Rows[mo]["name"] + @"') 
and project_id in (select id from project where proj_guid = N'" + dt_project.Rows[p]["proj_guid"] + "' and name = N'" + dt_project.Rows[p]["name"] + @"')
AND material_variance_subdiv_id_proj = (SELECT id FROM Material_Variance_Subdivision_Project WHERE f_del=0 and project_id in (select id from project where proj_guid = N'" + dt_project.Rows[p]["proj_guid"] + "' and name = N'" + dt_project.Rows[p]["name"] + @"') and NAME = N'" + @mat_var_sub + @"' AND material_variance_id = (SELECT id FROM   material_variance WHERE variance = N'" + @mat_var_name + @"'  AND material_id = (SELECT id FROM material WHERE  NAME =  N'" + @mat_name + @"'))) AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end and  city_id in (select id from city where country_id in (select id from country_code where  code = N'" + country_name + @"') and name = N'" + city_name + @"');", conn);

                                                cmd_insert.ExecuteNonQuery();
                                            }
                                            //}
                                        }
                                    }
                                }

                            }
                        }
                //    }
                //}

                return "Success";
            }
            catch (Exception ex)
            {
                if (conM.State == ConnectionState.Open)
                {
                    conM.Close();
                }
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
                return "Failure";
            }
        }


        public void delete_subdiv_project(string mat_name, String mat_var_name, String mat_var_sub, string user, string proj_guid, string proj_name)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            SqlCommand cmd1;
            SqlCommand cmd_seq;
            DataTable dt;
            SqlDataAdapter sda;
            Int64? seq;
            int count = 0;
            int countMaster = 0;
            SqlCommand cmd_MasterDtl;
            SqlDataAdapter sda_MasterDtl;
            DataTable dt_MasterDtl;
            string revit_category = string.Empty;
            string family_type = string.Empty;
            string property = string.Empty;
            string design_rule_factor = string.Empty;
            string keywords = string.Empty;
            string quantity_extraction_formula = string.Empty;

            try
            {
                //conn.Open();

                using (conn = new SqlConnection(connection_string))
                {
                    conn.Open();
                    using (SqlCommand cmdGetProjectCount = new SqlCommand(@"SELECT COUNT(*) FROM Material_Variance_Subdivision_Project where material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and project_id = (select id from Project where proj_guid =N'" + proj_guid + "' and name= N'" + proj_name + "') and name = N'" + mat_var_sub + @"';", conn))
                    {
                        count = (int)cmdGetProjectCount.ExecuteScalar();
                    }
                }
                if (count == 1)
                {
                    using (SqlConnection con = new SqlConnection(connection_string))
                    {
                        con.Open();
                        using (cmd1 = new SqlCommand(@"SELECT max(seq) seq FROM Material_Variance_Subdivision_Project where material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and project_id = (select id from Project where proj_guid =N'" + proj_guid + "' and name= N'" + proj_name + "') and name = N'" + mat_var_sub + "' ; ", con))
                        {
                            sda = new SqlDataAdapter(cmd1);
                            dt = new DataTable("mat");
                            sda.Fill(dt);
                            seq = Convert.ToInt64(dt.Rows[0]["seq"]);
                        }
                        using (cmd_seq = new SqlCommand((@"update Material_Variance_Subdivision_Project
                        set seq = seq - 1 where material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and project_id = (select id from Project where proj_guid =N'" + proj_guid + "' and name= N'" + proj_name + "') and seq > 1 and seq > " + seq), con))
                        {
                            cmd_seq.ExecuteNonQuery();
                            SqlCommand cmd_project = new SqlCommand(@"Update Material_Variance_subdivision_Project set f_del=1,seq = null WHERE project_id = (select id from Project where proj_guid =N'" + proj_guid + "' and name= N'" + proj_name + "') and Name=N'" + mat_var_sub + @"' and material_variance_id =(select id from material_variance where variance=N'" + mat_var_name + @"' and material_id = (select id from material where name=N'" + mat_name + @"'))", con);
                            cmd_project.ExecuteNonQuery();
                        }
                    }

                    //conn.Close();
                }
                else
                {
                    long city_id = 0;
                    using (conn = new SqlConnection(connection_string))
                    {
                        conn.Open();
                        SqlCommand cmd_cityid = new SqlCommand("select city_id from Project where proj_guid = N'" + proj_guid + "' and name = N'" + proj_name + @"';", conn);
                        city_id = (long)cmd_cityid.ExecuteScalar();

                        
                    }
                    using (conn = new SqlConnection(connection_string))
                    {
                        conn.Open();
                        using (SqlCommand cmdMasterCount = new SqlCommand(@"SELECT COUNT(*) FROM Material_Variance_Subdivision where material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and name = N'" + mat_var_sub + @"' and city_id = "+ city_id+";", conn))
                        {
                            countMaster = (int)cmdMasterCount.ExecuteScalar();
                        }
                    }

                    if (countMaster == 1)
                    {
                        using (SqlConnection con = new SqlConnection(connection_string))
                        {
                            con.Open();
                            using (cmd_MasterDtl = new SqlCommand(@"select revit_category,family_type,property,design_rule_factor,keywords,quantity_extraction_formula from Material_Variance_Subdivision_dtl where material_var_subdiv_id = (select id from Material_Variance_Subdivision where name =N'" + mat_var_sub + "' and city_id = "+city_id+" and material_variance_id = (select id from Material_Variance where variance =N'" + mat_var_name + "' and material_id = (select id from Material where name = N'" + mat_name + "')));", con))
                            {
                                sda_MasterDtl = new SqlDataAdapter(cmd_MasterDtl);
                                dt_MasterDtl = new DataTable("dtl");
                                sda_MasterDtl.Fill(dt_MasterDtl);
                                if (dt_MasterDtl.Rows.Count > 0)
                                {
                                    revit_category = dt_MasterDtl.Rows[0]["revit_category"].ToString();
                                    family_type = dt_MasterDtl.Rows[0]["family_type"].ToString();
                                    property = dt_MasterDtl.Rows[0]["property"].ToString();
                                    design_rule_factor = dt_MasterDtl.Rows[0]["design_rule_factor"].ToString();
                                    keywords = dt_MasterDtl.Rows[0]["keywords"].ToString();
                                    quantity_extraction_formula = dt_MasterDtl.Rows[0]["quantity_extraction_formula"].ToString();
                                }
                            }

                            using (cmd1 = new SqlCommand(@"SELECT max(seq) seq FROM Material_Variance_Subdivision where material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and name = N'" + mat_var_sub + "' and city_id = "+city_id+"; ", con))
                            {
                                sda = new SqlDataAdapter(cmd1);
                                dt = new DataTable("mat");
                                sda.Fill(dt);
                                seq = Convert.ToInt64(dt.Rows[0]["seq"]);
                            }

                            using (cmd1 = new SqlCommand(@"INSERT into Material_Variance_Subdivision_Project(material_variance_id,name,created_by,created_on,unit_of_measurement,f_del,seq,specification,project_id)		
VALUES((select id from Material_Variance where variance = N'" + mat_var_name + @"' and material_id = (select id from Material where name = N'" + mat_name + @"')),
N'" + mat_var_sub + @"',N'" + user + @"',CURRENT_TIMESTAMP,(select Unit_Of_Measurement from Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and city_id = "+city_id+" and material_variance_id =(select id from Material_Variance where variance = N'" + mat_var_name + @"' and
 material_id = (select id from Material where name = N'" + mat_name + @"'))),
1," + seq + @",(select specification from Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and city_id = "+city_id+" and material_variance_id =(select id from Material_Variance where variance = N'" + mat_var_name + @"' and
 material_id = (select id from Material where name = N'" + mat_name + @"'))),(select id from Project where proj_guid =N'" + proj_guid + @"' and name= N'" + proj_name + @"'))", con))
                            {

                                cmd1.ExecuteNonQuery();
                            }

                            using (cmd_seq = new SqlCommand((@"update Material_Variance_Subdivision_Project 
                        set seq = seq - 1 where material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and project_id = (select id from Project where proj_guid =N'" + proj_guid + "' and name= N'" + proj_name + "') and seq > 1 and seq > " + seq), con))
                            {
                                cmd_seq.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd_id = new SqlCommand(@"SELECT id FROM Material_Variance_Subdivision_Project where name = N'" + @mat_var_sub + @"' and material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 1 and project_id = (select id from Project where proj_guid =N'" + proj_guid + "' and name= N'" + proj_name + "'); ", con))
                            {
                                SqlDataAdapter sda_id = new SqlDataAdapter(cmd_id);
                                DataTable dt_id = new DataTable("mat");
                                sda_id.Fill(dt_id);
                                Int64 id = Convert.ToInt64(dt_id.Rows[0]["id"]);


                                if (!string.IsNullOrEmpty(revit_category) || !string.IsNullOrEmpty(family_type) || !string.IsNullOrEmpty(property) || !string.IsNullOrEmpty(design_rule_factor) || !string.IsNullOrEmpty(keywords) || !string.IsNullOrEmpty(quantity_extraction_formula))
                                {
                                    using (SqlCommand cmd11 = new SqlCommand((@"INSERT INTO Material_Variance_Subdivision_dtl_Project 
                        (material_var_subdiv_proj_id, 
                         revit_category, 
                         family_type, 
                         property, 
                         design_rule_factor,
                         keywords,
                         quantity_extraction_formula,project_id) 
            VALUES    (" + id + ",N'" + revit_category + "',N'" + family_type + "',N'" + property + "',N'" + design_rule_factor + "',N'" + keywords + "',N'" + quantity_extraction_formula + "',(select id from Project where proj_guid =N'" + proj_guid + "' and name= N'" + proj_name + "'))"), con))
                                        cmd11.ExecuteNonQuery();
                                }
                            }
                        }
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


        public bool SubdivExistsInMaster(string mat_name, String mat_var_name, String mat_var_sub, string user, string proj_guid, string proj_name,string city_name, string country_name)
        {
            bool isexist = false;
            int countMaster;
            SqlConnection conn = new SqlConnection(connection_string);
            using (conn = new SqlConnection(connection_string))
            {
                conn.Open();
                using (SqlCommand cmdMasterCount = new SqlCommand(@"SELECT COUNT(*) FROM Material_Variance_Subdivision where material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and name = N'" + mat_var_sub + @"' and city_id = (select id from City where name = N'" + city_name + @"' and country_id = (select id from Country_Code where 
													country = N'" + country_name + @"'));", conn))
                {
                    countMaster = (int)cmdMasterCount.ExecuteScalar();
                }

                if (countMaster == 1)
                {
                    isexist = true;
                }
            }

            return isexist;
        }

    }
}
