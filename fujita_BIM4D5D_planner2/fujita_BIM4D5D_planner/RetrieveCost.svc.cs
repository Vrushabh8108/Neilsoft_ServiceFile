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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service9" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service9.svc or Service9.svc.cs at the Solution Explorer and start debugging.
    public class Service9 : RetrieveCost
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        #region Copy
        //        public List<cost_dtl> readcostdtl(string proj_id, string proj_name, int proj_version)
//        {
//            SqlConnection con = new SqlConnection(connection_string);
//            ConnectionState state = con.State;
//            try
//            {


//                SqlCommand cmd;
//                SqlDataAdapter sda;
//                DataTable dt;
//                List<cost_dtl> cost_dtl1 = new List<cost_dtl>();
//                using (con = new SqlConnection(connection_string))
//                {
//                    cmd = new SqlCommand(@"select element_id,element_type_id,JSON_VALUE(elemnt_dtl,'$.ElementName') as ElementName,JSON_VALUE(elemnt_dtl,'$.ElementVolume') as ElementVolume,JSON_VALUE(elemnt_dtl,'$.CalculatedCost') as CalculatedCost,JSON_VALUE(elemnt_dtl,'$.AssignedCost') as AssignedCost
//                                            ,f_virtual,JSON_VALUE(elemnt_dtl,'$.IsOutsourcing') as IsOutsourcing,JSON_VALUE(elemnt_dtl,'$.IsMaterial1') as IsMaterial1,
//                                             JSON_VALUE(elemnt_dtl,'$.IsMaterial2') as IsMaterial2,JSON_VALUE(elemnt_dtl,'$.IsLabor1') as IsLabor1, 
//                                              JSON_VALUE(elemnt_dtl,'$.IsLabor2') as IsLabor2,JSON_VALUE(elemnt_dtl,'$.IsOther') as IsOther,
//                                                JSON_VALUE(elemnt_dtl,'$.Job') as Job,JSON_VALUE(elemnt_dtl,'$.Variance') as Variance,
//                                                    JSON_VALUE(elemnt_dtl,'$.SubVariance') as SubVariance,JSON_VALUE(elemnt_dtl,'$.Quantity') as Quantity,JSON_VALUE(elemnt_dtl,'$.IsFinishCostApplied') as IsFinishCostApplied,
//                                                    JSON_VALUE(elemnt_dtl,'$.FinishArea') as FinishArea,JSON_VALUE(elemnt_dtl,'$.FinishAreaCost') as FinishAreaCost,
//                                                        JSON_VALUE(elemnt_dtl,'$.Unit') as Unit,JSON_VALUE(elemnt_dtl,'$.UnitCost') as UnitCost,
//                                                    JSON_VALUE(elemnt_dtl,'$.TotalCost') as TotalCost,
//                                                    JSON_VALUE(elemnt_dtl,'$.OutsourcingCost') as OutsourcingCost,JSON_VALUE(elemnt_dtl,'$.Material1Cost') as Material1Cost,
//                                                    JSON_VALUE(elemnt_dtl,'$.Material2Cost') as Material2Cost,JSON_VALUE(elemnt_dtl,'$.Labor1Cost') as Labor1Cost,
//                                                    JSON_VALUE(elemnt_dtl,'$.Labor2Cost') as Labor2Cost,JSON_VALUE(elemnt_dtl,'$.OtherCost') as OtherCost,
//                                                    JSON_VALUE(elemnt_dtl,'$.Specification') as Specifications,
//                                                    JSON_VALUE(elemnt_dtl,'$.ConcreteGradeDetails') as ConcreteGradeDetails,
//                                                    JSON_VALUE(elemnt_dtl,'$.ConcreteGradeTotalCost') as ConcreteGradeTotalCost,
//                                                    JSON_VALUE(elemnt_dtl,'$.ConcreteGradeAssignedCost') as ConcreteGradeAssignedCost,
//                                                    JSON_VALUE(elemnt_dtl,'$.isedited') as isedited from Revit_project_model where proj_version_id in(select id from Revit_project_version where version=" + proj_version + " and project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') )and f_virtual = 1;", con);
//                    sda = new SqlDataAdapter(cmd);
//                    dt = new DataTable("cost");
//                    sda.Fill(dt);
//                    for (int i = 0; i < dt.Rows.Count; i++)
//                    {
//                        //"Quantity":23.979,"Unit":"m²","UnitCost":380.7,"TotalCost":9128.8053,"AssignedCost":10000.0}
//                        cost_dtl cost_dtl11 = new cost_dtl
//                        {
//                            revit_element_type_id = string.IsNullOrEmpty(dt.Rows[i]["element_type_id"].ToString()) ? (Int64?)null : Convert.ToInt64(dt.Rows[i]["element_type_id"]),
//                            revit_element_id = string.IsNullOrEmpty(dt.Rows[i]["element_id"].ToString()) ? (Int64?)null : Convert.ToInt64(dt.Rows[i]["element_id"]),
//                            ElementName = Convert.ToString(dt.Rows[i]["ElementName"]),
//                            ElementVolume = string.IsNullOrEmpty(dt.Rows[i]["ElementVolume"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["ElementVolume"].ToString()),
//                            f_virtual = string.IsNullOrEmpty(dt.Rows[i]["f_virtual"].ToString()) ? (Int32?)null : Convert.ToInt32(dt.Rows[i]["f_virtual"]),
//                            IsOutsourcing = string.IsNullOrEmpty(dt.Rows[i]["IsOutsourcing"].ToString()) ? (bool?)null : Convert.ToBoolean(dt.Rows[i]["IsOutsourcing"]),
//                            IsMaterial1 = string.IsNullOrEmpty(dt.Rows[i]["IsMaterial1"].ToString()) ? (bool?)null : Convert.ToBoolean(dt.Rows[i]["IsMaterial1"]),
//                            IsMaterial2 = string.IsNullOrEmpty(dt.Rows[i]["IsMaterial2"].ToString()) ? (bool?)null : Convert.ToBoolean(dt.Rows[i]["IsMaterial2"]),
//                            IsLabor1 = string.IsNullOrEmpty(dt.Rows[i]["IsLabor1"].ToString()) ? (bool?)null : Convert.ToBoolean(dt.Rows[i]["IsLabor1"]),
//                            IsLabor2 = string.IsNullOrEmpty(dt.Rows[i]["IsLabor2"].ToString()) ? (bool?)null : Convert.ToBoolean(dt.Rows[i]["IsLabor2"]),
//                            IsOther = string.IsNullOrEmpty(dt.Rows[i]["IsOther"].ToString()) ? (bool?)null : Convert.ToBoolean(dt.Rows[i]["IsOther"]),
//                            OutsourcingCost = string.IsNullOrEmpty(dt.Rows[i]["OutsourcingCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["OutsourcingCost"].ToString()),
//                            Material1Cost = string.IsNullOrEmpty(dt.Rows[i]["Material1Cost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["Material1Cost"].ToString()),
//                            Material2Cost = string.IsNullOrEmpty(dt.Rows[i]["Material2Cost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["Material2Cost"].ToString()),
//                            Labor1Cost = string.IsNullOrEmpty(dt.Rows[i]["Labor1Cost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["Labor1Cost"].ToString()),
//                            Labor2Cost = string.IsNullOrEmpty(dt.Rows[i]["Labor2Cost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["Labor2Cost"].ToString()),
//                            OtherCost = string.IsNullOrEmpty(dt.Rows[i]["OtherCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["OtherCost"].ToString()),
//                            Specifications = Convert.ToString(dt.Rows[i]["Specifications"]),
//                            Job = Convert.ToString(dt.Rows[i]["Job"]),
//                            Variance = Convert.ToString(dt.Rows[i]["Variance"]),
//                            SubVariance = Convert.ToString(dt.Rows[i]["SubVariance"]),
//                            Quantity = string.IsNullOrEmpty(dt.Rows[i]["Quantity"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["Quantity"].ToString()),
//                            Unit = Convert.ToString(dt.Rows[i]["Unit"]),
//                            UnitCost = string.IsNullOrEmpty(dt.Rows[i]["UnitCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["UnitCost"].ToString()),
//                            TotalCost = string.IsNullOrEmpty(dt.Rows[i]["TotalCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["TotalCost"].ToString()),
//                            CalculatedCost = string.IsNullOrEmpty(dt.Rows[i]["CalculatedCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["CalculatedCost"].ToString()),
//                            AssignedCost = string.IsNullOrEmpty(dt.Rows[i]["AssignedCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["AssignedCost"].ToString()),
//                            IsFinishCostApplied = string.IsNullOrEmpty(dt.Rows[i]["IsFinishCostApplied"].ToString()) ? (bool?)null : bool.Parse(dt.Rows[i]["IsFinishCostApplied"].ToString()),
//                            FinishArea = string.IsNullOrEmpty(dt.Rows[i]["FinishArea"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["FinishArea"].ToString()),
//                            FinishAreaCost = string.IsNullOrEmpty(dt.Rows[i]["FinishAreaCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["FinishAreaCost"].ToString()),
//                            ConcreteGradeDetails = Convert.ToString(dt.Rows[i]["ConcreteGradeDetails"]),
//                            ConcreteGradeTotalCost = Convert.ToString(dt.Rows[i]["ConcreteGradeTotalCost"]),
//                            ConcreteGradeAssignedCost = Convert.ToString(dt.Rows[i]["ConcreteGradeAssignedCost"]),
//                            isedited = string.IsNullOrEmpty(dt.Rows[i]["isedited"].ToString()) ? (bool?)null : Convert.ToBoolean(dt.Rows[i]["isedited"])
//                            //SubvariaceConcreteGrade = Convert.ToString(dt.Rows[i]["SubvariaceConcreteGrade"])

