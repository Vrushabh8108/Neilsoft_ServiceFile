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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service22" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service22.svc or Service22.svc.cs at the Solution Explorer and start debugging.
    public class Service22 : DesignRule
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        //        public List<design_rule_country> Getdesignrule_Floor(string country, string city)
        //        {

        //            SqlConnection conn = new SqlConnection(connection_string);
        //            ConnectionState state = conn.State;
        //            try
        //            {
        //                #region all country
        //                if (string.IsNullOrEmpty(country) && string.IsNullOrEmpty(city))
        //                {
        //                    List<design_rule_country> design_rule_country1 = new List<design_rule_country>();
        //                    conn.Open();
        //                    SqlCommand cmd_cnt = new SqlCommand("Select * from Country_code order by code;", conn);
        //                    SqlDataAdapter da_cnt = new SqlDataAdapter(cmd_cnt);
        //                    DataTable dt_cnt = new DataTable();
        //                    da_cnt.Fill(dt_cnt);

        //                    for (int a = 0; a < dt_cnt.Rows.Count; a++)
        //                    {
        //                        List<design_rule_city> design_rule_city1 = new List<design_rule_city>();
        //                        design_rule_country design_rule_country2 = new design_rule_country();
        //                        SqlCommand cmd_city = new SqlCommand("Select id,name from City where country_id = " + dt_cnt.Rows[a]["id"] + "order by name asc", conn);
        //                        SqlDataAdapter da_city = new SqlDataAdapter(cmd_city);
        //                        DataTable dt_city = new DataTable();
        //                        da_city.Fill(dt_city);

        //                        for (int a1 = 0; a1 < dt_city.Rows.Count; a1++)
        //                        {

        //                            List<design_rule_city> lst_design_rule_floor = new List<design_rule_city>();
        //                            design_rule_country design_rule_floor = new design_rule_country();
        //                            SqlCommand cmd_floor = new SqlCommand("select id,name from DesignRule_Floor where proj_id is NULL", conn);
        //                            SqlDataAdapter da_floor = new SqlDataAdapter(cmd_floor);
        //                            DataTable dt_floor = new DataTable();
        //                            da_floor.Fill(dt_floor);

        //                            design_rule_city design_rule_city2 = new design_rule_city();
        //                            List<design_rule_floor> lst_floor = new List<design_rule_floor>();

        //                            for (int f = 0; f < dt_floor.Rows.Count; f++)
        //                            {
        //                                design_rule_floor obj_design_rule_floor = new design_rule_floor();
        //                                List<design_rule> design_rule1 = new List<design_rule>();

        //                                SqlCommand cmd = new SqlCommand(" select dr.name,(select unit from Unit_Of_Measurement where id = dr.unit_of_measurement_id) unit_of_measurement,value,formula,dr.is_rebar from design_rule dr left outer join" + @"
        //(select ff.id id, ff.name, drc.design_rule_id, drc.value,drc.formula from DesignRule_Floor ff
        //left outer join  Design_Rule_Country drc on drc.floor_id = ff.id and drc.city_id = " + dt_city.Rows[a1]["id"] + " where ff.id =  " + dt_floor.Rows[f]["id"] + ")design on design.design_rule_id = dr.id ", conn);
        //                                SqlDataAdapter da = new SqlDataAdapter(cmd);
        //                                DataTable dt = new DataTable();
        //                                da.Fill(dt);
        //                                if (dt.Rows.Count > 0)
        //                                {
        //                                    for (int i = 0; i < dt.Rows.Count; i++)
        //                                    {
        //                                        design_rule design_rule2 = new design_rule();
        //                                        design_rule2.name = dt.Rows[i]["Name"].ToString();
        //                                        design_rule2.unit_of_measurement = dt.Rows[i]["unit_of_measurement"].ToString();
        //                                        design_rule2.value = string.IsNullOrEmpty(dt.Rows[i]["value"].ToString()) ? (Decimal?)null : Convert.ToDecimal(dt.Rows[i]["value"]);
        //                                        design_rule2.formula = dt.Rows[i]["formula"].ToString();
        //                                        design_rule2.isRebar = dt.Rows[i]["is_rebar"] == null ? 0 : Convert.ToInt16(dt.Rows[i]["is_rebar"]);
        //                                        design_rule1.Add(design_rule2);
        //                                    }
        //                                }

        //                                obj_design_rule_floor.floorname = dt_floor.Rows[f]["name"].ToString();
        //                                obj_design_rule_floor.design_rule = design_rule1;
        //                                lst_floor.Add(obj_design_rule_floor);
        //                            }

        //                            design_rule_city2.city = dt_city.Rows[a1]["name"].ToString();
        //                            design_rule_city2.design_rule_floor_data = lst_floor;
        //                            design_rule_city1.Add(design_rule_city2);


        //                        }
        //                        design_rule_country2.country = dt_cnt.Rows[a]["country"].ToString();
        //                        design_rule_country2.design_rule_city = design_rule_city1;
        //                        design_rule_country1.Add(design_rule_country2);
        //                    }

        //                    conn.Close();
        //                    return design_rule_country1;
        //                }
        //                #endregion

        //                #region City Specific
        //                else
        //                {
        //                    List<design_rule_country> design_rule_country1 = new List<design_rule_country>();
        //                    conn.Open();
        //                    SqlCommand cmd_cnt = new SqlCommand("Select * from Country_code where country = N'" + country + "' order by code;", conn);
        //                    SqlDataAdapter da_cnt = new SqlDataAdapter(cmd_cnt);
        //                    DataTable dt_cnt = new DataTable();
        //                    da_cnt.Fill(dt_cnt);

        //                    for (int a = 0; a < dt_cnt.Rows.Count; a++)
        //                    {
        //                        List<design_rule_city> design_rule_city1 = new List<design_rule_city>();
        //                        design_rule_country design_rule_country2 = new design_rule_country();
        //                        SqlCommand cmd_city = new SqlCommand("Select id,name from City where country_id = " + dt_cnt.Rows[a]["id"] + " and name= N'" + city + "' order by name asc;", conn);
        //                        SqlDataAdapter da_city = new SqlDataAdapter(cmd_city);
        //                        DataTable dt_city = new DataTable();
        //                        da_city.Fill(dt_city);

        //                        for (int a1 = 0; a1 < dt_city.Rows.Count; a1++)
        //                        {

        //                            List<design_rule_city> lst_design_rule_floor = new List<design_rule_city>();
        //                            design_rule_country design_rule_floor = new design_rule_country();
        //                            SqlCommand cmd_floor = new SqlCommand("select id,name from DesignRule_Floor where proj_id is NULL", conn);
        //                            SqlDataAdapter da_floor = new SqlDataAdapter(cmd_floor);
        //                            DataTable dt_floor = new DataTable();
        //                            da_floor.Fill(dt_floor);

        //                            design_rule_city design_rule_city2 = new design_rule_city();
        //                            List<design_rule_floor> lst_floor = new List<design_rule_floor>();

        //                            for (int f = 0; f < dt_floor.Rows.Count; f++)
        //                            {
        //                                design_rule_floor obj_design_rule_floor = new design_rule_floor();
        //                                List<design_rule> design_rule1 = new List<design_rule>();

        //                                SqlCommand cmd = new SqlCommand(" select dr.name,(select unit from Unit_Of_Measurement where id = dr.unit_of_measurement_id) unit_of_measurement,value,formula,dr.is_rebar from design_rule dr left outer join" + @"
        //(select ff.id id, ff.name, drc.design_rule_id, drc.value,drc.formula from DesignRule_Floor ff
        //left outer join  Design_Rule_Country drc on drc.floor_id = ff.id and drc.city_id = " + dt_city.Rows[a1]["id"] + " where ff.id =  " + dt_floor.Rows[f]["id"] + ")design on design.design_rule_id = dr.id ", conn);
        //                                SqlDataAdapter da = new SqlDataAdapter(cmd);
        //                                DataTable dt = new DataTable();
        //                                da.Fill(dt);
        //                                if (dt.Rows.Count > 0)
        //                                {
        //                                    for (int i = 0; i < dt.Rows.Count; i++)
        //                                    {
        //                                        design_rule design_rule2 = new design_rule();
        //                                        design_rule2.name = dt.Rows[i]["Name"].ToString();
        //                                        design_rule2.unit_of_measurement = dt.Rows[i]["unit_of_measurement"].ToString();
        //                                        design_rule2.value = string.IsNullOrEmpty(dt.Rows[i]["value"].ToString()) ? (Decimal?)null : Convert.ToDecimal(dt.Rows[i]["value"]);
        //                                        design_rule2.formula = dt.Rows[i]["formula"].ToString();
        //                                        design_rule2.isRebar = dt.Rows[i]["is_rebar"] == null ? 0 : Convert.ToInt16(dt.Rows[i]["is_rebar"]);
        //                                        design_rule1.Add(design_rule2);
        //                                    }
        //                                }

        //                                obj_design_rule_floor.floorname = dt_floor.Rows[f]["name"].ToString();
        //                                obj_design_rule_floor.design_rule = design_rule1;
        //                                lst_floor.Add(obj_design_rule_floor);
        //                            }

        //                            design_rule_city2.city = dt_city.Rows[a1]["name"].ToString();
        //                            design_rule_city2.design_rule_floor_data = lst_floor;
        //                            design_rule_city1.Add(design_rule_city2);


        //                        }
        //                        design_rule_country2.country = dt_cnt.Rows[a]["country"].ToString();
        //                        design_rule_country2.design_rule_city = design_rule_city1;
        //                        design_rule_country1.Add(design_rule_country2);
        //                    }

        //                    conn.Close();
        //                    return design_rule_country1;
        //                }
        //                #endregion
        //            }
        //            catch (System.Exception ex)
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


        public List<design_rule_country> Getdesignrule()
        {
            int cnt = 0;
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                List<design_rule_country> design_rule_country1 = new List<design_rule_country>();
                conn.Open();
                SqlCommand cmd_cnt = new SqlCommand("Select * from Country_code order by code;", conn);
                SqlDataAdapter da_cnt = new SqlDataAdapter(cmd_cnt);
                DataTable dt_cnt = new DataTable();
                da_cnt.Fill(dt_cnt);

                for (int a = 0; a < dt_cnt.Rows.Count; a++)
                {
                    List<design_rule_city> design_rule_city1 = new List<design_rule_city>();
                    design_rule_country design_rule_country2 = new design_rule_country();
                    SqlCommand cmd_city = new SqlCommand("Select * from City where country_id = " + dt_cnt.Rows[a]["id"] + "order by name asc", conn);
                    SqlDataAdapter da_city = new SqlDataAdapter(cmd_city);
                    DataTable dt_city = new DataTable();
                    da_city.Fill(dt_city);

                    for (int a1 = 0; a1 < dt_city.Rows.Count; a1++)
                    {
                        design_rule_city design_rule_city2 = new design_rule_city();
                        List<design_rule> design_rule1 = new List<design_rule>();
                        SqlCommand cmd = new SqlCommand("select dr.name,(select unit from Unit_Of_Measurement where id = dr.unit_of_measurement_id) unit_of_measurement,value,formula,is_rebar from design_rule dr left outer join " +
                                                       @" (select cc.id id, cc.name, drc.design_rule_id, drc.value,drc.formula from City cc
                                                            left outer join  Design_Rule_Country drc on drc.city_id = cc.id where cc.id = " + Convert.ToInt32(dt_city.Rows[a1]["id"]) + @")design on design.design_rule_id = dr.id ; ", conn);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            cnt += dt.Rows.Count;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                design_rule design_rule2 = new design_rule();
                                design_rule2.name = dt.Rows[i]["Name"].ToString();
                                design_rule2.unit_of_measurement = dt.Rows[i]["unit_of_measurement"].ToString();
                                design_rule2.value = string.IsNullOrEmpty(dt.Rows[i]["value"].ToString()) ? (Decimal?)null : Convert.ToDecimal(dt.Rows[i]["value"]);
                                design_rule2.formula = dt.Rows[i]["formula"].ToString();
                                design_rule2.isRebar = dt.Rows[i]["is_rebar"] == null ? 0 : Convert.ToInt16(dt.Rows[i]["is_rebar"]);
                                design_rule1.Add(design_rule2);
                            }
                        }


                        design_rule_city2.city = dt_city.Rows[a1]["name"].ToString();
                        design_rule_city2.design_rule = design_rule1;
                        design_rule_city1.Add(design_rule_city2);
                    }
                    design_rule_country2.country = dt_cnt.Rows[a]["country"].ToString();
                    design_rule_country2.design_rule_city = design_rule_city1;
                    design_rule_country1.Add(design_rule_country2);
                }

                conn.Close();
                return design_rule_country1;
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


        #region Copy
        //        public List<design_rule_country> Getdesignrule()
        //        {

        //            SqlConnection conn = new SqlConnection(connection_string);
        //            ConnectionState state = conn.State;
        //            try
        //            {
        //                List<design_rule_country> design_rule_country1 = new List<design_rule_country>();
        //                conn.Open();
        //                SqlCommand cmd_cnt = new SqlCommand("Select * from Country_code order by code;", conn);
        //                SqlDataAdapter da_cnt = new SqlDataAdapter(cmd_cnt);
        //                DataTable dt_cnt = new DataTable();
        //                da_cnt.Fill(dt_cnt);

        //                for (int a = 0; a < dt_cnt.Rows.Count; a++)
        //                {
        //                    List<design_rule_city> design_rule_city1 = new List<design_rule_city>();
        //                    design_rule_country design_rule_country2 = new design_rule_country();
        //                    SqlCommand cmd_city = new SqlCommand("Select * from City where country_id = " + dt_cnt.Rows[a]["id"] + "order by name asc", conn);
        //                    SqlDataAdapter da_city = new SqlDataAdapter(cmd_city);
        //                    DataTable dt_city = new DataTable();
        //                    da_city.Fill(dt_city);

        //                    for (int a1 = 0; a1 < dt_city.Rows.Count; a1++)
        //                    {
        //                        design_rule_city design_rule_city2 = new design_rule_city();
        //                        List<design_rule> design_rule1 = new List<design_rule>();
        //                        SqlCommand cmd = new SqlCommand("select dr.name,(select unit from Unit_Of_Measurement where id = dr.unit_of_measurement_id) unit_of_measurement,value,formula from design_rule dr left outer join " +
        //                                                       @" (select cc.id id, cc.name, drc.design_rule_id, drc.value,drc.formula from City cc
        //                                                            left outer join  Design_Rule_Country drc on drc.city_id = cc.id where cc.id = " + Convert.ToInt32(dt_city.Rows[a1]["id"]) + @")design on design.design_rule_id = dr.id ; ", conn);
        //                        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //                        DataTable dt = new DataTable();
        //                        da.Fill(dt);
        //                        if (dt.Rows.Count > 0)
        //                        {
        //                            for (int i = 0; i < dt.Rows.Count; i++)
        //                            {
        //                                design_rule design_rule2 = new design_rule();
        //                                design_rule2.name = dt.Rows[i]["Name"].ToString();
        //                                design_rule2.unit_of_measurement = dt.Rows[i]["unit_of_measurement"].ToString();
        //                                design_rule2.value = string.IsNullOrEmpty(dt.Rows[i]["value"].ToString()) ? (Decimal?)null : Convert.ToDecimal(dt.Rows[i]["value"]);
        //                                design_rule2.formula = dt.Rows[i]["formula"].ToString();
        //                                design_rule1.Add(design_rule2);
        //                            }
        //                        }


        //                        design_rule_city2.city = dt_city.Rows[a1]["name"].ToString();
        //                        design_rule_city2.design_rule = design_rule1;
        //                        design_rule_city1.Add(design_rule_city2);
        //                    }
        //                    design_rule_country2.country = dt_cnt.Rows[a]["country"].ToString();
        //                    design_rule_country2.design_rule_city = design_rule_city1;
        //                    design_rule_country1.Add(design_rule_country2);
        //                }

        //                conn.Close();
        //                return design_rule_country1;
        //            }
        //            catch (System.Exception ex)
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
        #endregion

        public List<design_rule> Getdesignrulecountry(string Country, string city)
        {

            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {

                List<design_rule> design_rule1 = new List<design_rule>();
                SqlCommand cmd = new SqlCommand("select dr.name,(select unit from Unit_Of_Measurement where id = dr.unit_of_measurement_id) unit_of_measurement,value,formula from design_rule dr left outer join " +
                                               @" (select cc.id id, cc.name, drc.design_rule_id, drc.value,drc.formula from City cc
                                                    left outer join  Design_Rule_Country drc on drc.city_id = cc.id where cc.id = (select id from city where name = N'" + city + "' and country_id in (select id from country_code where country = N'" + Country + @"')))design on design.design_rule_id = dr.id ; ", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        design_rule design_rule2 = new design_rule();
                        design_rule2.name = dt.Rows[i]["Name"].ToString();
                        design_rule2.unit_of_measurement = dt.Rows[i]["unit_of_measurement"].ToString();
                        design_rule2.value = string.IsNullOrEmpty(dt.Rows[i]["value"].ToString()) ? (Decimal?)null : Convert.ToDecimal(dt.Rows[i]["value"]);
                        design_rule2.formula = dt.Rows[i]["formula"].ToString();
                        design_rule1.Add(design_rule2);
                    }
                }


                conn.Close();
                return design_rule1;
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

        public int updatedesignrule([Optional]string city, [Optional]string country, design_rule design_rule, string created_by)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                if (city == "" && country == "")
                {
                    SqlCommand cmd_designrule = new SqlCommand("Insert into Design_rule (name,unit_of_measurement_id,is_rebar,created_on,created_by) values " + @"
(N'" + design_rule.name + "',(select id from unit_of_measurement where unit = N'" + design_rule.unit_of_measurement + "')," + design_rule.isRebar + ",CURRENT_TIMESTAMP,N'" + created_by + "')", conn);
                    cmd_designrule.ExecuteNonQuery();
                    SqlCommand cmd_cnt = new SqlCommand("Select * from Country_code order by code;", conn);
                    SqlDataAdapter da_cnt = new SqlDataAdapter(cmd_cnt);
                    DataTable dt_cnt = new DataTable();
                    da_cnt.Fill(dt_cnt);

                    for (int a = 0; a < dt_cnt.Rows.Count; a++)
                    {

                        SqlCommand cmd_city = new SqlCommand("Select * from City where country_id = " + dt_cnt.Rows[a]["id"] + "order by name asc", conn);
                        SqlDataAdapter da_city = new SqlDataAdapter(cmd_city);
                        DataTable dt_city = new DataTable();
                        da_city.Fill(dt_city);

                        for (int a1 = 0; a1 < dt_city.Rows.Count; a1++)
                        {
                            SqlCommand cmd1 = new SqlCommand("Insert into Design_Rule_Country (design_rule_id,city_id,value,formula) values ((select id from design_rule where name = N'" + design_rule.name + "')," + dt_city.Rows[a1]["id"] + "," + design_rule.value + ",N'" + design_rule.formula + "')", conn);
                            cmd1.ExecuteNonQuery();

                            SqlCommand cmd_proj = new SqlCommand("select name,proj_guid from Project where created_on >='2022-09-23' and f_active = 1 and city_id = " + dt_city.Rows[a1]["id"] + ";", conn);
                            SqlDataAdapter da_proj = new SqlDataAdapter(cmd_proj);
                            DataTable dt_proj = new DataTable();
                            da_proj.Fill(dt_proj);
                            for (int p = 0; p < dt_proj.Rows.Count; p++)
                            {
                                SqlCommand cmd_floor = new SqlCommand("select id from DesignRule_Floor where proj_id is null or proj_id = (select id from Project where name = N'" + dt_proj.Rows[p]["name"] + "' and proj_guid = N'" + dt_proj.Rows[p]["proj_guid"] + "')", conn);
                                SqlDataAdapter da_floor = new SqlDataAdapter(cmd_floor);
                                DataTable dt_floor = new DataTable();
                                da_floor.Fill(dt_floor);
                                for (int f = 0; f < dt_floor.Rows.Count; f++)
                                {
                                    SqlCommand cmd1_proj = new SqlCommand("Insert into Design_Rule_Country_Project(design_rule_project_id,city_id,value,formula,project_id,design_rule_id,floor_id_proj) " + @"
values (null," + dt_city.Rows[a1]["id"] + "," + design_rule.value + ",N'" + design_rule.formula + "',(select id from Project where name = N'" + dt_proj.Rows[p]["name"] + "' and proj_guid = N'" + dt_proj.Rows[p]["proj_guid"] + "'),(select id from design_rule where name = N'" + design_rule.name + "')," + dt_floor.Rows[f]["id"] + ")", conn);
                                    cmd1_proj.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                }
                else
                {
                    SqlCommand cmd_rule = new SqlCommand("Select count(*) from Design_Rule where name = N'" + design_rule.name + "';", conn);
                    int cnt_rule = Convert.ToInt32(cmd_rule.ExecuteScalar());
                    if (cnt_rule == 0)
                    {
                        SqlCommand cmd_designrule = new SqlCommand("Insert into Design_rule (name,unit_of_measurement_id) values (N'" + design_rule.name + "',(select id from unit_of_measurement where unit = N'" + design_rule.unit_of_measurement + "'))", conn);
                        cmd_designrule.ExecuteNonQuery();

                    }
                    SqlCommand cmd = new SqlCommand("Select count(*) from Design_Rule_Country where design_rule_id =(select id from design_rule where name = N'" + design_rule.name + "') and city_id = (select id from city where country_id in (select id from country_code where country = N'" + country + "') and name = N'" + city + "');", conn);
                    int cnt = Convert.ToInt32(cmd.ExecuteScalar());
                    if (cnt == 0)
                    {
                        SqlCommand cmd1 = new SqlCommand("Insert into Design_Rule_Country (design_rule_id,city_id,value,formula) values ((select id from design_rule where name = N'" + design_rule.name + "'),(select id from City where country_id in (select id from country_code where country = N'" + country + "') and name = N'" + city + "')," + design_rule.value + ",N'" + design_rule.formula + "')", conn);
                        cmd1.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmd2 = new SqlCommand("update Design_Rule_Country set value = " + design_rule.value + ",formula = N'" + design_rule.formula + "' where design_rule_id in ( select id from design_rule where  name = N'" + design_rule.name + "') and city_id = (select id from City where country_id in (select id from country_code where country = N'" + country + "') and name = N'" + city + "')  ;", conn);
                        cmd2.ExecuteNonQuery();
                    }
                }
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

        #region Copy
        //public int updatedesignrule([Optional]string city, [Optional]string country, design_rule design_rule)
        //{
        //    SqlConnection conn = new SqlConnection(connection_string);
        //    ConnectionState state = conn.State;
        //    try
        //    {
        //        conn.Open();
        //        if (city == "" && country == "")
        //        {
        //            SqlCommand cmd_designrule = new SqlCommand("Insert into Design_rule (name,unit_of_measurement_id) values (N'" + design_rule.name + "',(select id from unit_of_measurement where unit = N'" + design_rule.unit_of_measurement + "'))", conn);
        //            cmd_designrule.ExecuteNonQuery();
        //            SqlCommand cmd_cnt = new SqlCommand("Select * from Country_code order by code;", conn);
        //            SqlDataAdapter da_cnt = new SqlDataAdapter(cmd_cnt);
        //            DataTable dt_cnt = new DataTable();
        //            da_cnt.Fill(dt_cnt);

        //            for (int a = 0; a < dt_cnt.Rows.Count; a++)
        //            {

        //                SqlCommand cmd_city = new SqlCommand("Select * from City where country_id = " + dt_cnt.Rows[a]["id"] + "order by name asc", conn);
        //                SqlDataAdapter da_city = new SqlDataAdapter(cmd_city);
        //                DataTable dt_city = new DataTable();
        //                da_city.Fill(dt_city);

        //                for (int a1 = 0; a1 < dt_city.Rows.Count; a1++)
        //                {
        //                    SqlCommand cmd1 = new SqlCommand("Insert into Design_Rule_Country (design_rule_id,city_id,value,formula) values ((select id from design_rule where name = N'" + design_rule.name + "')," + dt_city.Rows[a1]["id"] + "," + design_rule.value + ",N'" + design_rule.formula + "')", conn);
        //                    cmd1.ExecuteNonQuery();
        //                }
        //            }

        //        }
        //        else
        //        {
        //            SqlCommand cmd_rule = new SqlCommand("Select count(*) from Design_Rule where name = N'" + design_rule.name + "';", conn);
        //            int cnt_rule = Convert.ToInt32(cmd_rule.ExecuteScalar());
        //            if (cnt_rule == 0)
        //            {
        //                SqlCommand cmd_designrule = new SqlCommand("Insert into Design_rule (name,unit_of_measurement_id) values (N'" + design_rule.name + "',(select id from unit_of_measurement where unit = N'" + design_rule.unit_of_measurement + "'))", conn);
        //                cmd_designrule.ExecuteNonQuery();

        //            }
        //            SqlCommand cmd = new SqlCommand("Select count(*) from Design_Rule_Country where design_rule_id =(select id from design_rule where name = N'" + design_rule.name + "') and city_id = (select id from city where country_id in (select id from country_code where country = N'" + country + "') and name = N'" + city + "');", conn);
        //            int cnt = Convert.ToInt32(cmd.ExecuteScalar());
        //            if (cnt == 0)
        //            {
        //                SqlCommand cmd1 = new SqlCommand("Insert into Design_Rule_Country (design_rule_id,city_id,value,formula) values ((select id from design_rule where name = N'" + design_rule.name + "'),(select id from City where country_id in (select id from country_code where country = N'" + country + "') and name = N'" + city + "')," + design_rule.value + ",N'" + design_rule.formula + "')", conn);
        //                cmd1.ExecuteNonQuery();
        //            }
        //            else
        //            {
        //                SqlCommand cmd2 = new SqlCommand("update Design_Rule_Country set value = " + design_rule.value + ",formula = N'" + design_rule.formula + "' where design_rule_id in ( select id from design_rule where  name = N'" + design_rule.name + "') and city_id = (select id from City where country_id in (select id from country_code where country = N'" + country + "') and name = N'" + city + "')  ;", conn);
        //                cmd2.ExecuteNonQuery();
        //            }
        //        }
        //        conn.Close();
        //        return 1;
        //    }
        //    catch (System.Exception ex)
        //    {


        //        if (state == ConnectionState.Open)
        //        {
        //            conn.Close();

        //        }
        //        Service17 exception1 = new Service17();
        //        exception1.SendErrorToText(ex);
        //        return 0;

        //    }

        //}
        #endregion

        //public int updatedesignrule_Floor([Optional]string city, [Optional]string country, design_rule design_rule, string floor_name)
        //{
        //    SqlConnection conn = new SqlConnection(connection_string);
        //    ConnectionState state = conn.State;
        //    try
        //    {
        //        conn.Open();

        //        if (city == "" && country == "")
        //        {
        //            SqlCommand cmd_designrule = new SqlCommand("Insert into Design_rule (name,unit_of_measurement_id,is_rebar) values (N'" + design_rule.name + "',(select id from unit_of_measurement where unit = N'" + design_rule.unit_of_measurement + "'), " + design_rule.isRebar + ")", conn);
        //            cmd_designrule.ExecuteNonQuery();
        //            SqlCommand cmd_cnt = new SqlCommand("Select * from Country_code order by code;", conn);
        //            SqlDataAdapter da_cnt = new SqlDataAdapter(cmd_cnt);
        //            DataTable dt_cnt = new DataTable();
        //            da_cnt.Fill(dt_cnt);

        //            for (int a = 0; a < dt_cnt.Rows.Count; a++)
        //            {

        //                SqlCommand cmd_city = new SqlCommand("Select * from City where country_id = " + dt_cnt.Rows[a]["id"] + "order by name asc", conn);
        //                SqlDataAdapter da_city = new SqlDataAdapter(cmd_city);
        //                DataTable dt_city = new DataTable();
        //                da_city.Fill(dt_city);

        //                for (int a1 = 0; a1 < dt_city.Rows.Count; a1++)
        //                {
        //                    SqlCommand cmd_floor = new SqlCommand("select id,name from DesignRule_Floor where proj_id is NULL", conn);
        //                    SqlDataAdapter da_floor = new SqlDataAdapter(cmd_floor);
        //                    DataTable dt_floor = new DataTable();
        //                    da_floor.Fill(dt_floor);
        //                    for (int f = 0; f < dt_floor.Rows.Count; f++)
        //                    {
        //                        SqlCommand cmd1 = new SqlCommand("Insert into Design_Rule_Country (design_rule_id,city_id,value,formula) values ((select id from design_rule where name = N'" + design_rule.name + "')," + dt_floor.Rows[f]["id"] + "," + design_rule.value + ",N'" + design_rule.formula + "')", conn);
        //                        cmd1.ExecuteNonQuery();
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            SqlCommand cmd_rule = new SqlCommand("Select count(*) from Design_Rule where name = N'" + design_rule.name + "';", conn);
        //            int cnt_rule = Convert.ToInt32(cmd_rule.ExecuteScalar());
        //            if (cnt_rule == 0)
        //            {
        //                SqlCommand cmd_designrule = new SqlCommand("Insert into Design_rule (name,unit_of_measurement_id,is_rebar) values (N'" + design_rule.name + "',(select id from unit_of_measurement where unit = N'" + design_rule.unit_of_measurement + "'), " + design_rule.isRebar + ")", conn);
        //                cmd_designrule.ExecuteNonQuery();

        //            }
        //            SqlCommand cmd = new SqlCommand("Select count(*) from Design_Rule_Country where design_rule_id = (select id from design_rule where name = N'" + design_rule.name + "') and city_id = (select id from city where country_id in (select id from country_code where country = N'" + country + @"') and name = N'" + city + @"') and floor_id = (select id from DesignRule_Floor where name = N'" + floor_name + "' and proj_id IS NULL);", conn);
        //            int cnt = Convert.ToInt32(cmd.ExecuteScalar());
        //            if (cnt == 0)
        //            {
        //                SqlCommand cmd1 = new SqlCommand("Insert into Design_Rule_Country (design_rule_id,city_id,value,formula,floor_id) values ((select id from design_rule where name = N'" + design_rule.name + "'),(select id from City where country_id in (select id from country_code where country = N'" + country + "') and name = N'" + city + "')," + design_rule.value + ",N'" + design_rule.formula + "',(select id from DesignRule_Floor where name = N'" + floor_name + "' and proj_id IS NULL))", conn);
        //                cmd1.ExecuteNonQuery();
        //            }
        //            else
        //            {
        //                SqlCommand cmd2 = new SqlCommand("update Design_Rule_Country set value = " + design_rule.value + ",formula = N'" + design_rule.formula + "' where design_rule_id in ( select id from design_rule where  name = N'" + design_rule.name + "') and floor_id =(select id from DesignRule_Floor where name = N'" + floor_name + "' and proj_id IS NULL) and city_id = (select id from City where country_id in (select id from country_code where country = N'" + country + "') and name = N'" + city + "');", conn);
        //                cmd2.ExecuteNonQuery();
        //            }
        //        }
        //        conn.Close();
        //        return 1;
        //    }
        //    catch (System.Exception ex)
        //    {


        //        if (state == ConnectionState.Open)
        //        {
        //            conn.Close();

        //        }
        //        Service17 exception1 = new Service17();
        //        exception1.SendErrorToText(ex);
        //        return 0;

        //    }

        //}

        public List<design_rule_country_project> Getdesignrule_Project(string country, string city, string proj_id, string proj_name)
        {
            int cnt2 = 0;
            int cnt1 = 0;
            int cnt10 = 0;
            SqlConnection conn = new SqlConnection(connection_string);
            //ConnectionState state = conn.State;
            try
            {
                #region All Country
                if (string.IsNullOrEmpty(country) && string.IsNullOrEmpty(city) && string.IsNullOrEmpty(proj_id) && string.IsNullOrEmpty(proj_name))
                {
                    List<design_rule_country_project> design_rule_country1 = new List<design_rule_country_project>();
                    conn.Open();
                    SqlCommand cmd_cnt = new SqlCommand("Select * from Country_code order by code;", conn);
                    SqlDataAdapter da_cnt = new SqlDataAdapter(cmd_cnt);
                    DataTable dt_cnt = new DataTable();
                    da_cnt.Fill(dt_cnt);

                    for (int a = 0; a < dt_cnt.Rows.Count; a++)
                    {
                        cnt10++;
                        List<design_rule_city_project> design_rule_city1 = new List<design_rule_city_project>();
                        design_rule_country_project design_rule_country2 = new design_rule_country_project();
                        SqlCommand cmd_city = new SqlCommand("Select id,name from City where country_id = " + dt_cnt.Rows[a]["id"] + "order by name asc", conn);
                        SqlDataAdapter da_city = new SqlDataAdapter(cmd_city);
                        DataTable dt_city = new DataTable();
                        da_city.Fill(dt_city);

                        for (int a1 = 0; a1 < dt_city.Rows.Count; a1++)
                        {
                            cnt10++;
                            design_rule_city_project design_rule_city2 = new design_rule_city_project();


                            SqlCommand cmd_project = new SqlCommand(@"select name,proj_guid from Project where created_on >='2022-09-23' and f_active = 1 and city_id = " + dt_city.Rows[a1]["id"], conn);
                            SqlDataAdapter da_project = new SqlDataAdapter(cmd_project);
                            DataTable dt_project = new DataTable();
                            da_project.Fill(dt_project);
                            List<design_rule_project> list_Proj = new List<design_rule_project>();

                            for (int p = 0; p < dt_project.Rows.Count; p++)
                            {
                                cnt10++;
                                SqlCommand cmd_floor_project = new SqlCommand(@"select df.id,df.name from DesignRule_Floor df where df.proj_id is NULL or df.proj_id = 
(select proj.id from Project proj where proj.proj_guid =N'" + dt_project.Rows[p]["proj_guid"].ToString() + "' and proj.name = N'" + dt_project.Rows[p]["name"].ToString() + @"')", conn);
                                SqlDataAdapter da_floor_project = new SqlDataAdapter(cmd_floor_project);
                                DataTable dt_floor_project = new DataTable();
                                da_floor_project.Fill(dt_floor_project);

                                List<design_rule_project_floor> list_floor = new List<design_rule_project_floor>();

                                cnt1 += dt_floor_project.Rows.Count;
                                for (int f = 0; f < dt_floor_project.Rows.Count; f++)
                                {
                                    cnt10++;
                                    List<design_rule> design_rule1 = new List<design_rule>();

                                    SqlCommand cmd = new SqlCommand("select dr_proj.name,(select unit from Unit_Of_Measurement where id = dr_proj.unit_of_measurement_id) unit_of_measurement,value,formula from Design_Rule_Project dr_proj " + @"
left outer join (select ff.id id, ff.name, drc_proj.design_rule_project_id, drc_proj.value, drc_proj.formula,drc_proj.project_id from DesignRule_Floor ff
left outer join  Design_Rule_Country_Project drc_proj on drc_proj.floor_id_proj = ff.id and city_id = " + dt_city.Rows[a1]["id"] + " and drc_proj.project_id =(select id from project where proj_guid = N'" + dt_project.Rows[p]["proj_guid"].ToString() + @"' and name = N'" + dt_project.Rows[p]["name"].ToString() + @"') where 
ff.id = " + dt_floor_project.Rows[f]["id"] + @")design_proj on design_proj.design_rule_project_id = dr_proj.id where dr_proj.project_id = 
(select id from project where proj_guid = N'" + dt_project.Rows[p]["proj_guid"].ToString() + @"' and name = N'" + dt_project.Rows[p]["name"].ToString() + @"')
UNION
select dr.name,(select unit from Unit_Of_Measurement where id = dr.unit_of_measurement_id) unit_of_measurement,value,formula from design_rule dr left outer join
(select ff.id id, ff.name, drc.design_rule_project_id, drc.value,drc.formula,drc.design_rule_id,drc.project_id from DesignRule_Floor ff left outer join  
Design_Rule_Country_Project drc on drc.floor_id_proj = ff.id 
and city_id = " + dt_city.Rows[a1]["id"] + " and drc.project_id = (select id from project where proj_guid = N'" + dt_project.Rows[p]["proj_guid"].ToString() + @"' and name = N'" + dt_project.Rows[p]["name"].ToString() + @"') where ff.id = " + dt_floor_project.Rows[f]["id"] + @")design on design.design_rule_id = 
dr.id
where dr.is_rebar = 1 ", conn);



                               

                                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                                    DataTable dt = new DataTable();
                                    da.Fill(dt);
                                    if (dt.Rows.Count > 0)
                                    {
                                        cnt2 += dt.Rows.Count;
                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {
                                            cnt10++;

                                            design_rule design_rule2 = new design_rule();
                                            design_rule2.name = dt.Rows[i]["Name"].ToString();
                                            design_rule2.unit_of_measurement = dt.Rows[i]["unit_of_measurement"].ToString();
                                            design_rule2.value = string.IsNullOrEmpty(dt.Rows[i]["value"].ToString()) ? (Decimal?)null : Convert.ToDecimal(dt.Rows[i]["value"]);
                                            design_rule2.formula = dt.Rows[i]["formula"].ToString();
                                            design_rule2.isRebar = 1;//only need rebar indices in project specific(where dr.is_rebar = 1 )
                                            design_rule1.Add(design_rule2);
                                        }
                                    }

                                    design_rule_project_floor design_rule_proj_floor = new design_rule_project_floor();
                                    design_rule_proj_floor.proj_floorname = dt_floor_project.Rows[f]["name"].ToString();
                                    design_rule_proj_floor.design_rule = design_rule1;
                                    list_floor.Add(design_rule_proj_floor);

                                }

                                design_rule_project design_rule_proj = new design_rule_project();
                                design_rule_proj.project_id = dt_project.Rows[p]["proj_guid"].ToString();
                                design_rule_proj.project_name = dt_project.Rows[p]["name"].ToString();
                                design_rule_proj.design_rule_proj_floor = list_floor;
                                list_Proj.Add(design_rule_proj);
                            }

                            design_rule_city2.city = dt_city.Rows[a1]["name"].ToString();
                            design_rule_city2.design_rule_proj = list_Proj;
                            design_rule_city1.Add(design_rule_city2);
                        }

                        design_rule_country2.country = dt_cnt.Rows[a]["country"].ToString();
                        design_rule_country2.design_rule_city = design_rule_city1;
                        design_rule_country1.Add(design_rule_country2);
                    }

                    conn.Close();

                    return design_rule_country1;
                }
                #endregion

                #region Project Specific
                else
                {
                    List<design_rule_country_project> design_rule_country1 = new List<design_rule_country_project>();
                    conn.Open();
                    SqlCommand cmd_cnt = new SqlCommand("Select * from Country_code where country = N'" + country + "' order by code;", conn);
                    SqlDataAdapter da_cnt = new SqlDataAdapter(cmd_cnt);
                    DataTable dt_cnt = new DataTable();
                    da_cnt.Fill(dt_cnt);

                    for (int a = 0; a < dt_cnt.Rows.Count; a++)
                    {
                        List<design_rule_city_project> design_rule_city1 = new List<design_rule_city_project>();
                        design_rule_country_project design_rule_country2 = new design_rule_country_project();
                        SqlCommand cmd_city = new SqlCommand("Select id,name from City where country_id = " + dt_cnt.Rows[a]["id"] + " and name= N'" + city + "' order by name asc;", conn);
                        SqlDataAdapter da_city = new SqlDataAdapter(cmd_city);
                        DataTable dt_city = new DataTable();
                        da_city.Fill(dt_city);

                        for (int a1 = 0; a1 < dt_city.Rows.Count; a1++)
                        {

                            design_rule_city_project design_rule_city2 = new design_rule_city_project();


                            SqlCommand cmd_project = new SqlCommand(@"select name,proj_guid from Project where created_on >='2022-09-23' and f_active = 1 and  proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "' and city_id = " + dt_city.Rows[a1]["id"], conn);
                            SqlDataAdapter da_project = new SqlDataAdapter(cmd_project);
                            DataTable dt_project = new DataTable();
                            da_project.Fill(dt_project);
                            List<design_rule_project> list_Proj = new List<design_rule_project>();

                            for (int p = 0; p < dt_project.Rows.Count; p++)
                            {
                                SqlCommand cmd_floor_project = new SqlCommand(@"select df.id,df.name from DesignRule_Floor df where df.proj_id is NULL or df.proj_id = 
(select proj.id from Project proj where proj.proj_guid =N'" + dt_project.Rows[p]["proj_guid"].ToString() + "' and proj.name = N'" + dt_project.Rows[p]["name"].ToString() + @"')", conn);
                                SqlDataAdapter da_floor_project = new SqlDataAdapter(cmd_floor_project);
                                DataTable dt_floor_project = new DataTable();
                                da_floor_project.Fill(dt_floor_project);

                                List<design_rule_project_floor> list_floor = new List<design_rule_project_floor>();

                                for (int f = 0; f < dt_floor_project.Rows.Count; f++)
                                {
                                    List<design_rule> design_rule1 = new List<design_rule>();

                                    SqlCommand cmd = new SqlCommand("select dr.name,(select unit from Unit_Of_Measurement where id = dr.unit_of_measurement_id) unit_of_measurement,value,formula from design_rule dr left outer join " + @"
(select ff.id id, ff.name, drc.design_rule_id, drc.value,drc.formula,drc.project_id from DesignRule_Floor ff left outer join  Design_Rule_Country_Project drc on drc.floor_id_proj = ff.id 
and city_id = " + dt_city.Rows[a1]["id"] + @" and drc.project_id = (select id from project where proj_guid = N'" + dt_project.Rows[p]["proj_guid"].ToString() + "' and name = N'" + dt_project.Rows[p]["name"].ToString() + @"') where ff.id = " + dt_floor_project.Rows[f]["id"] + @")design on design.design_rule_id = dr.id
where dr.is_rebar = 1
UNION 
select dr_proj.name,(select unit from Unit_Of_Measurement where id = dr_proj.unit_of_measurement_id) unit_of_measurement,value,formula from Design_Rule_Project dr_proj 
left outer join (select ff.id id, ff.name, drc_proj.design_rule_project_id, drc_proj.value, drc_proj.formula,drc_proj.project_id from DesignRule_Floor ff
left outer join  Design_Rule_Country_Project drc_proj on drc_proj.floor_id_proj = ff.id and city_id = " + dt_city.Rows[a1]["id"] + @" and drc_proj.project_id = (select id from project where proj_guid = N'" + dt_project.Rows[p]["proj_guid"].ToString() + "' and name = N'" + dt_project.Rows[p]["name"].ToString() + @"'))design_proj on design_proj.design_rule_project_id = dr_proj.id where dr_proj.project_id = 
(select id from project where proj_guid = N'" + dt_project.Rows[p]["proj_guid"].ToString() + "' and name = N'" + dt_project.Rows[p]["name"].ToString() + @"')", conn);

                                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                                    DataTable dt = new DataTable();
                                    da.Fill(dt);
                                    if (dt.Rows.Count > 0)
                                    {
                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {
                                            design_rule design_rule2 = new design_rule();
                                            design_rule2.name = dt.Rows[i]["Name"].ToString();
                                            design_rule2.unit_of_measurement = dt.Rows[i]["unit_of_measurement"].ToString();
                                            design_rule2.value = string.IsNullOrEmpty(dt.Rows[i]["value"].ToString()) ? (Decimal?)null : Convert.ToDecimal(dt.Rows[i]["value"]);
                                            design_rule2.formula = dt.Rows[i]["formula"].ToString();
                                            design_rule2.isRebar = 1;//only need rebar indices in project specific(where dr.is_rebar = 1 )
                                            design_rule1.Add(design_rule2);
                                        }
                                    }

                                    design_rule_project_floor design_rule_proj_floor = new design_rule_project_floor();
                                    design_rule_proj_floor.proj_floorname = dt_floor_project.Rows[f]["name"].ToString();
                                    design_rule_proj_floor.design_rule = design_rule1;
                                    list_floor.Add(design_rule_proj_floor);

                                }

                                design_rule_project design_rule_proj = new design_rule_project();
                                design_rule_proj.project_id = dt_project.Rows[p]["proj_guid"].ToString();
                                design_rule_proj.project_name = dt_project.Rows[p]["name"].ToString();
                                design_rule_proj.design_rule_proj_floor = list_floor;
                                list_Proj.Add(design_rule_proj);
                            }

                            design_rule_city2.city = dt_city.Rows[a1]["name"].ToString();
                            design_rule_city2.design_rule_proj = list_Proj;
                            design_rule_city1.Add(design_rule_city2);
                        }

                        design_rule_country2.country = dt_cnt.Rows[a]["country"].ToString();
                        design_rule_country2.design_rule_city = design_rule_city1;
                        design_rule_country1.Add(design_rule_country2);
                    }

                    conn.Close();

                    return design_rule_country1;
                }
                #endregion
            }
            catch (System.Exception ex)
            {

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();

                }
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
                return null;

            }
        }

        public int updatedesignrule_Project([Optional]string city, [Optional]string country, design_rule design_rule, string floor_name, string proj_id, string proj_name, string created_by)
        {
            int result = 0;
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                #region Addition of rebar indice
                if (floor_name == "" && !string.IsNullOrEmpty(proj_id) && !string.IsNullOrEmpty(proj_name))
                {
                    SqlCommand cmd_rule = new SqlCommand("Select count(*) from Design_Rule_Project where name = N'" + design_rule.name + "' and project_id = (select id from Project where proj_guid =N'" + proj_id + "' and name= N'" + proj_name + "');", conn);
                    int cnt_rule_proj = Convert.ToInt32(cmd_rule.ExecuteScalar());
                    if (cnt_rule_proj == 0)
                    {
                        cmd_rule = new SqlCommand("Select count(*) from Design_Rule where is_rebar = 1 and name = N'" + design_rule.name + "';", conn);
                        int cnt_rule_master = Convert.ToInt32(cmd_rule.ExecuteScalar());
                        if (cnt_rule_master == 0)
                        {
                            SqlCommand cmd_designrule = new SqlCommand("Insert into Design_Rule_Project (name,unit_of_measurement_id,project_id,created_by) values (N'" + design_rule.name + "',(select id from unit_of_measurement where unit = N'" + design_rule.unit_of_measurement + "'),(select id from Project where proj_guid =N'" + proj_id + "' and name= N'" + proj_name + "'),N'" + created_by + "')", conn);
                            cmd_designrule.ExecuteNonQuery();
                            cnt_rule_proj = 1;

                        }
                        else
                        {
                            result = 0;
                        }
                    }
                    if (cnt_rule_proj == 1)
                    {
                        SqlCommand cmd_floor_master = new SqlCommand("select id from DesignRule_Floor where proj_id is null or proj_id = (select id from Project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "');", conn);
                        SqlDataAdapter da_floor_master = new SqlDataAdapter(cmd_floor_master);
                        DataTable dt_floor_master = new DataTable();
                        da_floor_master.Fill(dt_floor_master);
                        for (int f = 0; f < dt_floor_master.Rows.Count; f++)
                        {
                            SqlCommand cmd1 = new SqlCommand("Insert into Design_Rule_Country_Project (design_rule_project_id,city_id,value,formula,project_id,floor_id_proj) values " + @"
((select id from Design_Rule_Project where name = N'" + design_rule.name + "' and project_id = (select id from Project where proj_guid =N'" + proj_id + "' and name= N'" + proj_name + "')), " + @"
(select id from City where name = N'" + city + "' and country_id = (select id from Country_Code where country = N'" + country + "'))," + design_rule.value + ",N'" + design_rule.formula + "', " + @" 
(select id from Project where proj_guid =N'" + proj_id + "' and name= N'" + proj_name + "')," + dt_floor_master.Rows[f]["id"] + ");", conn);
                            cmd1.ExecuteNonQuery();
                            result = 1;
                        }
                    }

                }
                #endregion
                else
                {
                    SqlCommand cmd_rule = new SqlCommand("Select count(*) from Design_Rule_Project where name = N'" + design_rule.name + "' and project_id = (select id from Project where proj_guid =N'" + proj_id + "' and name= N'" + proj_name + "');", conn);
                    int cnt_rule_proj = Convert.ToInt32(cmd_rule.ExecuteScalar());
                    if (cnt_rule_proj == 0)
                    {
                        cmd_rule = new SqlCommand("Select count(*) from Design_Rule where is_rebar = 1 and name = N'" + design_rule.name + "';", conn);
                        int cnt_rule_master = Convert.ToInt32(cmd_rule.ExecuteScalar());
                        if (cnt_rule_master == 0)
                        {
                            SqlCommand cmd_designrule = new SqlCommand("Insert into Design_Rule_Project (name,unit_of_measurement_id,project_id) values (N'" + design_rule.name + "',(select id from unit_of_measurement where unit = N'" + design_rule.unit_of_measurement + "'),(select id from Project where proj_guid =N'" + proj_id + "' and name= N'" + proj_name + "'))", conn);
                            cmd_designrule.ExecuteNonQuery();
                            cnt_rule_proj = 1;

                        }
                    }

                    if (cnt_rule_proj == 1)
                    {

                        SqlCommand cmd = new SqlCommand("Select count(*) from Design_Rule_Country_Project where design_rule_project_id = (select id from Design_Rule_Project where name = N'" + design_rule.name + "') " + @"
and project_id = (select id from Project where proj_guid =N'" + proj_id + "' and name= N'" + proj_name + "') and city_id = (select id from city where country_id in (select id from country_code where country = N'" + country + "') " + @"
and name = N'" + city + "') and project_id = (select id from Project where proj_guid =N'" + proj_id + "' and name= N'" + proj_name + "') and floor_id_proj = (Select id from DesignRule_Floor where name = N'" + floor_name + "')", conn);
                        int cnt = Convert.ToInt32(cmd.ExecuteScalar());
                        if (cnt == 0)
                        {

                            SqlCommand cmd1 = new SqlCommand("Insert into Design_Rule_Country_Project (design_rule_project_id,city_id,value,formula,project_id,floor_id_proj) values " + @"
((select id from Design_Rule_Project where name = N'" + design_rule.name + "' and project_id = (select id from Project where proj_guid =N'" + proj_id + "' and name= N'" + proj_name + "')), " + @"
(select id from City where name = N'" + city + "' and country_id = (select id from Country_Code where country = N'" + country + "'))," + design_rule.value + ",N'" + design_rule.formula + "', " + @" 
(select id from Project where proj_guid =N'" + proj_id + "' and name= N'" + proj_name + "'),(Select id from DesignRule_Floor where name = N'" + floor_name + "'));", conn);
                            cmd1.ExecuteNonQuery();

                        }
                        else
                        {
                            SqlCommand cmd2 = new SqlCommand("update Design_Rule_Country_Project set value = " + design_rule.value + ",formula = N'" + design_rule.formula + "' where design_rule_project_id in ( select id from Design_Rule_Project where  name = N'" + design_rule.name + "' and " + @"
project_id = (select id from Project where proj_guid =N'" + proj_id + "' and name= N'" + proj_name + "')) and project_id = (select id from Project where proj_guid =N'" + proj_id + "' " + @"
and name= N'" + proj_name + "') and floor_id_proj = (select id from DesignRule_Floor where name = N'" + floor_name + "') and city_id = (select id from City where country_id in (select id from country_code where country = N'" + country + "') and name = N'" + city + "');", conn);
                            cmd2.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("Select count(*) from Design_Rule_Country_Project where design_rule_id = (select id from Design_Rule where name = N'" + design_rule.name + "') " + @"
and project_id = (select id from Project where proj_guid =N'" + proj_id + "' and name= N'" + proj_name + "') and city_id = (select id from city where country_id in (select id from country_code where country = N'" + country + "') " + @"
and name = N'" + city + "') and project_id = (select id from Project where proj_guid =N'" + proj_id + "' and name= N'" + proj_name + "') and floor_id_proj = (Select id from DesignRule_Floor where name = N'" + floor_name + "');", conn);
                        int cnt = Convert.ToInt32(cmd.ExecuteScalar());
                        if (cnt == 0)
                        {
                            SqlCommand cmd1 = new SqlCommand("Insert into Design_Rule_Country_Project (design_rule_project_id,city_id,value,formula,project_id,design_rule_id,floor_id_proj) values " + @"
((select id from Design_Rule_Project where name = N'" + design_rule.name + "' and project_id = (select id from Project where proj_guid =N'" + proj_id + "' and name= N'" + proj_name + "')), " + @"
(select id from City where name = N'" + city + "' and country_id = (select id from Country_Code where country = N'" + country + "'))," + design_rule.value + ",N'" + design_rule.formula + "', " + @" 
(select id from Project where proj_guid =N'" + proj_id + "' and name= N'" + proj_name + "'),(select id from Design_Rule where name =N'" + design_rule.name + "' and is_rebar = 1),(Select id from DesignRule_Floor where name = N'" + floor_name + "'));", conn);
                            cmd1.ExecuteNonQuery();
                        }
                        else
                        {
                            SqlCommand cmd2 = new SqlCommand("update Design_Rule_Country_Project set value = " + design_rule.value + ",formula = N'" + design_rule.formula + "' where design_rule_id in ( select id from Design_Rule where  name = N'" + design_rule.name + "' and " + @"
project_id = (select id from Project where proj_guid =N'" + proj_id + "' and name= N'" + proj_name + "')) and project_id = (select id from Project where proj_guid =N'" + proj_id + "' " + @"
and name= N'" + proj_name + "') and floor_id_proj = (select id from DesignRule_Floor where name = N'" + floor_name + "') and city_id = (select id from City where country_id in (select id from country_code where country = N'" + country + "') and name = N'" + city + "')  ;", conn);
                            cmd2.ExecuteNonQuery();
                        }
                    }

                    result = 1;

                }

                conn.Close();
                return result;
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

        public void CopyRebarIndices(string country, string city, string proj_id, string proj_name)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            try
            {
                conn.Open();
                SqlCommand cmd_cnt = new SqlCommand("Select * from Country_code where country = N'" + country + "' order by code;", conn);
                SqlDataAdapter da_cnt = new SqlDataAdapter(cmd_cnt);
                DataTable dt_cnt = new DataTable();
                da_cnt.Fill(dt_cnt);

                for (int a = 0; a < dt_cnt.Rows.Count; a++)
                {
                    List<design_rule_city> design_rule_city1 = new List<design_rule_city>();
                    design_rule_country design_rule_country2 = new design_rule_country();
                    SqlCommand cmd_city = new SqlCommand("Select id,name from City where country_id = " + dt_cnt.Rows[a]["id"] + " and name = N'" + city + "' order by name asc;", conn);
                    SqlDataAdapter da_city = new SqlDataAdapter(cmd_city);
                    DataTable dt_city = new DataTable();
                    da_city.Fill(dt_city);

                    for (int a1 = 0; a1 < dt_city.Rows.Count; a1++)
                    {
                        SqlCommand cmd = new SqlCommand("select dr.name,(select unit from Unit_Of_Measurement where id = dr.unit_of_measurement_id) unit_of_measurement,value,formula,is_rebar from design_rule dr left outer join " +
                                                   @" (select cc.id id, cc.name, drc.design_rule_id, drc.value,drc.formula from City cc
                                                            left outer join  Design_Rule_Country drc on drc.city_id = cc.id where cc.id = " + Convert.ToInt32(dt_city.Rows[a1]["id"]) + @")design on design.design_rule_id = dr.id ", conn);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                design_rule design_rule2 = new design_rule();
                                design_rule2.name = dt.Rows[i]["Name"].ToString();
                                design_rule2.unit_of_measurement = dt.Rows[i]["unit_of_measurement"].ToString();
                                design_rule2.value = string.IsNullOrEmpty(dt.Rows[i]["value"].ToString()) ? (Decimal?)null : Convert.ToDecimal(dt.Rows[i]["value"]);
                                design_rule2.formula = dt.Rows[i]["formula"].ToString();
                                design_rule2.isRebar = dt.Rows[i]["is_rebar"] == null ? 0 : Convert.ToInt16(dt.Rows[i]["is_rebar"]);

                                SqlCommand cmd_floor = new SqlCommand("select id,name from DesignRule_Floor where proj_id is NULL", conn);
                                SqlDataAdapter da_floor = new SqlDataAdapter(cmd_floor);
                                DataTable dt_floor = new DataTable();
                                da_floor.Fill(dt_floor);

                                for (int f = 0; f < dt_floor.Rows.Count; f++)
                                {
                                    SqlCommand cmd1 = new SqlCommand("Insert into Design_Rule_Country_Project(design_rule_project_id,city_id,value,formula,project_id,design_rule_id,floor_id_proj) " + @"
values (null," + dt_city.Rows[a1]["id"] + "," + design_rule2.value + ",N'" + design_rule2.formula + "',(select id from Project where name = N'" + proj_name + "' and proj_guid = N'" + proj_id + "'),(select id from design_rule where name = N'" + design_rule2.name + "')," + dt_floor.Rows[f]["id"] + ")", conn);
                                    cmd1.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }

                conn.Close();
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

        public string InsertFloor(string floorname, string copy_from_floor, string proj_id, string proj_name, string city, string country)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            try
            {
                string result = string.Empty;
                List<design_rule_country> design_rule_country1 = new List<design_rule_country>();
                conn.Open();
                #region Master
                if (string.IsNullOrEmpty(proj_id) && string.IsNullOrEmpty(proj_id))
                {
                    SqlCommand cmd_floor = new SqlCommand("Select COUNT(*) from DesignRule_Floor where name = N'" + floorname + "' and proj_id IS NULL;", conn);
                    int count = Convert.ToInt32(cmd_floor.ExecuteScalar());
                    if (count == 0)
                    {
                        SqlCommand cmd_insert = new SqlCommand("Insert into DesignRule_Floor (name) values (N'" + floorname + "');", conn);
                        cmd_insert.ExecuteNonQuery();
                        SqlCommand cmd_city = new SqlCommand("Select DISTINCT(city_id) from Design_Rule_Country", conn);
                        SqlDataAdapter da_city = new SqlDataAdapter(cmd_city);
                        DataTable dt_city = new DataTable();
                        da_city.Fill(dt_city);
                        for (int c = 0; c < dt_city.Rows.Count; c++)
                        {
                            SqlCommand cmd_rule = new SqlCommand("select * from Design_Rule_Country where city_id =" + dt_city.Rows[c]["city_id"] + " and floor_id = (select id from DesignRule_Floor where name = N'" + copy_from_floor + "');", conn);
                            SqlDataAdapter da_rule = new SqlDataAdapter(cmd_rule);
                            DataTable dt_rule = new DataTable();
                            da_rule.Fill(dt_rule);

                            for (int d = 0; d < dt_rule.Rows.Count; d++)
                            {
                                SqlCommand cmd_insert_newfloor = new SqlCommand("insert into Design_Rule_Country (design_rule_id,city_id,value,formula,floor_id) values (" + dt_rule.Rows[d]["design_rule_id"] + "," + dt_city.Rows[c]["city_id"] + "," + dt_rule.Rows[d]["value"] + ",N'" + dt_rule.Rows[d]["formula"] + "',(select id from DesignRule_Floor where proj_id is null and name = N'" + floorname + "'));", conn);
                                cmd_insert_newfloor.ExecuteNonQuery();
                            }

                            //                            SqlCommand cmd_project = new SqlCommand(@"select name,proj_guid from Project where created_on >='2022-09-23' and f_active = 1 and city_id = " + dt_city.Rows[c]["city_id"], conn);
                            //                            SqlDataAdapter da_project = new SqlDataAdapter(cmd_project);
                            //                            DataTable dt_project = new DataTable();
                            //                            da_project.Fill(dt_project);
                            //                            for (int p = 0; p < dt_project.Rows.Count; p++)
                            //                            {
                            //                                SqlCommand cmd_rule_proj = new SqlCommand("select * from Design_Rule_Country_Project where city_id =" + dt_city.Rows[c]["city_id"] + " and floor_id_proj = (select id from DesignRule_Floor where name = N'" + copy_from_floor + "') and project_id =(select id from Project where proj_guid =N'" + dt_project.Rows[p]["proj_guid"] + "' and name = N'" + dt_project.Rows[p]["name"] + "');", conn);
                            //                                SqlDataAdapter da_rule_proj = new SqlDataAdapter(cmd_rule_proj);
                            //                                DataTable dt_rule_proj = new DataTable();
                            //                                da_rule_proj.Fill(dt_rule_proj);

                            //                                for (int dp = 0; dp < dt_rule_proj.Rows.Count; dp++)
                            //                                {
                            //                                    SqlCommand cmd_insert_newfloor_proj = new SqlCommand("insert into Design_Rule_Country_Project (design_rule_id,city_id,value,formula,floor_id_proj,design_rule_project_id,project_id) values "+@"
                            //                                        (" + dt_rule_proj.Rows[dp]["design_rule_id"] + "," + dt_city.Rows[c]["city_id"] + "," + dt_rule_proj.Rows[dp]["value"] + ",N'" + dt_rule_proj.Rows[dp]["formula"] + "',(select id from DesignRule_Floor where proj_id is null and name = N'" + floorname + "')," + dt_rule_proj.Rows[dp]["design_rule_project_id"] + "," + dt_rule_proj.Rows[dp]["project_id"] + ");", conn);
                            //                                    cmd_insert_newfloor_proj.ExecuteNonQuery();
                            //                                }

                            //                            }

                        }
                        result = "Success";
                    }
                    else
                    {
                        result = "Exists";
                    }

                }
                #endregion

                #region Project Specific
                else
                {
                    SqlCommand cmd_floor = new SqlCommand("Select COUNT(*) from DesignRule_Floor where name = N'" + floorname + "' and proj_id = (Select id from Project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "');", conn);
                    int count = Convert.ToInt32(cmd_floor.ExecuteScalar());
                    if (count == 0)
                    {

                        SqlCommand cmd_inMaster = new SqlCommand("Select COUNT(*) from DesignRule_Floor where name = N'" + floorname + "' and proj_id Is NULL;", conn);
                        int count_master = Convert.ToInt32(cmd_inMaster.ExecuteScalar());

                        if (count_master == 0)
                        {
                            SqlCommand cmd_insert = new SqlCommand("Insert into DesignRule_Floor (name,proj_id) values (N'" + floorname + "', (Select id from Project where proj_guid = N'" + proj_id + "' and name =N'" + proj_name + "'));", conn);
                            cmd_insert.ExecuteNonQuery();

                            SqlCommand cmd_newfloorid = new SqlCommand("select id from DesignRule_Floor where name = N'" + floorname + "' and proj_id = (select id from Project where proj_guid = N'" + proj_id + "' and name =N'" + proj_name + "');", conn);
                            Int64 newfloorid = Convert.ToInt64(cmd_newfloorid.ExecuteScalar());

                            //SqlCommand cmd_city = new SqlCommand("select id from city where name = N'" + city + "' and country_id = (select id from Country_Code where country = N'" + country + "')", conn);
                            //SqlDataAdapter da_city = new SqlDataAdapter(cmd_city);
                            //DataTable dt_city = new DataTable();
                            //da_city.Fill(dt_city);
                            //for (int c = 0; c < dt_city.Rows.Count; c++)
                            //{

                            SqlCommand cmd_rule = new SqlCommand("select * from Design_Rule_Country_Project where project_id = (select id from Project where (proj_guid = N'" + proj_id + @"' 
                                and name = N'" + proj_name + @"') and floor_id_proj = (select id from DesignRule_Floor where name = N'" + copy_from_floor + @"')) and  
                                (design_rule_id in (select id from Design_Rule where is_rebar = 1) or design_rule_project_id in (select id from Design_Rule_Project where project_id = 
(select id from Project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "')));", conn);


                            //SqlCommand cmd_rule = new SqlCommand("select * from Design_Rule_Country_Project where city_id = " + dt_city.Rows[c]["id"] + " and design_rule_id in (select id from Design_Rule where is_rebar = 1) or design_rule_project_id in (select id from Design_Rule_Project where project_id = (select id from Project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "'));", conn);
                            //SqlCommand cmd_rule = new SqlCommand("select * from Design_Rule_Country where city_id = " + dt_city.Rows[c]["id"] + " and design_rule_id in (select id from Design_Rule where is_rebar = 1);", conn);
                            SqlDataAdapter da_rule = new SqlDataAdapter(cmd_rule);
                            DataTable dt_rule = new DataTable();
                            da_rule.Fill(dt_rule);

                            //SqlCommand cmd_pf = new SqlCommand("select id from DesignRule_Floor where proj_id IS NULL or proj_id = (select id from Project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "');", conn);
                            //SqlDataAdapter da_pf = new SqlDataAdapter(cmd_pf);
                            //DataTable dt_pf = new DataTable();
                            //da_pf.Fill(dt_pf);

                            //for (int pf = 0; pf < dt_pf.Rows.Count; pf++)
                            //{
                            for (int d = 0; d < dt_rule.Rows.Count; d++)
                            {
                                if (dt_rule.Rows[d]["design_rule_id"] == System.DBNull.Value)
                                {
                                    SqlCommand cmd_insert_d = new SqlCommand("insert into Design_Rule_Country_Project (city_id,value,formula,project_id,floor_id_proj,design_rule_project_id) values (" + dt_rule.Rows[d]["city_id"] + "," + dt_rule.Rows[d]["value"] + ",N'" + dt_rule.Rows[d]["formula"] + "',(select id from Project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "')," + newfloorid + "," + dt_rule.Rows[d]["design_rule_project_id"] + ")", conn);
                                    cmd_insert_d.ExecuteNonQuery();
                                }
                                else
                                {
                                    SqlCommand cmd_insert_d = new SqlCommand("insert into Design_Rule_Country_Project (city_id,value,formula,project_id,design_rule_id,floor_id_proj) values (" + dt_rule.Rows[d]["city_id"] + "," + dt_rule.Rows[d]["value"] + ",N'" + dt_rule.Rows[d]["formula"] + "',(select id from Project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "')," + dt_rule.Rows[d]["design_rule_id"] + "," + newfloorid + ")", conn);
                                    cmd_insert_d.ExecuteNonQuery();
                                }

                            }
                            //}
                            //}

                            result = "Success";
                        }
                        else
                        {
                            result = "Exists";
                        }
                    }
                    else
                    {
                        result = "Exists";
                    }
                }
                #endregion

                conn.Close();
                return result;

            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
                return "failure";
            }
        }

        public List<string> GetExistingFloors(string proj_id, string proj_name)
        {
            List<string> lstFloors = new List<string>();
            SqlConnection conn = new SqlConnection(connection_string);
            try
            {
                conn.Open();

                if (String.IsNullOrEmpty(proj_id) && String.IsNullOrEmpty(proj_name))
                {
                    SqlCommand cmd_floor = new SqlCommand("Select name from DesignRule_Floor where proj_id IS NULL;", conn);
                    SqlDataAdapter da_cnt = new SqlDataAdapter(cmd_floor);
                    DataTable dt_cnt = new DataTable();
                    da_cnt.Fill(dt_cnt);
                    for (int i = 0; i < dt_cnt.Rows.Count; i++)
                    {
                        lstFloors.Add(dt_cnt.Rows[i]["name"].ToString());
                    }
                    conn.Close();
                    return lstFloors;
                }
                else
                {
                    SqlCommand cmd_floor = new SqlCommand("Select name from DesignRule_Floor where proj_id IS NULL or proj_id = (Select id from Project where proj_guid = N'" + proj_id + "' and name =N'" + proj_name + "');", conn);
                    SqlDataAdapter da_cnt = new SqlDataAdapter(cmd_floor);
                    DataTable dt_cnt = new DataTable();
                    da_cnt.Fill(dt_cnt);
                    for (int i = 0; i < dt_cnt.Rows.Count; i++)
                    {
                        lstFloors.Add(dt_cnt.Rows[i]["name"].ToString());
                    }
                    conn.Close();
                    return lstFloors;
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

                return lstFloors;
            }
        }

        public void Updateallfloors()
        {
            SqlConnection conn = new SqlConnection(connection_string);
            try
            {
                conn.Open();

                SqlCommand cmd_floor = new SqlCommand("select DISTINCT(city_id) from Design_Rule_Country;", conn);
                SqlDataAdapter da_cnt = new SqlDataAdapter(cmd_floor);
                DataTable dt_cnt = new DataTable();
                da_cnt.Fill(dt_cnt);
                for (int i = 0; i < dt_cnt.Rows.Count; i++)
                {
                    SqlCommand cmd1 = new SqlCommand("select * from Design_Rule_Country where city_id = " + dt_cnt.Rows[i]["city_id"] + ";", conn);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    for (int i1 = 0; i1 < dt1.Rows.Count; i1++)
                    {
                        SqlCommand cmd_floor_master = new SqlCommand("select id from DesignRule_Floor where proj_id is null;", conn);
                        SqlDataAdapter da_floor_master = new SqlDataAdapter(cmd_floor_master);
                        DataTable dt_floor_master = new DataTable();
                        da_floor_master.Fill(dt_floor_master);
                        for (int f = 0; f < dt_floor_master.Rows.Count; f++)
                        {
                            //if (dt1.Rows[i1]["floor_id"].ToString() == string.Empty)
                            //{
                            SqlCommand cmd_insert = new SqlCommand("insert into Design_Rule_Country (design_rule_id,city_id,value,formula,floor_id) " + @"
                            values(" + dt1.Rows[i1]["design_rule_id"] + "," + dt_cnt.Rows[i]["city_id"] + "," + dt1.Rows[i1]["value"] + ",N'" + dt1.Rows[i1]["formula"] + "'," + dt_floor_master.Rows[f]["id"] + ");", conn);
                            cmd_insert.ExecuteNonQuery();
                            //} 
                        }
                    }
                }

                conn.Close();
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

        public void Updateallfloors_Project()
        {
            SqlConnection conn = new SqlConnection(connection_string);
            try
            {
                conn.Open();

                SqlCommand cmd_floor = new SqlCommand("select DISTINCT(city_id) from Design_Rule_Country;", conn);
                SqlDataAdapter da_cnt = new SqlDataAdapter(cmd_floor);
                DataTable dt_cnt = new DataTable();
                da_cnt.Fill(dt_cnt);

                for (int i = 0; i < dt_cnt.Rows.Count; i++)
                {
                    SqlCommand cmd_proj = new SqlCommand("select name,proj_guid from Project where created_on >='2022-09-23' and f_active = 1 and city_id = " + dt_cnt.Rows[i]["city_id"] + ";", conn);
                    SqlDataAdapter da_proj = new SqlDataAdapter(cmd_proj);
                    DataTable dt_proj = new DataTable();
                    da_proj.Fill(dt_proj);


                    for (int p = 0; p < dt_proj.Rows.Count; p++)
                    {
                        SqlCommand cmd1 = new SqlCommand("select * from Design_Rule where is_rebar = 1 UNION select * from Design_Rule_Project where project_id = (select id from Project where proj_guid = N'" + dt_proj.Rows[p]["proj_guid"] + "' and name = N'" + dt_proj.Rows[p]["name"] + "');", conn);
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        for (int i1 = 0; i1 < dt1.Rows.Count; i1++)
                        {
                            SqlCommand cmd_floor_master = new SqlCommand("select id from DesignRule_Floor where proj_id is null or proj_id = (select id from Project where proj_guid = N'" + dt_proj.Rows[p]["proj_guid"] + "' and name = N'" + dt_proj.Rows[p]["name"] + "');", conn);
                            SqlDataAdapter da_floor_master = new SqlDataAdapter(cmd_floor_master);
                            DataTable dt_floor_master = new DataTable();
                            da_floor_master.Fill(dt_floor_master);
                            for (int f = 0; f < dt_floor_master.Rows.Count; f++)
                            {

                                SqlCommand cmd_insert = new SqlCommand("select * from Design_Rule_Country where city_id = " + dt_cnt.Rows[i]["city_id"] + " and design_rule_id = " + dt1.Rows[i1]["id"] + ";", conn);
                                SqlDataAdapter da_insert = new SqlDataAdapter(cmd_insert);
                                DataTable dt_insert = new DataTable();
                                da_insert.Fill(dt_insert);

                                for (int cc = 0; cc < dt_insert.Rows.Count; cc++)
                                {
                                    if (dt1.Rows[i1]["is_rebar"].ToString() == "1")
                                    {

                                        SqlCommand cmd = new SqlCommand("insert into Design_Rule_Country_Project(design_rule_project_id,city_id,value,formula,project_id,design_rule_id,floor_id_proj) " + @"
values(NULL," + dt_cnt.Rows[i]["city_id"] + "," + dt_insert.Rows[cc]["value"] + " ,N'" + dt_insert.Rows[cc]["formula"] + "',(select id from Project where proj_guid = N'" + dt_proj.Rows[p]["proj_guid"] + "' and name = N'" + dt_proj.Rows[p]["name"] + "')," + dt_insert.Rows[cc]["design_rule_id"] + "," + dt_floor_master.Rows[f]["id"] + ");", conn);
                                        cmd.ExecuteNonQuery();

                                    }
                                    else
                                    {
                                        SqlCommand cmd = new SqlCommand("insert into Design_Rule_Country_Project(design_rule_project_id,city_id,value,formula,project_id,design_rule_id,floor_id_proj) " + @"
values(" + dt1.Rows[i1]["is_rebar"] + "," + dt_cnt.Rows[i]["city_id"] + "," + dt_insert.Rows[cc]["value"] + " ,N'" + dt_insert.Rows[cc]["formula"] + "',(select id from Project where proj_guid = N'" + dt_proj.Rows[p]["proj_guid"] + "' and name = N'" + dt_proj.Rows[p]["name"] + "'),NULL," + dt_floor_master.Rows[f]["id"] + ");", conn);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }

                    }
                }

                conn.Close();
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

        public void Updateallfloors_city()
        {
            SqlConnection conn = new SqlConnection(connection_string);
            try
            {
                conn.Open();

                //SqlCommand cmd_floor = new SqlCommand("select DISTINCT(city_id) from Design_Rule_Country;", conn);
                //SqlDataAdapter da_cnt = new SqlDataAdapter(cmd_floor);
                //DataTable dt_cnt = new DataTable();
                //da_cnt.Fill(dt_cnt);
                //for (int i = 0; i < dt_cnt.Rows.Count; i++)
                //{
                SqlCommand cmd1 = new SqlCommand("select * from Design_Rule_Country where city_id = 2;", conn);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                for (int i1 = 0; i1 < dt1.Rows.Count; i1++)
                {
                    SqlCommand cmd_floor_master = new SqlCommand("select id from DesignRule_Floor where proj_id is null and id not in (1);", conn);
                    SqlDataAdapter da_floor_master = new SqlDataAdapter(cmd_floor_master);
                    DataTable dt_floor_master = new DataTable();
                    da_floor_master.Fill(dt_floor_master);
                    for (int f = 0; f < dt_floor_master.Rows.Count; f++)
                    {
                        //if (dt1.Rows[i1]["floor_id"].ToString() == string.Empty)
                        //{
                        SqlCommand cmd_insert = new SqlCommand("insert into Design_Rule_Country (design_rule_id,city_id,value,formula,floor_id) " + @"
                            values(" + dt1.Rows[i1]["design_rule_id"] + ",2," + dt1.Rows[i1]["value"] + ",N'" + dt1.Rows[i1]["formula"] + "'," + dt_floor_master.Rows[f]["id"] + ");", conn);
                        cmd_insert.ExecuteNonQuery();
                        //} 
                    }
                }
                //}

                conn.Close();
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

        public void UpdateONEfloors_Project()
        {
            SqlConnection conn = new SqlConnection(connection_string);
            try
            {
                conn.Open();

                SqlCommand cmd_floor = new SqlCommand("select DISTINCT(city_id) from Design_Rule_Country;", conn);
                SqlDataAdapter da_cnt = new SqlDataAdapter(cmd_floor);
                DataTable dt_cnt = new DataTable();
                da_cnt.Fill(dt_cnt);

                for (int i = 0; i < dt_cnt.Rows.Count; i++)
                {
                    SqlCommand cmd_proj = new SqlCommand("select name,proj_guid from Project where created_on >='2022-09-23' and f_active = 1 and city_id = " + dt_cnt.Rows[i]["city_id"] + ";", conn);
                    SqlDataAdapter da_proj = new SqlDataAdapter(cmd_proj);
                    DataTable dt_proj = new DataTable();
                    da_proj.Fill(dt_proj);


                    for (int p = 0; p < dt_proj.Rows.Count; p++)
                    {
                        SqlCommand cmd1 = new SqlCommand("select * from Design_Rule where is_rebar = 1 UNION select * from Design_Rule_Project where project_id = (select id from Project where proj_guid = N'" + dt_proj.Rows[p]["proj_guid"] + "' and name = N'" + dt_proj.Rows[p]["name"] + "');", conn);
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        for (int i1 = 0; i1 < dt1.Rows.Count; i1++)
                        {
                            SqlCommand cmd_floor_master = new SqlCommand("select id from DesignRule_Floor where name = 'GL';", conn);
                            SqlDataAdapter da_floor_master = new SqlDataAdapter(cmd_floor_master);
                            DataTable dt_floor_master = new DataTable();
                            da_floor_master.Fill(dt_floor_master);
                            for (int f = 0; f < dt_floor_master.Rows.Count; f++)
                            {

                                SqlCommand cmd_insert = new SqlCommand("select * from Design_Rule_Country where city_id = " + dt_cnt.Rows[i]["city_id"] + " and design_rule_id = " + dt1.Rows[i1]["id"] + ";", conn);
                                SqlDataAdapter da_insert = new SqlDataAdapter(cmd_insert);
                                DataTable dt_insert = new DataTable();
                                da_insert.Fill(dt_insert);

                                for (int cc = 0; cc < dt_insert.Rows.Count; cc++)
                                {
                                    if (dt1.Rows[i1]["is_rebar"].ToString() == "1")
                                    {

                                        SqlCommand cmd = new SqlCommand("insert into Design_Rule_Country_Project(design_rule_project_id,city_id,value,formula,project_id,design_rule_id,floor_id_proj) " + @"
values(NULL," + dt_cnt.Rows[i]["city_id"] + "," + dt_insert.Rows[cc]["value"] + " ,N'" + dt_insert.Rows[cc]["formula"] + "',(select id from Project where proj_guid = N'" + dt_proj.Rows[p]["proj_guid"] + "' and name = N'" + dt_proj.Rows[p]["name"] + "')," + dt_insert.Rows[cc]["design_rule_id"] + "," + dt_floor_master.Rows[f]["id"] + ");", conn);
                                        cmd.ExecuteNonQuery();

                                    }
                                    else
                                    {
                                        SqlCommand cmd = new SqlCommand("insert into Design_Rule_Country_Project(design_rule_project_id,city_id,value,formula,project_id,design_rule_id,floor_id_proj) " + @"
values(" + dt1.Rows[i1]["is_rebar"] + "," + dt_cnt.Rows[i]["city_id"] + "," + dt_insert.Rows[cc]["value"] + " ,N'" + dt_insert.Rows[cc]["formula"] + "',(select id from Project where proj_guid = N'" + dt_proj.Rows[p]["proj_guid"] + "' and name = N'" + dt_proj.Rows[p]["name"] + "'),NULL," + dt_floor_master.Rows[f]["id"] + ");", conn);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }

                    }
                }

                conn.Close();
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



        ///Code Added By Vrushabh
        public List<design_rule_country_project> Getdesignrule_Project_Test(string country, string city, string proj_id, string proj_name)
        {

            SqlConnection conn = new SqlConnection(connection_string);
            try
            {

                conn.Open();
                SqlCommand cmd_cnt = new SqlCommand();

//                cmd_cnt.CommandText = @"Select Country_Code.country as CountryName,City.name as CityName,Project.name as project_name, Project.proj_guid as proj_guid,design_proj.Floorname,design_proj.floor_id as floor_id,drp.name,(select unit from Unit_Of_Measurement where id = drp.unit_of_measurement_id) unit_of_measurement,value,formula,'ProjectSpecific' as DRM  from Design_Rule_Project drp left outer join 
//(Select drc_proj.design_rule_project_id,DesignRule_Floor.name as Floorname,DesignRule_Floor.id as floor_id,drc_proj.project_id,drc_proj.value,drc_proj.formula from Design_Rule_Country_Project drc_proj right outer join DesignRule_Floor on DesignRule_Floor.id=drc_proj.floor_id_proj  ) design_proj on drp.id=design_proj.design_rule_project_id
//right outer join Project on Project.id=design_proj.project_id  right outer join City on Project.city_id=City.id   Right outer join  Country_Code on Country_Code.id=City.country_id and Project.created_on >='2022-09-23' and f_active = 1
//union all
//Select Country_Code.country as CountryName,City.name as CityName,Project.name as project_name,Project.proj_guid as proj_guid,design.Floorname,design.floor_id as floor_id,dr.name,(select unit from Unit_Of_Measurement where id = dr.unit_of_measurement_id) unit_of_measurement,value,formula,'Master' as DRM   from Design_Rule dr left outer join 
//(Select drc_proj.design_rule_id,DesignRule_Floor.name as Floorname,DesignRule_Floor.id as floor_id,drc_proj.project_id,drc_proj.value,drc_proj.formula from Design_Rule_Country_Project drc_proj right outer join DesignRule_Floor on DesignRule_Floor.id=drc_proj.floor_id_proj  ) design on dr.id=design.design_rule_id
//right outer join Project on Project.id=design.project_id right outer join City on Project.city_id=City.id   Right outer join  Country_Code on Country_Code.id=City.country_id
//where dr.is_rebar = 1 and Project.created_on >='2022-09-23' and f_active = 1 
//
//ORDER BY 
//    CountryName ASC,
//    CityName ASC,
//    project_name asc,
//    floor_id asc";

                cmd_cnt.CommandText = @"select Country_Code.country as CountryName,City.name as CityName,M.project_name,M.proj_guid,M.Floorname,M.floor_id,M.name,M.unit_of_measurement,M.value,M.formula,'ProjectSpecific' as DRM  from(Select Project.name as project_name, Project.proj_guid as proj_guid,Project.city_id as Cid,design_proj.Floorname,design_proj.floor_id as floor_id,drp.name,(select unit from Unit_Of_Measurement where id = drp.unit_of_measurement_id) unit_of_measurement,value,formula from Design_Rule_Project drp left outer join 
(Select drc_proj.design_rule_project_id,DesignRule_Floor.name as Floorname,DesignRule_Floor.id as floor_id,drc_proj.project_id,drc_proj.value,drc_proj.formula from Design_Rule_Country_Project drc_proj right outer join DesignRule_Floor on DesignRule_Floor.id=drc_proj.floor_id_proj  ) design_proj on drp.id=design_proj.design_rule_project_id
right outer join Project on Project.id=design_proj.project_id where Project.created_on >='2022-09-23' and f_active = 1 ) M
right outer join City on M.Cid=City.id  
Right outer join  Country_Code on Country_Code.id=City.country_id 

union all

 Select Country_Code.country as CountryName,City.name as CityName,M.project_name,M.proj_guid,M.Floorname,M.floor_id,M.name,M.unit_of_measurement,M.value,M.formula,'Master' as DRM from (Select Project.name as project_name,Project.proj_guid as proj_guid,Project.city_id as Cid,design.Floorname,design.floor_id as floor_id,dr.name,(select unit from Unit_Of_Measurement where id = dr.unit_of_measurement_id) unit_of_measurement,value,formula,dr.is_rebar as is_rebar   from Design_Rule dr left outer join 
(Select drc_proj.design_rule_id,DesignRule_Floor.name as Floorname,DesignRule_Floor.id as floor_id,drc_proj.project_id,drc_proj.value,drc_proj.formula from Design_Rule_Country_Project drc_proj right outer join DesignRule_Floor on DesignRule_Floor.id=drc_proj.floor_id_proj  ) design on dr.id=design.design_rule_id
right outer join Project on Project.id=design.project_id and Project.created_on >='2022-09-23' and f_active = 1) M
right outer join City on M.Cid=City.id 
right outer join  Country_Code on Country_Code.id=City.country_id
where M.is_rebar = 1

ORDER BY 
    CountryName ASC,
    CityName ASC,
    project_name asc,
    floor_id asc";

                cmd_cnt.Connection = conn;

                SqlDataReader reader = cmd_cnt.ExecuteReader();

                var designRuleDataList = new List<desig_Rule_project>();

                while (reader.Read())
                {
                    var designRuleData = new desig_Rule_project
                    {
                        Country = reader["CountryName"].ToString(),
                        City = reader["CityName"].ToString(),
                        Project = reader["project_name"].ToString(),
                        Project_Id = reader["proj_guid"].ToString(),
                        Floor = reader["Floorname"].ToString(),
                        floor_id = reader["floor_id"].ToString(),
                        DesignRuleName = reader["name"].ToString(),
                        UnitOfMeasurement = reader["unit_Of_measurement"].ToString(),
                        Value = reader["value"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(reader["value"]),
                        Formula = reader["formula"].ToString(),
                    };

                    designRuleDataList.Add(designRuleData);
                }




                List<design_rule_country_project> design_rule_country1 = new List<design_rule_country_project>();

                foreach (var cntry in designRuleDataList.GroupBy(x => x.Country).ToList())
                {
                    List<design_rule_city_project> design_rule_city1 = new List<design_rule_city_project>();
                    foreach (var city1 in cntry.GroupBy(x => x.City).ToList())
                    {
                        List<design_rule_project> list_Proj = new List<design_rule_project>();
                        foreach (var proj in city1.GroupBy(x => new { x.Project, x.Project_Id }).ToList())
                        {
                            List<design_rule_project_floor> list_floor = new List<design_rule_project_floor>();
                            if (string.IsNullOrEmpty(proj.Key.Project))
                            {
                                continue;
                            }
                            foreach (var floorname in proj.GroupBy(x => x.Floor).Select(y => new { floor = y.Key, Details = y.ToList() }).ToList())
                            {
                                if (floorname == null) {
                                    continue;
                                }
                                List<design_rule> design_rule1 = new List<design_rule>();
                                foreach (var designRule in floorname.Details)
                                {

                                    design_rule design_rule2 = new design_rule();
                                    design_rule2.name = designRule.DesignRuleName;
                                    design_rule2.unit_of_measurement = designRule.UnitOfMeasurement;
                                    design_rule2.value = designRule.Value;
                                    design_rule2.formula = designRule.Formula;
                                    design_rule1.Add(design_rule2);
                                }

                                design_rule_project_floor floor = new design_rule_project_floor();
                                floor.proj_floorname = floorname.floor;
                                floor.design_rule = design_rule1;
                                list_floor.Add(floor);
                            }
                            design_rule_project project = new design_rule_project();
                            project.project_name = proj.Key.Project;
                            project.project_id = proj.Key.Project_Id;
                            project.design_rule_proj_floor = list_floor;
                            list_Proj.Add(project);
                        }

                        design_rule_city_project cityobj = new design_rule_city_project();
                        cityobj.city = city1.Key;
                        cityobj.design_rule_proj = list_Proj;
                        design_rule_city1.Add(cityobj);
                    }

                    design_rule_country_project countryObj = new design_rule_country_project();
                    countryObj.country = cntry.Key;
                    countryObj.design_rule_city = design_rule_city1;
                    design_rule_country1.Add(countryObj);
                }


                return design_rule_country1;
            }

            catch (System.Exception ex)
            {

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();

                }
                Service17 exception1 = new Service17();
                exception1.SendErrorToText(ex);
                return null;

            }


        }

    }
}