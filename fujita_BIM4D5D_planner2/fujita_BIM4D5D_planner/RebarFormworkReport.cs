using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService15" in both code and config file together.
    [ServiceContract]
    public interface RebarFormworkReport
    {
        [OperationContract]
        rep_dtl showallrepdetail(string proj_id, string proj_name, int f_show_rebar);
        [OperationContract]
        Int64 Put(List<rebar_dtl> rebar_dtl1, List<fw_dtl> fw_dtl1, string proj_id, string proj_name);
    }
    [DataContract]
    public class rep_dtl
    {
        [DataMember]
        public DataTable rep_detail
        {
            get;
            set;
        }
    }
    public class rebar_dtl
    {
        [DataMember]
        public Int64 element_id { get; set; }
        [DataMember]
        public string element_name { get; set; }
        [DataMember]
        public decimal weight { get; set; }
        [DataMember]
        public Int64 no_of_mainbar { get; set; }
        [DataMember]
        public Int64 no_of_stirrups { get; set; }
    }
    public class fw_dtl
    {
        [DataMember]
        public Int64 element_id { get; set; }
        [DataMember]
        public string element_name { get; set; }
        [DataMember]
        public string lb { get; set; }
        [DataMember]
        public decimal area { get; set; }
        [DataMember]
        public decimal cost { get; set; }
        [DataMember]
        public decimal total_cost { get; set; }
    }
}