using fujita_BIM4D5D_planner;
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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service2" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service2.svc or Service2.svc.cs at the Solution Explorer and start debugging.
    public class Service2 : Materialdtl
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        SqlCommand cmd1;
        SqlDataAdapter sda1;
        DataTable dt1;
        SqlCommand cmd_city;
        SqlDataAdapter sda_city;
        DataTable dt_city;
        SqlCommand cmd_mat;
        SqlDataAdapter sda_mat;
        DataTable dt_project;
        SqlCommand cmd_project;
        SqlDataAdapter sda_project;
        DataTable dt_mat;
        SqlDataAdapter sda_mat_var;
        SqlCommand cmd_mat_var;
        DataTable dt_mat_var;
        Mat_detail mat_subdiv = new Mat_detail();


        public List<Mat_detail_list> FindMaterialdtl(String mat, String Mat_var, string Username, string Password)
        {
            try
            {
                using (con = new SqlConnection(connection_string))
                {
                    List<Mat_detail_list> Mat_detail_list = new List<Mat_detail_list>();

                    cmd = new SqlCommand(@"SELECT distinct id,code,currency FROM Country_Code where id in (select countryid from user_country_map where userid = (select id from Username_Password where username = N'" + Username + "' and password = N'" + Password + "') and (accessid = 1 or accessid = 2)) order by code asc ", con);
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable("country");
                    sda.Fill(dt);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        List<city_dtl> city_dtl = new List<city_dtl>();
                        cmd_city = new SqlCommand(@"SELECT id,name from City where country_id = " + dt.Rows[i]["id"] + "order by name asc", con);
                        sda_city = new SqlDataAdapter(cmd_city);
                        dt_city = new DataTable("city");
                        sda_city.Fill(dt_city);
                        for (int c = 0; c < dt_city.Rows.Count; c++)
                        {
                            List<cost_dtl> cost_dtl1 = new List<cost_dtl>();
                            List<project_list> proj_dtl = new List<project_list>();
                            
                            cmd1 = new SqlCommand(@"SELECT subdivision,city_name,country_code,currency,unit_of_measurement,outsourcing,material1,material2,labor1,labor2,other FROM 
                                               (SELECT pivottable.[MAT_VAR_SUB] AS Subdivision,pivottable.country_code,pivottable.currency,city_name,city_id,pivottable.[Unit_of_measurement],pivottable.[id]  AS id, pivottable.[1]  AS Outsourcing, pivottable.[2]  AS Material1, pivottable.[3] AS Material2, pivottable.[4] AS Labor1, pivottable.[5] AS Labor2, pivottable.[6] AS Other FROM
                                               (SELECT var_sub.id   id, var_sub.NAME MAT_VAR_SUB, material_option_id, cost,(select code from country_code where id = cc_id) country_code,(select currency from country_code where id = cc_id) currency,city_name ,c_id city_id, (SELECT UNIT FROM UNIT_OF_MEASUREMENT WHERE ID =unit_of_measurement) unit_of_measurement FROM (SELECT * FROM  material_variance WHERE  variance = '" + Mat_var + "')var LEFT OUTER JOIN (select  c.name city_name,c.id c_id,cc.id cc_id,var_sub.* from material_variance_subdivision var_sub, Country_Code cc, city c where c.country_id = cc.id and f_del = 0)var_sub ON var.id = var_sub.material_variance_id LEFT OUTER JOIN cost_master COST_MAST ON COST_MAST.material_variance_subdiv_id = var_sub.id and COST_MAST.city_id = c_id AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end WHERE  material_id = (SELECT id FROM   material WHERE  NAME = '" + mat + @"')
                                                ) AS source_table PIVOT(Max([cost]) FOR material_option_id IN([1],[2],[3],[4],[5],[6])) AS pivottable) AS test where city_id = (select id from city where country_id = (select id from Country_code where code = N'" + Convert.ToString(dt.Rows[i]["code"]) + "') and name = N'" + Convert.ToString(dt_city.Rows[c]["name"]) + "') ORDER BY test.id", con);

                            sda1 = new SqlDataAdapter(cmd1);
                            dt1 = new DataTable("cost");
                            sda1.Fill(dt1);
                            for (int j = 0; j < dt1.Rows.Count; j++)
                            {
                                cost_dtl cost_dtl2 = new cost_dtl
                                {
                                    subdivision = Convert.ToString(dt1.Rows[j]["subdivision"]),
                                    unit_of_measurement = Convert.ToString(dt1.Rows[j]["unit_of_measurement"]),
                                    outsourcing = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[j]["outsourcing"])) ? (Decimal?)null : Convert.ToDecimal(dt1.Rows[j]["outsourcing"]),
                                    material1 = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[j]["material1"])) ? (Decimal?)null : Convert.ToDecimal(dt1.Rows[j]["material1"]),
                                    material2 = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[j]["material2"])) ? (Decimal?)null : Convert.ToDecimal(dt1.Rows[j]["material2"]),
                                    labor1 = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[j]["labor1"])) ? (Decimal?)null : Convert.ToDecimal(dt1.Rows[j]["labor1"]),
                                    labor2 = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[j]["labor2"])) ? (Decimal?)null : Convert.ToDecimal(dt1.Rows[j]["labor2"]),
                                    other = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[j]["other"])) ? (Decimal?)null : Convert.ToDecimal(dt1.Rows[j]["other"])
                                };
                                cost_dtl1.Add(cost_dtl2);
                            }
                            city_dtl Mat_detail_list11 = new city_dtl()
                            {
                                city_name = Convert.ToString(dt_city.Rows[c]["name"]),
                                cost_dtl = cost_dtl1
                            };
                            city_dtl.Add(Mat_detail_list11);
                        }
                        Mat_detail_list Mat_detail_list1 = new Mat_detail_list()
                        {
                            country_code = Convert.ToString(dt.Rows[i]["code"]),
                            currency = Convert.ToString(dt.Rows[i]["currency"]),
                            city_dtl = city_dtl
                        };
                        Mat_detail_list.Add(Mat_detail_list1);
                    }



                    return Mat_detail_list;
                }
            }
            catch (Exception ex)
            {
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();                    
                }
                return null;
            }
            
        }
        
        public List<Mat_detail_list1> FindMaterialdtlall(string Username, string Password)
        {
            try
            {
                using (con = new SqlConnection(connection_string))
                {
                    List<Mat_detail_list1> Mat_detail_list = new List<Mat_detail_list1>();

                    cmd = new SqlCommand(@"SELECT distinct id,code,currency FROM Country_Code where id in (select countryid from user_country_map where userid = (select id from Username_Password where username = N'" + Username + "' and password = N'" + Password + "') and (accessid = 1 or accessid = 2)) order by code asc ", con);
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable("country");
                    sda.Fill(dt);
                   
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        List<city_dtl1> city_dtl = new List<city_dtl1>();
                        cmd_city = new SqlCommand(@"SELECT id,name from City where country_id = " + dt.Rows[i]["id"] + "order by name asc", con);
                        
                        sda_city = new SqlDataAdapter(cmd_city);
                        dt_city = new DataTable("city");
                        sda_city.Fill(dt_city);
                        for (int c = 0; c < dt_city.Rows.Count; c++)
                        {
                            List<mat_dtl> mat_dtl = new List<mat_dtl>();
                            cmd_mat = new SqlCommand(@"SELECT id,name from Material ORDER BY case when seq is null then 1 else 0 end,seq;", con);
                            sda_mat = new SqlDataAdapter(cmd_mat);
                            dt_mat = new DataTable("material");
                            sda_mat.Fill(dt_mat);
                            for (int m = 0; m < dt_mat.Rows.Count; m++)
                            {
                                List<mat_var_dtl> mat_var_dtl = new List<mat_var_dtl>();
                                cmd_mat_var = new SqlCommand(@"SELECT id,variance from Material_variance where material_id = " + dt_mat.Rows[m]["id"] + "; ", con);
                                sda_mat_var = new SqlDataAdapter(cmd_mat_var);
                                dt_mat_var = new DataTable("material");
                                sda_mat_var.Fill(dt_mat_var);
                                for (int m1 = 0; m1 < dt_mat_var.Rows.Count; m1++)
                                {
                                    List<cost_dtl> cost_dtl1 = new List<cost_dtl>();
                                   
                                    cmd1 = new SqlCommand(@"SELECT subdivision,city_name,country_code,currency,unit_of_measurement,outsourcing,material1,material2,labor1,labor2,other FROM 
                                       (SELECT pivottable.[MAT_VAR_SUB] AS Subdivision,pivottable.country_code,pivottable.currency,city_name,city_id,pivottable.[Unit_of_measurement],pivottable.[id]  AS id, pivottable.[1]  AS Outsourcing, pivottable.[2]  AS Material1, pivottable.[3] AS Material2, pivottable.[4] AS Labor1, pivottable.[5] AS Labor2, pivottable.[6] AS Other FROM
                                       (SELECT var_sub.id   id, var_sub.NAME MAT_VAR_SUB, material_option_id, cost,(select code from country_code where id = cc_id) country_code,(select currency from country_code where id = cc_id) currency,city_name ,c_id city_id, (SELECT UNIT FROM UNIT_OF_MEASUREMENT WHERE ID =unit_of_measurement) unit_of_measurement FROM (SELECT * FROM  material_variance where id = " + dt_mat_var.Rows[m1]["id"] + ")var LEFT OUTER JOIN (select  c.name city_name,c.id c_id,cc.id cc_id,var_sub.* from material_variance_subdivision var_sub, Country_Code cc, city c where c.country_id = cc.id and f_del = 0 and city_id = " + dt_city.Rows[c]["id"] + ")var_sub ON var.id = var_sub.material_variance_id LEFT OUTER JOIN cost_master COST_MAST ON COST_MAST.material_variance_subdiv_id = var_sub.id and COST_MAST.city_id = c_id AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end WHERE  material_id in (SELECT id FROM   material where id = " + dt_mat.Rows[m]["id"] + " )) AS source_table PIVOT(Max([cost]) FOR material_option_id IN([1],[2],[3],[4],[5],[6])) AS pivottable) AS test where city_id = (select id from city where country_id = (select id from Country_code where code = N'" + Convert.ToString(dt.Rows[i]["code"]) + "') and name = N'" + Convert.ToString(dt_city.Rows[c]["name"]) + "') ORDER BY test.id", con);

                                    sda1 = new SqlDataAdapter(cmd1);
                                    dt1 = new DataTable("cost");
                                    sda1.Fill(dt1);
                                    for (int j = 0; j < dt1.Rows.Count; j++)
                                    {
                                        cost_dtl cost_dtl2 = new cost_dtl
                                        {
                                            subdivision = Convert.ToString(dt1.Rows[j]["subdivision"]),
                                            unit_of_measurement = Convert.ToString(dt1.Rows[j]["unit_of_measurement"]),
                                            outsourcing = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[j]["outsourcing"])) ? (Decimal?)null : Convert.ToDecimal(dt1.Rows[j]["outsourcing"]),
                                            material1 = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[j]["material1"])) ? (Decimal?)null : Convert.ToDecimal(dt1.Rows[j]["material1"]),
                                            material2 = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[j]["material2"])) ? (Decimal?)null : Convert.ToDecimal(dt1.Rows[j]["material2"]),
                                            labor1 = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[j]["labor1"])) ? (Decimal?)null : Convert.ToDecimal(dt1.Rows[j]["labor1"]),
                                            labor2 = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[j]["labor2"])) ? (Decimal?)null : Convert.ToDecimal(dt1.Rows[j]["labor2"]),
                                            other = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[j]["other"])) ? (Decimal?)null : Convert.ToDecimal(dt1.Rows[j]["other"])
                                        };
                                        cost_dtl1.Add(cost_dtl2);
                                    }
                                    mat_var_dtl mat_var_dtl1 = new mat_var_dtl()
                                    {
                                        mat_var_name = Convert.ToString(dt_mat_var.Rows[m1]["variance"]),
                                        cost_dtl = cost_dtl1
                                    };
                                    mat_var_dtl.Add(mat_var_dtl1);
                                }
                                mat_dtl mat_dtl1 = new mat_dtl()
                                {
                                    mat_name = Convert.ToString(dt_mat.Rows[m]["name"]),
                                    mat_var_dtl = mat_var_dtl
                                };
                                mat_dtl.Add(mat_dtl1);
                            }

                            city_dtl1 Mat_detail_list11 = new city_dtl1()
                            {
                                city_name = Convert.ToString(dt_city.Rows[c]["name"]),
                                mat_dtl = mat_dtl
                            };
                            city_dtl.Add(Mat_detail_list11);
                        }
                        Mat_detail_list1 Mat_detail_list1 = new Mat_detail_list1()
                        {
                            country_code = Convert.ToString(dt.Rows[i]["code"]),
                            currency = Convert.ToString(dt.Rows[i]["currency"]),
                            city_dtl1 = city_dtl
                        };
                        Mat_detail_list.Add(Mat_detail_list1);
                    }

                    return Mat_detail_list;
                }
            }
            catch (Exception ex)
            {
               Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return null;
            }
            
        }

        public List<Mat_detail_list1_AllJobs> FindMaterialdtlall_Project(string Username, string Password)
        {
            try
            {
                using (con = new SqlConnection(connection_string))
                {
                    List<Mat_detail_list1_AllJobs> Mat_detail_list = new List<Mat_detail_list1_AllJobs>();

                    //cmd = new SqlCommand(@"SELECT distinct id,code,currency FROM Country_Code order by code asc", con);  //order by code asc - done at app side
                    cmd = new SqlCommand(@"SELECT distinct id,code,currency FROM Country_Code where id in (select countryid from user_country_map where userid = (select id from Username_Password where username =N'" + Username + "'  and password = N'" + Password + "') and (accessid = 1 or accessid = 2)) order by code asc ", con); 
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable("country");
                    sda.Fill(dt);
                    //mat_subdiv.Mat_det = dt;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        List<city_dtl1_AllJobs> city_dtl = new List<city_dtl1_AllJobs>();
                        cmd_city = new SqlCommand(@"SELECT id,name from City where country_id = " + dt.Rows[i]["id"] + "order by name asc", con);
                        sda_city = new SqlDataAdapter(cmd_city);
                        dt_city = new DataTable("city");
                        sda_city.Fill(dt_city);

                        for (int c = 0; c < dt_city.Rows.Count; c++)
                        {
                            cmd_project = new SqlCommand(@"select name,proj_guid from Project where created_on >='2022-09-23' and f_active = 1 and city_id = " + dt_city.Rows[c]["id"], con);
                            sda_project = new SqlDataAdapter(cmd_project);
                            dt_project = new DataTable("project");
                            sda_project.Fill(dt_project);

                            List<project_list_AllJobs> proj_dtl1 = new List<project_list_AllJobs>();

                            for (int p = 0; p < dt_project.Rows.Count; p++)
                            {
                                string proj_id = dt_project.Rows[p]["proj_guid"].ToString();
                                string proj_name = dt_project.Rows[p]["name"].ToString();

                                List<mat_dtl> mat_dtl = new List<mat_dtl>();
                                cmd_mat = new SqlCommand(@"SELECT id,name from Material ORDER BY case when seq is null then 1 else 0 end,seq;", con);
                                sda_mat = new SqlDataAdapter(cmd_mat);
                                dt_mat = new DataTable("material");
                                sda_mat.Fill(dt_mat);
                                for (int m = 0; m < dt_mat.Rows.Count; m++)
                                {
                                    List<mat_var_dtl> mat_var_dtl = new List<mat_var_dtl>();
                                    cmd_mat_var = new SqlCommand(@"SELECT id,variance from Material_variance where material_id = " + dt_mat.Rows[m]["id"] + "; ", con);
                                    sda_mat_var = new SqlDataAdapter(cmd_mat_var);
                                    dt_mat_var = new DataTable("material");
                                    sda_mat_var.Fill(dt_mat_var);
                                    for (int m1 = 0; m1 < dt_mat_var.Rows.Count; m1++)
                                    {
                                        List<cost_dtl> cost_dtl1 = new List<cost_dtl>();

                                        #region old query
                                        //                                    cmd1 = new SqlCommand(@"SELECT subdivision,city_name,country_code,currency,unit_of_measurement,outsourcing,material1,material2,labor1,labor2,other FROM 
                                        //                                       (SELECT pivottable.[MAT_VAR_SUB] AS Subdivision,pivottable.country_code,pivottable.currency,city_name,city_id,pivottable.[Unit_of_measurement],pivottable.[id]  AS id, pivottable.[1]  AS Outsourcing, pivottable.[2]  AS Material1, pivottable.[3] AS Material2, pivottable.[4] AS Labor1, pivottable.[5] AS Labor2, pivottable.[6] AS Other FROM
                                        //                                       (SELECT var_sub.id   id, var_sub.NAME MAT_VAR_SUB, material_option_id, cost, (select code from country_code where id = cc_id) country_code,(select currency from country_code where id = cc_id) currency,city_name ,c_id city_id, (SELECT UNIT FROM UNIT_OF_MEASUREMENT WHERE ID = unit_of_measurement) unit_of_measurement FROM (SELECT * FROM  material_variance where id = " + dt_mat_var.Rows[m1]["id"] + ")var LEFT OUTER JOIN (select  c.name city_name, c.id c_id, cc.id cc_id, var_sub.* from material_variance_subdivision var_sub, Country_Code cc, city c where c.country_id = cc.id and f_del = 0)var_sub ON var.id = var_sub.material_variance_id LEFT OUTER JOIN cost_master_project COST_MAST ON COST_MAST.material_variance_subdiv_id = var_sub.id and COST_MAST.city_id = c_id AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end and project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"') WHERE material_id in (SELECT id FROM   material where id = " + dt_mat.Rows[m]["id"] + " )) AS source_table PIVOT(Max([cost]) FOR material_option_id IN([1],[2],[3],[4],[5],[6])) AS pivottable) AS test where city_id = (select id from city where country_id = (select id from Country_code where code = N'" + Convert.ToString(dt.Rows[i]["code"]) + "') and name = N'" + Convert.ToString(dt_city.Rows[c]["name"]) + "') ORDER BY test.id", con);
                                        //                                    cmd1 = new SqlCommand(@"SELECT subdivision,city_name,country_code,currency,unit_of_measurement,outsourcing,material1,material2,labor1,labor2,other FROM 
                                        //                                       (SELECT pivottable.[MAT_VAR_SUB] AS Subdivision,pivottable.country_code,pivottable.currency,city_name,city_id,pivottable.[Unit_of_measurement],pivottable.[id]  AS id, pivottable.[1]  AS Outsourcing, pivottable.[2]  AS Material1, pivottable.[3] AS Material2, pivottable.[4] AS Labor1, pivottable.[5] AS Labor2, pivottable.[6] AS Other FROM
                                        //                                       (SELECT var_sub.id   id, var_sub.NAME MAT_VAR_SUB, material_option_id, cost, (select code from country_code where id = cc_id) country_code,(select currency from country_code where id = cc_id) currency,city_name ,c_id city_id, (SELECT UNIT FROM UNIT_OF_MEASUREMENT WHERE ID = unit_of_measurement) unit_of_measurement FROM (SELECT * FROM  material_variance where id = " + dt_mat_var.Rows[m1]["id"] + ")var LEFT OUTER JOIN (select  c.name city_name, c.id c_id, cc.id cc_id, var_sub.* from material_variance_subdivision var_sub, Country_Code cc, city c where c.country_id = cc.id and f_del = 0)var_sub ON var.id = var_sub.material_variance_id INNER JOIN cost_master_project COST_MAST ON COST_MAST.material_variance_subdiv_id = var_sub.id and COST_MAST.city_id = c_id AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end and project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"') WHERE material_id in (SELECT id FROM   material where id = " + dt_mat.Rows[m]["id"] + " )) AS source_table PIVOT(Max([cost]) FOR material_option_id IN([1],[2],[3],[4],[5],[6])) AS pivottable) AS test where city_id = (select id from city where country_id = (select id from Country_code where code = N'" + Convert.ToString(dt.Rows[i]["code"]) + "') and name = N'" + Convert.ToString(dt_city.Rows[c]["name"]) + "') ORDER BY test.id", con);

                                        #endregion

                                        cmd1 = new SqlCommand(@"SELECT subdivision,city_name,country_code,currency,unit_of_measurement,outsourcing,material1,material2,labor1,labor2,other FROM 
(SELECT pivottable.[MAT_VAR_SUB] AS Subdivision,pivottable.country_code,pivottable.currency,city_name,city_id,pivottable.[Unit_of_measurement],pivottable.[id]  AS id,pivottable.[seq]  AS seq,
 pivottable.[1]  AS Outsourcing, pivottable.[2]  AS Material1, pivottable.[3] AS Material2, pivottable.[4] AS Labor1, pivottable.[5] AS Labor2, pivottable.[6] AS Other FROM
(SELECT var_sub.id   id,var_sub.seq   seq, var_sub.NAME MAT_VAR_SUB, material_option_id, cost, (select code from country_code where id = cc_id) country_code,
(select currency from country_code where id = cc_id) currency,city_name ,c_id city_id, (SELECT UNIT FROM UNIT_OF_MEASUREMENT WHERE ID = unit_of_measurement) 
unit_of_measurement FROM (SELECT * FROM  material_variance where id = " + dt_mat_var.Rows[m1]["id"] + @")var LEFT OUTER JOIN (select  c.name city_name, c.id c_id, cc.id cc_id, var_sub.* from material_variance_subdivision var_sub, Country_Code cc, city c where c.country_id = cc.id and f_del = 0 and city_id = " + dt_city.Rows[c]["id"] + " and NOT EXISTS (select 1 from Material_Variance_Subdivision_Project where var_sub.name = Material_Variance_Subdivision_Project.name and Material_Variance_Subdivision_Project.f_del =1 and project_id = (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"')) and created_on <= (select created_on from Project where id = (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"')))var_sub ON var.id = var_sub.material_variance_id LEFT OUTER JOIN cost_master_project COST_MAST ON COST_MAST.material_variance_subdiv_id = var_sub.id 
and COST_MAST.city_id = c_id AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end and project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"') WHERE material_id in (SELECT id FROM  material where id = " + dt_mat.Rows[m]["id"] + @")
UNION
(SELECT var_sub.id   id,var_sub.seq   seq, var_sub.NAME MAT_VAR_SUB, material_option_id, cost, (select code from country_code where id = cc_id) country_code,
(select currency from country_code where id = cc_id) currency,city_name ,c_id city_id, (SELECT UNIT FROM UNIT_OF_MEASUREMENT WHERE ID = unit_of_measurement) 
unit_of_measurement FROM (SELECT * FROM  material_variance where id = " + dt_mat_var.Rows[m1]["id"] + @" )var LEFT OUTER JOIN 
(select  c.name city_name, c.id c_id, cc.id cc_id, var_sub.* from Material_Variance_Subdivision_Project var_sub, Country_Code cc, city c where c.country_id = cc.id 
and f_del = 0 and project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"'))var_sub ON var.id = var_sub.material_variance_id LEFT OUTER JOIN cost_master_project COST_MAST ON COST_MAST.material_variance_subdiv_id_proj = var_sub.id 
and COST_MAST.city_id = c_id AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end and var_sub.project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"') WHERE material_id in (SELECT id FROM  material where id = " + dt_mat.Rows[m]["id"] + @")
)
) AS source_table
 PIVOT(Max([cost]) FOR material_option_id IN([1],[2],[3],[4],[5],[6])) AS pivottable) AS test where city_id = (select id from city where 
 country_id = (select id from Country_code where code = N'" + Convert.ToString(dt.Rows[i]["code"]) + "') and name = N'" + Convert.ToString(dt_city.Rows[c]["name"]) + "') ORDER BY test.seq", con);



                                        sda1 = new SqlDataAdapter(cmd1);
                                        dt1 = new DataTable("cost");
                                        sda1.Fill(dt1);
                                        for (int j = 0; j < dt1.Rows.Count; j++)
                                        {
                                           

                                            cost_dtl cost_dtl2 = new cost_dtl
                                            {
                                                subdivision = Convert.ToString(dt1.Rows[j]["subdivision"]),
                                                unit_of_measurement = Convert.ToString(dt1.Rows[j]["unit_of_measurement"]),
                                                outsourcing = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[j]["outsourcing"])) ? (Decimal?)null : Convert.ToDecimal(dt1.Rows[j]["outsourcing"]),
                                                material1 = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[j]["material1"])) ? (Decimal?)null : Convert.ToDecimal(dt1.Rows[j]["material1"]),
                                                material2 = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[j]["material2"])) ? (Decimal?)null : Convert.ToDecimal(dt1.Rows[j]["material2"]),
                                                labor1 = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[j]["labor1"])) ? (Decimal?)null : Convert.ToDecimal(dt1.Rows[j]["labor1"]),
                                                labor2 = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[j]["labor2"])) ? (Decimal?)null : Convert.ToDecimal(dt1.Rows[j]["labor2"]),
                                                other = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[j]["other"])) ? (Decimal?)null : Convert.ToDecimal(dt1.Rows[j]["other"])
                                            };

                                           
                                            cost_dtl1.Add(cost_dtl2);
                                        }

                                        mat_var_dtl mat_var_dtl1 = new mat_var_dtl()
                                        {
                                            mat_var_name = Convert.ToString(dt_mat_var.Rows[m1]["variance"]),
                                            cost_dtl = cost_dtl1
                                        };
                                        mat_var_dtl.Add(mat_var_dtl1);
                                    }
                                    mat_dtl mat_dtl1 = new mat_dtl()
                                    {
                                        mat_name = Convert.ToString(dt_mat.Rows[m]["name"]),
                                        mat_var_dtl = mat_var_dtl
                                    };
                                    mat_dtl.Add(mat_dtl1);
                                }
                                project_list_AllJobs proj_dtl = new project_list_AllJobs();
                                proj_dtl.projectName = dt_project.Rows[p]["name"].ToString();
                                proj_dtl.projectID = dt_project.Rows[p]["proj_guid"].ToString();
                                proj_dtl.mat_dtl = mat_dtl;

                                proj_dtl1.Add(proj_dtl);

                            }
                            city_dtl1_AllJobs Mat_detail_list11 = new city_dtl1_AllJobs()
                            {
                                city_name = Convert.ToString(dt_city.Rows[c]["name"]),
                                proj_dtl = proj_dtl1
                            };

                            city_dtl.Add(Mat_detail_list11);

                        }
                        Mat_detail_list1_AllJobs Mat_detail_list1 = new Mat_detail_list1_AllJobs()
                        {
                            country_code = Convert.ToString(dt.Rows[i]["code"]),
                            currency = Convert.ToString(dt.Rows[i]["currency"]),
                            city_dtl1 = city_dtl
                        };
                        Mat_detail_list.Add(Mat_detail_list1);
                    }

                    return Mat_detail_list;
                }
            }
            catch (Exception ex)
            {
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return null;
            }
            
        }

        public List<Mat_detail_list_project> FindMaterialdtl_Project(String mat, String Mat_var, string Username, string Password)
        {
            try
            {
                using (con = new SqlConnection(connection_string))
                {
                    List<Mat_detail_list_project> Mat_detail_list = new List<Mat_detail_list_project>();

                    //cmd = new SqlCommand(@"SELECT distinct id,code,currency FROM Country_Code order by code asc", con);
                    cmd = new SqlCommand(@"SELECT distinct id,code,currency FROM Country_Code where id in (select countryid from user_country_map where userid = (select id from Username_Password where username =N'" + Username + "'  and password = N'" + Password + "') and (accessid = 1 or accessid = 2)) order by code asc ", con);
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable("country");
                    sda.Fill(dt);

                    string proj_id = string.Empty;
                    string proj_name = string.Empty;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        List<city_dtl_project> city_dtl = new List<city_dtl_project>();
                        cmd_city = new SqlCommand(@"SELECT id,name from City where country_id = " + dt.Rows[i]["id"] + "order by name asc", con);
                        sda_city = new SqlDataAdapter(cmd_city);
                        dt_city = new DataTable("city");
                        sda_city.Fill(dt_city);
                        for (int c = 0; c < dt_city.Rows.Count; c++)
                        {
                            cmd_project = new SqlCommand(@"select name,proj_guid from Project where created_on >='2022-09-23' and f_active = 1 and city_id = " + dt_city.Rows[c]["id"], con);
                            sda_project = new SqlDataAdapter(cmd_project);
                            dt_project = new DataTable("project");
                            sda_project.Fill(dt_project);

                            List<project_list> proj_dtl1 = new List<project_list>();

                            for (int p = 0; p < dt_project.Rows.Count; p++)
                            {
                                proj_id = dt_project.Rows[p]["proj_guid"].ToString();
                                proj_name = dt_project.Rows[p]["name"].ToString();


                                List<cost_dtl> cost_dtl1 = new List<cost_dtl>();

                                //                            cmd1 = new SqlCommand(@"SELECT subdivision,city_name,country_code,currency,unit_of_measurement,outsourcing,material1,material2,labor1,labor2,other FROM 
                                //                                       (SELECT pivottable.[MAT_VAR_SUB] AS Subdivision,pivottable.country_code,pivottable.currency,city_name,city_id,pivottable.[Unit_of_measurement],pivottable.[id]  AS id, pivottable.[1]  AS Outsourcing, pivottable.[2]  AS Material1, pivottable.[3] AS Material2, pivottable.[4] AS Labor1, pivottable.[5] AS Labor2, pivottable.[6] AS Other FROM
                                //                                       (SELECT var_sub.id   id, var_sub.NAME MAT_VAR_SUB, material_option_id, cost,(select code from country_code where id = cc_id) country_code,(select currency from country_code where id = cc_id) currency,city_name ,c_id city_id, (SELECT UNIT FROM UNIT_OF_MEASUREMENT WHERE ID =unit_of_measurement) unit_of_measurement FROM (SELECT * FROM  material_variance WHERE  variance = '" + Mat_var + "')var LEFT OUTER JOIN (select  c.name city_name,c.id c_id,cc.id cc_id,var_sub.* from material_variance_subdivision var_sub, Country_Code cc, city c where c.country_id = cc.id and f_del = 0)var_sub ON var.id = var_sub.material_variance_id LEFT OUTER JOIN Cost_Master_Project COST_MAST ON COST_MAST.material_variance_subdiv_id = var_sub.id and COST_MAST.city_id = c_id AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end and project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"')  WHERE  material_id = (SELECT id FROM   material WHERE  NAME = '" + mat + @"')) AS source_table PIVOT(Max([cost]) FOR material_option_id IN([1],[2],[3],[4],[5],[6])) AS pivottable) AS test where city_id = (select id from city where country_id = (select id from Country_code where code = N'" + Convert.ToString(dt.Rows[i]["code"]) + "') and name = N'" + Convert.ToString(dt_city.Rows[c]["name"]) + "') ORDER BY test.id", con);

                                //                            cmd1 = new SqlCommand(@"SELECT subdivision,city_name,country_code,currency,unit_of_measurement,outsourcing,material1,material2,labor1,labor2,other FROM (SELECT pivottable.[MAT_VAR_SUB] AS Subdivision,pivottable.country_code,pivottable.currency,city_name,city_id,pivottable.[Unit_of_measurement],pivottable.[id]  AS id, pivottable.[1]  AS Outsourcing, pivottable.[2]  AS Material1, pivottable.[3] AS Material2, pivottable.[4] AS Labor1, pivottable.[5] AS Labor2, pivottable.[6] AS Other FROM
                                //                                                   (SELECT var_sub.id   id, var_sub.NAME MAT_VAR_SUB, material_option_id, cost,(select code from country_code where id = cc_id) country_code,(select currency from country_code where id = cc_id) currency,city_name ,c_id city_id, 
                                //                                                   (SELECT UNIT FROM UNIT_OF_MEASUREMENT WHERE ID =unit_of_measurement) unit_of_measurement FROM (SELECT * FROM  material_variance WHERE  variance = '" + Mat_var + "')var LEFT OUTER JOIN (select  c.name city_name,c.id c_id,cc.id cc_id,var_sub.* from material_variance_subdivision var_sub, Country_Code cc, city c where c.country_id = cc.id and f_del = 0)var_sub ON var.id = var_sub.material_variance_id INNER JOIN Cost_Master_Project COST_MAST ON COST_MAST.material_variance_subdiv_id = var_sub.id and COST_MAST.city_id = c_id AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end and project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"')  WHERE  material_id = (SELECT id FROM   material WHERE  NAME = '" + mat + @"')) AS source_table PIVOT(Max([cost]) FOR material_option_id IN([1],[2],[3],[4],[5],[6])) AS pivottable) AS test where city_id = (select id from city where country_id = (select id from Country_code where code = N'" + Convert.ToString(dt.Rows[i]["code"]) + "') and name = N'" + Convert.ToString(dt_city.Rows[c]["name"]) + "') ORDER BY test.id", con);


                                cmd1 = new SqlCommand(@"SELECT subdivision,city_name,country_code,currency,unit_of_measurement,outsourcing,material1,material2,labor1,labor2,other FROM (SELECT pivottable.[MAT_VAR_SUB] AS Subdivision,pivottable.country_code,pivottable.currency,city_name,city_id,pivottable.[Unit_of_measurement],pivottable.[id]  AS id,pivottable.[seq]  AS seq,
pivottable.[1]  AS Outsourcing, pivottable.[2]  AS Material1, pivottable.[3] AS Material2, pivottable.[4] AS Labor1, pivottable.[5] AS Labor2, pivottable.[6] AS Other FROM (SELECT var_sub.id   id,var_sub.seq   seq, var_sub.NAME MAT_VAR_SUB, material_option_id, cost,(select code from country_code where id = cc_id) country_code,(select currency from country_code where id = cc_id) currency,city_name ,c_id city_id, (SELECT UNIT FROM UNIT_OF_MEASUREMENT WHERE ID =unit_of_measurement) unit_of_measurement FROM (SELECT * FROM  material_variance WHERE variance = '" + Mat_var + "')var LEFT OUTER JOIN (select  c.name city_name,c.id c_id,cc.id cc_id,var_sub.* from Material_Variance_Subdivision var_sub, Country_Code cc, city c where c.country_id = cc.id and f_del = 0 and NOT EXISTS (SELECT 1 FROM Material_Variance_Subdivision_Project WHERE  var_sub.name = Material_Variance_Subdivision_Project.name and Material_Variance_Subdivision_Project.f_del = 1 and Material_Variance_Subdivision_Project.project_id = (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"')) and created_on <= (select created_on from Project where id = (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"')))var_sub 
 ON var.id = var_sub.material_variance_id LEFT OUTER JOIN Cost_Master_Project COST_MAST ON  COST_MAST.material_variance_subdiv_id = var_sub.id and COST_MAST.city_id = c_id AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end and 
 COST_MAST.project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"') WHERE  material_id = (SELECT id FROM   material WHERE  NAME = '" + mat + @"') union (SELECT var_sub_project.id   id,var_sub_project.seq   seq, var_sub_project.NAME MAT_VAR_SUB_project, material_option_id, cost,(select code from country_code where id = cc_id) country_code,(select currency from country_code where id = cc_id) currency,city_name ,c_id city_id, (SELECT UNIT FROM UNIT_OF_MEASUREMENT WHERE ID =unit_of_measurement) unit_of_measurement FROM (SELECT * FROM  material_variance WHERE variance = '" + Mat_var + "')var LEFT OUTER JOIN (select  c.name city_name,c.id c_id,cc.id cc_id,var_sub_project.* from Material_Variance_Subdivision_Project var_sub_project, Country_Code cc, city c where c.country_id = cc.id and f_del = 0 and project_id = (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"'))var_sub_project  ON var.id = var_sub_project.material_variance_id LEFT OUTER JOIN Cost_Master_Project COST_MAST ON  COST_MAST.material_variance_subdiv_id_proj = var_sub_project.id and COST_MAST.city_id = c_id AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end and COST_MAST.project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"') WHERE  material_id = (SELECT id FROM material WHERE  NAME = '" + mat + @"'))) AS source_table PIVOT(Max([cost]) FOR material_option_id IN([1],[2],[3],[4],[5],[6])) AS pivottable) AS test where city_id = (select id from city where country_id = (select id from Country_code where code = N'" + Convert.ToString(dt.Rows[i]["code"]) + "') and name = N'" + Convert.ToString(dt_city.Rows[c]["name"]) + "') ORDER BY test.seq", con);
                                sda1 = new SqlDataAdapter(cmd1);
                                dt1 = new DataTable("cost");
                                sda1.Fill(dt1);
                                for (int j = 0; j < dt1.Rows.Count; j++)
                                {
                                    cost_dtl cost_dtl2 = new cost_dtl
                                    {
                                        subdivision = Convert.ToString(dt1.Rows[j]["subdivision"]),
                                        unit_of_measurement = Convert.ToString(dt1.Rows[j]["unit_of_measurement"]),
                                        outsourcing = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[j]["outsourcing"])) ? (Decimal?)null : Convert.ToDecimal(dt1.Rows[j]["outsourcing"]),
                                        material1 = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[j]["material1"])) ? (Decimal?)null : Convert.ToDecimal(dt1.Rows[j]["material1"]),
                                        material2 = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[j]["material2"])) ? (Decimal?)null : Convert.ToDecimal(dt1.Rows[j]["material2"]),
                                        labor1 = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[j]["labor1"])) ? (Decimal?)null : Convert.ToDecimal(dt1.Rows[j]["labor1"]),
                                        labor2 = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[j]["labor2"])) ? (Decimal?)null : Convert.ToDecimal(dt1.Rows[j]["labor2"]),
                                        other = string.IsNullOrEmpty(Convert.ToString(dt1.Rows[j]["other"])) ? (Decimal?)null : Convert.ToDecimal(dt1.Rows[j]["other"])
                                    };
                                    cost_dtl1.Add(cost_dtl2);
                                }

                                project_list proj_dtl = new project_list();
                                proj_dtl.projectName = dt_project.Rows[p]["name"].ToString();
                                proj_dtl.projectID = dt_project.Rows[p]["proj_guid"].ToString();
                                proj_dtl.cost_dtl = cost_dtl1;

                                proj_dtl1.Add(proj_dtl);

                            }

                            city_dtl_project Mat_detail_list11 = new city_dtl_project()
                            {
                                city_name = Convert.ToString(dt_city.Rows[c]["name"]),
                                projectList = proj_dtl1
                            };
                            city_dtl.Add(Mat_detail_list11);

                        }
                        Mat_detail_list_project Mat_detail_list1 = new Mat_detail_list_project()
                        {
                            country_code = Convert.ToString(dt.Rows[i]["code"]),
                            currency = Convert.ToString(dt.Rows[i]["currency"]),
                            city_dtl = city_dtl
                        };
                        Mat_detail_list.Add(Mat_detail_list1);
                    }

                    return Mat_detail_list;
                }
            }
            catch (Exception ex)
            {
                 Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return null;
            }
            
        }

        public List<test> FindAllMaterial_Test()
        {
            List<test> Mat_detail_list = new List<test>();

            try
            {

                using (con = new SqlConnection(connection_string))
                {

//                    cmd = new SqlCommand(@"select * from 
//(select pivottable.material_id,pivottable.material,pivottable.material_variance_id,pivottable.variance,pivottable.subdiv_id,pivottable.subdivision,
//pivottable.unit,pivottable.city_id,
//pivottable.city_name,pivottable.country,pivottable.currency,pivottable.[1]  AS Outsourcing, pivottable.[2]  AS Material1, pivottable.[3] AS Material2, 
//pivottable.[4] AS Labor1, pivottable.[5] AS Labor2, pivottable.[6] AS Other
//from (select M.id as material_id,M.name as material, MV.id as material_variance_id,MV.variance as variance,MVS.id as subdiv_id,MVS.name as subdivision,
//(select unit from Unit_Of_Measurement where id = MVS.unit_of_measurement) as unit,MVS.city_id as city_id,
//(select name from City where id = MVS.city_id) as city_name,CM.cost as Cost,CM.material_option_id as material_opt,
//(select country from Country_Code where id = (select country_id from City where id = MVS.city_id)) as country,
//(select currency from Country_Code where id = (select country_id from City where id = MVS.city_id)) as currency
//from Material M
//LEFT OUTER JOIN Material_Variance MV 
//ON M.id= MV.material_id
//LEFT OUTER JOIN Material_Variance_Subdivision MVS
//ON MV.id = MVS.material_variance_id
//LEFT JOIN Cost_Master CM ON MVS.id = CM.material_variance_subdiv_id and CURRENT_TIMESTAMP BETWEEN CM.eff_date AND CM.eff_date_end
//where f_del = 0
//) src 
//PIVOT(Max([Cost]) 
//FOR material_opt IN([1],[2],[3],[4],[5],[6])) as pivottable)
//AS test order by country", con);

                    cmd = new SqlCommand(@"select * from 
(select pivottable.material_id,pivottable.material,pivottable.material_seq,pivottable.material_variance_id,pivottable.variance,pivottable.material_var_seq,
pivottable.subdiv_id,pivottable.subdivision,pivottable.material_subdiv_seq,
pivottable.unit,pivottable.city_id,
pivottable.city_name,pivottable.country,pivottable.currency,pivottable.[1]  AS Outsourcing, pivottable.[2]  AS Material1, pivottable.[3] AS Material2, 
pivottable.[4] AS Labor1, pivottable.[5] AS Labor2, pivottable.[6] AS Other
from (select M.id as material_id,M.name as material,M.seq as material_seq, MV.id as material_variance_id,MV.variance as variance,MV.seq as material_var_seq,
MVS.id as subdiv_id,MVS.name as subdivision,MVS.seq as material_subdiv_seq,
(select unit from Unit_Of_Measurement where id = MVS.unit_of_measurement) as unit,MVS.city_id as city_id,
(select name from City where id = MVS.city_id) as city_name,CM.cost as Cost,CM.material_option_id as material_opt,
(select country from Country_Code where id = (select country_id from City where id = MVS.city_id)) as country,
(select currency from Country_Code where id = (select country_id from City where id = MVS.city_id)) as currency
from Material M
LEFT OUTER JOIN Material_Variance MV 
ON M.id= MV.material_id
LEFT OUTER JOIN Material_Variance_Subdivision MVS
ON MV.id = MVS.material_variance_id
LEFT JOIN Cost_Master CM ON MVS.id = CM.material_variance_subdiv_id and CURRENT_TIMESTAMP BETWEEN CM.eff_date AND CM.eff_date_end
where f_del = 0
) src 
PIVOT(Max([Cost]) 
FOR material_opt IN([1],[2],[3],[4],[5],[6])) as pivottable)
AS test order by country,material_seq,material_var_seq,material_subdiv_seq ", con);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            test obj = new test();
                            obj.material_id = Convert.ToInt64(reader["material_id"]);
                            obj.material = reader["material"].ToString();
                            obj.material_variance_id = Convert.ToInt64(reader["material_variance_id"]);
                            obj.variance = reader["variance"].ToString();
                            obj.subdiv_id = Convert.ToInt64(reader["subdiv_id"]);
                            obj.subdivision = reader["subdivision"].ToString();
                            obj.city_id = Convert.ToInt64(reader["city_id"]);
                            obj.city_name = reader["city_name"].ToString();
                            obj.unit = reader["unit"].ToString();
                            obj.Outsourcing = reader["Outsourcing"] == System.DBNull.Value ? 0 : Convert.ToDecimal(reader["Outsourcing"]);
                            obj.Material1 = reader["Material1"] == System.DBNull.Value ? 0 : Convert.ToDecimal(reader["Material1"]);
                            obj.Material2 = reader["Material2"] == System.DBNull.Value ? 0 : Convert.ToDecimal(reader["Material2"]);
                            obj.Labor1 = reader["Labor1"] == System.DBNull.Value ? 0 : Convert.ToDecimal(reader["Labor1"]);
                            obj.Labor2 = reader["Labor2"] == System.DBNull.Value ? 0 : Convert.ToDecimal(reader["Labor2"]);
                            obj.Other = reader["Other"] == System.DBNull.Value ? 0 : Convert.ToDecimal(reader["Other"]);
                            obj.country = reader["country"].ToString();
                            obj.currency = reader["currency"].ToString();
                            Mat_detail_list.Add(obj);
                        }
                    }

                    return Mat_detail_list;
                }
            }
            catch (Exception ex)
            {
                return Mat_detail_list;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }




    }
}
