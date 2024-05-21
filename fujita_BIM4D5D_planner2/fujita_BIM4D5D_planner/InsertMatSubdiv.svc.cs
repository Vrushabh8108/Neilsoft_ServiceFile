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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service5" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service5.svc or Service5.svc.cs at the Solution Explorer and start debugging.
    public class Service5 : InsertMatSubdiv
    {
        int alreadyexistsinproject = 0;
        int alreadydeletedProj = 0;
        Int64? id;
        int seq;
        int inCostMasterProjectMaster = 0;
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        //        public string ins_mat_subdiv_NotCitySpecific(String mat_name, String mat_var_name, string mat_var_sub, string user_info, string unit_of_measurement, [Optional] Int64? prev_seq, [Optional]string revit_category, [Optional]string family_type, [Optional]string property, [Optional]string design_rule_factor, [Optional]string keywords, [Optional]string quantity_extraction_formula, [Optional]string specification)
        //        {
        //            SqlConnection conn = new SqlConnection(connection_string);
        //            ConnectionState state = conn.State;
        //            try
        //            {
        //                conn.Open();
        //                int check;
        //                SqlCommand cmd1;
        //                SqlCommand cmd_seq;
        //                DataTable dt;
        //                SqlDataAdapter sda;
        //                SqlCommand cmd_id;
        //                DataTable dt_id;
        //                SqlDataAdapter sda_id;
        //                Int64? seq;
        //                Int64? id;

        //                if (mat_var_sub.Contains("'"))
        //                {
        //                    mat_var_sub = mat_var_sub.Replace("'", "''");
        //                }
        //                if (prev_seq == null)
        //                {
        //                    cmd1 = new SqlCommand(@"SELECT coalesce(max(seq)+1,1) seq FROM Material_Variance_Subdivision where material_variance_id in  (
        //                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
        //                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 ; ", conn);
        //                    sda = new SqlDataAdapter(cmd1);
        //                    dt = new DataTable("mat");
        //                    sda.Fill(dt);
        //                    seq = Convert.ToInt64(dt.Rows[0]["seq"]);
        //                }
        //                else
        //                {
        //                    seq = prev_seq + 1;
        //                    cmd_seq = new SqlCommand((@"update Material_Variance_Subdivision 
        //                        set seq = seq + 1 where material_variance_id in  (
        //                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
        //                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0  and seq > " + prev_seq), conn);
        //                    cmd_seq.ExecuteNonQuery();
        //                }
        //                string query_cnt = "Select isnull((select f_del from Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and material_variance_id in (
        //                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
        //                                                    select id from Material where name=N'" + mat_name + @"'))),2)";
        //                using (SqlCommand command = new SqlCommand(query_cnt, conn))
        //                {
        //                    check = (int)command.ExecuteScalar();
        //                }
        //                if (check == 1)
        //                {
        //                    SqlCommand cmd_upd = new SqlCommand(@"update Material_Variance_Subdivision set f_del = 0,seq = " + seq + " where name = N'" + mat_var_sub + @"' and material_variance_id in (
        //                                                    select id from Material_Variance where variance = N'" + mat_var_name + @"' and material_id in (
        //                                                    select id from Material where name = N'" + mat_name + @"'))", conn);
        //                    cmd_upd.ExecuteNonQuery();
        //                    conn.Close();
        //                    return "Success";
        //                }
        //                else
        //                {
        //                    if (check == 2)
        //                    {
        //                        SqlCommand cmd = new SqlCommand((@"INSERT INTO material_variance_subdivision 
        //                        (material_variance_id, 
        //                         NAME, 
        //                         created_by, 
        //                         created_on, 
        //                         unit_of_measurement,
        //                         f_del,
        //                         seq,
        //                         specification) 
        //            VALUES      ((SELECT id 
        //                          FROM   material_variance 
        //                          WHERE  variance = '" + @mat_var_name + @"' 
        //                                 AND material_id = (SELECT id 
        //                                                    FROM   material 
        //                                                    WHERE  NAME = '" + mat_name + @"')), 
        //                         N'" + @mat_var_sub + @"', 
        //                         N'" + @user_info + @"', 
        //                         CURRENT_TIMESTAMP, 
        //                         (SELECT ID FROM UNIT_OF_MEASUREMENT WHERE UNIT = N'" + @unit_of_measurement + @"'),0," + seq + ",N'" +
        //                         @specification + "')"), conn);
        //                        cmd.ExecuteNonQuery();
        //                        if (revit_category != null || family_type != null || property != null || design_rule_factor != null || keywords != null || quantity_extraction_formula != null)
        //                        {
        //                            cmd_id = new SqlCommand(@"SELECT id FROM Material_Variance_Subdivision where name = N'" + @mat_var_sub + @"' and material_variance_id in  (
        //                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
        //                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 ; ", conn);
        //                            sda_id = new SqlDataAdapter(cmd_id);
        //                            dt_id = new DataTable("mat");
        //                            sda_id.Fill(dt_id);
        //                            id = Convert.ToInt64(dt_id.Rows[0]["id"]);
        //                            SqlCommand cmd11 = new SqlCommand((@"INSERT INTO Material_Variance_Subdivision_dtl 
        //                        (material_var_subdiv_id, 
        //                         revit_category, 
        //                         family_type, 
        //                         property, 
        //                         design_rule_factor,
        //                         keywords,
        //                         quantity_extraction_formula) 
        //            VALUES    (" + id + ",N'" + revit_category + "',N'" + family_type + "',N'" + property + "',N'" + design_rule_factor + "',N'" + keywords + "',N'" + quantity_extraction_formula + "')"), conn);
        //                            cmd11.ExecuteNonQuery();
        //                        }
        //                        conn.Close();
        //                        return "Success";
        //                    }

        //                    else
        //                    {
        //                        conn.Close();
        //                        throw new Exception("The material variance sub already exists");

        //                    }
        //                }
        //            }
        //            catch (System.Exception ex)
        //            {
        //                if (conn.State == ConnectionState.Open)
        //                {
        //                    conn.Close();
        //                }
        //                Service17 exception1 = new Service17();
        //                exception1.SendErrorToText(ex);
        //                return ex.Message;

        //            }

        //        }


        //        public string ins_mat_subdiv(String mat_name, String mat_var_name, string mat_var_sub, string user_info, string unit_of_measurement, [Optional] Int64? prev_seq, [Optional]string revit_category, [Optional]string family_type, [Optional]string property, [Optional]string design_rule_factor, [Optional]string keywords, [Optional]string quantity_extraction_formula, [Optional]string specification, string city_name, string country_name, bool f_allCountry, bool f_allCity, bool f_singleCity, int f_all_elements, [Optional]string element_filter)
        //        {
        //            SqlConnection conn = new SqlConnection(connection_string);
        //            try
        //            {               

        //                conn.Open();

        //                if (mat_var_sub.Contains("'"))
        //                {
        //                    mat_var_sub = mat_var_sub.Replace("'", "''");
        //                }

        //                if (f_allCountry)
        //                {
        //                    SqlCommand cmd_country = new SqlCommand("select id,country from Country_Code order by country;", conn);
        //                    SqlDataAdapter sda_country = new SqlDataAdapter(cmd_country);
        //                    DataTable dt_country = new DataTable("ctry");
        //                    sda_country.Fill(dt_country);

        //                    for (int c = 0; c < dt_country.Rows.Count; c++)
        //                    {
        //                        SqlCommand cmd_city = new SqlCommand("select id,name from City where country_id = (select id from Country_Code where country = N'" + dt_country.Rows[c]["country"] + "')", conn);
        //                        SqlDataAdapter sda_city = new SqlDataAdapter(cmd_city);
        //                        DataTable dt_city = new DataTable("cty");
        //                        sda_city.Fill(dt_city);
        //                        for (int cc = 0; cc < dt_city.Rows.Count; cc++)
        //                        {
        //                            insert_Subdiv(mat_name, mat_var_name, mat_var_sub, user_info, unit_of_measurement, prev_seq, revit_category, family_type, property, design_rule_factor, keywords, quantity_extraction_formula, specification, Convert.ToString(dt_city.Rows[cc]["name"]), Convert.ToString(dt_country.Rows[c]["country"]), f_all_elements, element_filter);
        //                        }

        //                    }

        //                    conn.Close();
        //                    return "Success";
        //                }
        //                else if (f_allCity)
        //                {
        //                    SqlCommand cmd_country = new SqlCommand("select id,country from Country_Code where country = N'" + country_name + "'", conn);
        //                    SqlDataAdapter sda_country = new SqlDataAdapter(cmd_country);
        //                    DataTable dt_country = new DataTable("ctry");
        //                    sda_country.Fill(dt_country);

        //                    for (int c = 0; c < dt_country.Rows.Count; c++)
        //                    {
        //                        SqlCommand cmd_city = new SqlCommand("select id,name from City where country_id = (select id from Country_Code where country = N'" + dt_country.Rows[c]["country"] + "')", conn);
        //                        SqlDataAdapter sda_city = new SqlDataAdapter(cmd_city);
        //                        DataTable dt_city = new DataTable("cty");
        //                        sda_city.Fill(dt_city);
        //                        for (int cc = 0; cc < dt_city.Rows.Count; cc++)
        //                        {
        //                            insert_Subdiv(mat_name, mat_var_name, mat_var_sub, user_info, unit_of_measurement, prev_seq, revit_category, family_type, property, design_rule_factor, keywords, quantity_extraction_formula, specification, Convert.ToString(dt_city.Rows[cc]["name"]), Convert.ToString(dt_country.Rows[c]["country"]), f_all_elements, element_filter);
        //                        }

        //                    }

        //                    conn.Close();
        //                    return "Success";
        //                }
        //                else if (f_singleCity)
        //                {
        //                    insert_Subdiv(mat_name, mat_var_name, mat_var_sub, user_info, unit_of_measurement, prev_seq, revit_category, family_type, property, design_rule_factor, keywords, quantity_extraction_formula, specification, city_name, country_name, f_all_elements, element_filter);
        //                    conn.Close();
        //                    return "Success";
        //                }
        //                else
        //                {
        //                    conn.Close();
        //                    return "Failure";
        //                }



        //            }
        //            catch (System.Exception ex)
        //            {

        //                if (conn.State == ConnectionState.Open)
        //                {
        //                    conn.Close();
        //                }
        //                Service17 exception1 = new Service17();
        //                exception1.SendErrorToText(ex);
        //                return ex.Message;

        //            }

        //        }

        //        private string insert_Subdiv(String mat_name, String mat_var_name, string mat_var_sub, string user_info, string unit_of_measurement, [Optional] Int64? prev_seq, [Optional]string revit_category, [Optional]string family_type, [Optional]string property, [Optional]string design_rule_factor, [Optional]string keywords, [Optional]string quantity_extraction_formula, [Optional]string specification, string city_name, string country_name, int f_all_elements, [Optional]string element_filter)
        //        {
        //            SqlConnection conn = new SqlConnection(connection_string);
        //            int check;
        //            SqlCommand cmd1;
        //            SqlCommand cmd_seq;
        //            DataTable dt;
        //            SqlDataAdapter sda;
        //            SqlCommand cmd_id;
        //            DataTable dt_id;
        //            SqlDataAdapter sda_id;
        //            Int64? seq;
        //            Int64? id;       

        //            try
        //            {                
        //                conn.Open();
        //                if (prev_seq == null)
        //                {
        //                    cmd1 = new SqlCommand(@"SELECT coalesce(max(seq)+1,1) seq FROM Material_Variance_Subdivision where material_variance_id in  (
        //                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
        //                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_name + "')); ", conn);
        //                    sda = new SqlDataAdapter(cmd1);
        //                    dt = new DataTable("mat");
        //                    sda.Fill(dt);
        //                    seq = Convert.ToInt64(dt.Rows[0]["seq"]);
        //                }
        //                else
        //                {
        //                    seq = prev_seq + 1;
        //                    cmd_seq = new SqlCommand((@"update Material_Variance_Subdivision 
        //                        set seq = seq + 1 where material_variance_id in  (
        //                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
        //                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_name + "')) and seq > " + prev_seq), conn);
        //                    cmd_seq.ExecuteNonQuery();
        //                }
        //                string query_cnt = "Select isnull((select f_del from Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_name + @"')) and material_variance_id in ( 
        //                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
        //                                                    select id from Material where name=N'" + mat_name + @"'))),2)";
        //                using (SqlCommand command = new SqlCommand(query_cnt, conn))
        //                {
        //                    check = (int)command.ExecuteScalar();
        //                }
        //                if (check == 1)
        //                {
        //                    SqlCommand cmd_upd = new SqlCommand(@"update Material_Variance_Subdivision set f_del = 0,seq = " + seq + " where name = N'" + mat_var_sub + @"' and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_name + @"')) and material_variance_id in (
        //                                                    select id from Material_Variance where variance = N'" + mat_var_name + @"' and material_id in (
        //                                                    select id from Material where name = N'" + mat_name + @"'))", conn);
        //                    cmd_upd.ExecuteNonQuery();
        //                    conn.Close();
        //                    return "Success";
        //                }
        //                else
        //                {
        //                    if (check == 2)
        //                    {
        //                        SqlCommand cmd = new SqlCommand((@"INSERT INTO material_variance_subdivision 
        //                        (material_variance_id, 
        //                         NAME, 
        //                         created_by, 
        //                         created_on, 
        //                         unit_of_measurement,
        //                         f_del,
        //                         seq,
        //                         specification,city_id) 
        //            VALUES      ((SELECT id 
        //                          FROM   material_variance 
        //                          WHERE  variance = '" + @mat_var_name + @"' 
        //                                 AND material_id = (SELECT id 
        //                                                    FROM   material 
        //                                                    WHERE  NAME = '" + mat_name + @"')), 
        //                         N'" + @mat_var_sub + @"', 
        //                         N'" + @user_info + @"', 
        //                         CURRENT_TIMESTAMP, 
        //                         (SELECT ID FROM UNIT_OF_MEASUREMENT WHERE UNIT = N'" + @unit_of_measurement + @"'),0," + seq + ",N'" +
        //                         @specification + "',(select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_name + @"')))"), conn);
        //                        cmd.ExecuteNonQuery();
        //                        if (revit_category != null || family_type != null || property != null || design_rule_factor != null || keywords != null || quantity_extraction_formula != null)
        //                        {
        //                            cmd_id = new SqlCommand(@"SELECT id FROM Material_Variance_Subdivision where name = N'" + @mat_var_sub + @"' and material_variance_id in  (
        //                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
        //                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_name + @"')) ; ", conn);
        //                            sda_id = new SqlDataAdapter(cmd_id);
        //                            dt_id = new DataTable("mat");
        //                            sda_id.Fill(dt_id);
        //                            id = Convert.ToInt64(dt_id.Rows[0]["id"]);
        //                            SqlCommand cmd11 = new SqlCommand((@"INSERT INTO Material_Variance_Subdivision_dtl 
        //                        (material_var_subdiv_id, 
        //                         revit_category, 
        //                         family_type, 
        //                         property, 
        //                         design_rule_factor,
        //                         keywords,
        //                         quantity_extraction_formula,f_all_elements,element_filter,updated_by) 
        //            VALUES    (" + id + ",N'" + revit_category + "',N'" + family_type + "',N'" + property + "',N'" + design_rule_factor + "',N'" + keywords + "',N'" + quantity_extraction_formula + "'," + f_all_elements + ",N'" + element_filter + "',N'"+user_info+"')"), conn);
        //                            cmd11.ExecuteNonQuery();
        //                        }
        //                        conn.Close();
        //                        return "Success";
        //                    }

        //                    else
        //                    {
        //                        conn.Close();
        //                        throw new Exception("The material variance sub already exists");

        //                    }
        //                }



        //            }
        //            catch (Exception ex)
        //            {
        //                ConnectionState state = conn.State;
        //                if (conn.State == ConnectionState.Open)
        //                {
        //                    conn.Close();
        //                }
        //                Service17 exception1 = new Service17();
        //                exception1.SendErrorToText(ex);
        //                return "Failure" + ex.Message;
        //            }
        //        }

        public string ins_mat_subdiv(String mat_name, String mat_var_name, string mat_var_sub, string user_info, string unit_of_measurement, [Optional] Int64? prev_seq, [Optional]string revit_category, [Optional]string family_type, [Optional]string property, [Optional]string design_rule_factor, [Optional]string keywords, [Optional]string quantity_extraction_formula, [Optional]string specification, string city_name, string country_name, bool f_allCountry, bool f_allCity, bool f_singleCity, int f_all_elements, [Optional]string element_filter, string sortingConditions)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            try
            {

                conn.Open();

                if (mat_var_sub.Contains("'"))
                {
                    mat_var_sub = mat_var_sub.Replace("'", "''");
                }

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
                            insert_Subdiv(mat_name, mat_var_name, mat_var_sub, user_info, unit_of_measurement, prev_seq, revit_category, family_type, property, design_rule_factor, keywords, quantity_extraction_formula, specification, Convert.ToString(dt_city.Rows[cc]["name"]), Convert.ToString(dt_country.Rows[c]["country"]), f_all_elements, element_filter, sortingConditions);
                        }

                    }

                    conn.Close();
                    return "Success";
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
                            insert_Subdiv(mat_name, mat_var_name, mat_var_sub, user_info, unit_of_measurement, prev_seq, revit_category, family_type, property, design_rule_factor, keywords, quantity_extraction_formula, specification, Convert.ToString(dt_city.Rows[cc]["name"]), Convert.ToString(dt_country.Rows[c]["country"]), f_all_elements, element_filter, sortingConditions);
                        }

                    }

                    conn.Close();
                    return "Success";
                }
                else if (f_singleCity)
                {
                    insert_Subdiv(mat_name, mat_var_name, mat_var_sub, user_info, unit_of_measurement, prev_seq, revit_category, family_type, property, design_rule_factor, keywords, quantity_extraction_formula, specification, city_name, country_name, f_all_elements, element_filter, sortingConditions);
                    conn.Close();
                    return "Success";
                }
                else
                {
                    conn.Close();
                    return "Failure";
                }



            }
            catch (System.Exception ex)
            {

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
                return ex.Message;

            }

        }

        private string insert_Subdiv(String mat_name, String mat_var_name, string mat_var_sub, string user_info, string unit_of_measurement, [Optional] Int64? prev_seq, [Optional]string revit_category, [Optional]string family_type, [Optional]string property, [Optional]string design_rule_factor, [Optional]string keywords, [Optional]string quantity_extraction_formula, [Optional]string specification, string city_name, string country_name, int f_all_elements, [Optional]string element_filter, string sortingConditions)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            int check;
            SqlCommand cmd1;
            SqlCommand cmd_seq;
            DataTable dt;
            SqlDataAdapter sda;
            SqlCommand cmd_id;
            DataTable dt_id;
            SqlDataAdapter sda_id;
            Int64? seq;
            Int64? id;

            try
            {
                conn.Open();
                if (prev_seq == null)
                {
                    cmd1 = new SqlCommand(@"SELECT coalesce(max(seq)+1,1) seq FROM Material_Variance_Subdivision where material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_name + "')); ", conn);
                    sda = new SqlDataAdapter(cmd1);
                    dt = new DataTable("mat");
                    sda.Fill(dt);
                    seq = Convert.ToInt64(dt.Rows[0]["seq"]);
                }
                else
                {
                    seq = prev_seq + 1;
                    cmd_seq = new SqlCommand((@"update Material_Variance_Subdivision 
                        set seq = seq + 1 where material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_name + "')) and seq > " + prev_seq), conn);
                    cmd_seq.ExecuteNonQuery();
                }
                string query_cnt = "Select isnull((select f_del from Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_name + @"')) and material_variance_id in ( 
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"'))),2)";
                using (SqlCommand command = new SqlCommand(query_cnt, conn))
                {
                    check = (int)command.ExecuteScalar();
                }
                if (check == 1)
                {
                    SqlCommand cmd_upd = new SqlCommand(@"update Material_Variance_Subdivision set f_del = 0,seq = " + seq + " where name = N'" + mat_var_sub + @"' and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_name + @"')) and material_variance_id in (
                                                    select id from Material_Variance where variance = N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name = N'" + mat_name + @"'))", conn);
                    cmd_upd.ExecuteNonQuery();
                    conn.Close();
                    return "Success";
                }
                else
                {
                    if (check == 2)
                    {

                        SqlCommand cmd = new SqlCommand((@"INSERT INTO material_variance_subdivision 
                                                (material_variance_id, 
                                                 NAME, 
                                                 created_by, 
                                                 created_on, 
                                                 unit_of_measurement,
                                                 f_del,
                                                 seq,
                                                 specification,city_id) 
                                    VALUES      ((SELECT id 
                                                  FROM   material_variance 
                                                  WHERE  variance = '" + @mat_var_name + @"' 
                                                         AND material_id = (SELECT id 
                                                                            FROM   material 
                                                                            WHERE  NAME = '" + mat_name + @"')), 
                                                 N'" + @mat_var_sub + @"', 
                                                 N'" + @user_info + @"', 
                                                 CURRENT_TIMESTAMP, 
                                                 (SELECT ID FROM UNIT_OF_MEASUREMENT WHERE UNIT = N'" + @unit_of_measurement + @"'),0," + seq + ",N'" +
                         @specification + "',(select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_name + @"')))"), conn);

                        //New Code Added For Adding Sorting Conditions
//                        SqlCommand cmd = new SqlCommand(@"INSERT INTO material_variance_subdivision 
//                        (material_variance_id,NAME, created_by, created_on, unit_of_measurement,f_del,seq,specification,city_id,Sorting_Logic) 
//                         VALUES  ((SELECT id FROM   material_variance WHERE  variance = @mat_var_name AND material_id = (SELECT id FROM   material WHERE  NAME = @mat_name)), 
//                         @mat_var_sub, @user_info, CURRENT_TIMESTAMP, (SELECT ID FROM UNIT_OF_MEASUREMENT WHERE UNIT = @unit_of_measurement),0,@seq,@specification,
//                         (SELECT id FROM city WHERE name = @city_name AND country_id = (SELECT id FROM Country_Code WHERE country = @country_name)),@sortingConditions)", conn);

//                        cmd.Parameters.AddWithValue("@mat_var_name", mat_var_name);
//                        cmd.Parameters.AddWithValue("@mat_name", mat_name);
//                        cmd.Parameters.AddWithValue("@mat_var_sub", mat_var_sub);
//                        cmd.Parameters.AddWithValue("@user_info", user_info);
//                        cmd.Parameters.AddWithValue("@unit_of_measurement", unit_of_measurement);
//                        cmd.Parameters.AddWithValue("@seq", seq);
//                        cmd.Parameters.AddWithValue("@specification", specification);
//                        cmd.Parameters.AddWithValue("@city_name", city_name);
//                        cmd.Parameters.AddWithValue("@country_name", country_name);
//                        cmd.Parameters.AddWithValue("@sortingConditions", sortingConditions);


                        cmd.ExecuteNonQuery();
                        if (revit_category != null || family_type != null || property != null || design_rule_factor != null || keywords != null || quantity_extraction_formula != null || sortingConditions!=null)
                        {
                            cmd_id = new SqlCommand(@"SELECT id FROM Material_Variance_Subdivision where name = N'" + @mat_var_sub + @"' and material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_name + @"')) ; ", conn);
                            sda_id = new SqlDataAdapter(cmd_id);
                            dt_id = new DataTable("mat");
                            sda_id.Fill(dt_id);
                            id = Convert.ToInt64(dt_id.Rows[0]["id"]);
                            SqlCommand cmd11 = new SqlCommand((@"INSERT INTO Material_Variance_Subdivision_dtl 
                        (material_var_subdiv_id, 
                         revit_category, 
                         family_type, 
                         property, 
                         design_rule_factor,
                         keywords,
                         quantity_extraction_formula,f_all_elements,element_filter,updated_by,Sorting_Logic) 
            VALUES    (" + id + ",N'" + revit_category + "',N'" + family_type + "',N'" + property + "',N'" + design_rule_factor + "',N'" + keywords + "',N'" + quantity_extraction_formula + "'," + f_all_elements + ",N'" + element_filter + "',N'" + user_info +"',N'" + sortingConditions+ "')"), conn);
                            cmd11.ExecuteNonQuery();
                        }
                        conn.Close();
                        return "Success";
                    }

                    else
                    {
                        conn.Close();
                        throw new Exception("The material variance sub already exists");

                    }
                }



            }
            catch (Exception ex)
            {
                ConnectionState state = conn.State;
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
                return "Failure" + ex.Message;
            }
        }



        //        public Mat_var_subdiv_dtl view_mat_var_subdiv_dtl(String mat_name, String mat_var_name, string mat_var_sub)
        //        {
        //            SqlConnection conn = new SqlConnection(connection_string);
        //            ConnectionState state = conn.State;
        //            try
        //            {
        //                SqlCommand cmd;
        //                SqlDataAdapter sda;
        //                DataTable dt;
        //                Mat_var_subdiv_dtl Mat_var_subdiv_dtl1 = new Mat_var_subdiv_dtl();
        //                using (conn = new SqlConnection(connection_string))
        //                {
        //                    cmd = new SqlCommand(@"select (select name from Material_Variance_Subdivision where id = Material_Variance_Subdivision_dtl.material_var_subdiv_id) mat_var_subdiv_name,
        //                                (select specification from Material_Variance_Subdivision where id = Material_Variance_Subdivision_dtl.material_var_subdiv_id) specification,revit_category,family_type,property,design_rule_factor,keywords,quantity_extraction_formula from Material_Variance_Subdivision_dtl where material_var_subdiv_id in
        //                                            (SELECT id FROM Material_Variance_Subdivision where name = N'" + @mat_var_sub + @"' and material_variance_id in  (
        //                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
        //                                                    select id from Material where name=N'" + mat_name + @"')))", conn);
        //                    sda = new SqlDataAdapter(cmd);
        //                    dt = new DataTable("dtl");
        //                    sda.Fill(dt);
        //                    if (dt.Rows.Count > 0)
        //                    {
        //                        Mat_var_subdiv_dtl1.mat_var_subdiv_name = Convert.ToString(dt.Rows[0]["mat_var_subdiv_name"]);
        //                        Mat_var_subdiv_dtl1.specification = Convert.ToString(dt.Rows[0]["specification"]);
        //                        Mat_var_subdiv_dtl1.revit_category = Convert.ToString(dt.Rows[0]["revit_category"]);
        //                        Mat_var_subdiv_dtl1.family_type = Convert.ToString(dt.Rows[0]["family_type"]);
        //                        Mat_var_subdiv_dtl1.property = Convert.ToString(dt.Rows[0]["property"]);
        //                        Mat_var_subdiv_dtl1.design_rule_factor = Convert.ToString(dt.Rows[0]["design_rule_factor"]);
        //                        Mat_var_subdiv_dtl1.keywords = Convert.ToString(dt.Rows[0]["keywords"]);
        //                        Mat_var_subdiv_dtl1.quantity_extraction_formula = Convert.ToString(dt.Rows[0]["quantity_extraction_formula"]);
        //                    }

        //                }
        //                return Mat_var_subdiv_dtl1;
        //            }
        //            catch (System.Exception ex)
        //            {


        //                if (state == ConnectionState.Open)
        //                {
        //                    conn.Close();

        //                }
        //                return null;
        //                Service17 exception1 = new Service17();
        //                exception1.SendErrorToText(ex);

        //            }

        //        }

        public Mat_var_subdiv_dtl view_mat_var_subdiv_dtl(String mat_name, String mat_var_name, string mat_var_sub, string proj_guid, string proj_name, string city_name, string country_name)
        {
            int count = 0;
            SqlConnection conn;
            try
            {
                SqlCommand cmd;
                SqlDataAdapter sda;
                DataTable dt;
                Mat_var_subdiv_dtl Mat_var_subdiv_dtl1 = new Mat_var_subdiv_dtl();

                if (mat_var_sub.Contains("'"))
                {
                    mat_var_sub = mat_var_sub.Replace("'", "''");
                }

                using (conn = new SqlConnection(connection_string))
                {
                    conn.Open();
                    string query = @"SELECT COUNT(*) FROM Material_Variance_Subdivision_Project where material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and project_id = (select id from Project where proj_guid =N'" + proj_guid + "' and name= N'" + proj_name + "') and name = N'" + mat_var_sub + @"';";
                    using (SqlCommand cmdGetProjectCount = new SqlCommand(query, conn))
                    //                    SqlCommand cmdGetProjectCount = new SqlCommand(@"SELECT COUNT(*) FROM Material_Variance_Subdivision_Project where material_variance_id in  (
                    //                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                    //                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and project_id = (select id from Project where proj_guid =N'" + proj_guid + "' and name= N'" + proj_name + "') and name = N'" + mat_var_sub + @"';", conn);
                    {
                        count = (int)cmdGetProjectCount.ExecuteScalar();
                    }


                    if (count == 1)
                    {
                        cmd = new SqlCommand(@"select (select name from Material_Variance_Subdivision_Project where f_del = 0 and Project_id in (select id from project where proj_guid = N'" + proj_guid + "' and name = N'" + proj_name + @"') and id = Material_Variance_Subdivision_dtl_Project.material_var_subdiv_proj_id) mat_var_subdiv_name,
(select specification from Material_Variance_Subdivision_Project where f_del = 0 and Project_id in (select id from project where proj_guid = N'" + proj_guid + "' and name = N'" + proj_name + @"') and id = Material_Variance_Subdivision_dtl_Project.material_var_subdiv_proj_id) specification,
revit_category,family_type,property,design_rule_factor,keywords,quantity_extraction_formula,f_all_elements,element_filter from Material_Variance_Subdivision_dtl_Project where material_var_subdiv_proj_id in
(SELECT id FROM Material_Variance_Subdivision_Project where name = N'" + @mat_var_sub + @"' and material_variance_id in  (
select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
select id from Material where name=N'" + mat_name + @"')))", conn);
                        sda = new SqlDataAdapter(cmd);
                        dt = new DataTable("dtl");
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            Mat_var_subdiv_dtl1.mat_var_subdiv_name = Convert.ToString(dt.Rows[0]["mat_var_subdiv_name"]);
                            Mat_var_subdiv_dtl1.specification = Convert.ToString(dt.Rows[0]["specification"]);
                            Mat_var_subdiv_dtl1.revit_category = Convert.ToString(dt.Rows[0]["revit_category"]);
                            Mat_var_subdiv_dtl1.family_type = Convert.ToString(dt.Rows[0]["family_type"]);
                            Mat_var_subdiv_dtl1.property = Convert.ToString(dt.Rows[0]["property"]);
                            Mat_var_subdiv_dtl1.design_rule_factor = Convert.ToString(dt.Rows[0]["design_rule_factor"]);
                            Mat_var_subdiv_dtl1.keywords = Convert.ToString(dt.Rows[0]["keywords"]);
                            Mat_var_subdiv_dtl1.quantity_extraction_formula = Convert.ToString(dt.Rows[0]["quantity_extraction_formula"]);
                            Mat_var_subdiv_dtl1.f_all_elements = Convert.ToInt16(dt.Rows[0]["f_all_elements"]);
                            Mat_var_subdiv_dtl1.element_filter = Convert.ToString(dt.Rows[0]["element_filter"]);
                        }
                    }
                    else
                    {
                        query = @"SELECT COUNT(*) FROM Material_Variance_Subdivision where material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and name = N'" + mat_var_sub + @"' and city_id = (select id from City where name = N'" + city_name + @"' and country_id = (select id from Country_Code where 
													country = N'" + country_name + @"'));";

                        cmd = new SqlCommand(query, conn);

                        count = (int)cmd.ExecuteScalar();
                        if (count == 1)
                        {
                            query = @"SELECT COUNT(*) FROM Material_Variance_Subdivision_dtl_Project where material_var_subdiv_id in  (select id from Material_Variance_Subdivision where name =N'" + mat_var_sub + @"' and f_del = 0 and city_id = (select id from City where name = N'" + city_name + @"' and country_id = (select id from Country_Code where 
													country = N'" + country_name + @"')) and material_variance_id in (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"'))) and project_id = 
													(select id from Project where proj_guid =N'" + proj_guid + @"' and name= N'" + proj_name + @"')";
                            cmd = new SqlCommand(query, conn);

                            count = (int)cmd.ExecuteScalar();
                            if (count == 1)
                            {

                                cmd = new SqlCommand(@"select (select name from Material_Variance_Subdivision where f_del = 0 and city_id = (select id from City where name = N'" + city_name + @"' and country_id = (select id from Country_Code where 
													country = N'" + country_name + @"')) and id = Material_Variance_Subdivision_dtl_Project.material_var_subdiv_id) mat_var_subdiv_name,
(select specification from Material_Variance_Subdivision_Project where f_del = 0 and Project_id in (select id from project where 
proj_guid = N'" + proj_guid + @"' and name = N'" + proj_name + @"') and 
id = Material_Variance_Subdivision_dtl_Project.material_var_subdiv_proj_id) specification,
revit_category,family_type,property,design_rule_factor,keywords,quantity_extraction_formula,f_all_elements,element_filter from Material_Variance_Subdivision_dtl_Project where Project_id in (select id from project where 
proj_guid = N'" + proj_guid + @"' and name = N'" + proj_name + @"') and material_var_subdiv_id in
(SELECT id FROM Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and city_id = (select id from City where name = N'" + city_name + @"' and country_id = (select id from Country_Code where 
													country = N'" + country_name + @"')) and material_variance_id in  (
select id from Material_Variance where variance= N'" + mat_var_name + @"' and material_id in (
select id from Material where name=N'" + mat_name + @"')))", conn);
                                sda = new SqlDataAdapter(cmd);
                                dt = new DataTable("dtl");
                                sda.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    Mat_var_subdiv_dtl1.mat_var_subdiv_name = Convert.ToString(dt.Rows[0]["mat_var_subdiv_name"]);
                                    Mat_var_subdiv_dtl1.specification = Convert.ToString(dt.Rows[0]["specification"]);
                                    Mat_var_subdiv_dtl1.revit_category = Convert.ToString(dt.Rows[0]["revit_category"]);
                                    Mat_var_subdiv_dtl1.family_type = Convert.ToString(dt.Rows[0]["family_type"]);
                                    Mat_var_subdiv_dtl1.property = Convert.ToString(dt.Rows[0]["property"]);
                                    Mat_var_subdiv_dtl1.design_rule_factor = Convert.ToString(dt.Rows[0]["design_rule_factor"]);
                                    Mat_var_subdiv_dtl1.keywords = Convert.ToString(dt.Rows[0]["keywords"]);
                                    Mat_var_subdiv_dtl1.quantity_extraction_formula = Convert.ToString(dt.Rows[0]["quantity_extraction_formula"]);
                                    Mat_var_subdiv_dtl1.f_all_elements = Convert.ToInt16(dt.Rows[0]["f_all_elements"]);
                                    Mat_var_subdiv_dtl1.element_filter = Convert.ToString(dt.Rows[0]["element_filter"]);
                                }
                            }
                            else
                            {

                                cmd = new SqlCommand(@"select (select name from Material_Variance_Subdivision where id = Material_Variance_Subdivision_dtl.material_var_subdiv_id) mat_var_subdiv_name,
                                (select specification from Material_Variance_Subdivision where id = Material_Variance_Subdivision_dtl.material_var_subdiv_id) specification,revit_category,family_type,property,design_rule_factor,keywords,quantity_extraction_formula,f_all_elements,element_filter from Material_Variance_Subdivision_dtl where material_var_subdiv_id in
                                            (SELECT id FROM Material_Variance_Subdivision where name = N'" + @mat_var_sub + @"' and city_id = (select id from City where name = N'" + city_name + @"' and country_id = (select id from Country_Code where 
													country = N'" + country_name + @"')) and material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')))", conn);
                                sda = new SqlDataAdapter(cmd);
                                dt = new DataTable("dtl");
                                sda.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    Mat_var_subdiv_dtl1.mat_var_subdiv_name = Convert.ToString(dt.Rows[0]["mat_var_subdiv_name"]);
                                    Mat_var_subdiv_dtl1.specification = Convert.ToString(dt.Rows[0]["specification"]);
                                    Mat_var_subdiv_dtl1.revit_category = Convert.ToString(dt.Rows[0]["revit_category"]);
                                    Mat_var_subdiv_dtl1.family_type = Convert.ToString(dt.Rows[0]["family_type"]);
                                    Mat_var_subdiv_dtl1.property = Convert.ToString(dt.Rows[0]["property"]);
                                    Mat_var_subdiv_dtl1.design_rule_factor = Convert.ToString(dt.Rows[0]["design_rule_factor"]);
                                    Mat_var_subdiv_dtl1.keywords = Convert.ToString(dt.Rows[0]["keywords"]);
                                    Mat_var_subdiv_dtl1.quantity_extraction_formula = Convert.ToString(dt.Rows[0]["quantity_extraction_formula"]);
                                    Mat_var_subdiv_dtl1.f_all_elements = Convert.ToInt16(dt.Rows[0]["f_all_elements"]);
                                    Mat_var_subdiv_dtl1.element_filter = Convert.ToString(dt.Rows[0]["element_filter"]);
                                }
                            }
                        }
                        else
                        {

                            cmd = new SqlCommand(@"select (select name from Material_Variance_Subdivision where id = Material_Variance_Subdivision_dtl.material_var_subdiv_id) mat_var_subdiv_name,
                                (select specification from Material_Variance_Subdivision where id = Material_Variance_Subdivision_dtl.material_var_subdiv_id) specification,revit_category,family_type,property,design_rule_factor,keywords,quantity_extraction_formula,f_all_elements,element_filter from Material_Variance_Subdivision_dtl where material_var_subdiv_id in
                                            (SELECT id FROM Material_Variance_Subdivision where name = N'" + @mat_var_sub + @"' and city_id = (select id from City where name = N'" + city_name + @"' and country_id = (select id from Country_Code where 
													country = N'" + country_name + @"')) and material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')))", conn);
                            sda = new SqlDataAdapter(cmd);
                            dt = new DataTable("dtl");
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                Mat_var_subdiv_dtl1.mat_var_subdiv_name = Convert.ToString(dt.Rows[0]["mat_var_subdiv_name"]);
                                Mat_var_subdiv_dtl1.specification = Convert.ToString(dt.Rows[0]["specification"]);
                                Mat_var_subdiv_dtl1.revit_category = Convert.ToString(dt.Rows[0]["revit_category"]);
                                Mat_var_subdiv_dtl1.family_type = Convert.ToString(dt.Rows[0]["family_type"]);
                                Mat_var_subdiv_dtl1.property = Convert.ToString(dt.Rows[0]["property"]);
                                Mat_var_subdiv_dtl1.design_rule_factor = Convert.ToString(dt.Rows[0]["design_rule_factor"]);
                                Mat_var_subdiv_dtl1.keywords = Convert.ToString(dt.Rows[0]["keywords"]);
                                Mat_var_subdiv_dtl1.quantity_extraction_formula = Convert.ToString(dt.Rows[0]["quantity_extraction_formula"]);
                                Mat_var_subdiv_dtl1.f_all_elements = Convert.ToInt16(dt.Rows[0]["f_all_elements"]);
                                Mat_var_subdiv_dtl1.element_filter = Convert.ToString(dt.Rows[0]["element_filter"]);
                            }
                        }
                    }
                }

                return Mat_var_subdiv_dtl1;
            }
            catch (System.Exception ex)
            {
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
                return null;
            }

        }

        //        public string upd_mat_var_subdiv_dtl(String mat_name, String mat_var_name, string mat_var_sub, [Optional]string revit_category, [Optional]string family_type, [Optional]string property, [Optional]string design_rule_factor, [Optional]string keywords, [Optional]string quantity_extraction_formula)
        //        {
        //            SqlConnection conn = new SqlConnection(connection_string);
        //            ConnectionState state = conn.State;
        //            try
        //            {
        //                conn.Open();

        //                SqlCommand cmd = new SqlCommand((@"update Material_Variance_Subdivision_dtl
        //                            set revit_category=N'" + revit_category + "',family_type=N'" + family_type + "',property=N'" + property + "',design_rule_factor=N'" + design_rule_factor + "',keywords=N'" + keywords + "',quantity_extraction_formula=N'" + quantity_extraction_formula + @"' 
        //                            where material_var_subdiv_id in (SELECT id FROM Material_Variance_Subdivision where name = N'" + @mat_var_sub + @"' and material_variance_id in  (
        //                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
        //                                                    select id from Material where name=N'" + mat_name + @"')))"), conn);
        //                cmd.ExecuteNonQuery();

        //                return "Success";
        //            }
        //            catch (System.Exception ex)
        //            {


        //                if (state == ConnectionState.Open)
        //                {
        //                    conn.Close();

        //                }
        //                return ex.Message;
        //                Service17 exception1 = new Service17();
        //                exception1.SendErrorToText(ex);

        //            }
        //        }

        public string ins_mat_subdiv_proj(String mat_name, String mat_var_name, string mat_var_sub, string user_info, string unit_of_measurement, [Optional] Int64? prev_seq, [Optional]string revit_category, [Optional]string family_type, [Optional]string property, [Optional]string design_rule_factor, [Optional]string keywords, [Optional]string quantity_extraction_formula, [Optional]string specification, string proj_id, string proj_name, int f_all_elements, [Optional]string element_filter, string sortingCondtions)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                if (mat_var_sub.Contains("'"))
                {
                    mat_var_sub = mat_var_sub.Replace("'", "''");
                }
                conn.Open();
                int check;
                SqlCommand cmd1;
                SqlCommand cmd_seq;
                DataTable dt;
                SqlDataAdapter sda;
                SqlCommand cmd_id;
                DataTable dt_id;
                SqlDataAdapter sda_id;
                Int64? seq;
                Int64? id;


                SqlCommand cmd_cityid = new SqlCommand("select city_id from Project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"';", conn);
                long city_id = (long)cmd_cityid.ExecuteScalar();


                string queryCheckInMaster = "Select isnull((select f_del from Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and city_id = " + city_id + @" and material_variance_id in (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"'))),2)";
                using (SqlCommand command = new SqlCommand(queryCheckInMaster, conn))
                {
                    check = (int)command.ExecuteScalar();
                }
                if (check == 2)
                {
                    if (prev_seq == null)
                    {
                        cmd1 = new SqlCommand(@"SELECT coalesce(max(seq)+1,1) seq FROM Material_Variance_Subdivision_Project where material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and  project_id = (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"'); ", conn);
                        sda = new SqlDataAdapter(cmd1);
                        dt = new DataTable("mat");
                        sda.Fill(dt);
                        seq = Convert.ToInt64(dt.Rows[0]["seq"]);
                    }
                    else
                    {
                        seq = prev_seq + 1;
                        cmd_seq = new SqlCommand((@"update Material_Variance_Subdivision_Project 
                        set seq = seq + 1 where material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and  project_id = (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"')  and seq > " + prev_seq), conn);
                        cmd_seq.ExecuteNonQuery();
                    }
                    string query_cnt = "Select isnull((select f_del from Material_Variance_Subdivision_Project where name = N'" + mat_var_sub + @"' and material_variance_id in (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and project_id = (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"')),2)";
                    using (SqlCommand command = new SqlCommand(query_cnt, conn))
                    {
                        check = (int)command.ExecuteScalar();
                    }
                    if (check == 1) //if deleted previously activate it
                    {
                        SqlCommand cmd_upd = new SqlCommand(@"update Material_Variance_Subdivision_Project set f_del = 0,seq = " + seq + " where name = N'" + mat_var_sub + @"' and material_variance_id in (
                                                    select id from Material_Variance where variance = N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name = N'" + mat_name + @"') and project_id = (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"'))", conn);
                        cmd_upd.ExecuteNonQuery();
                        conn.Close();
                        return "Success";
                    }
                    else
                    {
                        if (check == 2)
                        {
                            SqlCommand cmd = new SqlCommand((@"INSERT INTO material_variance_subdivision_Project
                        (material_variance_id, 
                         NAME, 
                         created_by, 
                         created_on, 
                         unit_of_measurement,
                         f_del,
                         seq,
                         specification,project_id) 
            VALUES      ((SELECT id 
                          FROM   material_variance 
                          WHERE  variance = '" + @mat_var_name + @"' 
                                 AND material_id = (SELECT id 
                                                    FROM   material 
                                                    WHERE  NAME = '" + mat_name + @"')), 
                         N'" + @mat_var_sub + @"', 
                         N'" + @user_info + @"', 
                         CURRENT_TIMESTAMP, 
                         (SELECT ID FROM UNIT_OF_MEASUREMENT WHERE UNIT = N'" + @unit_of_measurement + @"'),0," + seq + ",N'" +
                             @specification + "', (select id from Project where proj_guid =N'" + proj_id + "' and name = N'" + proj_name + @"'))"), conn);


                            cmd.ExecuteNonQuery();

                     if (revit_category != null || family_type != null || property != null || design_rule_factor != null || keywords != null || quantity_extraction_formula != null || sortingCondtions!= null)
                     {
                         cmd_id = new SqlCommand(@"SELECT id FROM Material_Variance_Subdivision_Project where name = N'" + @mat_var_sub + @"' and material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and project_id = (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"') ; ", conn);
                         sda_id = new SqlDataAdapter(cmd_id);
                         dt_id = new DataTable("mat");
                         sda_id.Fill(dt_id);
                         id = Convert.ToInt64(dt_id.Rows[0]["id"]);
//                                SqlCommand cmd11 = new SqlCommand((@"INSERT INTO Material_Variance_Subdivision_dtl_Project 
//                        (material_var_subdiv_proj_id, 
//                         revit_category, 
//                         family_type, 
//                         property, 
//                         design_rule_factor,
//                         keywords,
//                         quantity_extraction_formula,project_id,updated_by,f_all_elements,element_filter) 
//            VALUES    (" + id + ",N'" + revit_category + "',N'" + family_type + "',N'" + property + "',N'" + design_rule_factor + "',N'" + keywords + "',N'" + quantity_extraction_formula + "',(select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"'),N'" + user_info + "'," + f_all_elements + ",N'" + element_filter + "')"), conn);

                             SqlCommand cmd11 = new SqlCommand(@"INSERT INTO Material_Variance_Subdivision_dtl_Project 
                    (material_var_subdiv_proj_id, 
                     revit_category, 
                     family_type, 
                     property, 
                     design_rule_factor,
                     keywords,
                     quantity_extraction_formula,
                     project_id,
                     updated_by,
                     f_all_elements,
                     element_filter,
                     Sorting_Logic) 
                VALUES (" + id + ",N'" + revit_category + "',N'" + family_type + "',N'" + property + "',N'" + design_rule_factor + "',N'" + keywords + "',N'" + quantity_extraction_formula + "',(select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "'),N'" + user_info + "'," + f_all_elements + ",N'" + element_filter + "',N'" + sortingCondtions + "')",conn);
                         cmd11.ExecuteNonQuery();
                            }
                            conn.Close();
                            return "Success";
                        }

                        else
                        {
                            conn.Close();
                            throw new Exception("The material variance sub already exists");

                        }
                    }
                }
                else
                {
                    conn.Close();
                    throw new Exception("The material variance sub already exists");
                }

            }
            catch (System.Exception ex)
            {
                if (state == ConnectionState.Open)
                {
                    conn.Close();

                }
                return ex.Message;
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);

            }

        }

        //        public string upd_mat_var_subdiv_dtl_Project(String mat_name, String mat_var_name, string mat_var_sub, [Optional]string unit_of_measurement, [Optional] Int64? prev_seq, [Optional]string user_info, [Optional]string revit_category, [Optional]string family_type, [Optional]string property, [Optional]string design_rule_factor, [Optional]string keywords, [Optional]string quantity_extraction_formula, [Optional]string specification, string proj_guid, string proj_name, int f_all_elements, [Optional]string element_filter)
        //        {
        //            int count = 0;
        //            Int64 unit = 0;
        //            Int64? current_seq;
        //            SqlConnection conn = new SqlConnection(connection_string);
        //            ConnectionState state = conn.State;
        //            try
        //            {
        //                conn.Open();

        //                SqlCommand cmd_cityid = new SqlCommand("select city_id from Project where proj_guid = N'" + proj_guid + "' and name = N'" + proj_name + @"';", conn);
        //                long city_id = (long)cmd_cityid.ExecuteScalar();

        //                string query = @"SELECT COUNT(*) FROM Material_Variance_Subdivision_Project where material_variance_id in  (
        //                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
        //                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and project_id = (select id from Project where proj_guid =N'" + proj_guid + "' and name= N'" + proj_name + "') and name = N'" + mat_var_sub + @"';";
        //                SqlCommand cmdIsProject = new SqlCommand(query, conn);
        //                count = (int)cmdIsProject.ExecuteScalar();
        //                #region Project Specific
        //                if (count == 1)//in project
        //                {
        //                    SqlCommand cmd = new SqlCommand((@"update Material_Variance_Subdivision_dtl_Project
        //                            set revit_category=N'" + revit_category + "',family_type=N'" + family_type + "',property=N'" + property + "',design_rule_factor=N'" + design_rule_factor + "',keywords=N'" + keywords + "',quantity_extraction_formula=N'" + quantity_extraction_formula + "',f_all_elements="+f_all_elements+",element_filter=N'"+element_filter+"',updated_by=N'"+user_info+@"' 
        //                            where Project_id in (select id from project where proj_guid = N'" + proj_guid + "' and name = N'" + proj_name + @"') and material_var_subdiv_proj_id in (SELECT id FROM Material_Variance_Subdivision_Project where name = N'" + @mat_var_sub + @"' and material_variance_id in  (
        //                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
        //                                                    select id from Material where name=N'" + mat_name + @"')))"), conn);
        //                    cmd.ExecuteNonQuery();

        //                    if (!string.IsNullOrEmpty(unit_of_measurement))
        //                    {
        //                        //cmd = new SqlCommand((@"select unit_of_measurement from Material_Variance_Subdivision_Project where material_variance_id = (select id from Material_Variance where variance = N'" + mat_var_name + @"') and name = N'" + mat_var_sub + @"' and project_id = (select id from Project where proj_guid =N'" + proj_guid + "' and name= N'" + proj_name + "')"), conn);

        //                        cmd = new SqlCommand((@"select id from Unit_Of_Measurement where unit = N'" + unit_of_measurement + @"'"), conn);
        //                        unit = (Int64)cmd.ExecuteScalar();
        //                        cmd = new SqlCommand((@"update Material_Variance_Subdivision_Project set unit_of_measurement = " + unit + @",specification=N'" + specification + @"',created_by = N'" + user_info + @"' where material_variance_id in  (
        //                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
        //                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and project_id =
        //													 (select id from Project where proj_guid =N'" + proj_guid + @"' and name= N'" + proj_name + @"') 
        //													 and name = N'" + mat_var_sub + @"';"), conn);
        //                        cmd.ExecuteNonQuery();

        //                    }
        //                    else
        //                    {
        //                        cmd = new SqlCommand((@"select unit_of_measurement from Material_Variance_Subdivision_Project where material_variance_id = (select id from Material_Variance where variance = N'" + mat_var_name + @"') and name = N'" + mat_var_sub + @"' and project_id = (select id from Project where proj_guid =N'" + proj_guid + "' and name= N'" + proj_name + "')"), conn);
        //                        unit = (Int64)cmd.ExecuteScalar();
        //                        cmd = new SqlCommand((@"update Material_Variance_Subdivision_Project set unit_of_measurement = " + unit + @",specification=N'" + specification + @"',created_by = N'" + user_info + @"' where material_variance_id in  (
        //                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
        //                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and project_id =
        //													 (select id from Project where proj_guid =N'" + proj_guid + @"' and name= N'" + proj_name + @"') 
        //													 and name = N'" + mat_var_sub + @"';"), conn);
        //                        cmd.ExecuteNonQuery();

        //                    }

        //                    conn.Close();

        //                    return "Success";
        //                } 
        //                #endregion
        //                else
        //                {
        //                    query = @"SELECT COUNT(*) FROM Material_Variance_Subdivision where material_variance_id in  (
        //                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
        //                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and name = N'" + mat_var_sub + @"' and city_id = " +city_id+ ";";
        //                    SqlCommand cmdIsMaster = new SqlCommand(query, conn);
        //                    count = (int)cmdIsMaster.ExecuteScalar();

        //                    if (count == 1)
        //                    {

        //                        if (!string.IsNullOrEmpty(unit_of_measurement))
        //                        {
        //                            cmdIsMaster = new SqlCommand((@"select id from Unit_Of_Measurement where unit = N'" + unit_of_measurement + @"'"), conn);
        //                            unit = (Int64)cmdIsMaster.ExecuteScalar();

        //                        }
        //                        else
        //                        {
        //                            cmdIsMaster = new SqlCommand((@"select unit_of_measurement from Material_Variance_Subdivision_Project where material_variance_id = (select id from Material_Variance where variance = N'" + mat_var_name + @"') and name = N'" + mat_var_sub + @"' and project_id = (select id from Project where proj_guid =N'" + proj_guid + "' and name= N'" + proj_name + "')"), conn);
        //                            unit = (Int64)cmdIsMaster.ExecuteScalar();
        //                        }


        //                        //29thJan 24
        ////                        query = @"select seq from Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and city_id = "+city_id+@" and material_variance_id =(select id from Material_Variance 
        ////                                  where variance = N'" + mat_var_name + @"' and
        ////                                  material_id = (select id from Material where name = N'" + mat_name + @"'))";

        ////                        cmdIsMaster = new SqlCommand(query, conn);
        ////                        seq = (int)cmdIsMaster.ExecuteScalar();


        //                        //Check in Material_Variance_Subdivision_Project first then update 

        //                        using (SqlConnection con = new SqlConnection(connection_string))
        //                        {
        //                            con.Open();
        //                            using (SqlCommand cmdGetProjectCount = new SqlCommand(@"SELECT COUNT(*) FROM Material_Variance_Subdivision_dtl_Project
        //where material_var_subdiv_id in ( select id from Material_Variance_Subdivision where material_variance_id in
        //(select id from Material_Variance where variance=N'" + mat_var_name + @"' 
        //and material_id in (select id from Material where name=N'" + mat_name + @"')) and name = N'" + mat_var_sub + @"') 
        //and project_id = (select id from Project where 
        //proj_guid =N'" + proj_guid + @"' and 
        //name= N'" + proj_name + @"');", con))
        //                            {
        //                                alreadyexistsinproject = (int)cmdGetProjectCount.ExecuteScalar();
        //                            }


        //                            //if (alreadyexistsinproject == 0)
        //                            //{
        //                                int countProject = 0;
        //                                using (SqlCommand cmdGetProjectCount = new SqlCommand("(select COUNT(id) from Project where proj_guid =N'" + proj_guid + @"' and name= N'" + proj_name + @"'and created_on >= 
        // (select created_on from Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and city_id = " + city_id + @" and material_variance_id = 
        // (select id from Material_Variance where variance = N'" + mat_var_name + @"' and material_id = (select id from Material where name = N'" + mat_name + @"'))))", con))
        //                                {
        //                                    countProject = (int)cmdGetProjectCount.ExecuteScalar();
        //                                }
        //                                if (countProject == 1)
        //                                {
        //                                    SqlCommand cmd_id = new SqlCommand(@"SELECT id FROM Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and material_variance_id in  (
        //                                                    select id from Material_Variance where variance= N'" + mat_var_name + @"' and material_id in (
        //                                                    select id from Material where name= N'" + mat_name + @"')) and f_del = 0 and city_id = " + city_id + "; ", con);
        //                                    SqlDataAdapter sda_id = new SqlDataAdapter(cmd_id);
        //                                    DataTable dt_id = new DataTable("mat");
        //                                    sda_id.Fill(dt_id);
        //                                    id = Convert.ToInt64(dt_id.Rows[0]["id"]);

        //                                    if (!string.IsNullOrEmpty(revit_category) || !string.IsNullOrEmpty(family_type) || !string.IsNullOrEmpty(property) || !string.IsNullOrEmpty(design_rule_factor) || !string.IsNullOrEmpty(keywords) || !string.IsNullOrEmpty(quantity_extraction_formula))
        //                                    {
        //                                        if (alreadyexistsinproject == 1)
        //                                        {
        //                                            SqlCommand cmd = new SqlCommand((@"update Material_Variance_Subdivision_dtl_Project
        //                            set revit_category=N'" + revit_category + "',family_type=N'" + family_type + "',property=N'" + property + "',design_rule_factor=N'" + design_rule_factor + "',keywords=N'" + keywords + "',quantity_extraction_formula=N'" + quantity_extraction_formula + "',f_all_elements=" + f_all_elements + ",element_filter=N'" + element_filter + "',updated_by=N'" + user_info + @"' 
        //                            where Project_id in (select id from project where proj_guid = N'" + proj_guid + "' and name = N'" + proj_name + @"') and material_var_subdiv_id in (SELECT id FROM Material_Variance_Subdivision where name = N'" + @mat_var_sub + @"' and material_variance_id in  (
        //                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
        //                                                    select id from Material where name=N'" + mat_name + @"')))"), conn);
        //                                            cmd.ExecuteNonQuery();
        //                                        }
        //                                        else if(alreadyexistsinproject == 0)
        //                                        {

        //                                            SqlCommand cmd11 = new SqlCommand((@"INSERT INTO Material_Variance_Subdivision_dtl_Project 
        //                        (material_var_subdiv_id, 
        //                         revit_category, 
        //                         family_type, 
        //                         property, 
        //                         design_rule_factor,
        //                         keywords,
        //                         quantity_extraction_formula,project_id,updated_by,f_all_elements,element_filter) 
        //            VALUES    (" + id + ",N'" + revit_category + "',N'" + family_type + "',N'" + property + "',N'" + design_rule_factor + "',N'" + keywords + "',N'" + quantity_extraction_formula + "',(select id from Project where proj_guid =N'" + proj_guid + @"' and name= N'" + proj_name + @"'),N'" + user_info + "'," + f_all_elements + ",N'" + element_filter + "' )"), con);
        //                                            cmd11.ExecuteNonQuery();
        //                                        }
        //                                    }
        //                                }
        //                            //}

        //                        }


        //                        #region 29thJAN24
        //                        //                                    using (SqlConnection con = new SqlConnection(connection_string))
        ////                                    {
        ////                                        con.Open();
        ////                                        using (SqlCommand cmdGetProjectCount = new SqlCommand(@"SELECT COUNT(*) FROM Material_Variance_Subdivision_Project where material_variance_id in  (
        ////                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
        ////                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and project_id = (select id from Project where proj_guid =N'" + proj_guid + @"' and name= N'" + proj_name + @"') and name = N'" + mat_var_sub + @"';", con))
        ////                                        {
        ////                                            alreadyexistsinproject = (int)cmdGetProjectCount.ExecuteScalar();
        ////                                        }

        ////                                        if (alreadyexistsinproject == 0) //AND DELETED 
        ////                                        {

        ////                                            // check if it exist in project dtl and tehn update the formula
        ////                                            using (SqlCommand cmdGetProjectCount = new SqlCommand(@"SELECT COUNT(*) FROM Material_Variance_Subdivision_Project where material_variance_id in  (
        ////                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
        ////                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 1 and project_id = (select id from Project where proj_guid =N'" + proj_guid + @"' and name= N'" + proj_name + @"') and name = N'" + mat_var_sub + @"';", con))
        ////                                            {
        ////                                                // if deleted add in project dtl and tehn update the formula
        ////                                                alreadydeletedProj = (int)cmdGetProjectCount.ExecuteScalar();
        ////                                            }
        ////                                        }
        ////                                        if (alreadyexistsinproject == 0 && alreadydeletedProj == 0)
        ////                                        {
        ////                                            int countProject = 0;
        ////                                            using (SqlCommand cmdGetProjectCount = new SqlCommand("(select COUNT(id) from Project where proj_guid =N'" + proj_guid + @"' and name= N'" + proj_name + @"'and created_on >= 
        //// (select created_on from Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and city_id = "+city_id +@" and material_variance_id = 
        //// (select id from Material_Variance where variance = N'" + mat_var_name + @"' and material_id = (select id from Material where name = N'" + mat_name + @"'))))", con))
        ////                                            {
        ////                                                countProject = (int)cmdGetProjectCount.ExecuteScalar();
        ////                                            }
        ////                                            if (countProject == 1)
        ////                                            {
        ////                                                SqlCommand cmd_id = new SqlCommand(@"SELECT id FROM Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and material_variance_id in  (
        ////                                                    select id from Material_Variance where variance= N'" + mat_var_name + @"' and material_id in (
        ////                                                    select id from Material where name= N'" + mat_name + @"')) and f_del = 0 and city_id = "+city_id+"; ", con);
        ////                                                SqlDataAdapter sda_id = new SqlDataAdapter(cmd_id);
        ////                                                DataTable dt_id = new DataTable("mat");
        ////                                                sda_id.Fill(dt_id);
        ////                                                id = Convert.ToInt64(dt_id.Rows[0]["id"]);

        ////                                                if (!string.IsNullOrEmpty(revit_category) || !string.IsNullOrEmpty(family_type) || !string.IsNullOrEmpty(property) || !string.IsNullOrEmpty(design_rule_factor) || !string.IsNullOrEmpty(keywords) || !string.IsNullOrEmpty(quantity_extraction_formula))
        ////                                                {
        ////                                                    SqlCommand cmd11 = new SqlCommand((@"INSERT INTO Material_Variance_Subdivision_dtl_Project 
        ////                        (material_var_subdiv_id, 
        ////                         revit_category, 
        ////                         family_type, 
        ////                         property, 
        ////                         design_rule_factor,
        ////                         keywords,
        ////                         quantity_extraction_formula,project_id,updated_by,f_all_elements,element_filter) 
        ////            VALUES    (" + id + ",N'" + revit_category + "',N'" + family_type + "',N'" + property + "',N'" + design_rule_factor + "',N'" + keywords + "',N'" + quantity_extraction_formula + "',(select id from Project where proj_guid =N'" + proj_guid + @"' and name= N'" + proj_name + @"'),N'"+user_info+"',"+f_all_elements+",N'"+element_filter+"' )"), con);
        ////                                                    cmd11.ExecuteNonQuery();
        ////                                                }
        ////                                            }
        ////                                        }

        //                        //                                    }

        //                        #endregion 

        //                        conn.Close();
        //                        return "Success";
        //                    }
        //                    else
        //                    {
        //                        conn.Close();
        //                        return "Failure";
        //                    }

        //                }

        //            }
        //            catch (System.Exception ex)
        //            {


        //                if (conn.State == ConnectionState.Open)
        //                {
        //                    conn.Close();
        //                }
        //                Service17 exception1 = new Service17();
        //                exception1.SendErrorToText(ex);

        //                return ex.Message;

        //            }
        //        }


        public string upd_mat_var_subdiv_dtl_Project(String mat_name, String mat_var_name, string mat_var_sub, [Optional]string unit_of_measurement, [Optional] Int64? prev_seq, [Optional]string user_info, [Optional]string revit_category, [Optional]string family_type, [Optional]string property, [Optional]string design_rule_factor, [Optional]string keywords, [Optional]string quantity_extraction_formula, [Optional]string specification, string proj_guid, string proj_name, int f_all_elements, [Optional]string element_filter, string sortingConditions)
        {
            int count = 0;
            Int64 unit = 0;
            Int64? current_seq;
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();

                SqlCommand cmd_cityid = new SqlCommand("select city_id from Project where proj_guid = N'" + proj_guid + "' and name = N'" + proj_name + @"';", conn);
                long city_id = (long)cmd_cityid.ExecuteScalar();

                string query = @"SELECT COUNT(*) FROM Material_Variance_Subdivision_Project where material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and project_id = (select id from Project where proj_guid =N'" + proj_guid + "' and name= N'" + proj_name + "') and name = N'" + mat_var_sub + @"';";
                SqlCommand cmdIsProject = new SqlCommand(query, conn);
                count = (int)cmdIsProject.ExecuteScalar();
                #region Project Specific
                if (count == 1)//in project
                {
                    SqlCommand cmd = new SqlCommand((@"update Material_Variance_Subdivision_dtl_Project
                            set revit_category=N'" + revit_category + "',family_type=N'" + family_type + "',property=N'" + property + "',design_rule_factor=N'" + design_rule_factor + "',keywords=N'" + keywords + "',quantity_extraction_formula=N'" + quantity_extraction_formula + "',f_all_elements=" + f_all_elements + ",element_filter=N'" + element_filter + "',updated_by=N'" + user_info + @"' 
                            where Project_id in (select id from project where proj_guid = N'" + proj_guid + "' and name = N'" + proj_name + @"') and material_var_subdiv_proj_id in (SELECT id FROM Material_Variance_Subdivision_Project where name = N'" + @mat_var_sub + @"' and material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')))"), conn);
                    cmd.ExecuteNonQuery();

                    //--------------------------------------------------------------------------------------------------------
                    if (!string.IsNullOrEmpty(sortingConditions))
                    {
                         SqlCommand cmd1 = new SqlCommand((@"update Material_Variance_Subdivision_dtl_Project set Sorting_Logic=N'"+sortingConditions+@"' 
                            where Project_id in (select id from project where proj_guid = N'" + proj_guid + "' and name = N'" + proj_name + @"') and material_var_subdiv_proj_id in (SELECT id FROM Material_Variance_Subdivision_Project where name = N'" + @mat_var_sub + @"' and material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')))"),conn);
                        cmd1.ExecuteNonQuery();
                    }
                    //--------------------------------------------------------------------------------------------------------


                    if (!string.IsNullOrEmpty(unit_of_measurement))
                    {
                        //cmd = new SqlCommand((@"select unit_of_measurement from Material_Variance_Subdivision_Project where material_variance_id = (select id from Material_Variance where variance = N'" + mat_var_name + @"') and name = N'" + mat_var_sub + @"' and project_id = (select id from Project where proj_guid =N'" + proj_guid + "' and name= N'" + proj_name + "')"), conn);

                        cmd = new SqlCommand((@"select id from Unit_Of_Measurement where unit = N'" + unit_of_measurement + @"'"), conn);
                        unit = (Int64)cmd.ExecuteScalar();
                        cmd = new SqlCommand((@"update Material_Variance_Subdivision_Project set unit_of_measurement = " + unit + @",specification=N'" + specification + @"',created_by = N'" + user_info + @"' where material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and project_id =
													 (select id from Project where proj_guid =N'" + proj_guid + @"' and name= N'" + proj_name + @"') 
													 and name = N'" + mat_var_sub + @"';"), conn);
                        cmd.ExecuteNonQuery();

                    }
                    else
                    {
                        cmd = new SqlCommand((@"select unit_of_measurement from Material_Variance_Subdivision_Project where material_variance_id = (select id from Material_Variance where variance = N'" + mat_var_name + @"') and name = N'" + mat_var_sub + @"' and project_id = (select id from Project where proj_guid =N'" + proj_guid + "' and name= N'" + proj_name + "')"), conn);
                        unit = (Int64)cmd.ExecuteScalar();
                        cmd = new SqlCommand((@"update Material_Variance_Subdivision_Project set unit_of_measurement = " + unit + @",specification=N'" + specification + @"',created_by = N'" + user_info + @"' where material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and project_id =
													 (select id from Project where proj_guid =N'" + proj_guid + @"' and name= N'" + proj_name + @"') 
													 and name = N'" + mat_var_sub + @"';"), conn);
                        cmd.ExecuteNonQuery();

                    }

                    conn.Close();

                    return "Success";
                }
                #endregion
                else
                {
                    query = @"SELECT COUNT(*) FROM Material_Variance_Subdivision where material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and name = N'" + mat_var_sub + @"' and city_id = " + city_id + ";";
                    SqlCommand cmdIsMaster = new SqlCommand(query, conn);
                    count = (int)cmdIsMaster.ExecuteScalar();

                    if (count == 1)
                    {
                        if (!string.IsNullOrEmpty(unit_of_measurement))
                        {
                            cmdIsMaster = new SqlCommand((@"select id from Unit_Of_Measurement where unit = N'" + unit_of_measurement + @"'"), conn);
                            unit = (Int64)cmdIsMaster.ExecuteScalar();

                        }
                        else
                        {
                            cmdIsMaster = new SqlCommand((@"select unit_of_measurement from Material_Variance_Subdivision_Project where material_variance_id = (select id from Material_Variance where variance = N'" + mat_var_name + @"') and name = N'" + mat_var_sub + @"' and project_id = (select id from Project where proj_guid =N'" + proj_guid + "' and name= N'" + proj_name + "')"), conn);
                            unit = (Int64)cmdIsMaster.ExecuteScalar();
                        }


                        //29thJan 24
                        //                        query = @"select seq from Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and city_id = "+city_id+@" and material_variance_id =(select id from Material_Variance 
                        //                                  where variance = N'" + mat_var_name + @"' and
                        //                                  material_id = (select id from Material where name = N'" + mat_name + @"'))";

                        //                        cmdIsMaster = new SqlCommand(query, conn);
                        //                        seq = (int)cmdIsMaster.ExecuteScalar();


                        //Check in Material_Variance_Subdivision_Project first then update 

                        using (SqlConnection con = new SqlConnection(connection_string))
                        {
                            con.Open();
                            using (SqlCommand cmdGetProjectCount = new SqlCommand(@"SELECT COUNT(*) FROM Material_Variance_Subdivision_dtl_Project
where material_var_subdiv_id in ( select id from Material_Variance_Subdivision where material_variance_id in
(select id from Material_Variance where variance=N'" + mat_var_name + @"' 
and material_id in (select id from Material where name=N'" + mat_name + @"')) and name = N'" + mat_var_sub + @"') 
and project_id = (select id from Project where 
proj_guid =N'" + proj_guid + @"' and 
name= N'" + proj_name + @"');", con))
                            {
                                alreadyexistsinproject = (int)cmdGetProjectCount.ExecuteScalar();
                            }


                           
                            //if (alreadyexistsinproject == 0)
                            //{
                            int countProject = 0;
                            using (SqlCommand cmdGetProjectCount = new SqlCommand("(select COUNT(id) from Project where proj_guid =N'" + proj_guid + @"' and name= N'" + proj_name + @"'and created_on >= 
 (select created_on from Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and city_id = " + city_id + @" and material_variance_id = 
 (select id from Material_Variance where variance = N'" + mat_var_name + @"' and material_id = (select id from Material where name = N'" + mat_name + @"'))))", con))
                            {
                                countProject = (int)cmdGetProjectCount.ExecuteScalar();
                            }
                            if (countProject == 1)
                            {
                                SqlCommand cmd_id = new SqlCommand(@"SELECT id FROM Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and material_variance_id in  (
                                                    select id from Material_Variance where variance= N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name= N'" + mat_name + @"')) and f_del = 0 and city_id = " + city_id + "; ", con);
                                SqlDataAdapter sda_id = new SqlDataAdapter(cmd_id);
                                DataTable dt_id = new DataTable("mat");
                                sda_id.Fill(dt_id);
                                id = Convert.ToInt64(dt_id.Rows[0]["id"]);

                                if (!string.IsNullOrEmpty(revit_category) || !string.IsNullOrEmpty(family_type) || !string.IsNullOrEmpty(property) || !string.IsNullOrEmpty(design_rule_factor) || !string.IsNullOrEmpty(keywords) || !string.IsNullOrEmpty(quantity_extraction_formula) || !String.IsNullOrEmpty(sortingConditions))
                                {
                                    if (alreadyexistsinproject == 1)
                                    {

                                        SqlCommand cmd = new SqlCommand((@"update Material_Variance_Subdivision_dtl_Project
                                        set revit_category=N'" + revit_category + "',family_type=N'" + family_type + "',property=N'" + property + "',design_rule_factor=N'" + design_rule_factor + "',keywords=N'" + keywords + "',quantity_extraction_formula=N'" + quantity_extraction_formula + "',f_all_elements=" + f_all_elements + ",element_filter=N'" + element_filter + "',updated_by=N'" + user_info + @"' 
                                        where Project_id in (select id from project where proj_guid = N'" + proj_guid + "' and name = N'" + proj_name + @"') and material_var_subdiv_id in (SELECT id FROM Material_Variance_Subdivision where name = N'" + @mat_var_sub + @"' and material_variance_id in  (
                                        select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (select id from Material where name=N'" + mat_name + @"')))"), conn);
                                        cmd.ExecuteNonQuery();


                                        // Update Sorting Logic separately if not null or empty
                                        //-------------------------------------------------------------------------------------------------------------------------------------------------
                                        if (!string.IsNullOrEmpty(sortingConditions))
                                        {
                                            string sortingQuery = "UPDATE Material_Variance_Subdivision_dtl_Project SET Sorting_Logic = '" + sortingConditions + "' WHERE Project_id IN (SELECT id FROM project WHERE proj_guid = '" + proj_guid + "' AND name = '" + proj_name + "') AND material_var_subdiv_id IN (SELECT id FROM Material_Variance_Subdivision WHERE name = '" + mat_var_sub + "' AND material_variance_id IN (SELECT id FROM Material_Variance WHERE variance = '" + mat_var_name + "' AND material_id IN (SELECT id FROM Material WHERE name = '" + mat_name + "')))";
                                            SqlCommand sortingCmd = new SqlCommand(sortingQuery, conn);
                                            sortingCmd.ExecuteNonQuery();
                                        }
                                        //----------------------------------------------------------------------------------------------------------------------------------------------------
                                      
                                    }
                                    else if (alreadyexistsinproject == 0)
                                    {
                            
//                                        SqlCommand cmd11 = new SqlCommand((@"INSERT INTO Material_Variance_Subdivision_dtl_Project (material_var_subdiv_id, revit_category, family_type, property, design_rule_factor,
//                                                                           keywords,quantity_extraction_formula,project_id,updated_by,f_all_elements,element_filter)
//                                        VALUES    (" + id + ",N'" + revit_category + "',N'" + family_type + "',N'" + property + "',N'" + design_rule_factor + "',N'" + keywords + "',N'" + quantity_extraction_formula + "',(select id from Project where proj_guid =N'" + proj_guid + @"' and name= N'" + proj_name + @"'),N'" + user_info + "'," + f_all_elements + ",N'" + element_filter + "' )"), con);
                                      
                                    
                                        //Above Code Committed and added SortingLogic to Query
                                        //----------------------------------------------------------------------------------------------------------------------------------------------------
                                        SqlCommand cmd11 = new SqlCommand("INSERT INTO Material_Variance_Subdivision_dtl_Project (material_var_subdiv_id, revit_category, family_type, property, design_rule_factor, keywords, quantity_extraction_formula, project_id, updated_by, f_all_elements, element_filter, Sorting_Logic) VALUES (N'"
                                        + id + "', N'" + revit_category + "', N'" + family_type + "', N'" + property + "', N'" + design_rule_factor + "', N'" + keywords + "', N'" + quantity_extraction_formula + "', (SELECT id FROM Project WHERE proj_guid = N'" + proj_guid + "' AND name = N'" + proj_name + "'), N'" + user_info + "', N'" + f_all_elements + "', N'" + element_filter + "', N'" + sortingConditions + "')", conn);

                                        int s = cmd11.ExecuteNonQuery();

                                        //-------------------------------------------------------------------------------------------------------------------------------------------------------
                                    }
                                }
                            }
                            //}

                        }

                        #region 29thJAN24
                        //                                    using (SqlConnection con = new SqlConnection(connection_string))
                        //                                    {
                        //                                        con.Open();
                        //                                        using (SqlCommand cmdGetProjectCount = new SqlCommand(@"SELECT COUNT(*) FROM Material_Variance_Subdivision_Project where material_variance_id in  (
                        //                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                        //                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and project_id = (select id from Project where proj_guid =N'" + proj_guid + @"' and name= N'" + proj_name + @"') and name = N'" + mat_var_sub + @"';", con))
                        //                                        {
                        //                                            alreadyexistsinproject = (int)cmdGetProjectCount.ExecuteScalar();
                        //                                        }

                        //                                        if (alreadyexistsinproject == 0) //AND DELETED 
                        //                                        {

                        //                                            // check if it exist in project dtl and tehn update the formula
                        //                                            using (SqlCommand cmdGetProjectCount = new SqlCommand(@"SELECT COUNT(*) FROM Material_Variance_Subdivision_Project where material_variance_id in  (
                        //                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                        //                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 1 and project_id = (select id from Project where proj_guid =N'" + proj_guid + @"' and name= N'" + proj_name + @"') and name = N'" + mat_var_sub + @"';", con))
                        //                                            {
                        //                                                // if deleted add in project dtl and tehn update the formula
                        //                                                alreadydeletedProj = (int)cmdGetProjectCount.ExecuteScalar();
                        //                                            }
                        //                                        }
                        //                                        if (alreadyexistsinproject == 0 && alreadydeletedProj == 0)
                        //                                        {
                        //                                            int countProject = 0;
                        //                                            using (SqlCommand cmdGetProjectCount = new SqlCommand("(select COUNT(id) from Project where proj_guid =N'" + proj_guid + @"' and name= N'" + proj_name + @"'and created_on >= 
                        // (select created_on from Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and city_id = "+city_id +@" and material_variance_id = 
                        // (select id from Material_Variance where variance = N'" + mat_var_name + @"' and material_id = (select id from Material where name = N'" + mat_name + @"'))))", con))
                        //                                            {
                        //                                                countProject = (int)cmdGetProjectCount.ExecuteScalar();
                        //                                            }
                        //                                            if (countProject == 1)
                        //                                            {
                        //                                                SqlCommand cmd_id = new SqlCommand(@"SELECT id FROM Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and material_variance_id in  (
                        //                                                    select id from Material_Variance where variance= N'" + mat_var_name + @"' and material_id in (
                        //                                                    select id from Material where name= N'" + mat_name + @"')) and f_del = 0 and city_id = "+city_id+"; ", con);
                        //                                                SqlDataAdapter sda_id = new SqlDataAdapter(cmd_id);
                        //                                                DataTable dt_id = new DataTable("mat");
                        //                                                sda_id.Fill(dt_id);
                        //                                                id = Convert.ToInt64(dt_id.Rows[0]["id"]);

                        //                                                if (!string.IsNullOrEmpty(revit_category) || !string.IsNullOrEmpty(family_type) || !string.IsNullOrEmpty(property) || !string.IsNullOrEmpty(design_rule_factor) || !string.IsNullOrEmpty(keywords) || !string.IsNullOrEmpty(quantity_extraction_formula))
                        //                                                {
                        //                                                    SqlCommand cmd11 = new SqlCommand((@"INSERT INTO Material_Variance_Subdivision_dtl_Project 
                        //                        (material_var_subdiv_id, 
                        //                         revit_category, 
                        //                         family_type, 
                        //                         property, 
                        //                         design_rule_factor,
                        //                         keywords,
                        //                         quantity_extraction_formula,project_id,updated_by,f_all_elements,element_filter) 
                        //            VALUES    (" + id + ",N'" + revit_category + "',N'" + family_type + "',N'" + property + "',N'" + design_rule_factor + "',N'" + keywords + "',N'" + quantity_extraction_formula + "',(select id from Project where proj_guid =N'" + proj_guid + @"' and name= N'" + proj_name + @"'),N'"+user_info+"',"+f_all_elements+",N'"+element_filter+"' )"), con);
                        //                                                    cmd11.ExecuteNonQuery();
                        //                                                }
                        //                                            }
                        //                                        }

                        //                                    }

                        #endregion

                        conn.Close();
                        return "Success";
                    }
                    else
                    {
                        conn.Close();
                        return "Failure";
                    }

                }

            }
            catch (System.Exception ex)
            {


                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);

                return ex.Message;

            }
        }

        public string upd_mat_var_subdiv_dtl(String mat_name, String mat_var_name, string mat_var_sub, [Optional]string unit_of_measurement, [Optional] Int64? prev_seq, [Optional]string user_info, [Optional]string revit_category, [Optional]string family_type, [Optional]string property, [Optional]string design_rule_factor, [Optional]string keywords, [Optional]string quantity_extraction_formula, [Optional]string specification, string city_name, string country_name, bool f_allCountry, bool f_allCity, bool f_singleCity, int f_all_elements, [Optional]string element_filter,string sortingCondtitions)
        {
            int count = 0;
            Int64 unit = 0;
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();

                if (mat_var_sub.Contains("'"))
                {
                    mat_var_sub = mat_var_sub.Replace("'", "''");
                }

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
                            UpdateFormula_Master(mat_name, mat_var_name, mat_var_sub, unit_of_measurement, prev_seq, user_info, revit_category, family_type, property, design_rule_factor, keywords, quantity_extraction_formula, specification, Convert.ToString(dt_city.Rows[cc]["name"]), Convert.ToString(dt_country.Rows[c]["country"]), f_all_elements, element_filter, sortingCondtitions);
                        }

                    }

                    conn.Close();
                    return "Success";
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
                            UpdateFormula_Master(mat_name, mat_var_name, mat_var_sub, unit_of_measurement, prev_seq, user_info, revit_category, family_type, property, design_rule_factor, keywords, quantity_extraction_formula, specification, Convert.ToString(dt_city.Rows[cc]["name"]), Convert.ToString(dt_country.Rows[c]["country"]), f_all_elements, element_filter, sortingCondtitions);
                        }

                    }

                    conn.Close();
                    return "Success";
                }
                else if (f_singleCity)
                {
                    UpdateFormula_Master(mat_name, mat_var_name, mat_var_sub, unit_of_measurement, prev_seq, user_info, revit_category, family_type, property, design_rule_factor, keywords, quantity_extraction_formula, specification, city_name, country_name, f_all_elements, element_filter, sortingCondtitions);
                    conn.Close();
                    return "Success";
                }
                else
                {
                    conn.Close();
                    return "Failure";
                }


            }
            catch (System.Exception ex)
            {


                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);

                return ex.Message;

            }
        }


        private string UpdateFormula_Master(String mat_name, String mat_var_name, string mat_var_sub, [Optional]string unit_of_measurement, [Optional] Int64? prev_seq, [Optional]string user_info, [Optional]string revit_category, [Optional]string family_type, [Optional]string property, [Optional]string design_rule_factor, [Optional]string keywords, [Optional]string quantity_extraction_formula, [Optional]string specification, string city_name, string country_name, int f_all_elements, [Optional]string element_filter, string sortingCondtitions)
        {
            int count = 0;
            Int64 unit = 0;
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();

                string query = @"SELECT COUNT(*) FROM Material_Variance_Subdivision where material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_name + "')) and name = N'" + mat_var_sub + @"';";
                SqlCommand cmdIsMaster = new SqlCommand(query, conn);
                count = (int)cmdIsMaster.ExecuteScalar();

                if (count == 1)
                {
                       query = @"SELECT COUNT(*) FROM Material_Variance_Subdivision_dtl where material_var_subdiv_id in  ( select  id from Material_Variance_Subdivision where material_variance_id in(
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_name + "')) and name = N'" + mat_var_sub + @"');";
                    cmdIsMaster = new SqlCommand(query, conn);
                    count = (int)cmdIsMaster.ExecuteScalar();
                    if (count == 1)
                    {

                        SqlCommand cmd = new SqlCommand((@"update Material_Variance_Subdivision_dtl
                            set updated_by = N'" + user_info + "',revit_category=N'" + revit_category + "',family_type=N'" + family_type + "',property=N'" + property + "',design_rule_factor=N'" + design_rule_factor + "',keywords=N'" + keywords + "',quantity_extraction_formula=N'" + quantity_extraction_formula + "',f_all_elements=" + f_all_elements + ",element_filter=N'" + element_filter + @"' 
                            where material_var_subdiv_id in (SELECT id FROM Material_Variance_Subdivision where name = N'" + @mat_var_sub + @"' and f_del = 0 and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_name + @"')) and material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')))"), conn);
                        cmd.ExecuteNonQuery();

                        //------------------------------------------------------------------------------------------------------------------------------------------------------
                        if(!string.IsNullOrEmpty(sortingCondtitions)){
                            SqlCommand sortingCmd = new SqlCommand(@"UPDATE Material_Variance_Subdivision_dtl SET Sorting_Logic = @sortingLogic WHERE material_var_subdiv_id IN (SELECT id FROM Material_Variance_Subdivision WHERE name = '" + mat_var_sub + @"' AND f_del = 0 AND city_id = (SELECT id FROM city WHERE name = '" + city_name + @"' AND country_id = (
                         SELECT id FROM Country_Code WHERE country = '" + country_name + @"' )) AND material_variance_id IN (SELECT id FROM Material_Variance WHERE variance = '" + mat_var_name + @"' AND material_id IN (SELECT id FROM Material WHERE name = '" + mat_name + @"')))", conn);
                            sortingCmd.Parameters.AddWithValue("@sortingLogic", sortingCondtitions);
                          sortingCmd.ExecuteNonQuery();
                        }
                       
                        //------------------------------------------------------------------------------------------------------------------------------------------------------

                    }
                    else
                    {
//                        SqlCommand cmd = new SqlCommand((@"Insert INTO Material_Variance_Subdivision_dtl
//                            (material_var_subdiv_id,revit_category,family_type,property,design_rule_factor,keywords,quantity_extraction_formula,updated_by,f_all_elements,element_filter)
//							VALUES((SELECT id FROM Material_Variance_Subdivision where name = N'" + @mat_var_sub + @"' and f_del = 0 and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_name + @"')) and material_variance_id in  (
//                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
//                                                    select id from Material where name=N'" + mat_name + @"'))),N'" + revit_category + "',N'" + family_type + "',N'" + property + "',N'" + design_rule_factor + "',N'" + keywords + "',N'" + quantity_extraction_formula + "',N'" + user_info + "'," + f_all_elements + ",N'" + element_filter + @"') "), conn);
//                        cmd.ExecuteNonQuery();

                        //---------------------------------------------------------------------------------------------------------------------------------------------------------------
                        SqlCommand cmd = new SqlCommand((@"Insert INTO Material_Variance_Subdivision_dtl
                            (material_var_subdiv_id,revit_category,family_type,property,design_rule_factor,keywords,quantity_extraction_formula,updated_by,f_all_elements,element_filter,Sorting_Logic)
                            VALUES((SELECT id FROM Material_Variance_Subdivision where name = N'" + @mat_var_sub + @"' and f_del = 0 and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_name + @"')) and material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"'))),N'" + revit_category + "',N'" + family_type + "',N'" + property + "',N'" + design_rule_factor + "',N'" + keywords + "',N'" + quantity_extraction_formula + "',N'" + user_info + "'," + f_all_elements + ",N'" + element_filter + @"', N'sortingConditions') "), conn);
                        cmd.ExecuteNonQuery();
                        //---------------------------------------------------------------------------------------------------------------------------------------------------------------
                    }



                    if (!string.IsNullOrEmpty(unit_of_measurement))
                    {
                        //cmd = new SqlCommand((@"select unit_of_measurement from Material_Variance_Subdivision_Project where material_variance_id = (select id from Material_Variance where variance = N'" + mat_var_name + @"') and name = N'" + mat_var_sub + @"' and project_id = (select id from Project where proj_guid =N'" + proj_guid + "' and name= N'" + proj_name + "')"), conn);

                        SqlCommand cmd = new SqlCommand((@"select id from Unit_Of_Measurement where unit = N'" + unit_of_measurement + @"'"), conn);
                        unit = (Int64)cmd.ExecuteScalar();
                        cmd = new SqlCommand((@"update Material_Variance_Subdivision set unit_of_measurement = " + unit + @",specification=N'" + specification + @"',created_by = N'" + user_info + @"' where material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_name + @"'))
													 and name = N'" + mat_var_sub + @"';"), conn);
                        cmd.ExecuteNonQuery();

                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand((@"select unit_of_measurement from Material_Variance_Subdivision where material_variance_id = (select id from Material_Variance where variance = N'" + mat_var_name + @"') and f_del = 0 and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_name + "')) and name = N'" + mat_var_sub + @"'"), conn);
                        unit = (Int64)cmd.ExecuteScalar();
                        cmd = new SqlCommand((@"update Material_Variance_Subdivision set unit_of_measurement = " + unit + @",specification=N'" + specification + @"',created_by = N'" + user_info + @"' where material_variance_id in  (
                                                    select id from Material_Variance where variance=N'" + mat_var_name + @"' and material_id in (
                                                    select id from Material where name=N'" + mat_name + @"')) and f_del = 0 and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_name + "')) and name = N'" + mat_var_sub + @"';"), conn);
                        cmd.ExecuteNonQuery();
                    }

                    return "Success";
                }
                else
                {

                    return "Failure";
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

                return ex.Message;
            }
        }

    }
}
