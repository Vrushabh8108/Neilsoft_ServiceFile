using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service23" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service23.svc or Service23.svc.cs at the Solution Explorer and start debugging.
    public class Service23 : CostAnalysisRep
    {
        string subvarianceName = string.Empty;
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
//        public CostAnalysisReport GetCostReport(string Country, string City, DateTime FromDate, DateTime ToDate, [Optional]bool isWeekly, [Optional]bool isMonthly, [Optional]bool isYearly, List<InputSubvariance> listSubvariance)
//        {
            
//            //return date;// returns 09/25/2011
//            //DateTime FromDate = DateTime.Parse(FromDate1, System.Globalization.CultureInfo.InvariantCulture);
//            SqlConnection conn = new SqlConnection(connection_string);
//            ConnectionState state = conn.State;
//            try
//            {
//                conn.Open();
//                CostAnalysisReport CostAnalysisRep1 = new CostAnalysisReport();
//                CostAnalysisRep1.Country = Country;
//                CostAnalysisRep1.City = City;
//                List<year_week_month_dtl> year_week_month_dtl1 = new List<year_week_month_dtl>();
//                    double no_of_loop = 0;
//                DateTime eff_start_date = FromDate.Date.Add(new TimeSpan(0, 0, 0));
//                DateTime eff_end_date = ToDate.Date.Add(new TimeSpan(23, 59, 0));

//                if (isWeekly == true)
//                {
//                    double diff = Math.Round((((ToDate - FromDate).TotalDays) / 7),2);
//                    no_of_loop = Math.Ceiling(diff);
//                    eff_start_date = FromDate;
//                    eff_end_date = FromDate.AddDays(6).Date.Add(new TimeSpan(23,59,0));
//                    if (eff_end_date > ToDate)
//                    {
//                        eff_end_date = ToDate;
//                    }
//                }
//                if (isMonthly == true)
//                {
//                    double diff = Math.Round((((ToDate - FromDate).TotalDays) / 30),2);
//                    no_of_loop = Math.Ceiling(diff);
//                    eff_start_date = FromDate;
//                    eff_end_date = FromDate.AddMonths(1);
//                    eff_end_date = eff_end_date.AddDays(-1).Date.Add(new TimeSpan(23, 59, 0));
//                    if (eff_end_date > ToDate)
//                    {
//                        eff_end_date = ToDate;
//                    }
//                }
//                if (isYearly == true)
//                {
//                    double diff = Math.Round((((ToDate - FromDate).TotalDays) / 365),2);
//                    no_of_loop = Math.Ceiling(diff);
//                    eff_start_date = FromDate;
//                    eff_end_date = FromDate.AddYears(1);
//                    eff_end_date = eff_end_date.AddDays(-1).Date.Add(new TimeSpan(23, 59, 0));
//                    if (eff_end_date > ToDate)
//                    {
//                        eff_end_date = ToDate;
//                    }
//                }
//                if (isWeekly == true || isMonthly == true || isYearly == true)
//                {
//                    int i = 0;
//                    // for (int i = 0; i < no_of_loop; i++)
//                    //{
//                   do
//                    {
//                        if (eff_start_date > ToDate)
//                        {
//                            eff_start_date = ToDate.Date.Add(new TimeSpan(0, 0, 0));
//                        }
//                        if (eff_end_date > ToDate)
//                        {
//                            eff_end_date = ToDate.Date.Add(new TimeSpan(23, 59, 0));
//                        }

//                        year_week_month_dtl year_week_month_dtl2 = new year_week_month_dtl();
//                        year_week_month_dtl2.no = i;
//                        year_week_month_dtl2.from_date = eff_start_date;
//                        year_week_month_dtl2.to_date = eff_end_date;
//                        List<Job> Job1 = new List<Job>();
//                        for (int s = 0; s < listSubvariance.Count; s++)
//                        {
//                            SqlCommand cmd_cnt = new SqlCommand("Select * from Material where name =N'" + listSubvariance[s].Job + @"';", conn);
//                            SqlDataAdapter da_cnt = new SqlDataAdapter(cmd_cnt);
//                            DataTable dt_cnt = new DataTable();
//                            da_cnt.Fill(dt_cnt);
//                            string materialOptionName;

//                            for (int a = 0; a < dt_cnt.Rows.Count; a++)
//                            {
//                                Job Job2 = new Job();
//                                Job2.JobName = dt_cnt.Rows[a]["name"].ToString();
//                                SqlCommand cmd_var = new SqlCommand("Select * from Material_variance where variance =N'" + listSubvariance[s].Subtype + @"' and material_id = " + dt_cnt.Rows[a]["id"] + ";", conn);
//                                SqlDataAdapter da_var = new SqlDataAdapter(cmd_var);
//                                DataTable dt_var = new DataTable();
//                                da_var.Fill(dt_var);
//                                List<JobSubType> JobSubType1 = new List<JobSubType>();
//                                for (int a1 = 0; a1 < dt_var.Rows.Count; a1++)
//                                {
//                                    JobSubType JobSubType2 = new JobSubType();
//                                    List<JobSubTypeSubVarianceInfo> JobSubTypeSubVarianceInfo1 = new List<JobSubTypeSubVarianceInfo>();

//                                    JobSubType2.JobTypeName = dt_var.Rows[a1]["variance"].ToString();
//                                    //SqlCommand cmd_var_sub = new SqlCommand("Select  id,name,(select name from Unit_Of_Measurement where id = unit_of_measurement) unit from Material_Variance_Subdivision where material_variance_id = " + dt_var.Rows[a1]["id"] + ";", conn);
//                                    SqlCommand cmd_var_sub = new SqlCommand("Select  id,name,(select name from Unit_Of_Measurement where id = unit_of_measurement) unit from Material_Variance_Subdivision where name=N'" + listSubvariance[s].Subvariance + @"' and material_variance_id = " + dt_var.Rows[a1]["id"] + " and f_del=0;", conn);
//                                    SqlDataAdapter da_var_sub = new SqlDataAdapter(cmd_var_sub);
//                                    DataTable dt_var_sub = new DataTable();
//                                    da_var_sub.Fill(dt_var_sub);
//                                    for (int a2 = 0; a2 < dt_var_sub.Rows.Count; a2++)
//                                    {
//                                        JobSubTypeSubVarianceInfo JobSubTypeSubVarianceInfo2 = new JobSubTypeSubVarianceInfo();
//                                        SqlCommand cmd_cost = new SqlCommand("select ISNULL((case when eff_date>PARSE('" + eff_start_date + "' as datetime using 'en-US') then eff_date else PARSE('" + eff_start_date + "' as datetime using 'en-US') end ),PARSE('" + eff_start_date + "' as datetime using 'en-US')) as eff_date,ISNULL((case when eff_date_end < PARSE('" + eff_end_date + "' as datetime using 'en-US') then eff_date_end else PARSE('" + eff_end_date + "' as datetime using 'en-US') end),PARSE('" + eff_end_date + "' as datetime using 'en-US')) as eff_date_end,cost,mv.id material_option_id " +
//                                                                                @"from material_option mv left outer join(select * from cost_master cm
//                                                                        where city_id in (Select id from City where name = N'" + City + @"' and country_id in (select id from Country_code where country = N'" + Country + @"')) and material_variance_subdiv_id = " + dt_var_sub.Rows[a2]["id"] +
//                                                                                @" and(PARSE('" + eff_start_date + @"' as datetime using 'en-US') between eff_date and eff_date_end
//                                                                        or PARSE('" + eff_end_date + "' as datetime using 'en-US') between eff_date and eff_date_end)) cm on mv.id = cm.material_option_id ORDER BY material_option_id ", conn);
//                                        /*SqlCommand cmd_cost = new SqlCommand("select ISNULL((case when eff_date>CONVERT(Datetime,'" + eff_start_date + "' ,105) then eff_date else CONVERT(Datetime,'" + eff_start_date + "',105) end ),CONVERT(Datetime,'" + eff_start_date + "' ,105)) as eff_date,ISNULL((case when eff_date_end < CONVERT(Datetime,'" + eff_end_date + "' ,105) then eff_date_end else CONVERT(Datetime,'" + eff_end_date + "' ,105) end),CONVERT(Datetime,'" + eff_end_date + "',105)) as eff_date_end,cost,mv.id material_option_id " +
//                                                                               @"from material_option mv left outer join(select * from cost_master cm
//                                                                            where city_id in (Select id from City where name = N'" + City + @"' and country_id in (select id from Country_code where country = N'" + Country + @"')) and material_variance_subdiv_id = " + dt_var_sub.Rows[a2]["id"] +
//                                                                               @" and(CONVERT(Datetime,'" + eff_start_date + @"' ,105) between eff_date and eff_date_end
//                                                                            or CONVERT(Datetime,'" + eff_end_date + "' ,105) between eff_date and eff_date_end)) cm on mv.id = cm.material_option_id ORDER BY material_option_id ", conn);*/
//                                        SqlDataAdapter da_cost = new SqlDataAdapter(cmd_cost);
//                                        DataTable dt_cost = new DataTable();

//                                        da_cost.Fill(dt_cost);
//                                        JobSubTypeSubVarianceInfo2.JobSubTypeSubVarianceName = dt_var_sub.Rows[a2]["name"].ToString();
//                                        JobSubTypeSubVarianceInfo2.JobSubTypeSubVarianceUnit = dt_var_sub.Rows[a2]["unit"].ToString();
//                                        List<SubvarianceCost> SubvarianceCost1 = new List<SubvarianceCost>();
//                                        for (int a3 = 1; a3 < 7; a3++)
//                                        {
//                                            SubvarianceCost SubvarianceCost2 = new SubvarianceCost();

//                                            if (a3 == 1)
//                                            {
//                                                SubvarianceCost2.Materialoption = "OutSourcing";
//                                            }
//                                            else if (a3 == 2)
//                                            {
//                                                SubvarianceCost2.Materialoption = "Material1";
//                                            }
//                                            else if (a3 == 3)
//                                            {
//                                                SubvarianceCost2.Materialoption = "Material2";
//                                            }
//                                            else if (a3 == 4)
//                                            {
//                                                SubvarianceCost2.Materialoption = "Labor1";
//                                            }
//                                            else if (a3 == 5)
//                                            {
//                                                SubvarianceCost2.Materialoption = "Labor2";
//                                            }
//                                            else if (a3 == 6)
//                                            {
//                                                SubvarianceCost2.Materialoption = "Other";
//                                            }
//                                            DataView dv = new DataView(dt_cost);
//                                            dv.RowFilter = "material_option_id = " + a3 + "";
//                                            List<cost_dtl_mat> cost_dtl_mat1 = new List<cost_dtl_mat>();
//                                            for (int a4 = 0; a4 < dv.Count; a4++)
//                                            {
//                                                cost_dtl_mat cost_dtl_mat2 = new cost_dtl_mat();
//                                                cost_dtl_mat2.eff_date = Convert.ToDateTime(dv[a4]["eff_date"]);
//                                                cost_dtl_mat2.eff_end_date = Convert.ToDateTime(dv[a4]["eff_date_end"]);
//                                                cost_dtl_mat2.cost = string.IsNullOrEmpty(dv[a4]["cost"].ToString()) ? (Double?)null : Convert.ToDouble(dv[a4]["cost"]);
//                                                cost_dtl_mat1.Add(cost_dtl_mat2);
//                                            }
//                                            SubvarianceCost2.cost_dtl = cost_dtl_mat1;
//                                            SubvarianceCost1.Add(SubvarianceCost2);
//                                        }
//                                        JobSubTypeSubVarianceInfo2.SubvarianceCost = SubvarianceCost1;
//                                        JobSubTypeSubVarianceInfo1.Add(JobSubTypeSubVarianceInfo2);
//                                    }
//                                    JobSubType2.ListJobTypeSubVarienceInfo = JobSubTypeSubVarianceInfo1;
//                                    JobSubType1.Add(JobSubType2);
//                                }
//                                Job2.ListJobType = JobSubType1;
//                                Job1.Add(Job2);
//                            }
//                        }
//                        if (isWeekly == true)
//                        {
//                            eff_start_date = eff_end_date.AddDays(1).Date.Add(new TimeSpan(0, 0, 0));
//                            /*if (eff_start_date > ToDate)
//                            {
//                                eff_start_date = ToDate;
//                            }*/
//                            eff_end_date = eff_start_date.AddDays(6).Date.Add(new TimeSpan(23, 59, 0));
//                           /* if (eff_end_date > ToDate)
//                            {
//                                eff_end_date = ToDate;
//                            }*/
//                        }
//                        if (isMonthly == true)
//                        {
//                            eff_start_date = eff_end_date.AddDays(1).Date.Add(new TimeSpan(0, 0, 0));
//                            //if (eff_start_date > ToDate)
//                            // {
//                            //    eff_start_date = ToDate;
//                            // }
//                            eff_end_date = eff_start_date.AddMonths(1);
//                            eff_end_date = eff_end_date.AddDays(-1).Date.Add(new TimeSpan(23, 59, 0));
//                           // if (eff_end_date > ToDate)
//                           // {
//                           //     eff_end_date = ToDate;
//                            //}
//                        }
//                        if (isYearly == true)
//                        {
//                            eff_start_date = eff_end_date.AddDays(1).Date.Add(new TimeSpan(0, 0, 0));
//                            // if (eff_start_date > ToDate)
//                            // {
//                            //     eff_start_date = ToDate;
//                            // }
//                            eff_end_date = eff_start_date.AddYears(1);
//                            eff_end_date = eff_end_date.AddDays(-1).Date.Add(new TimeSpan(23, 59, 0));
//                           // if (eff_end_date > ToDate)
//                           // {
//                           //     eff_end_date = ToDate;
//                           // }
//                        }
//                        year_week_month_dtl2.ListJob = Job1;
//                        year_week_month_dtl1.Add(year_week_month_dtl2);
//                        i = i + 1;
//                    }while (eff_end_date <= ToDate || eff_start_date <= ToDate);
                   
//                }
//                CostAnalysisRep1.year_week_month_dtl = year_week_month_dtl1;
//                return CostAnalysisRep1;

//            }
//          catch (System.Exception ex)
//             {


//                if (state == ConnectionState.Open)
//                {
//                    conn.Close();

//                }
//                Service17 exception1 = new Service17();
//                exception1.SendErrorToText(ex);
//                return null;

//            }
//        }

        public CostAnalysisReport GetCostReport(string Country, string City, DateTime FromDate, DateTime ToDate, [Optional]bool isMonthly, [Optional]bool isQuater, [Optional]bool isYearly, List<InputSubvariance> listSubvariance)
        {

            //return date;// returns 09/25/2011
            //DateTime FromDate = DateTime.Parse(FromDate1, System.Globalization.CultureInfo.InvariantCulture);
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                conn.Open();
                CostAnalysisReport CostAnalysisRep1 = new CostAnalysisReport();
                CostAnalysisRep1.Country = Country;
                CostAnalysisRep1.City = City;
                List<year_week_month_dtl> year_week_month_dtl1 = new List<year_week_month_dtl>();
                double no_of_loop = 0;
                DateTime eff_start_date = FromDate.Date.Add(new TimeSpan(0, 0, 0));
                DateTime eff_end_date = ToDate.Date.Add(new TimeSpan(23, 59, 0));

                if (isQuater == true)
                {
                    double diff = Math.Round((((ToDate - FromDate).TotalDays) / 91), 2);
                    no_of_loop = Math.Ceiling(diff);
                    eff_start_date = FromDate;
                    eff_end_date = FromDate.AddMonths(3);
                    eff_end_date = eff_end_date.AddDays(-1).Date.Add(new TimeSpan(23, 59, 0));
                    if (eff_end_date > ToDate)
                    {
                        eff_end_date = ToDate;
                    }
                }
                if (isMonthly == true)
                {
                    double diff = Math.Round((((ToDate - FromDate).TotalDays) / 30), 2);
                    no_of_loop = Math.Ceiling(diff);
                    eff_start_date = FromDate;
                    eff_end_date = FromDate.AddMonths(1);
                    eff_end_date = eff_end_date.AddDays(-1).Date.Add(new TimeSpan(23, 59, 0));
                    if (eff_end_date > ToDate)
                    {
                        eff_end_date = ToDate;
                    }
                }
                if (isYearly == true)
                {
                    double diff = Math.Round((((ToDate - FromDate).TotalDays) / 365), 2);
                    no_of_loop = Math.Ceiling(diff);
                    eff_start_date = FromDate;
                    eff_end_date = FromDate.AddYears(1);
                    eff_end_date = eff_end_date.AddDays(-1).Date.Add(new TimeSpan(23, 59, 0));
                    if (eff_end_date > ToDate)
                    {
                        eff_end_date = ToDate;
                    }
                }
                if (isQuater == true || isMonthly == true || isYearly == true)
                {
                    int i = 0;
                    // for (int i = 0; i < no_of_loop; i++)
                    //{
                    do
                    {
                        if (eff_start_date > ToDate)
                        {
                            eff_start_date = ToDate.Date.Add(new TimeSpan(0, 0, 0));
                        }
                        if (eff_end_date > ToDate)
                        {
                            eff_end_date = ToDate.Date.Add(new TimeSpan(23, 59, 0));
                        }

                        year_week_month_dtl year_week_month_dtl2 = new year_week_month_dtl();
                        year_week_month_dtl2.no = i;
                        year_week_month_dtl2.from_date = eff_start_date;
                        year_week_month_dtl2.to_date = eff_end_date;
                        List<Job> Job1 = new List<Job>();
                        for (int s = 0; s < listSubvariance.Count; s++)
                        {
                            SqlCommand cmd_cnt = new SqlCommand("Select * from Material where name =N'" + listSubvariance[s].Job + @"';", conn);
                            SqlDataAdapter da_cnt = new SqlDataAdapter(cmd_cnt);
                            DataTable dt_cnt = new DataTable();
                            da_cnt.Fill(dt_cnt);
                            string materialOptionName;

                            for (int a = 0; a < dt_cnt.Rows.Count; a++)
                            {
                                Job Job2 = new Job();
                                Job2.JobName = dt_cnt.Rows[a]["name"].ToString();
                                SqlCommand cmd_var = new SqlCommand("Select * from Material_variance where variance =N'" + listSubvariance[s].Subtype + @"' and material_id = " + dt_cnt.Rows[a]["id"] + ";", conn);
                                SqlDataAdapter da_var = new SqlDataAdapter(cmd_var);
                                DataTable dt_var = new DataTable();
                                da_var.Fill(dt_var);
                                List<JobSubType> JobSubType1 = new List<JobSubType>();
                                for (int a1 = 0; a1 < dt_var.Rows.Count; a1++)
                                {
                                    JobSubType JobSubType2 = new JobSubType();
                                    List<JobSubTypeSubVarianceInfo> JobSubTypeSubVarianceInfo1 = new List<JobSubTypeSubVarianceInfo>();

                                    JobSubType2.JobTypeName = dt_var.Rows[a1]["variance"].ToString();
                                    //SqlCommand cmd_var_sub = new SqlCommand("Select  id,name,(select name from Unit_Of_Measurement where id = unit_of_measurement) unit from Material_Variance_Subdivision where material_variance_id = " + dt_var.Rows[a1]["id"] + ";", conn);
                                    SqlCommand cmd_var_sub = new SqlCommand("Select  id,name,(select name from Unit_Of_Measurement where id = unit_of_measurement) unit from Material_Variance_Subdivision where name=N'" + listSubvariance[s].Subvariance + @"' and material_variance_id = " + dt_var.Rows[a1]["id"] + " and f_del=0 and city_id in (Select id from City where name = N'" + City + @"' and country_id in (select id from Country_code where country = N'" + Country + @"'));", conn);
                                    SqlDataAdapter da_var_sub = new SqlDataAdapter(cmd_var_sub);
                                    DataTable dt_var_sub = new DataTable();
                                    da_var_sub.Fill(dt_var_sub);
                                    for (int a2 = 0; a2 < dt_var_sub.Rows.Count; a2++)
                                    {
                                        JobSubTypeSubVarianceInfo JobSubTypeSubVarianceInfo2 = new JobSubTypeSubVarianceInfo();
                                        SqlCommand cmd_cost = new SqlCommand("select ISNULL((case when eff_date>PARSE('" + eff_start_date + "' as datetime using 'en-US') then eff_date else PARSE('" + eff_start_date + "' as datetime using 'en-US') end ),PARSE('" + eff_start_date + "' as datetime using 'en-US')) as eff_date,ISNULL((case when eff_date_end < PARSE('" + eff_end_date + "' as datetime using 'en-US') then eff_date_end else PARSE('" + eff_end_date + "' as datetime using 'en-US') end),PARSE('" + eff_end_date + "' as datetime using 'en-US')) as eff_date_end,cost,mv.id material_option_id " +
                                                                                @"from material_option mv left outer join(select * from cost_master cm
                                                                        where city_id in (Select id from City where name = N'" + City + @"' and country_id in (select id from Country_code where country = N'" + Country + @"')) and material_variance_subdiv_id = " + dt_var_sub.Rows[a2]["id"] +
                                                                                @" and(PARSE('" + eff_start_date + @"' as datetime using 'en-US') between eff_date and eff_date_end
                                                                        or PARSE('" + eff_end_date + "' as datetime using 'en-US') between eff_date and eff_date_end)) cm on mv.id = cm.material_option_id ORDER BY material_option_id ", conn);
                                        /*SqlCommand cmd_cost = new SqlCommand("select ISNULL((case when eff_date>CONVERT(Datetime,'" + eff_start_date + "' ,105) then eff_date else CONVERT(Datetime,'" + eff_start_date + "',105) end ),CONVERT(Datetime,'" + eff_start_date + "' ,105)) as eff_date,ISNULL((case when eff_date_end < CONVERT(Datetime,'" + eff_end_date + "' ,105) then eff_date_end else CONVERT(Datetime,'" + eff_end_date + "' ,105) end),CONVERT(Datetime,'" + eff_end_date + "',105)) as eff_date_end,cost,mv.id material_option_id " +
                                                                               @"from material_option mv left outer join(select * from cost_master cm
                                                                            where city_id in (Select id from City where name = N'" + City + @"' and country_id in (select id from Country_code where country = N'" + Country + @"')) and material_variance_subdiv_id = " + dt_var_sub.Rows[a2]["id"] +
                                                                               @" and(CONVERT(Datetime,'" + eff_start_date + @"' ,105) between eff_date and eff_date_end
                                                                            or CONVERT(Datetime,'" + eff_end_date + "' ,105) between eff_date and eff_date_end)) cm on mv.id = cm.material_option_id ORDER BY material_option_id ", conn);*/
                                        SqlDataAdapter da_cost = new SqlDataAdapter(cmd_cost);
                                        DataTable dt_cost = new DataTable();

                                        da_cost.Fill(dt_cost);
                                        JobSubTypeSubVarianceInfo2.JobSubTypeSubVarianceName = dt_var_sub.Rows[a2]["name"].ToString();
                                        JobSubTypeSubVarianceInfo2.JobSubTypeSubVarianceUnit = dt_var_sub.Rows[a2]["unit"].ToString();
                                        List<SubvarianceCost> SubvarianceCost1 = new List<SubvarianceCost>();
                                        for (int a3 = 1; a3 < 7; a3++)
                                        {
                                            SubvarianceCost SubvarianceCost2 = new SubvarianceCost();

                                            if (a3 == 1)
                                            {
                                                SubvarianceCost2.Materialoption = "OutSourcing";
                                            }
                                            else if (a3 == 2)
                                            {
                                                SubvarianceCost2.Materialoption = "Material1";
                                            }
                                            else if (a3 == 3)
                                            {
                                                SubvarianceCost2.Materialoption = "Material2";
                                            }
                                            else if (a3 == 4)
                                            {
                                                SubvarianceCost2.Materialoption = "Labor1";
                                            }
                                            else if (a3 == 5)
                                            {
                                                SubvarianceCost2.Materialoption = "Labor2";
                                            }
                                            else if (a3 == 6)
                                            {
                                                SubvarianceCost2.Materialoption = "Other";
                                            }
                                            DataView dv = new DataView(dt_cost);
                                            dv.RowFilter = "material_option_id = " + a3 + "";
                                            List<cost_dtl_mat> cost_dtl_mat1 = new List<cost_dtl_mat>();
                                            for (int a4 = 0; a4 < dv.Count; a4++)
                                            {
                                                cost_dtl_mat cost_dtl_mat2 = new cost_dtl_mat();
                                                cost_dtl_mat2.eff_date = Convert.ToDateTime(dv[a4]["eff_date"]);
                                                cost_dtl_mat2.eff_end_date = Convert.ToDateTime(dv[a4]["eff_date_end"]);
                                                cost_dtl_mat2.cost = string.IsNullOrEmpty(dv[a4]["cost"].ToString()) ? (Double?)null : Convert.ToDouble(dv[a4]["cost"]);
                                                cost_dtl_mat1.Add(cost_dtl_mat2);
                                            }
                                            SubvarianceCost2.cost_dtl = cost_dtl_mat1;
                                            SubvarianceCost1.Add(SubvarianceCost2);
                                        }
                                        JobSubTypeSubVarianceInfo2.SubvarianceCost = SubvarianceCost1;
                                        JobSubTypeSubVarianceInfo1.Add(JobSubTypeSubVarianceInfo2);
                                    }
                                    JobSubType2.ListJobTypeSubVarienceInfo = JobSubTypeSubVarianceInfo1;
                                    JobSubType1.Add(JobSubType2);
                                }
                                Job2.ListJobType = JobSubType1;
                                Job1.Add(Job2);
                            }
                        }
                        if (isQuater == true)
                        {
                            eff_start_date = eff_end_date.AddDays(1).Date.Add(new TimeSpan(0, 0, 0));
                            /*if (eff_start_date > ToDate)
                            {
                                eff_start_date = ToDate;
                            }*/
                            eff_end_date = eff_start_date.AddMonths(3);
                            eff_end_date = eff_end_date.AddDays(-1).Date.Add(new TimeSpan(23, 59, 0));
                            /* if (eff_end_date > ToDate)
                             {
                                 eff_end_date = ToDate;
                             }*/
                        }
                        if (isMonthly == true)
                        {
                            eff_start_date = eff_end_date.AddDays(1).Date.Add(new TimeSpan(0, 0, 0));
                            //if (eff_start_date > ToDate)
                            // {
                            //    eff_start_date = ToDate;
                            // }
                            eff_end_date = eff_start_date.AddMonths(1);
                            eff_end_date = eff_end_date.AddDays(-1).Date.Add(new TimeSpan(23, 59, 0));
                            // if (eff_end_date > ToDate)
                            // {
                            //     eff_end_date = ToDate;
                            //}
                        }
                        if (isYearly == true)
                        {
                            eff_start_date = eff_end_date.AddDays(1).Date.Add(new TimeSpan(0, 0, 0));
                            // if (eff_start_date > ToDate)
                            // {
                            //     eff_start_date = ToDate;
                            // }
                            eff_end_date = eff_start_date.AddYears(1);
                            eff_end_date = eff_end_date.AddDays(-1).Date.Add(new TimeSpan(23, 59, 0));
                            // if (eff_end_date > ToDate)
                            // {
                            //     eff_end_date = ToDate;
                            // }
                        }
                        year_week_month_dtl2.ListJob = Job1;
                        year_week_month_dtl1.Add(year_week_month_dtl2);
                        i = i + 1;
                    } while (eff_end_date <= ToDate || eff_start_date <= ToDate);

                }
                CostAnalysisRep1.year_week_month_dtl = year_week_month_dtl1;
                return CostAnalysisRep1;

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


        public CostAnalysisReport GetCostReport_Project(string Country, string City, DateTime FromDate, DateTime ToDate, [Optional]bool isMonthly,[Optional]bool isQuater, [Optional]bool isYearly, string proj_guid, string proj_name, List<InputSubvariance> listSubvariance)
        {

            //return date;// returns 09/25/2011
            //DateTime FromDate = DateTime.Parse(FromDate1, System.Globalization.CultureInfo.InvariantCulture);
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {
                //var listSub = listSubvariance.Where(s => s.Subvariance.Contains("'")).ToList();
                //if (listSub.Count > 0)
                //{
                    listSubvariance.ForEach(s => s.Subvariance = s.Subvariance.Replace("'", "''"));
                //}
                conn.Open();
                CostAnalysisReport CostAnalysisRep1 = new CostAnalysisReport();
                CostAnalysisRep1.Country = Country;
                CostAnalysisRep1.City = City;
                List<year_week_month_dtl> year_week_month_dtl1 = new List<year_week_month_dtl>();
                double no_of_loop = 0;
                DateTime eff_start_date = FromDate.Date.Add(new TimeSpan(0, 0, 0));
                DateTime eff_end_date = ToDate.Date.Add(new TimeSpan(23, 59, 0));

                if (isQuater == true)
                {
                    double diff = Math.Round((((ToDate - FromDate).TotalDays) / 91), 2);
                    no_of_loop = Math.Ceiling(diff);
                    eff_start_date = FromDate;
                    eff_end_date = FromDate.AddMonths(3);
                    eff_end_date = eff_end_date.AddDays(-1).Date.Add(new TimeSpan(23, 59, 0));
                    if (eff_end_date > ToDate)
                    {
                        eff_end_date = ToDate;
                    }
                }
                if (isMonthly == true)
                {
                    double diff = Math.Round((((ToDate - FromDate).TotalDays) / 30), 2);
                    no_of_loop = Math.Ceiling(diff);
                    eff_start_date = FromDate;
                    eff_end_date = FromDate.AddMonths(1);
                    eff_end_date = eff_end_date.AddDays(-1).Date.Add(new TimeSpan(23, 59, 0));
                    if (eff_end_date > ToDate)
                    {
                        eff_end_date = ToDate;
                    }
                }
                if (isYearly == true)
                {
                    double diff = Math.Round((((ToDate - FromDate).TotalDays) / 365), 2);
                    no_of_loop = Math.Ceiling(diff);
                    eff_start_date = FromDate;
                    eff_end_date = FromDate.AddYears(1);
                    eff_end_date = eff_end_date.AddDays(-1).Date.Add(new TimeSpan(23, 59, 0));
                    if (eff_end_date > ToDate)
                    {
                        eff_end_date = ToDate;
                    }
                }
                if (isQuater == true || isMonthly == true || isYearly == true)
                {
                    int i = 0;
                    // for (int i = 0; i < no_of_loop; i++)
                    //{
                    do
                    {
                        if (eff_start_date > ToDate)
                        {
                            eff_start_date = ToDate.Date.Add(new TimeSpan(0, 0, 0));
                        }
                        if (eff_end_date > ToDate)
                        {
                            eff_end_date = ToDate.Date.Add(new TimeSpan(23, 59, 0));
                        }

                        year_week_month_dtl year_week_month_dtl2 = new year_week_month_dtl();
                        year_week_month_dtl2.no = i;
                        year_week_month_dtl2.from_date = eff_start_date;
                        year_week_month_dtl2.to_date = eff_end_date;
                        List<Job> Job1 = new List<Job>();
                        for (int s = 0; s < listSubvariance.Count; s++)
                        {
                            SqlCommand cmd_cnt = new SqlCommand("Select * from Material where name =N'" + listSubvariance[s].Job + @"';", conn);
                            SqlDataAdapter da_cnt = new SqlDataAdapter(cmd_cnt);
                            DataTable dt_cnt = new DataTable();
                            da_cnt.Fill(dt_cnt);
                            string materialOptionName;

                            for (int a = 0; a < dt_cnt.Rows.Count; a++)
                            {
                                Job Job2 = new Job();
                                Job2.JobName = dt_cnt.Rows[a]["name"].ToString();
                                SqlCommand cmd_var = new SqlCommand("Select * from Material_variance where variance =N'" + listSubvariance[s].Subtype + @"' and material_id = " + dt_cnt.Rows[a]["id"] + ";", conn);
                                SqlDataAdapter da_var = new SqlDataAdapter(cmd_var);
                                DataTable dt_var = new DataTable();
                                da_var.Fill(dt_var);
                                List<JobSubType> JobSubType1 = new List<JobSubType>();
                                for (int a1 = 0; a1 < dt_var.Rows.Count; a1++)
                                {
                                    JobSubType JobSubType2 = new JobSubType();
                                    List<JobSubTypeSubVarianceInfo> JobSubTypeSubVarianceInfo1 = new List<JobSubTypeSubVarianceInfo>();

                                    JobSubType2.JobTypeName = dt_var.Rows[a1]["variance"].ToString();
                                    //SqlCommand cmd_var_sub = new SqlCommand("Select  id,name,(select name from Unit_Of_Measurement where id = unit_of_measurement) unit from Material_Variance_Subdivision where material_variance_id = " + dt_var.Rows[a1]["id"] + ";", conn);
                                    SqlCommand cmd_var_sub = new SqlCommand("(SELECT id,name,(SELECT UNIT FROM UNIT_OF_MEASUREMENT WHERE ID = unit_of_measurement )unit FROM material_variance_subdivision mat_var_sub WHERE name=N'" + listSubvariance[s].Subvariance + @"' and  material_variance_id =" + dt_var.Rows[a1]["id"] + " and f_del = 0 and city_id in (Select id from City where name = N'" + City + @"' and country_id in (select id from Country_code where country = N'" + Country + @"')) and created_on <= (select created_on from Project where proj_guid=N'" + proj_guid + @"' and name=N'" + proj_name + @"' and  NOT EXISTS (SELECT 1 FROM Material_Variance_Subdivision_Project WHERE  mat_var_sub.name = Material_Variance_Subdivision_Project.name and Material_Variance_Subdivision_Project.f_del = 1 and Material_Variance_Subdivision_Project.project_id = (select id from Project where proj_guid=N'" + proj_guid + @"' and name=N'" + proj_name + @"'))) UNION (SELECT id,name,(SELECT UNIT FROM UNIT_OF_MEASUREMENT WHERE ID = unit_of_measurement )unit  FROM  material_variance_subdivision_Project mat_var_sub WHERE name=N'" + listSubvariance[s].Subvariance + @"' and material_variance_id = " + dt_var.Rows[a1]["id"] + " and project_id = (select id from Project where proj_guid=N'" + proj_guid + @"' and name=N'" + proj_name + @"') and f_del = 0));", conn);
                                    SqlDataAdapter da_var_sub = new SqlDataAdapter(cmd_var_sub);
                                    DataTable dt_var_sub = new DataTable();
                                    da_var_sub.Fill(dt_var_sub);
                                    for (int a2 = 0; a2 < dt_var_sub.Rows.Count; a2++)
                                    {
                                        subvarianceName = dt_var_sub.Rows[a2]["name"].ToString();
                                        subvarianceName = subvarianceName.Replace("'","''");

                                        JobSubTypeSubVarianceInfo JobSubTypeSubVarianceInfo2 = new JobSubTypeSubVarianceInfo();
                                        SqlCommand cmd_cost = new SqlCommand("select ISNULL((case when eff_date>PARSE('" + eff_start_date + "' as datetime using 'en-US') then eff_date else PARSE('" + eff_start_date + "' as datetime using 'en-US') end ),PARSE('" + eff_start_date + "' as datetime using 'en-US')) as eff_date,ISNULL((case when eff_date_end < PARSE('" + eff_end_date + "' as datetime using 'en-US') then eff_date_end else PARSE('" + eff_end_date + "' as datetime using 'en-US') end),PARSE('" + eff_end_date + "' as datetime using 'en-US')) as eff_date_end,cost,mv.id material_option_id " +
                                                                                @"from material_option mv left outer join(select * from Cost_Master_Project cm where project_id = (select id from Project where proj_guid=N'" + proj_guid + @"' and name=N'" + proj_name + @"') and city_id in (Select id from City where name = N'" + City + @"' and country_id in (select id from Country_code where country = N'" + Country + @"')) and (material_variance_subdiv_id = " + dt_var_sub.Rows[a2]["id"] +
                                                                                @" and N'" + subvarianceName + @"' = (select name from Material_Variance_Subdivision where id = " + dt_var_sub.Rows[a2]["id"] + @") and material_variance_subdiv_id_proj is null) or (material_variance_subdiv_id_proj = " + dt_var_sub.Rows[a2]["id"] + @" and N'" + subvarianceName + @"' = (select name from Material_Variance_Subdivision_Project where id = " + dt_var_sub.Rows[a2]["id"] + @") and material_variance_subdiv_id is null) and(PARSE('" + eff_start_date + @"' as datetime using 'en-US') between eff_date and eff_date_end
                                                                        or PARSE('" + eff_end_date + "' as datetime using 'en-US') between eff_date and eff_date_end)) cm on mv.id = cm.material_option_id ORDER BY material_option_id ", conn);



                                        //26thoct22
                                        //                                    SqlCommand cmd_cost = new SqlCommand("select ISNULL((case when eff_date>PARSE('" + eff_start_date + "' as datetime using 'en-US') then eff_date else PARSE('" + eff_start_date + "' as datetime using 'en-US') end ),PARSE('" + eff_start_date + "' as datetime using 'en-US')) as eff_date,ISNULL((case when eff_date_end < PARSE('" + eff_end_date + "' as datetime using 'en-US') then eff_date_end else PARSE('" + eff_end_date + "' as datetime using 'en-US') end),PARSE('" + eff_end_date + "' as datetime using 'en-US')) as eff_date_end,cost,mv.id material_option_id " +
                                        //                                                                            @"from material_option mv left outer join(select * from cost_master cm
                                        //                                                                        where city_id in (Select id from City where name = N'" + City + @"' and country_id in (select id from Country_code where country = N'" + Country + @"')) and material_variance_subdiv_id = " + dt_var_sub.Rows[a2]["id"] +
                                        //                                                                            @" and(PARSE('" + eff_start_date + @"' as datetime using 'en-US') between eff_date and eff_date_end
                                        //                                                                        or PARSE('" + eff_end_date + "' as datetime using 'en-US') between eff_date and eff_date_end)) cm on mv.id = cm.material_option_id ORDER BY material_option_id ", conn);
                                        //                                    /*SqlCommand cmd_cost = new SqlCommand("select ISNULL((case when eff_date>CONVERT(Datetime,'" + eff_start_date + "' ,105) then eff_date else CONVERT(Datetime,'" + eff_start_date + "',105) end ),CONVERT(Datetime,'" + eff_start_date + "' ,105)) as eff_date,ISNULL((case when eff_date_end < CONVERT(Datetime,'" + eff_end_date + "' ,105) then eff_date_end else CONVERT(Datetime,'" + eff_end_date + "' ,105) end),CONVERT(Datetime,'" + eff_end_date + "',105)) as eff_date_end,cost,mv.id material_option_id " +
                                        //                                                                           @"from material_option mv left outer join(select * from cost_master cm
                                        //                                                                        where city_id in (Select id from City where name = N'" + City + @"' and country_id in (select id from Country_code where country = N'" + Country + @"')) and material_variance_subdiv_id = " + dt_var_sub.Rows[a2]["id"] +
                                        //                                                                           @" and(CONVERT(Datetime,'" + eff_start_date + @"' ,105) between eff_date and eff_date_end
                                        //                                                                        or CONVERT(Datetime,'" + eff_end_date + "' ,105) between eff_date and eff_date_end)) cm on mv.id = cm.material_option_id ORDER BY material_option_id ", conn);*/
                                        SqlDataAdapter da_cost = new SqlDataAdapter(cmd_cost);
                                        DataTable dt_cost = new DataTable();

                                        da_cost.Fill(dt_cost);
                                        JobSubTypeSubVarianceInfo2.JobSubTypeSubVarianceName = dt_var_sub.Rows[a2]["name"].ToString();
                                        JobSubTypeSubVarianceInfo2.JobSubTypeSubVarianceUnit = dt_var_sub.Rows[a2]["unit"].ToString();
                                        List<SubvarianceCost> SubvarianceCost1 = new List<SubvarianceCost>();
                                        for (int a3 = 1; a3 < 7; a3++)
                                        {
                                            SubvarianceCost SubvarianceCost2 = new SubvarianceCost();

                                            if (a3 == 1)
                                            {
                                                SubvarianceCost2.Materialoption = "OutSourcing";
                                            }
                                            else if (a3 == 2)
                                            {
                                                SubvarianceCost2.Materialoption = "Material1";
                                            }
                                            else if (a3 == 3)
                                            {
                                                SubvarianceCost2.Materialoption = "Material2";
                                            }
                                            else if (a3 == 4)
                                            {
                                                SubvarianceCost2.Materialoption = "Labor1";
                                            }
                                            else if (a3 == 5)
                                            {
                                                SubvarianceCost2.Materialoption = "Labor2";
                                            }
                                            else if (a3 == 6)
                                            {
                                                SubvarianceCost2.Materialoption = "Other";
                                            }
                                            DataView dv = new DataView(dt_cost);
                                            dv.RowFilter = "material_option_id = " + a3 + "";
                                            List<cost_dtl_mat> cost_dtl_mat1 = new List<cost_dtl_mat>();
                                            for (int a4 = 0; a4 < dv.Count; a4++)
                                            {
                                                cost_dtl_mat cost_dtl_mat2 = new cost_dtl_mat();
                                                cost_dtl_mat2.eff_date = Convert.ToDateTime(dv[a4]["eff_date"]);
                                                cost_dtl_mat2.eff_end_date = Convert.ToDateTime(dv[a4]["eff_date_end"]);
                                                cost_dtl_mat2.cost = string.IsNullOrEmpty(dv[a4]["cost"].ToString()) ? (Double?)null : Convert.ToDouble(dv[a4]["cost"]);
                                                cost_dtl_mat1.Add(cost_dtl_mat2);
                                            }
                                            SubvarianceCost2.cost_dtl = cost_dtl_mat1;
                                            SubvarianceCost1.Add(SubvarianceCost2);
                                        }
                                        JobSubTypeSubVarianceInfo2.SubvarianceCost = SubvarianceCost1;
                                        JobSubTypeSubVarianceInfo1.Add(JobSubTypeSubVarianceInfo2);
                                    }
                                    JobSubType2.ListJobTypeSubVarienceInfo = JobSubTypeSubVarianceInfo1;
                                    JobSubType1.Add(JobSubType2);
                                }
                                Job2.ListJobType = JobSubType1;
                                Job1.Add(Job2);
                            }
                        }
                        if (isQuater == true)
                        {
                            eff_start_date = eff_end_date.AddDays(1).Date.Add(new TimeSpan(0, 0, 0));
                            /*if (eff_start_date > ToDate)
                            {
                                eff_start_date = ToDate;
                            }*/
                            eff_end_date = eff_start_date.AddMonths(3);
                            eff_end_date = eff_end_date.AddDays(-1).Date.Add(new TimeSpan(23, 59, 0));
                            /* if (eff_end_date > ToDate)
                             {
                                 eff_end_date = ToDate;
                             }*/
                        }
                        if (isMonthly == true)
                        {
                            eff_start_date = eff_end_date.AddDays(1).Date.Add(new TimeSpan(0, 0, 0));
                            //if (eff_start_date > ToDate)
                            // {
                            //    eff_start_date = ToDate;
                            // }
                            eff_end_date = eff_start_date.AddMonths(1);
                            eff_end_date = eff_end_date.AddDays(-1).Date.Add(new TimeSpan(23, 59, 0));
                            // if (eff_end_date > ToDate)
                            // {
                            //     eff_end_date = ToDate;
                            //}
                        }
                        if (isYearly == true)
                        {
                            eff_start_date = eff_end_date.AddDays(1).Date.Add(new TimeSpan(0, 0, 0));
                            // if (eff_start_date > ToDate)
                            // {
                            //     eff_start_date = ToDate;
                            // }
                            eff_end_date = eff_start_date.AddYears(1);
                            eff_end_date = eff_end_date.AddDays(-1).Date.Add(new TimeSpan(23, 59, 0));
                            // if (eff_end_date > ToDate)
                            // {
                            //     eff_end_date = ToDate;
                            // }
                        }
                        year_week_month_dtl2.ListJob = Job1;
                        year_week_month_dtl1.Add(year_week_month_dtl2);
                        i = i + 1;
                    } while (eff_end_date <= ToDate || eff_start_date <= ToDate);

                }
                CostAnalysisRep1.year_week_month_dtl = year_week_month_dtl1;
                return CostAnalysisRep1;

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
       
    }
}
