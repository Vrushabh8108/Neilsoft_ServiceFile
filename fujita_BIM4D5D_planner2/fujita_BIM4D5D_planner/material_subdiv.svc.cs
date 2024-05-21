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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service4" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service4.svc or Service4.svc.cs at the Solution Explorer and start debugging.
    public class Service4 : material_subdiv
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        Mat_sub_div mat_subdiv = new Mat_sub_div();
        public Mat_sub_div Findmaterialsub(string mat,string mat_var,string city_name,string country_name)
        {
            try
            {
                using (con = new SqlConnection(connection_string))
                {
                    con.Open();
                    cmd = new SqlCommand(@"(SELECT (SELECT NAME 
                             FROM   material mat 
                             WHERE  mat.id = (SELECT material_id 
                                              FROM   material_variance 
                                              WHERE  id = 
                            mat_var_sub.material_variance_id))material, 
                            (SELECT variance 
                             FROM   material_variance mat_var 
                             WHERE  mat_var.id = mat_var_sub.material_variance_id) 
                            material_variance, 
                            NAME, 
                            (SELECT UNIT FROM UNIT_OF_MEASUREMENT WHERE ID = unit_of_measurement ),
                            specification
                     FROM   material_variance_subdivision mat_var_sub 
                     WHERE  material_variance_id = (SELECT id 
                                                    FROM   material_variance 
                                                    WHERE  variance = N'" + @mat_var + @"' 
                                                           AND material_id = (SELECT id 
                                                                              FROM   material 
                                                                              WHERE  NAME = 
                                                                             N'" + @mat + @"'))
                    and f_del = 0 and city_id = (SELECT id FROM City WHERE name = N'" + @city_name + @"' AND country_id = (SELECT id FROM Country_Code WHERE country = N'" + @country_name + @"'))) 
                    ORDER  BY mat_var_sub.seq ", con);
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable("Paging");
                    sda.Fill(dt);
                    mat_subdiv.Mat_sub_division = dt;

                }

                return mat_subdiv;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    Service17 exception1 = new Service17();
                    exception1.SendErrorToText(ex);
                }

                return null;
            }
        }

        public Mat_sub_div Findmaterialsub_proj(string mat, string mat_var,string proj_id,string proj_name)
        {
            try
            {
                using (con = new SqlConnection(connection_string))
                {
                    #region
                    //                cmd = new SqlCommand(@"(SELECT (SELECT NAME 
                    //                             FROM   material mat 
                    //                             WHERE  mat.id = (SELECT material_id 
                    //                                              FROM   material_variance 
                    //                                              WHERE  id = 
                    //                            mat_var_sub.material_variance_id))material, 
                    //                            (SELECT variance 
                    //                             FROM   material_variance mat_var 
                    //                             WHERE  mat_var.id = mat_var_sub.material_variance_id) 
                    //                            material_variance, 
                    //                            NAME, 
                    //                            (SELECT UNIT FROM UNIT_OF_MEASUREMENT WHERE ID = unit_of_measurement ),
                    //                            specification
                    //                     FROM   material_variance_subdivision mat_var_sub 
                    //                     WHERE  material_variance_id = (SELECT id 
                    //                                                    FROM   material_variance 
                    //                                                    WHERE  variance = '" + @mat_var + @"' 
                    //                                                           AND material_id = (SELECT id 
                    //                                                                              FROM   material 
                    //                                                                              WHERE  NAME = 
                    //                                                                             '" + @mat + @"'))
                    //                    and f_del = 0 and created_on <= (select created_on from Project where proj_guid='1d974444-cd41-4759-92c5-e366a9405ee0' and name='STtestmodelAddSubPW') and exists
                    //                    (select 1 from cost_master_project where CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end 
                    //                     and material_variance_subdiv_master_id = mat_var_sub.id and project_id in (select id from project where 
                    //					 proj_guid=N'" + proj_id+@"' and name=N'"+proj_name+@"')))
                    //                    ORDER  BY mat_var_sub.seq ", con);
                    #endregion
                    con.Open();
                    

                    SqlCommand cmd_cityid = new SqlCommand("select city_id from Project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"';", con);
                    long city_id = (long)cmd_cityid.ExecuteScalar();

                    

                    cmd = new SqlCommand(@"(SELECT (SELECT NAME 
                             FROM   material mat 
                             WHERE  mat.id = (SELECT material_id 
                                              FROM   material_variance 
                                              WHERE  id = 
                            mat_var_sub.material_variance_id))material, 
                            (SELECT variance 
                             FROM   material_variance mat_var 
                             WHERE  mat_var.id = mat_var_sub.material_variance_id) 
                            material_variance, 
                            NAME, 
                            (SELECT UNIT FROM UNIT_OF_MEASUREMENT WHERE ID = unit_of_measurement ),
                            specification,seq
                     FROM   material_variance_subdivision mat_var_sub 
                     WHERE  material_variance_id = (SELECT id 
                                                    FROM   material_variance 
                                                    WHERE  variance = N'" + @mat_var + @"' 
                                                           AND material_id = (SELECT id 
                                                                              FROM   material 
                                                                              WHERE  NAME = 
                                                                             N'" + @mat + @"'))
                    and f_del = 0 and city_id = "+city_id+@" and 
					created_on <= (select created_on from Project where proj_guid=N'" + proj_id + @"' and 
					name=N'" + proj_name + @"') and  NOT EXISTS (SELECT 1 
                   FROM   Material_Variance_Subdivision_Project 
                   WHERE  mat_var_sub.name = Material_Variance_Subdivision_Project.name and Material_Variance_Subdivision_Project.Project_id in (select id from project where proj_guid = N'" + proj_id + @"' and name = N'" + proj_name + @"') and Material_Variance_Subdivision_Project.f_del = 1))
				    UNION  
					 (SELECT (SELECT NAME 
                             FROM   material mat 
                             WHERE  mat.id = (SELECT material_id 
                                              FROM   material_variance 
                                              WHERE  id = 
                            mat_var_sub.material_variance_id))material, 
                            (SELECT variance 
                             FROM   material_variance  mat_var 
                             WHERE  mat_var.id = mat_var_sub.material_variance_id) 
                            material_variance, 
                            NAME, 
                            (SELECT UNIT FROM UNIT_OF_MEASUREMENT WHERE ID = unit_of_measurement ),
                            specification,seq
                     FROM   material_variance_subdivision_Project mat_var_sub 
                     WHERE  material_variance_id = (SELECT id 
                                                    FROM   material_variance 
                                                    WHERE  variance = N'" + @mat_var + @"' 
                                                           AND material_id = (SELECT id 
                                                                              FROM   material 
                                                                              WHERE  NAME = 
                                                                             N'" + @mat + @"'))
                    and f_del = 0 and project_id = (select id from project where proj_guid = N'" + proj_id + @"' and name = N'" + proj_name + @"'))

				   order by mat_var_sub.seq", con);

                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable("Paging");
                    sda.Fill(dt);
                    mat_subdiv.Mat_sub_division = dt;
                }

                return mat_subdiv;

            }
            catch (Exception ex)
            {
               if (con.State == ConnectionState.Open)
                {
                    con.Close();

                }
                return null;
            }

        }
        public Mat_sub_div Findmaterialsub_categoryid(Int64 mat_id, Int64 mat_var)
        {
            using (con = new SqlConnection(connection_string))
            {
                cmd = new SqlCommand(@"(SELECT (SELECT NAME 
                             FROM   material mat 
                             WHERE  mat.id = (SELECT material_id 
                                              FROM   material_variance 
                                              WHERE  id = 
                            mat_var_sub.material_variance_id))material, 
                            (SELECT variance 
                             FROM   material_variance mat_var 
                             WHERE  mat_var.id = mat_var_sub.material_variance_id) 
                            material_variance, 
                            NAME, 
                            (SELECT UNIT FROM UNIT_OF_MEASUREMENT WHERE ID = unit_of_measurement ),
                            specification
                     FROM   material_variance_subdivision mat_var_sub 
                     WHERE  material_variance_id = (SELECT id 
                                                    FROM   material_variance 
                                                    WHERE  category_id = -1 *" + mat_var + @"
                                                           AND material_id = (SELECT id 
                                                                              FROM   material 
                                                                              WHERE  category_id = -1 *" + mat_id + @"))
                    and f_del = 0) 
                    ORDER  BY mat_var_sub.seq ", con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable("Paging");
                sda.Fill(dt);
                mat_subdiv.Mat_sub_division = dt;

            }

            return mat_subdiv;
        }
        public Mat_sub_div Findmaterialsub_categoryid_name(Int64 mat_id, string mat_var)
        {
            using (con = new SqlConnection(connection_string))
            {
                cmd = new SqlCommand(@"(SELECT (SELECT NAME 
                             FROM   material mat 
                             WHERE  mat.id = (SELECT material_id 
                                              FROM   material_variance 
                                              WHERE  id = 
                            mat_var_sub.material_variance_id))material, 
                            (SELECT variance 
                             FROM   material_variance mat_var 
                             WHERE  mat_var.id = mat_var_sub.material_variance_id) 
                            material_variance, 
                            NAME, 
                            (SELECT UNIT FROM UNIT_OF_MEASUREMENT WHERE ID = unit_of_measurement ),
                            specification
                     FROM   material_variance_subdivision mat_var_sub 
                     WHERE  material_variance_id = (SELECT id 
                                                    FROM   material_variance 
                                                    WHERE  variance = '" + @mat_var + @"' 
                                                           AND material_id = (SELECT id 
                                                                              FROM   material 
                                                                              WHERE  category_id = -1 *" + mat_id + @"))
                    and f_del = 0) 
                    ORDER  BY mat_var_sub.seq ", con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable("Paging");
                sda.Fill(dt);
                mat_subdiv.Mat_sub_division = dt;

            }

            return mat_subdiv;
        }
    }
}
