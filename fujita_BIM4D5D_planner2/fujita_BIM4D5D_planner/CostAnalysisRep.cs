using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService23" in both code and config file together.
    [ServiceContract]
    public interface CostAnalysisRep
    {
        [OperationContract]
        CostAnalysisReport GetCostReport(string Country, string City, DateTime FromDate, DateTime ToDate, [Optional]bool isMonthly,[Optional]bool isQuater, [Optional]bool isYearly,List<InputSubvariance> listSubvariance);
        [OperationContract]
        CostAnalysisReport GetCostReport_Project(string Country, string City, DateTime FromDate, DateTime ToDate, [Optional]bool isMonthly,[Optional]bool isQuater, [Optional]bool isYearly, string proj_guid, string proj_name, List<InputSubvariance> listSubvariance);
    }

    [DataContract]

    public class InputSubvariance
    {
        [DataMember]
        public string Job { get; set; }
        [DataMember]
        public string Subtype { get; set; }
        [DataMember]
        public string Subvariance { get; set; }
    }



    [DataContract]

    public class CostAnalysisReport
    {
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public List<year_week_month_dtl> year_week_month_dtl { get; set; }
    }

    [DataContract]
    public class year_week_month_dtl
    { 
        [DataMember]
        public Int32 no { get; set; }
        [DataMember]
        public DateTime from_date { get; set; }
        [DataMember]
        public DateTime to_date { get; set; }
        [DataMember]
        public List<Job> ListJob { get; set; }
    }

    [DataContract]
    public class Job
    {
        [DataMember]
        public string JobName  { get; set; }
        [DataMember]
        public List<JobSubType> ListJobType { get; set; }
    }
    [DataContract]

    public class JobSubType
    {
        [DataMember]
        public string JobTypeName { get; set; }
        [DataMember]
        public List<JobSubTypeSubVarianceInfo> ListJobTypeSubVarienceInfo { get; set; }
    }
    [DataContract]

    public class JobSubTypeSubVarianceInfo
    {
        [DataMember]
        public string JobSubTypeSubVarianceName { get; set; }
        [DataMember]
        public string JobSubTypeSubVarianceUnit { get; set; }
        [DataMember]
        public List<SubvarianceCost> SubvarianceCost { get; set; }
    }
    [DataContract]
    public class SubvarianceCost
    {
        [DataMember]
        public string Materialoption { get; set; }
        [DataMember]
        public List<cost_dtl_mat> cost_dtl { get; set; }
    }
    [DataContract]
    public class cost_dtl_mat
    {
        [DataMember]
        public DateTime eff_date { get; set; }
        [DataMember]
        public DateTime eff_end_date { get; set; }
        [DataMember]
        public double? cost { get; set; }
    }
}