//                        };
//                        cost_dtl1.Add(cost_dtl11);
//                    }

//                    return cost_dtl1;
//                }
//            }
//            catch (System.Exception ex)

//            {

//                if (state == ConnectionState.Open)
//                {
//                    con.Close();
//                }

//                return null;
//            }

        //        }
        #endregion
        public List<cost_dtl> readcostdtl(string proj_id, string proj_name, int proj_version)
        {
            SqlConnection con = new SqlConnection(connection_string);
            ConnectionState state = con.State;
            try
            {


                SqlCommand cmd;
                SqlDataAdapter sda;
                DataTable dt;
                List<cost_dtl> cost_dtl1 = new List<cost_dtl>();
                using (con = new SqlConnection(connection_string))
                {
                    cmd = new SqlCommand(@"select element_id,element_type_id,JSON_VALUE(elemnt_dtl,'$.ElementName') as ElementName,JSON_VALUE(elemnt_dtl,'$.ElementVolume') as ElementVolume,JSON_VALUE(elemnt_dtl,'$.CalculatedCost') as CalculatedCost,JSON_VALUE(elemnt_dtl,'$.AssignedCost') as AssignedCost
                                            ,f_virtual,JSON_VALUE(elemnt_dtl,'$.IsOutsourcing') as IsOutsourcing,JSON_VALUE(elemnt_dtl,'$.IsMaterial1') as IsMaterial1,
                                             JSON_VALUE(elemnt_dtl,'$.IsMaterial2') as IsMaterial2,JSON_VALUE(elemnt_dtl,'$.IsLabor1') as IsLabor1, 
                                              JSON_VALUE(elemnt_dtl,'$.IsLabor2') as IsLabor2,JSON_VALUE(elemnt_dtl,'$.IsOther') as IsOther,
                                                JSON_VALUE(elemnt_dtl,'$.Job') as Job,JSON_VALUE(elemnt_dtl,'$.Variance') as Variance,
                                                    JSON_VALUE(elemnt_dtl,'$.SubVariance') as SubVariance,JSON_VALUE(elemnt_dtl,'$.Quantity') as Quantity,JSON_VALUE(elemnt_dtl,'$.UserInputQuantity') as UserInputQuantity,JSON_VALUE(elemnt_dtl,'$.IsFinishCostApplied') as IsFinishCostApplied,
                                                    JSON_VALUE(elemnt_dtl,'$.FinishArea') as FinishArea,JSON_VALUE(elemnt_dtl,'$.FinishAreaCost') as FinishAreaCost,
                                                        JSON_VALUE(elemnt_dtl,'$.Unit') as Unit,JSON_VALUE(elemnt_dtl,'$.UnitCost') as UnitCost,
                                                    JSON_VALUE(elemnt_dtl,'$.TotalCost') as TotalCost,
                                                    JSON_VALUE(elemnt_dtl,'$.OutsourcingCost') as OutsourcingCost,JSON_VALUE(elemnt_dtl,'$.Material1Cost') as Material1Cost,
                                                    JSON_VALUE(elemnt_dtl,'$.Material2Cost') as Material2Cost,JSON_VALUE(elemnt_dtl,'$.Labor1Cost') as Labor1Cost,
                                                    JSON_VALUE(elemnt_dtl,'$.Labor2Cost') as Labor2Cost,JSON_VALUE(elemnt_dtl,'$.OtherCost') as OtherCost,
                                                    JSON_VALUE(elemnt_dtl,'$.Specification') as Specifications,
                                                    JSON_VALUE(elemnt_dtl,'$.ConcreteGradeDetails') as ConcreteGradeDetails,
                                                    JSON_VALUE(elemnt_dtl,'$.ConcreteGradeTotalCost') as ConcreteGradeTotalCost,
                                                    JSON_VALUE(elemnt_dtl,'$.ConcreteGradeAssignedCost') as ConcreteGradeAssignedCost,
                                                    JSON_VALUE(elemnt_dtl,'$.ConcreteGradeUserInputQuantity') as ConcreteGradeUserInputQuantity,
                                                    JSON_VALUE(elemnt_dtl,'$.ConcreteGradeIsQuantityEdited') as ConcreteGradeIsQuantityEdited,
                                                    JSON_VALUE(elemnt_dtl,'$.isedited') as isedited from Revit_project_model where proj_version_id in(select id from Revit_project_version where version=" + proj_version + " and project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') )and f_virtual = 1;", con);
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable("cost");
                    sda.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //"Quantity":23.979,"Unit":"m²","UnitCost":380.7,"TotalCost":9128.8053,"AssignedCost":10000.0}
                        cost_dtl cost_dtl11 = new cost_dtl
                        {
                            revit_element_type_id = string.IsNullOrEmpty(dt.Rows[i]["element_type_id"].ToString()) ? (Int64?)null : Convert.ToInt64(dt.Rows[i]["element_type_id"]),
                            revit_element_id = string.IsNullOrEmpty(dt.Rows[i]["element_id"].ToString()) ? (Int64?)null : Convert.ToInt64(dt.Rows[i]["element_id"]),
                            ElementName = Convert.ToString(dt.Rows[i]["ElementName"]),
                            ElementVolume = string.IsNullOrEmpty(dt.Rows[i]["ElementVolume"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["ElementVolume"].ToString()),
                            f_virtual = string.IsNullOrEmpty(dt.Rows[i]["f_virtual"].ToString()) ? (Int32?)null : Convert.ToInt32(dt.Rows[i]["f_virtual"]),
                            IsOutsourcing = string.IsNullOrEmpty(dt.Rows[i]["IsOutsourcing"].ToString()) ? (bool?)null : Convert.ToBoolean(dt.Rows[i]["IsOutsourcing"]),
                            IsMaterial1 = string.IsNullOrEmpty(dt.Rows[i]["IsMaterial1"].ToString()) ? (bool?)null : Convert.ToBoolean(dt.Rows[i]["IsMaterial1"]),
                            IsMaterial2 = string.IsNullOrEmpty(dt.Rows[i]["IsMaterial2"].ToString()) ? (bool?)null : Convert.ToBoolean(dt.Rows[i]["IsMaterial2"]),
                            IsLabor1 = string.IsNullOrEmpty(dt.Rows[i]["IsLabor1"].ToString()) ? (bool?)null : Convert.ToBoolean(dt.Rows[i]["IsLabor1"]),
                            IsLabor2 = string.IsNullOrEmpty(dt.Rows[i]["IsLabor2"].ToString()) ? (bool?)null : Convert.ToBoolean(dt.Rows[i]["IsLabor2"]),
                            IsOther = string.IsNullOrEmpty(dt.Rows[i]["IsOther"].ToString()) ? (bool?)null : Convert.ToBoolean(dt.Rows[i]["IsOther"]),
                            OutsourcingCost = string.IsNullOrEmpty(dt.Rows[i]["OutsourcingCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["OutsourcingCost"].ToString()),
                            Material1Cost = string.IsNullOrEmpty(dt.Rows[i]["Material1Cost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["Material1Cost"].ToString()),
                            Material2Cost = string.IsNullOrEmpty(dt.Rows[i]["Material2Cost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["Material2Cost"].ToString()),
                            Labor1Cost = string.IsNullOrEmpty(dt.Rows[i]["Labor1Cost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["Labor1Cost"].ToString()),
                            Labor2Cost = string.IsNullOrEmpty(dt.Rows[i]["Labor2Cost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["Labor2Cost"].ToString()),
                            OtherCost = string.IsNullOrEmpty(dt.Rows[i]["OtherCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["OtherCost"].ToString()),
                            Specifications = Convert.ToString(dt.Rows[i]["Specifications"]),
                            Job = Convert.ToString(dt.Rows[i]["Job"]),
                            Variance = Convert.ToString(dt.Rows[i]["Variance"]),
                            SubVariance = Convert.ToString(dt.Rows[i]["SubVariance"]),
                            Quantity = string.IsNullOrEmpty(dt.Rows[i]["Quantity"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["Quantity"].ToString()),
                            UserInputQuantity = string.IsNullOrEmpty(dt.Rows[i]["UserInputQuantity"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["UserInputQuantity"].ToString()),
                            Unit = Convert.ToString(dt.Rows[i]["Unit"]),
                            UnitCost = string.IsNullOrEmpty(dt.Rows[i]["UnitCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["UnitCost"].ToString()),
                            TotalCost = string.IsNullOrEmpty(dt.Rows[i]["TotalCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["TotalCost"].ToString()),
                            CalculatedCost = string.IsNullOrEmpty(dt.Rows[i]["CalculatedCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["CalculatedCost"].ToString()),
                            AssignedCost = string.IsNullOrEmpty(dt.Rows[i]["AssignedCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["AssignedCost"].ToString()),
                            IsFinishCostApplied = string.IsNullOrEmpty(dt.Rows[i]["IsFinishCostApplied"].ToString()) ? (bool?)null : bool.Parse(dt.Rows[i]["IsFinishCostApplied"].ToString()),
                            FinishArea = string.IsNullOrEmpty(dt.Rows[i]["FinishArea"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["FinishArea"].ToString()),
                            FinishAreaCost = string.IsNullOrEmpty(dt.Rows[i]["FinishAreaCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["FinishAreaCost"].ToString()),
                            ConcreteGradeDetails = Convert.ToString(dt.Rows[i]["ConcreteGradeDetails"]),
                            ConcreteGradeTotalCost = Convert.ToString(dt.Rows[i]["ConcreteGradeTotalCost"]),
                            ConcreteGradeAssignedCost = Convert.ToString(dt.Rows[i]["ConcreteGradeAssignedCost"]),
                            ConcreteGradeIsQuantityEdited = Convert.ToString(dt.Rows[i]["ConcreteGradeIsQuantityEdited"]),
                            ConcreteGradeUserInputQuantity = Convert.ToString(dt.Rows[i]["ConcreteGradeUserInputQuantity"]),
                            isedited = string.IsNullOrEmpty(dt.Rows[i]["isedited"].ToString()) ? (bool?)null : Convert.ToBoolean(dt.Rows[i]["isedited"])
                            //SubvariaceConcreteGrade = Convert.ToString(dt.Rows[i]["SubvariaceConcreteGrade"])

                        };
                        cost_dtl1.Add(cost_dtl11);
                    }

                    return cost_dtl1;
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
        public List<cost_dtl_element> readcostdtlprojelement(string proj_id, string proj_name,List<Int64> element_id, int proj_version)
        {
            SqlConnection con = new SqlConnection(connection_string);
            ConnectionState state = con.State;
            try
            {
                SqlCommand cmd;
                SqlDataAdapter sda;
                DataTable dt;
                List<long> elem_id = element_id;
                List<cost_dtl_element> cost_dtl1 = new List<cost_dtl_element>();
                using (con = new SqlConnection(connection_string))
                {
                    for (int i = 0; i < elem_id.Count; i++)
                    {
                        cmd = new SqlCommand(@"select element_id,element_type_id,JSON_VALUE(elemnt_dtl,'$.ElementName') as ElementName,JSON_VALUE(elemnt_dtl,'$.ElementVolume') as ElementVolume,JSON_VALUE(elemnt_dtl,'$.TotalCalculatedCost') as CalculatedCost,JSON_VALUE(elemnt_dtl,'$.TotalAssignedCost') as AssignedCost
                                            ,JSON_VALUE(elemnt_dtl,'$.Quantity') as Quantity,JSON_VALUE(elemnt_dtl,'$.ElementConcreteCostData.ConcreteTotalCost') as ConcreteCost,JSON_VALUE(elemnt_dtl,'$.ElementRebarCostData.RebarTotalCost') as RebarCost,JSON_VALUE(elemnt_dtl,'$.ElementFormWorkCostData.FormWorkElementTotalCost') as FormWorkCost,
                                                        JSON_VALUE(elemnt_dtl,'$.Unit') as Unit,JSON_VALUE(elemnt_dtl,'$.IsFinishCostApplied') as IsFinishCostApplied,
                                                    JSON_VALUE(elemnt_dtl,'$.FinishArea') as FinishArea,JSON_VALUE(elemnt_dtl,'$.FinishAreaCost') as FinishAreaCost,JSON_VALUE(elemnt_dtl,'$.UnitCost') as UnitCost,
                                                    JSON_VALUE(elemnt_dtl,'$.TotalCost') as TotalCost from Revit_project_model where proj_version_id in(select id from Revit_project_version where project_id in(select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "')and version=" + proj_version + " ) and element_id =" + elem_id[i] + " and f_virtual = 0;", con);
                        sda = new SqlDataAdapter(cmd);
                        dt = new DataTable("cost");
                        sda.Fill(dt);
                        int cnt = dt.Rows.Count;
                        if (cnt > 0)
                        {
                            cost_dtl_element cost_dtl11 = new cost_dtl_element
                            {
                                revit_element_type_id = string.IsNullOrEmpty(dt.Rows[0]["element_type_id"].ToString()) ? (Int64?)null : Convert.ToInt64(dt.Rows[0]["element_type_id"]),
                                revit_element_id = string.IsNullOrEmpty(dt.Rows[0]["element_id"].ToString()) ? (Int64?)null : Convert.ToInt64(dt.Rows[0]["element_id"]),
                                ElementName = Convert.ToString(dt.Rows[i]["ElementName"]),
                                ElementVolume = string.IsNullOrEmpty(dt.Rows[i]["ElementVolume"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["ElementVolume"].ToString()),
                                Quantity = string.IsNullOrEmpty(dt.Rows[0]["Quantity"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[0]["Quantity"].ToString()),
                                Unit = Convert.ToString(dt.Rows[0]["Unit"]),
                                UnitCost = string.IsNullOrEmpty(dt.Rows[0]["UnitCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[0]["UnitCost"].ToString()),
                                ConcreteCost = string.IsNullOrEmpty(dt.Rows[0]["ConcreteCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[0]["ConcreteCost"].ToString()),
                                RebarCost = string.IsNullOrEmpty(dt.Rows[0]["RebarCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[0]["RebarCost"].ToString()),
                                FormWorkCost = string.IsNullOrEmpty(dt.Rows[0]["FormWorkCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[0]["FormWorkCost"].ToString()),
                                TotalCost = string.IsNullOrEmpty(dt.Rows[0]["TotalCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[0]["TotalCost"].ToString()),
                                CalculatedCost = string.IsNullOrEmpty(dt.Rows[0]["CalculatedCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[0]["CalculatedCost"].ToString()),
                                AssignedCost = string.IsNullOrEmpty(dt.Rows[0]["AssignedCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[0]["AssignedCost"].ToString()),
                                IsFinishCostApplied = string.IsNullOrEmpty(dt.Rows[i]["IsFinishCostApplied"].ToString()) ? (bool?)null : bool.Parse(dt.Rows[i]["IsFinishCostApplied"].ToString()),
                                FinishArea = string.IsNullOrEmpty(dt.Rows[i]["FinishArea"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["FinishArea"].ToString()),
                                FinishAreaCost = string.IsNullOrEmpty(dt.Rows[i]["FinishAreaCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["FinishAreaCost"].ToString())
                            };
                            cost_dtl1.Add(cost_dtl11);
                        }
                    }
                }
                return cost_dtl1;
            }
            catch(System.Exception ex)
            {
                if (state == ConnectionState.Open)
                {
                    con.Close();
                }

                return null;
            }
                    }
        public List<cost_dtl_element> readcostdtlprojrevitelement(string proj_id, string proj_name, int proj_version)
        {
            SqlConnection con = new SqlConnection(connection_string);
            ConnectionState state = con.State;
            try
            {
                SqlCommand cmd;
                SqlDataAdapter sda;
                DataTable dt;
                List<cost_dtl_element> cost_dtl1 = new List<cost_dtl_element>();
                using (con = new SqlConnection(connection_string))
                {

                    cmd = new SqlCommand(@"select element_id,element_type_id,JSON_VALUE(elemnt_dtl,'$.ElementName') as ElementName,JSON_VALUE(elemnt_dtl,'$.ElementVolume') as ElementVolume,JSON_VALUE(elemnt_dtl,'$.TotalCalculatedCost') as CalculatedCost,JSON_VALUE(elemnt_dtl,'$.TotalAssignedCost') as AssignedCost
                                            ,JSON_VALUE(elemnt_dtl,'$.Quantity') as Quantity,JSON_VALUE(elemnt_dtl,'$.ElementConcreteCostData.ConcreteTotalCost') as ConcreteCost,JSON_VALUE(elemnt_dtl,'$.ElementRebarCostData.RebarTotalCost') as RebarCost,JSON_VALUE(elemnt_dtl,'$.ElementFormWorkCostData.FormWorkElementTotalCost') as FormWorkCost,
                                                        JSON_VALUE(elemnt_dtl,'$.Unit') as Unit,JSON_VALUE(elemnt_dtl,'$.IsFinishCostApplied') as IsFinishCostApplied,
                                                    JSON_VALUE(elemnt_dtl,'$.FinishArea') as FinishArea,JSON_VALUE(elemnt_dtl,'$.FinishAreaCost') as FinishAreaCost,JSON_VALUE(elemnt_dtl,'$.UnitCost') as UnitCost,
                                                    JSON_VALUE(elemnt_dtl,'$.TotalCost') as TotalCost from Revit_project_model where proj_version_id in(select id from Revit_project_version where project_id in (select id from project where proj_guid = N'" + proj_id + "' and name = N'" + proj_name + "') and version=" + proj_version + ") and f_virtual = 0;", con);
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable("cost");
                    sda.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cost_dtl_element cost_dtl11 = new cost_dtl_element
                        {
                            revit_element_type_id = string.IsNullOrEmpty(dt.Rows[i]["element_type_id"].ToString()) ? (Int64?)null : Convert.ToInt64(dt.Rows[i]["element_type_id"]),
                            revit_element_id = string.IsNullOrEmpty(dt.Rows[i]["element_id"].ToString()) ? (Int64?)null : Convert.ToInt64(dt.Rows[i]["element_id"]),
                            ElementName = Convert.ToString(dt.Rows[i]["ElementName"]),
                            ElementVolume = string.IsNullOrEmpty(dt.Rows[i]["ElementVolume"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["ElementVolume"].ToString()),
                            Quantity = string.IsNullOrEmpty(dt.Rows[i]["Quantity"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["Quantity"].ToString()),
                            Unit = Convert.ToString(dt.Rows[i]["Unit"]),
                            UnitCost = string.IsNullOrEmpty(dt.Rows[i]["UnitCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["UnitCost"].ToString()),
                            ConcreteCost = string.IsNullOrEmpty(dt.Rows[i]["ConcreteCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["ConcreteCost"].ToString()),
                            RebarCost = string.IsNullOrEmpty(dt.Rows[i]["RebarCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["RebarCost"].ToString()),
                            FormWorkCost = string.IsNullOrEmpty(dt.Rows[i]["FormWorkCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["FormWorkCost"].ToString()),
                            TotalCost = string.IsNullOrEmpty(dt.Rows[i]["TotalCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["TotalCost"].ToString()),
                            CalculatedCost = string.IsNullOrEmpty(dt.Rows[i]["CalculatedCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["CalculatedCost"].ToString()),
                            AssignedCost = string.IsNullOrEmpty(dt.Rows[i]["AssignedCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["AssignedCost"].ToString()),
                            IsFinishCostApplied = string.IsNullOrEmpty(dt.Rows[i]["IsFinishCostApplied"].ToString()) ? (bool?)null : bool.Parse(dt.Rows[i]["IsFinishCostApplied"].ToString()),
                            FinishArea = string.IsNullOrEmpty(dt.Rows[i]["FinishArea"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["FinishArea"].ToString()),
                            FinishAreaCost = string.IsNullOrEmpty(dt.Rows[i]["FinishAreaCost"].ToString()) ? (decimal?)null : decimal.Parse(dt.Rows[i]["FinishAreaCost"].ToString())
                        };
                        cost_dtl1.Add(cost_dtl11);

                    }




                    return cost_dtl1;
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
    }
}
