using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BIM4D5D_service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService2" in both code and config file together.
    [ServiceContract]
    public interface Materialdtl
    {
        [OperationContract]
        List<Mat_detail_list> FindMaterialdtl(String mat, String Mat_var, string Username, string Password);
        [OperationContract]
        List<Mat_detail_list_project> FindMaterialdtl_Project(String mat, String Mat_var, string Username, string Password);
        [OperationContract]
        List<Mat_detail_list1> FindMaterialdtlall(string Username, string Password);
        [OperationContract]
        List<Mat_detail_list1_AllJobs> FindMaterialdtlall_Project(string Username, string Password);
        [OperationContract]
        List<test> FindAllMaterial_Test();
    }
    [DataContract]
    public class Mat_detail
    {
        [DataMember]
        public DataTable Mat_det
        {
            get;
            set;
        }
    }
    [DataContract]
    public class Mat_detail_list
    {
        [DataMember]
        public string country_code { get; set; }
        [DataMember]
        public string currency { get; set; }
        [DataMember]
        public List<city_dtl> city_dtl { get; set; }
    }
    [DataContract]
    public class Mat_detail_list_project
    {
        [DataMember]
        public string country_code { get; set; }
        [DataMember]
        public string currency { get; set; }
        [DataMember]
        public List<city_dtl_project> city_dtl { get; set; }
    }
}
[DataContract]
public class Mat_detail_list1
{
    [DataMember]
    public string country_code { get; set; }
    [DataMember]
    public string currency { get; set; }
    [DataMember]
    public List<city_dtl1> city_dtl1 { get; set; }
}
[DataContract]
public class Mat_detail_list1_AllJobs
{
    [DataMember]
    public string country_code { get; set; }
    [DataMember]
    public string currency { get; set; }
    [DataMember]
    public List<city_dtl1_AllJobs> city_dtl1 { get; set; }
}
[DataContract]
public class city_dtl
{
    [DataMember]
    public string city_name { get; set; }
    [DataMember]
    public List<cost_dtl> cost_dtl { get; set; }
}
[DataContract]
public class city_dtl_project
{
    [DataMember]
    public string city_name { get; set; }
    [DataMember]
    public List<project_list> projectList { get; set; }
}
[DataContract]
public class city_dtl1
{
    [DataMember]
    public string city_name { get; set; }
    [DataMember]
    public List<mat_dtl> mat_dtl { get; set; }
}
[DataContract]
public class city_dtl1_AllJobs
{
    [DataMember]
    public string city_name { get; set; }
    [DataMember]
    public List<project_list_AllJobs> proj_dtl { get; set; }
}
[DataContract]
public class mat_dtl
{
    [DataMember]
    public string mat_name { get; set; }
    [DataMember]
    public List<mat_var_dtl> mat_var_dtl { get; set; }
}
[DataContract]
public class mat_var_dtl
{
    [DataMember]
    public string mat_var_name { get; set; }
    [DataMember]
    public List<cost_dtl> cost_dtl { get; set; }
}

[DataContract]
public class project_list
{
    [DataMember]
    public string projectName { get; set; }
    [DataMember]
    public string projectID { get; set; }
    [DataMember]
    public List<cost_dtl> cost_dtl { get; set; }
}
[DataContract]
public class project_list_AllJobs
{
    [DataMember]
    public string projectName { get; set; }
    [DataMember]
    public string projectID { get; set; }
    [DataMember]
    public List<mat_dtl> mat_dtl { get; set; }
}


[DataContract]
public class cost_dtl
{
    [DataMember]
    public string subdivision { get; set; }
    [DataMember]
    public string unit_of_measurement { get; set; }
    [DataMember]
    public decimal? outsourcing { get; set; }
    [DataMember]
    public decimal? material1 { get; set; }
    [DataMember]
    public decimal? material2 { get; set; }
    [DataMember]
    public decimal? labor1 { get; set; }
    [DataMember]
    public decimal? labor2 { get; set; }
    [DataMember]
    public decimal? other { get; set; }
}

[DataContract]
public class test
{
    [DataMember]
    public Int64 material_id { get; set; }
    [DataMember]
    public string material { get; set; }
    [DataMember]
    public Int64 material_variance_id { get; set; }
    [DataMember]
    public string variance { get; set; }
    [DataMember]
    public Int64 subdiv_id { get; set; }
    [DataMember]
    public string subdivision { get; set; }
    [DataMember]
    public string unit { get; set; }
    [DataMember]
    public Int64 city_id { get; set; }
    [DataMember]
    public string city_name { get; set; }    
    [DataMember]
    public decimal? Outsourcing { get; set; }
    [DataMember]
    public decimal? Material1 { get; set; }
    [DataMember]
    public decimal? Material2 { get; set; }
    [DataMember]
    public decimal? Labor1 { get; set; }
    [DataMember]
    public decimal? Labor2 { get; set; }
    [DataMember]
    public decimal? Other { get; set; }
    [DataMember]
    public string country { get; set; }
    [DataMember]
    public string currency { get; set; }
}

    