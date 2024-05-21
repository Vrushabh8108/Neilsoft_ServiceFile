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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service3" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service3.svc or Service3.svc.cs at the Solution Explorer and start debugging.
    public class Service3 : cost_insert_update
    {
        int checkMaster = 0;
        int checkProject = 0;
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();

//        public void cost_ins_upd_NotCitySpecific(String mat_name, String mat_var_name, string mat_var_sub, string mat_var_opt, decimal cost, string user_info, string country_code, string city_name)
//        {
//            SqlConnection conn = new SqlConnection(connection_string);
//            ConnectionState state = conn.State;
//            try
//            {
//                List<string> MaterialDetails = new List<string>();
//                if (mat_var_sub.Contains("'"))
//                {
//                    mat_var_sub = mat_var_sub.Replace("'", "''");
//                }
//                conn.Open();
//                SqlCommand cmd = new SqlCommand((@"DECLARE @cnt INT; 
//
//                    BEGIN 
//                        SELECT @cnt = Count(*) 
//                        FROM   cost_master 
//                        WHERE  material_option_id = (SELECT id 
//                                                     FROM   material_option 
//                                                     WHERE  NAME = N'" + @mat_var_opt + @"') 
//                               AND material_variance_subdiv_id = 
//                                   (SELECT id 
//                                    FROM 
//                                   material_variance_subdivision 
//                                                                  WHERE f_del=0 and NAME = 
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
//                               AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end  and city_id in (select id from city where country_id in (select id from  country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"');
//
//
//                        IF @cnt = 0 
//                          INSERT INTO cost_master 
//                                      (material_variance_subdiv_id, 
//                                       eff_date, 
//                                       eff_date_end, 
//                                       cost, 
//                                       modified_by, 
//                                       modified_on, 
//                                       material_option_id,
//                                       city_id) 
//                          VALUES     ((SELECT id 
//                                       FROM   material_variance_subdivision 
//                                       WHERE f_del=0 and NAME = N'" + @mat_var_sub + @"' 
//                                              AND material_variance_id = (SELECT id 
//                                                                          FROM   material_variance 
//                                                                          WHERE 
//                                                  variance = N'" + @mat_var_name + @"' 
//                                                  AND material_id = (SELECT id 
//                                                                     FROM   material 
//                                                                     WHERE  NAME = 
//                                                      N'" + @mat_name + @"'))), 
//                                      CURRENT_TIMESTAMP, 
//                                      '9999-12-31 23:59:00.000', 
//                                      " + @cost + @", 
//                                      N'" + @user_info + @"', 
//                                      CURRENT_TIMESTAMP, 
//                                      (SELECT id 
//                                       FROM   material_option 
//                                       WHERE  NAME = N'" + @mat_var_opt + @"'),
//                                     (select id from city where country_id in (select id from country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"')); 
//                        ELSE 
//                          BEGIN 
//                              UPDATE cost_master 
//                              SET    eff_date_end = DateADD(Minute, -1, Current_TimeStamp) 
//                              WHERE  material_option_id = (SELECT id 
//                                                           FROM   material_option 
//                                                           WHERE  NAME = N'" + @mat_var_opt + @"') 
//                                     AND material_variance_subdiv_id = 
//                                         (SELECT id 
//                                          FROM 
//                                         material_variance_subdivision 
//                                                                        WHERE f_del=0 and NAME = 
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
//                                     AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end and city_id in (select id from city where country_id in (select id from country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"');
//                    
//                              INSERT INTO cost_master 
//                                          (material_variance_subdiv_id, 
//                                           eff_date, 
//                                           eff_date_end, 
//                                           cost, 
//                                           modified_by, 
//                                           modified_on, 
//                                           material_option_id,
//                                           city_id) 
//                              VALUES     ((SELECT id 
//                                           FROM   material_variance_subdivision 
//                                           WHERE  f_del = 0 and NAME = N'" + @mat_var_sub + @"' 
//                                                  AND material_variance_id = (SELECT id 
//                                                                              FROM 
//                                                      material_variance 
//                                                                              WHERE 
//                                                      variance = N'" + @mat_var_name + @"' 
//                                                      AND material_id = (SELECT id 
//                                                                         FROM   material 
//                                                                         WHERE  NAME = 
//                                                          N'" + @mat_name + @"'))), 
//                                          CURRENT_TIMESTAMP, 
//                                          '9999-12-31 23:59:00.000', 
//                                          " + @cost + @", 
//                                          N'" + @user_info + @"', 
//                                          CURRENT_TIMESTAMP, 
//                                          (SELECT id 
//                                           FROM   material_option 
//                                           WHERE  NAME = N'" + @mat_var_opt + @"'),
//                                           (select id from city where country_id in (select id from country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"')); 
//                          END; 
//                    END; "), conn);
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

        public void cost_ins_upd(String mat_name, String mat_var_name, string mat_var_sub, string mat_var_opt, decimal cost, string user_info, string country_code, string city_name)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                List<string> MaterialDetails = new List<string>();
                if (mat_var_sub.Contains("'"))
                {
                    mat_var_sub = mat_var_sub.Replace("'", "''");
                }
                conn.Open();
                SqlCommand cmd = new SqlCommand((@"DECLARE @cnt INT; 

                    BEGIN 
                        SELECT @cnt = Count(*) 
                        FROM   cost_master 
                        WHERE  material_option_id = (SELECT id 
                                                     FROM   material_option 
                                                     WHERE  NAME = N'" + @mat_var_opt + @"') 
                               AND material_variance_subdiv_id = 
                                   (SELECT id 
                                    FROM 
                                   material_variance_subdivision 
                                                                  WHERE f_del=0 and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_code + @"')) and NAME = 
                                   N'" + @mat_var_sub + @"' 
                                                                         AND 
                                       material_variance_id = (SELECT id 
                                                               FROM   material_variance 
                                                               WHERE 
                                       variance = N'" + @mat_var_name + @"' 
                                       AND material_id = (SELECT id 
                                                          FROM   material 
                                                          WHERE  NAME = 
                                           N'" + @mat_name + @"'))) 
                               AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end  and city_id in (select id from city where country_id in (select id from  country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"');


                        IF @cnt = 0 
                          INSERT INTO cost_master 
                                      (material_variance_subdiv_id, 
                                       eff_date, 
                                       eff_date_end, 
                                       cost, 
                                       modified_by, 
                                       modified_on, 
                                       material_option_id,
                                       city_id) 
                          VALUES     ((SELECT id 
                                       FROM   material_variance_subdivision 
                                       WHERE f_del=0 and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_code + "')) and NAME = N'" + @mat_var_sub + @"' 
                                              AND material_variance_id = (SELECT id 
                                                                          FROM   material_variance 
                                                                          WHERE 
                                                  variance = N'" + @mat_var_name + @"' 
                                                  AND material_id = (SELECT id 
                                                                     FROM   material 
                                                                     WHERE  NAME = 
                                                      N'" + @mat_name + @"'))), 
                                      CURRENT_TIMESTAMP, 
                                      '9999-12-31 23:59:00.000', 
                                      " + @cost + @", 
                                      N'" + @user_info + @"', 
                                      CURRENT_TIMESTAMP, 
                                      (SELECT id 
                                       FROM   material_option 
                                       WHERE  NAME = N'" + @mat_var_opt + @"'),
                                     (select id from city where country_id in (select id from country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"')); 
                        ELSE 
                          BEGIN 
                              UPDATE cost_master 
                              SET    eff_date_end = DateADD(Minute, -1, Current_TimeStamp) 
                              WHERE  material_option_id = (SELECT id 
                                                           FROM   material_option 
                                                           WHERE  NAME = N'" + @mat_var_opt + @"') 
                                     AND material_variance_subdiv_id = 
                                         (SELECT id 
                                          FROM 
                                         material_variance_subdivision 
                                                                        WHERE f_del=0 and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_code + @"')) and NAME = 
                                         N'" + @mat_var_sub + @"' 
                                                                               AND 
                                             material_variance_id = (SELECT id 
                                                                     FROM   material_variance 
                                                                     WHERE 
                                             variance = N'" + @mat_var_name + @"' 
                                             AND material_id = (SELECT id 
                                                                FROM   material 
                                                                WHERE  NAME = 
                                                 N'" + @mat_name + @"'))) 
                                     AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end and city_id in (select id from city where country_id in (select id from country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"');
                    
                              INSERT INTO cost_master 
                                          (material_variance_subdiv_id, 
                                           eff_date, 
                                           eff_date_end, 
                                           cost, 
                                           modified_by, 
                                           modified_on, 
                                           material_option_id,
                                           city_id) 
                              VALUES     ((SELECT id 
                                           FROM   material_variance_subdivision 
                                           WHERE  f_del = 0 and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_code + @"')) and NAME = N'" + @mat_var_sub + @"' 
                                                  AND material_variance_id = (SELECT id 
                                                                              FROM 
                                                      material_variance 
                                                                              WHERE 
                                                      variance = N'" + @mat_var_name + @"' 
                                                      AND material_id = (SELECT id 
                                                                         FROM   material 
                                                                         WHERE  NAME = 
                                                          N'" + @mat_name + @"'))), 
                                          CURRENT_TIMESTAMP, 
                                          '9999-12-31 23:59:00.000', 
                                          " + @cost + @", 
                                          N'" + @user_info + @"', 
                                          CURRENT_TIMESTAMP, 
                                          (SELECT id 
                                           FROM   material_option 
                                           WHERE  NAME = N'" + @mat_var_opt + @"'),
                                           (select id from city where country_id in (select id from country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"')); 
                          END; 
                    END;"), conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (System.Exception ex)
            {
                if (state == ConnectionState.Open)
                {
                    conn.Close();

                }
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);

            }

        }

//        public void cost_ins_upd_proj_28thoct22(String mat_name, String mat_var_name, string mat_var_sub, string mat_var_opt, decimal cost, string user_info, string country_code, string city_name, string proj_id, string proj_name)
//        {
//            SqlConnection conn = new SqlConnection(connection_string);
//            ConnectionState state = conn.State;
//            try
//            {
//                List<string> MaterialDetails = new List<string>();
//                conn.Open();
//                SqlCommand cmd = new SqlCommand((@"DECLARE @cnt INT; 
//
//                    BEGIN 
//                        SELECT @cnt = Count(*) 
//                        FROM   Cost_Master_Project
//                        WHERE  material_option_id = (SELECT id 
//                                                     FROM   material_option 
//                                                     WHERE  NAME = N'" + @mat_var_opt + @"') 
//                               and Project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"')
//                               AND material_variance_subdiv_id = 
//                                   (SELECT id 
//                                    FROM 
//                                   material_variance_subdivision 
//                                                                  WHERE f_del=0 and NAME = 
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
//                               AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end  and city_id in (select id from city where country_id in (select id from  country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"');
//
//
//                        IF @cnt = 0 
//                          INSERT INTO Cost_Master_Project 
//                                      (material_variance_subdiv_id, 
//                                       eff_date, 
//                                       eff_date_end, 
//                                       cost, 
//                                       modified_by, 
//                                       modified_on, 
//                                       material_option_id,
//                                       city_id,
//                                       project_id ) 
//                          VALUES     ((SELECT id 
//                                       FROM   material_variance_subdivision 
//                                       WHERE f_del=0 and NAME = N'" + @mat_var_sub + @"' 
//                                              AND material_variance_id = (SELECT id 
//                                                                          FROM   material_variance 
//                                                                          WHERE 
//                                                  variance = N'" + @mat_var_name + @"' 
//                                                  AND material_id = (SELECT id 
//                                                                     FROM   material 
//                                                                     WHERE  NAME = 
//                                                      N'" + @mat_name + @"'))), 
//                                      CURRENT_TIMESTAMP, 
//                                      '9999-12-31 23:59:00.000', 
//                                      " + @cost + @", 
//                                      N'" + @user_info + @"', 
//                                      CURRENT_TIMESTAMP, 
//                                      (SELECT id 
//                                       FROM   material_option 
//                                       WHERE  NAME = N'" + @mat_var_opt + @"'),
//                                     (select id from city where country_id in (select id from country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"'),
//                                     (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"')
//                                    ); 
//                        ELSE 
//                          BEGIN 
//                              UPDATE Cost_master_project 
//                              SET    eff_date_end = DateADD(Minute, -1, Current_TimeStamp) 
//                              WHERE  material_option_id = (SELECT id 
//                                                           FROM   material_option 
//                                                           WHERE  NAME = N'" + @mat_var_opt + @"') 
//                                     and project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"')
//                                     AND material_variance_subdiv_id = 
//                                         (SELECT id 
//                                          FROM 
//                                         material_variance_subdivision 
//                                                                        WHERE f_del=0 and NAME = 
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
//                                     AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end and city_id in (select id from city where country_id in (select id from country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"');
//                    
//                              INSERT INTO Cost_master_project 
//                                          (material_variance_subdiv_id, 
//                                           eff_date, 
//                                           eff_date_end, 
//                                           cost, 
//                                           modified_by, 
//                                           modified_on, 
//                                           material_option_id,
//                                           city_id,
//                                           project_id) 
//                              VALUES     ((SELECT id 
//                                           FROM   material_variance_subdivision 
//                                           WHERE  f_del = 0 and NAME = N'" + @mat_var_sub + @"' 
//                                                  AND material_variance_id = (SELECT id 
//                                                                              FROM 
//                                                      material_variance 
//                                                                              WHERE 
//                                                      variance = N'" + @mat_var_name + @"' 
//                                                      AND material_id = (SELECT id 
//                                                                         FROM   material 
//                                                                         WHERE  NAME = 
//                                                          N'" + @mat_name + @"'))), 
//                                          CURRENT_TIMESTAMP, 
//                                          '9999-12-31 23:59:00.000', 
//                                          " + @cost + @", 
//                                          N'" + @user_info + @"', 
//                                          CURRENT_TIMESTAMP, 
//                                          (SELECT id 
//                                           FROM   material_option 
//                                           WHERE  NAME = N'" + @mat_var_opt + @"'),
//                                           (select id from city where country_id in (select id from country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"'),
//                                           (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"')); 
//                          END; 
//                    END; "), conn);
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

//        public void cost_ins_upd_proj(String mat_name, String mat_var_name, string mat_var_sub, string mat_var_opt, decimal cost, string user_info, string country_code, string city_name, string proj_id, string proj_name)
//        {
//            SqlConnection conn = new SqlConnection(connection_string);
//            ConnectionState state = conn.State;
//            try
//            {
//                List<string> MaterialDetails = new List<string>();
//                conn.Open();

//                string query = "SELECT COUNT(id) from Material_Variance_Subdivision where f_del = 0 AND name =N'" + @mat_var_sub + @"' AND created_on <= (select created_on from Project where proj_guid =N'" + proj_id + "' and name = N'" + proj_name + @"') AND material_variance_id =(select id from Material_Variance WHERE variance = N'" + @mat_var_name + @"' and material_id =(select id from Material where name =N'" + @mat_name + @"'))";

//                using (SqlCommand cmdMaster = new SqlCommand(query, conn))
//                {
//                    checkMaster = (int)cmdMaster.ExecuteScalar();
//                }

//                if (checkMaster == 0)
//                {
//                    query = "SELECT COUNT(id) from Material_Variance_Subdivision_Project where f_del = 0 AND name =N'" + @mat_var_sub + @"' AND material_variance_id =(select id from Material_Variance WHERE variance = N'" + @mat_var_name + @"' and material_id =(select id from Material where name =N'" + @mat_name + @"'))";
//                    using (SqlCommand cmdProject = new SqlCommand(query, conn))
//                    {
//                        checkProject = (int)cmdProject.ExecuteScalar();
//                    }

//                }
//                if (checkMaster == 1)
//                {
//                    SqlCommand cmd = new SqlCommand((@"DECLARE @cnt INT; 
//
//                    BEGIN 
//                        SELECT @cnt = Count(*) 
//                        FROM   Cost_Master_Project
//                        WHERE  material_option_id = (SELECT id 
//                                                     FROM   material_option 
//                                                     WHERE  NAME = N'" + @mat_var_opt + @"') 
//                               and Project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"')
//                               AND material_variance_subdiv_id = 
//                                   (SELECT id 
//                                    FROM 
//                                   material_variance_subdivision 
//                                                                  WHERE f_del=0 and NAME = 
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
//                               AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end  and city_id in (select id from city where country_id in (select id from  country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"');
//
//
//
//                        IF @cnt = 0 
//                          INSERT INTO Cost_Master_Project 
//                                      (material_variance_subdiv_id, 
//                                       eff_date, 
//                                       eff_date_end, 
//                                       cost, 
//                                       modified_by, 
//                                       modified_on, 
//                                       material_option_id,
//                                       city_id,
//                                       project_id ) 
//                          VALUES     ((SELECT id 
//                                       FROM   material_variance_subdivision 
//                                       WHERE f_del=0 and NAME = N'" + @mat_var_sub + @"' 
//                                              AND material_variance_id = (SELECT id 
//                                                                          FROM   material_variance 
//                                                                          WHERE 
//                                                  variance = N'" + @mat_var_name + @"' 
//                                                  AND material_id = (SELECT id 
//                                                                     FROM   material 
//                                                                     WHERE  NAME = 
//                                                      N'" + @mat_name + @"'))), 
//                                      CURRENT_TIMESTAMP, 
//                                      '9999-12-31 23:59:00.000', 
//                                      " + @cost + @", 
//                                      N'" + @user_info + @"', 
//                                      CURRENT_TIMESTAMP, 
//                                      (SELECT id 
//                                       FROM   material_option 
//                                       WHERE  NAME = N'" + @mat_var_opt + @"'),
//                                     (select id from city where country_id in (select id from country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"'),
//                                     (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"')
//                                    ); 
//                        ELSE 
//                          BEGIN 
//                              UPDATE Cost_master_project 
//                              SET    eff_date_end = DateADD(Minute, -1, Current_TimeStamp) 
//                              WHERE  material_option_id = (SELECT id 
//                                                           FROM   material_option 
//                                                           WHERE  NAME = N'" + @mat_var_opt + @"') 
//                                     and project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"')
//                                     AND material_variance_subdiv_id = 
//                                         (SELECT id 
//                                          FROM 
//                                         material_variance_subdivision 
//                                                                        WHERE f_del=0 and NAME = 
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
//                                     AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end and city_id in (select id from city where country_id in (select id from country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"');
//                    
//                              INSERT INTO Cost_master_project 
//                                          (material_variance_subdiv_id, 
//                                           eff_date, 
//                                           eff_date_end, 
//                                           cost, 
//                                           modified_by, 
//                                           modified_on, 
//                                           material_option_id,
//                                           city_id,
//                                           project_id) 
//                              VALUES     ((SELECT id 
//                                           FROM   material_variance_subdivision 
//                                           WHERE  f_del = 0 and NAME = N'" + @mat_var_sub + @"' 
//                                                  AND material_variance_id = (SELECT id 
//                                                                              FROM 
//                                                      material_variance 
//                                                                              WHERE 
//                                                      variance = N'" + @mat_var_name + @"' 
//                                                      AND material_id = (SELECT id 
//                                                                         FROM   material 
//                                                                         WHERE  NAME = 
//                                                          N'" + @mat_name + @"'))), 
//                                          CURRENT_TIMESTAMP, 
//                                          '9999-12-31 23:59:00.000', 
//                                          " + @cost + @", 
//                                          N'" + @user_info + @"', 
//                                          CURRENT_TIMESTAMP, 
//                                          (SELECT id 
//                                           FROM   material_option 
//                                           WHERE  NAME = N'" + @mat_var_opt + @"'),
//                                           (select id from city where country_id in (select id from country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"'),
//                                           (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"')); 
//                          END; 
//                    END; "), conn);
//                    cmd.ExecuteNonQuery();
//                    conn.Close();
//                }

//                if (checkProject == 1)
//                {
//                    SqlCommand cmd = new SqlCommand((@"DECLARE @cnt INT; 
//
//                    BEGIN 
//                        SELECT @cnt = Count(*) 
//                        FROM   Cost_Master_Project
//                        WHERE  material_option_id = (SELECT id 
//                                                     FROM   material_option 
//                                                     WHERE  NAME = N'" + @mat_var_opt + @"') 
//                               and Project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"')
//                               AND material_variance_subdiv_id = 
//                                   (SELECT id 
//                                    FROM 
//                                   material_variance_subdivision_Project 
//                                                                  WHERE f_del=0 and NAME = 
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
//                               AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end  and city_id in (select id from city where country_id in (select id from  country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"');
//
//
//
//                        IF @cnt = 0 
//                          INSERT INTO Cost_Master_Project 
//                                      (material_variance_subdiv_id, 
//                                       eff_date, 
//                                       eff_date_end, 
//                                       cost, 
//                                       modified_by, 
//                                       modified_on, 
//                                       material_option_id,
//                                       city_id,
//                                       project_id,material_variance_subdiv_id_proj) 
//                          VALUES     (null, 
//                                      CURRENT_TIMESTAMP, 
//                                      '9999-12-31 23:59:00.000', 
//                                      " + @cost + @", 
//                                      N'" + @user_info + @"', 
//                                      CURRENT_TIMESTAMP, 
//                                      (SELECT id 
//                                       FROM   material_option 
//                                       WHERE  NAME = N'" + @mat_var_opt + @"'),
//                                     (select id from city where country_id in (select id from country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"'),
//                                     (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"'),(SELECT id 
//                                       FROM   material_variance_subdivision_Project 
//                                       WHERE f_del=0 and NAME = N'" + @mat_var_sub + @"' 
//                                              AND material_variance_id = (SELECT id 
//                                                                          FROM   material_variance 
//                                                                          WHERE 
//                                                  variance = N'" + @mat_var_name + @"' 
//                                                  AND material_id = (SELECT id 
//                                                                     FROM   material 
//                                                                     WHERE  NAME = 
//                                                      N'" + @mat_name + @"')))
//                                    ); 
//                        ELSE 
//                          BEGIN 
//                              UPDATE Cost_master_project 
//                              SET    eff_date_end = DateADD(Minute, -1, Current_TimeStamp) 
//                              WHERE  material_option_id = (SELECT id 
//                                                           FROM   material_option 
//                                                           WHERE  NAME = N'" + @mat_var_opt + @"') 
//                                     and project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"')
//                                     AND material_variance_subdiv_id_proj = 
//                                         (SELECT id 
//                                          FROM 
//                                         material_variance_subdivision_Project 
//                                                                        WHERE f_del=0 and NAME = 
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
//                                     AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end and city_id in (select id from city where country_id in (select id from country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"');
//                    
//                              INSERT INTO Cost_master_project 
//                                          (material_variance_subdiv_id, 
//                                           eff_date, 
//                                           eff_date_end, 
//                                           cost, 
//                                           modified_by, 
//                                           modified_on, 
//                                           material_option_id,
//                                           city_id,
//                                           project_id,material_variance_subdiv_id_proj) 
//                              VALUES     (null, 
//                                          CURRENT_TIMESTAMP, 
//                                          '9999-12-31 23:59:00.000', 
//                                          " + @cost + @", 
//                                          N'" + @user_info + @"', 
//                                          CURRENT_TIMESTAMP, 
//                                          (SELECT id 
//                                           FROM   material_option 
//                                           WHERE  NAME = N'" + @mat_var_opt + @"'),
//                                           (select id from city where country_id in (select id from country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"'),
//                                           (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"'),(SELECT id 
//                                           FROM   material_variance_subdivision_Project 
//                                           WHERE  f_del = 0 and NAME = N'" + @mat_var_sub + @"' 
//                                                  AND material_variance_id = (SELECT id 
//                                                                              FROM 
//                                                      material_variance 
//                                                                              WHERE 
//                                                      variance = N'" + @mat_var_name + @"' 
//                                                      AND material_id = (SELECT id 
//                                                                         FROM   material 
//                                                                         WHERE  NAME = 
//                                                          N'" + @mat_name + @"')))); 
//                          END; 
//                    END; "), conn);
//                    cmd.ExecuteNonQuery();
//                    conn.Close();
//                }

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


        public void cost_ins_upd_proj(String mat_name, String mat_var_name, string mat_var_sub, string mat_var_opt, decimal cost, string user_info, string country_code, string city_name, string proj_id, string proj_name)
        {
            SqlConnection conn;
            //SqlConnection conn = new SqlConnection(connection_string);
            //ConnectionState state = conn.State;
            try
            {
                List<string> MaterialDetails = new List<string>();

                if (mat_var_sub.Contains("'"))
                {
                    mat_var_sub = mat_var_sub.Replace("'", "''");
                }

                using (conn = new SqlConnection(connection_string))
                {
                    try
                    {
                        conn.Open();

                        string query = "SELECT COUNT(id) from Material_Variance_Subdivision where f_del = 0 AND name =N'" + @mat_var_sub + @"' and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_code + "')) AND created_on <= (select created_on from Project where proj_guid =N'" + proj_id + "' and name = N'" + proj_name + @"') AND material_variance_id =(select id from Material_Variance WHERE variance = N'" + @mat_var_name + @"' and material_id =(select id from Material where name =N'" + @mat_name + @"'))";

                        using (SqlCommand cmdMaster = new SqlCommand(query, conn))
                        {
                            checkMaster = (int)cmdMaster.ExecuteScalar();
                        }

                        if (checkMaster == 0)
                        {
                            query = "SELECT COUNT(id) from Material_Variance_Subdivision_Project where f_del = 0 AND Project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"') AND name =N'" + @mat_var_sub + @"' AND material_variance_id =(select id from Material_Variance WHERE variance = N'" + @mat_var_name + @"' and material_id =(select id from Material where name =N'" + @mat_name + @"'))";
                            using (SqlCommand cmdProject = new SqlCommand(query, conn))
                            {
                                checkProject = (int)cmdProject.ExecuteScalar();
                            }
                        }
                        if (checkMaster == 1)
                        {
                            SqlCommand cmd = new SqlCommand((@"DECLARE @cnt INT; 

                    BEGIN 
                        SELECT @cnt = Count(*) 
                        FROM   Cost_Master_Project
                        WHERE  material_option_id = (SELECT id 
                                                     FROM   material_option 
                                                     WHERE  NAME = N'" + @mat_var_opt + @"') 
                               and Project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"')
                               AND material_variance_subdiv_id = 
                                   (SELECT id 
                                    FROM 
                                   material_variance_subdivision 
                                                                  WHERE f_del=0 and NAME = 
                                   N'" + @mat_var_sub + @"' and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_code + @"'))
                                                                         AND 
                                       material_variance_id = (SELECT id 
                                                               FROM   material_variance 
                                                               WHERE 
                                       variance = N'" + @mat_var_name + @"' 
                                       AND material_id = (SELECT id 
                                                          FROM   material 
                                                          WHERE  NAME = 
                                           N'" + @mat_name + @"'))) 
                               AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end  and city_id in (select id from city where country_id in (select id from  country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"');



                        IF @cnt = 0 
                          INSERT INTO Cost_Master_Project 
                                      (material_variance_subdiv_id, 
                                       eff_date, 
                                       eff_date_end, 
                                       cost, 
                                       modified_by, 
                                       modified_on, 
                                       material_option_id,
                                       city_id,
                                       project_id ) 
                          VALUES     ((SELECT id 
                                       FROM   material_variance_subdivision 
                                       WHERE f_del=0 and NAME = N'" + @mat_var_sub + @"' and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_code + @"'))
                                              AND material_variance_id = (SELECT id 
                                                                          FROM   material_variance 
                                                                          WHERE 
                                                  variance = N'" + @mat_var_name + @"' 
                                                  AND material_id = (SELECT id 
                                                                     FROM   material 
                                                                     WHERE  NAME = 
                                                      N'" + @mat_name + @"'))), 
                                      CURRENT_TIMESTAMP, 
                                      '9999-12-31 23:59:00.000', 
                                      " + @cost + @", 
                                      N'" + @user_info + @"', 
                                      CURRENT_TIMESTAMP, 
                                      (SELECT id 
                                       FROM   material_option 
                                       WHERE  NAME = N'" + @mat_var_opt + @"'),
                                     (select id from city where country_id in (select id from country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"'),
                                     (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"')
                                    ); 
                        ELSE 
                          BEGIN 
                              UPDATE Cost_master_project 
                              SET    eff_date_end = DateADD(Minute, -1, Current_TimeStamp) 
                              WHERE  material_option_id = (SELECT id 
                                                           FROM   material_option 
                                                           WHERE  NAME = N'" + @mat_var_opt + @"') 
                                     and project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"')
                                     AND material_variance_subdiv_id = 
                                         (SELECT id 
                                          FROM 
                                         material_variance_subdivision 
                                                                        WHERE f_del=0 and NAME = 
                                         N'" + @mat_var_sub + @"' and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_code + @"'))
                                                                               AND 
                                             material_variance_id = (SELECT id 
                                                                     FROM   material_variance 
                                                                     WHERE 
                                             variance = N'" + @mat_var_name + @"' 
                                             AND material_id = (SELECT id 
                                                                FROM   material 
                                                                WHERE  NAME = 
                                                 N'" + @mat_name + @"'))) 
                                     AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end and city_id in (select id from city where country_id in (select id from country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"');
                    
                              INSERT INTO Cost_master_project 
                                          (material_variance_subdiv_id, 
                                           eff_date, 
                                           eff_date_end, 
                                           cost, 
                                           modified_by, 
                                           modified_on, 
                                           material_option_id,
                                           city_id,
                                           project_id) 
                              VALUES     ((SELECT id 
                                           FROM   material_variance_subdivision 
                                           WHERE  f_del = 0 and NAME = N'" + @mat_var_sub + @"' and city_id = (select id from city where name = N'" + city_name + "' and country_id = (select id from Country_Code where country = N'" + country_code + @"'))
                                                  AND material_variance_id = (SELECT id 
                                                                              FROM 
                                                      material_variance 
                                                                              WHERE 
                                                      variance = N'" + @mat_var_name + @"' 
                                                      AND material_id = (SELECT id 
                                                                         FROM   material 
                                                                         WHERE  NAME = 
                                                          N'" + @mat_name + @"'))), 
                                          CURRENT_TIMESTAMP, 
                                          '9999-12-31 23:59:00.000', 
                                          " + @cost + @", 
                                          N'" + @user_info + @"', 
                                          CURRENT_TIMESTAMP, 
                                          (SELECT id 
                                           FROM   material_option 
                                           WHERE  NAME = N'" + @mat_var_opt + @"'),
                                           (select id from city where country_id in (select id from country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"'),
                                           (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"')); 
                          END; 
                    END; "), conn);
                            cmd.ExecuteNonQuery();
                            //conn.Close();
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

                        if (checkProject == 1)
                        {
                            SqlCommand cmd = new SqlCommand((@"DECLARE @cnt INT; 

                    BEGIN 
                        SELECT @cnt = Count(*) 
                        FROM   Cost_Master_Project
                        WHERE  material_option_id = (SELECT id 
                                                     FROM   material_option 
                                                     WHERE  NAME = N'" + @mat_var_opt + @"') 
                               and Project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"')
                               AND material_variance_subdiv_id_proj = 
                                   (SELECT id 
                                    FROM 
                                   material_variance_subdivision_Project 
                                                                  WHERE f_del=0 and Project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"') and NAME = 
                                   N'" + @mat_var_sub + @"' 
                                                                         AND 
                                       material_variance_id = (SELECT id 
                                                               FROM   material_variance 
                                                               WHERE 
                                       variance = N'" + @mat_var_name + @"' 
                                       AND material_id = (SELECT id 
                                                          FROM   material 
                                                          WHERE  NAME = 
                                           N'" + @mat_name + @"'))) 
                               AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end  and city_id in (select id from city where country_id in (select id from  country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"');



                        IF @cnt = 0 
                          INSERT INTO Cost_Master_Project 
                                      (material_variance_subdiv_id, 
                                       eff_date, 
                                       eff_date_end, 
                                       cost, 
                                       modified_by, 
                                       modified_on, 
                                       material_option_id,
                                       city_id,
                                       project_id,material_variance_subdiv_id_proj) 
                          VALUES     (null, 
                                      CURRENT_TIMESTAMP, 
                                      '9999-12-31 23:59:00.000', 
                                      " + @cost + @", 
                                      N'" + @user_info + @"', 
                                      CURRENT_TIMESTAMP, 
                                      (SELECT id 
                                       FROM   material_option 
                                       WHERE  NAME = N'" + @mat_var_opt + @"'),
                                     (select id from city where country_id in (select id from country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"'),
                                     (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"'),(SELECT id 
                                       FROM   material_variance_subdivision_Project 
                                       WHERE f_del=0 and Project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"') and NAME = N'" + @mat_var_sub + @"' 
                                              AND material_variance_id = (SELECT id 
                                                                          FROM   material_variance 
                                                                          WHERE 
                                                  variance = N'" + @mat_var_name + @"' 
                                                  AND material_id = (SELECT id 
                                                                     FROM   material 
                                                                     WHERE  NAME = 
                                                      N'" + @mat_name + @"')))
                                    ); 
                        ELSE 
                          BEGIN 
                              UPDATE Cost_master_project 
                              SET    eff_date_end = DateADD(Minute, -1, Current_TimeStamp) 
                              WHERE  material_option_id = (SELECT id 
                                                           FROM   material_option 
                                                           WHERE  NAME = N'" + @mat_var_opt + @"') 
                                     and project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"')
                                     AND material_variance_subdiv_id_proj = 
                                         (SELECT id 
                                          FROM 
                                         material_variance_subdivision_Project 
                                                                        WHERE f_del=0 and Project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"') and NAME = 
                                         N'" + @mat_var_sub + @"' 
                                                                               AND 
                                             material_variance_id = (SELECT id 
                                                                     FROM   material_variance 
                                                                     WHERE 
                                             variance = N'" + @mat_var_name + @"' 
                                             AND material_id = (SELECT id 
                                                                FROM   material 
                                                                WHERE  NAME = 
                                                 N'" + @mat_name + @"'))) 
                                     AND CURRENT_TIMESTAMP BETWEEN eff_date AND eff_date_end and city_id in (select id from city where country_id in (select id from country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"');
                    
                              INSERT INTO Cost_master_project 
                                          (material_variance_subdiv_id, 
                                           eff_date, 
                                           eff_date_end, 
                                           cost, 
                                           modified_by, 
                                           modified_on, 
                                           material_option_id,
                                           city_id,
                                           project_id,material_variance_subdiv_id_proj) 
                              VALUES     (null, 
                                          CURRENT_TIMESTAMP, 
                                          '9999-12-31 23:59:00.000', 
                                          " + @cost + @", 
                                          N'" + @user_info + @"', 
                                          CURRENT_TIMESTAMP, 
                                          (SELECT id 
                                           FROM   material_option 
                                           WHERE  NAME = N'" + @mat_var_opt + @"'),
                                           (select id from city where country_id in (select id from country_code where code = N'" + country_code + @"') and name = N'" + city_name + @"'),
                                           (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"'),(SELECT id 
                                           FROM   material_variance_subdivision_Project 
                                           WHERE  f_del = 0 and Project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"') and NAME = N'" + @mat_var_sub + @"' 
                                                  AND material_variance_id = (SELECT id 
                                                                              FROM 
                                                      material_variance 
                                                                              WHERE 
                                                      variance = N'" + @mat_var_name + @"' 
                                                      AND material_id = (SELECT id 
                                                                         FROM   material 
                                                                         WHERE  NAME = 
                                                          N'" + @mat_name + @"')))); 
                          END; 
                    END; "), conn);
                            cmd.ExecuteNonQuery();
                            //conn.Close();
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
                //if (conn.State == ConnectionState.Open)
                //{
                //    conn.Close();
                //}
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);

            }

        }
    }
}
