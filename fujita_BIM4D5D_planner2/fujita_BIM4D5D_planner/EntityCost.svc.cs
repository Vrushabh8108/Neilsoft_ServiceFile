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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service14" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service14.svc or Service14.svc.cs at the Solution Explorer and start debugging.
    public class Service14 : EntityCost
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        SqlCommand cmd;
        public Nullable<Decimal> find_cost(string mat, string mat_var, string mat_var_sub, string mat_opt, string country_code, string city)
        {
            decimal? cost = null;
            //SqlConnection conn = new SqlConnection(connection_string);
            //ConnectionState state = conn.State;
            try
            {
                if (mat_var_sub.Contains("'"))
                {
                    mat_var_sub = mat_var_sub.Replace("'", "''");
                }

                using (SqlConnection conn = new SqlConnection(connection_string))
                {
                    try
                    {
                        conn.Open();
                        if (mat_opt == "TotalPrice")
                        {
                            using (SqlCommand cmd = new SqlCommand(@"select sum(cost) from cost_master where current_timestamp between eff_date and eff_date_end and city_id in (select id from city where name =N'" + city + "' and country_id in (select id from country_code where code = N'" + country_code + @"')) and material_variance_subdiv_id in (select 
                        id from MATERIAL_VARIANCE_SUBDIVISION where name = N'" + mat_var_sub + @"' and city_id = (select id from City where name = N'" + city + @"' and country_id = (select id from Country_Code where country = N'" + country_code + @"')) and material_variance_id in (
                        select id from material_variance where variance = N'" + mat_var + "' and material_id in (select id from material where name =N'" + mat + "')))", conn))
                            {
                                cost = (Decimal?)cmd.ExecuteScalar();
                            }
                        }
                        else
                        {
                            using (SqlCommand cmd = new SqlCommand(@"select top 1 cost from cost_master where material_option_id =(select id from material_option where name = '" + mat_opt +
                             @"') and current_timestamp between eff_date and eff_date_end and city_id in (select id from city where name =N'" + city + "' and country_id in (select id from country_code where code = N'" + country_code + @"')) and material_variance_subdiv_id in (select 
                        id from MATERIAL_VARIANCE_SUBDIVISION where name = N'" + mat_var_sub + @"' and city_id = (select id from City where name = N'" + city + @"' and country_id = (select id from Country_Code where country = N'" + country_code + @"')) and material_variance_id in (
                        select id from material_variance where variance = N'" + mat_var + "' and material_id in (select id from material where name =N'" + mat + "')))", conn))
                            {
                                cost = (Decimal?)cmd.ExecuteScalar();
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
                //conn.Close();
                return cost;
            }
            catch (Exception ex)
            {
                //if (conn.State == ConnectionState.Open)
                //{
                //    conn.Close();
                //}
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
                return null;
            }

        }
//        public Nullable<Decimal> find_cost_proj(string mat, string mat_var, string mat_var_sub, string mat_opt, string country_code, string city,string proj_id,string proj_name)
//        {
//            decimal? cost;
//            SqlConnection conn = new SqlConnection(connection_string);
//            ConnectionState state = conn.State;
//            try
//            {
//                conn.Open();
//                if (mat_opt == "TotalPrice")
//                {
//                    cmd = new SqlCommand(@"select sum(cost) from Cost_master_project where current_timestamp between eff_date and eff_date_end and project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"') and city_id in (select id from city where name =N'" + city + "' and country_id in (select id from country_code where code = N'" + country_code + @"')) and material_variance_subdiv_id in (select 
//                        id from MATERIAL_VARIANCE_SUBDIVISION where name = N'" + mat_var_sub + @"' and material_variance_id in (
//                        select id from material_variance where variance = N'" + mat_var + "' and material_id in (select id from material where name =N'" + mat + "')))", conn);
//                    cost = (Decimal?)cmd.ExecuteScalar();
//                }
//                else
//                {
//                    cmd = new SqlCommand(@"select top 1 cost from cost_master_project where material_option_id =(select id from material_option where name = '" + mat_opt +
//                     @"') and current_timestamp between eff_date and eff_date_end and project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"') and city_id in (select id from city where name =N'" + city + "' and country_id in (select id from country_code where code = N'" + country_code + @"')) and material_variance_subdiv_id in (select 
//                        id from MATERIAL_VARIANCE_SUBDIVISION where name = N'" + mat_var_sub + @"' and material_variance_id in (
//                        select id from material_variance where variance = N'" + mat_var + "' and material_id in (select id from material where name =N'" + mat + "')))", conn);
//                    cost = (Decimal?)cmd.ExecuteScalar();
//                }
//                conn.Close();
//                return cost;
//            }
//            catch (Exception ex)

//            {


//                if (state == ConnectionState.Open)
//                {
//                    conn.Close();
//                }
//                Service17 exception1 = new Service17();
//                exception1.SendErrorToText(ex);
//                return null;

//            }

//        }
        public Nullable<Decimal> find_cost_categoryid(Int64 mat_id, Int64 mat_var, Int64 mat_var_sub, string mat_opt, string country_code, string city)
        {
            decimal? cost;
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                if (mat_opt == "TotalPrice")
                {
                    cmd = new SqlCommand(@"select sum(cost) from cost_master where current_timestamp between eff_date and eff_date_end and city_id in (select id from city where name =N'" + city + "' and country_id in (select id from country_code where code = N'" + country_code + @"')) and material_variance_subdiv_id in (select 
                        id from MATERIAL_VARIANCE_SUBDIVISION where category_id = -1 *" + mat_var_sub + @" and material_variance_id in (
                        select id from material_variance where category_id = -1 *" + mat_var + @" and material_id in (select id from material where category_id = -1 *" + mat_id + ")))", conn);
                    cost = (Decimal?)cmd.ExecuteScalar();
                }
                else
                {
                    cmd = new SqlCommand(@"select top 1 cost from cost_master where material_option_id =(select id from material_option where name = '" + mat_opt +
                     @"') and current_timestamp between eff_date and eff_date_end and city_id in (select id from city where name =N'" + city + "' and country_id in (select id from country_code where code = N'" + country_code + @"')) and material_variance_subdiv_id in (select 
                        id from MATERIAL_VARIANCE_SUBDIVISION where category_id = -1 *" + mat_var_sub + @" and material_variance_id in (
                        select id from material_variance where category_id = -1 *" + mat_var + " and material_id in (select id from material where category_id = -1 *" + mat_id + ")))", conn);
                    cost = (Decimal?)cmd.ExecuteScalar();
                }
                conn.Close();
                return cost;
            }
            catch (Exception ex)

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
        public Nullable<Decimal> find_cost_categoryid_name(Int64 mat_id, string mat_var, string mat_var_sub, string mat_opt, string country_code, string city)
        {
            decimal? cost;
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                if (mat_opt == "TotalPrice")
                {
                    cmd = new SqlCommand(@"select sum(cost) from cost_master where current_timestamp between eff_date and eff_date_end and city_id in (select id from city where name =N'" + city + "' and country_id in (select id from country_code where code = N'" + country_code + @"')) and material_variance_subdiv_id in (select 
                        id from MATERIAL_VARIANCE_SUBDIVISION where name = N'" + mat_var_sub + @"' and material_variance_id in (
                        select id from material_variance  where variance = N'" + mat_var + "' and material_id in (select id from material where category_id = -1 *" + mat_id + ")))", conn);
                    cost = (Decimal?)cmd.ExecuteScalar();
                }
                else
                {
                    cmd = new SqlCommand(@"select top 1 cost from cost_master where material_option_id =(select id from material_option where name = '" + mat_opt +
                     @"') and current_timestamp between eff_date and eff_date_end and city_id in (select id from city where name =N'" + city + "' and country_id in (select id from country_code where code = N'" + country_code + @"')) and material_variance_subdiv_id in (select 
                        id from MATERIAL_VARIANCE_SUBDIVISION where name = N'" + mat_var_sub + @"' and material_variance_id in (
                        select id from material_variance  where variance = N'" + mat_var + "' and material_id in (select id from material where category_id = -1 *" + mat_id + ")))", conn);
                    cost = (Decimal?)cmd.ExecuteScalar();
                }
                conn.Close();
                return cost;
            }
            catch (Exception ex)

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
        public Nullable<Decimal> find_cost_categoryid_varianceid(Int64 mat_id, Int64 mat_var, string mat_var_sub, string mat_opt, string country_code, string city)
        {
            decimal? cost;
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                if (mat_opt == "TotalPrice")
                {
                    cmd = new SqlCommand(@"select sum(cost) from cost_master where current_timestamp between eff_date and eff_date_end and city_id in (select id from city where name =N'" + city + "' and country_id in (select id from country_code where code = N'" + country_code + @"')) and material_variance_subdiv_id in (select 
                        id from MATERIAL_VARIANCE_SUBDIVISION where name = N'" + mat_var_sub + @"' and material_variance_id in (
                        select id from material_variance where category_id = -1 *" + mat_var + @" and material_id in (select id from material where category_id = -1 *" + mat_id + ")))", conn);
                    cost = (Decimal?)cmd.ExecuteScalar();
                }
                else
                {
                    cmd = new SqlCommand(@"select top 1 cost from cost_master where material_option_id =(select id from material_option where name = '" + mat_opt +
                     @"') and current_timestamp between eff_date and eff_date_end and city_id in (select id from city where name =N'" + city + "' and country_id in (select id from country_code where code = N'" + country_code + @"')) and material_variance_subdiv_id in (select 
                        id from MATERIAL_VARIANCE_SUBDIVISION where name = N'" + mat_var_sub + @"' and material_variance_id in (
                        select id from material_variance where category_id = -1 *" + mat_var + " and material_id in (select id from material where category_id = -1 *" + mat_id + ")))", conn);
                    cost = (Decimal?)cmd.ExecuteScalar();
                }
                conn.Close();
                return cost;
            }
            catch (Exception ex)

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

        public Nullable<Decimal> find_cost_proj(string mat, string mat_var, string mat_var_sub, string mat_opt, string country_code, string city, string proj_id, string proj_name)
        {
            int isInMaster = 0;
            int isInProject = 0;
            decimal? cost=null;
            //SqlConnection conn = new SqlConnection(connection_string);
            //ConnectionState state = conn.State;
            try
            {
                if (mat_var_sub.Contains("'"))
                {
                    mat_var_sub = mat_var_sub.Replace("'", "''");
                }
                if (mat_opt == "TotalPrice")
                {
                    using (SqlConnection con = new SqlConnection(connection_string))
                    {
                        try
                        {
                            con.Open();
                            string query = "select COUNT(*) from Material_Variance_Subdivision_Project where name = N'" + mat_var_sub + @"' and material_variance_id in (select id from Material_Variance where variance= N'" + mat_var + @"' and material_id in (select id from Material where name= N'" + mat + @"')) and project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"') and f_del = 0";
                            using (SqlCommand cmd1 = new SqlCommand(query, con))
                            {
                                isInProject = (int)cmd1.ExecuteScalar();
                            }
                            if (isInProject == 1)
                            {
                                query = @"select sum(cost) from Cost_master_project where current_timestamp between eff_date and eff_date_end and project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"') and city_id in (select id from city where name =N'" + city + "' and country_id in (select id from country_code where code = N'" + country_code + @"')) and material_variance_subdiv_id_proj in (select 
                        id from MATERIAL_VARIANCE_SUBDIVISION_PROJECT where f_del = 0 and project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"') and name = N'" + mat_var_sub + @"' and material_variance_id in (
                        select id from material_variance where variance = N'" + mat_var + "' and material_id in (select id from material where name =N'" + mat + "')))";
                                using (SqlCommand cmd1 = new SqlCommand(query, con))
                                {
                                    cost = (Decimal?)cmd1.ExecuteScalar();
                                }
                            }
                            else
                            {
                                query = "select COUNT(*) from Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and material_variance_id in (select id from Material_Variance where variance= N'" + mat_var + @"' and material_id in (select id from Material where name= N'" + mat + @"')) and f_del = 0 and city_id = (select id from City where name = N'" + city + @"' and country_id = (select id from Country_Code where 
													country = N'" + country_code + @"')))";
                                using (SqlCommand cmd2 = new SqlCommand(query, con))
                                {
                                    isInMaster = (int)cmd2.ExecuteScalar();
                                }
                                if (isInMaster == 1)
                                {
                                    query = @"select sum(cost) from Cost_master_project where current_timestamp between eff_date and eff_date_end and project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"') and city_id in (select id from city where name =N'" + city + "' and country_id in (select id from country_code where code = N'" + country_code + @"')) and material_variance_subdiv_id in (select 
                        id from MATERIAL_VARIANCE_SUBDIVISION where f_del = 0 and city_id = (select id from City where name = N'" + city + @"' and country_id = (select id from Country_Code where country = N'" + country_code + @"')) and name = N'" + mat_var_sub + @"' and material_variance_id in (
                        select id from material_variance where variance = N'" + mat_var + "' and material_id in (select id from material where name =N'" + mat + "')))";
                                    using (SqlCommand cmd2 = new SqlCommand(query, con))
                                    {
                                        cost = (Decimal?)cmd2.ExecuteScalar();
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }
                            Service17 exception1 = new Service17();
                            exception1.SendErrorToText(ex);
                        }
                    }

                    return cost;
                }
                else
                {
                    using (SqlConnection con = new SqlConnection(connection_string))
                    {
                        try
                        {

                            con.Open();
                            string query = "select COUNT(*) from Material_Variance_Subdivision_Project where name = N'" + mat_var_sub + @"' and material_variance_id in (select id from Material_Variance where variance= N'" + mat_var + @"' and material_id in (select id from Material where name= N'" + mat + @"')) and project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"') and f_del = 0";
                            using (SqlCommand cmd1 = new SqlCommand(query, con))
                            {
                                isInProject = (int)cmd1.ExecuteScalar();
                            }
                            if (isInProject == 1)
                            {
                                query = @"select top 1 cost from cost_master_project where material_option_id =(select id from material_option where name = '" + mat_opt +
                         @"') and current_timestamp between eff_date and eff_date_end and project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"') and city_id in (select id from city where name =N'" + city + "' and country_id in (select id from country_code where code = N'" + country_code + @"')) and material_variance_subdiv_id_proj in (select 
                        id from MATERIAL_VARIANCE_SUBDIVISION_PROJECT where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"') and f_del = 0 and name = N'" + mat_var_sub + @"' and material_variance_id in (
                        select id from material_variance where variance = N'" + mat_var + "' and material_id in (select id from material where name =N'" + mat + "')))";
                                using (SqlCommand cmd1 = new SqlCommand(query, con))
                                {
                                    cost = (Decimal?)cmd1.ExecuteScalar();
                                }
                            }
                            else
                            {
                                query = "select COUNT(*) from Material_Variance_Subdivision where name = N'" + mat_var_sub + @"' and material_variance_id in (select id from Material_Variance where variance= N'" + mat_var + @"' and material_id in (select id from Material where name= N'" + mat + @"')) and f_del = 0 and city_id = (select id from City where name = N'" + city + @"' and country_id = (select id from Country_Code where 
													country = N'" + country_code + @"'));";
                                using (SqlCommand cmd2 = new SqlCommand(query, con))
                                {
                                    isInMaster = (int)cmd2.ExecuteScalar();
                                }
                                if (isInMaster == 1)
                                {
                                    query = @"select top 1 cost from cost_master_project where material_option_id =(select id from material_option where name = '" + mat_opt +
                         @"') and current_timestamp between eff_date and eff_date_end and project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + @"') and city_id in (select id from city where name =N'" + city + "' and country_id in (select id from country_code where code = N'" + country_code + @"')) and material_variance_subdiv_id in (select 
                        id from MATERIAL_VARIANCE_SUBDIVISION where f_del = 0 and city_id = (select id from City where name = N'" + city + @"' and country_id = (select id from Country_Code where country = N'" + country_code + @"')) and name = N'" + mat_var_sub + @"' and material_variance_id in (
                        select id from material_variance where variance = N'" + mat_var + "' and material_id in (select id from material where name =N'" + mat + "')))";
                                    using (SqlCommand cmd2 = new SqlCommand(query, con))
                                    {
                                        cost = (Decimal?)cmd2.ExecuteScalar();
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }
                            Service17 exception1 = new Service17();
                            exception1.SendErrorToText(ex);
                        }
                    }

                    return cost;
                }
            }
            catch (Exception ex)
            {
                //if (state == ConnectionState.Open)
                //{
                //    conn.Close();
                //}
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
                return null;

            }

        }
    }
}
