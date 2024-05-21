using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService10" in both code and config file together.
    [ServiceContract]
    public interface oldmsprojoffice
    {
        [OperationContract]
        List<msproj_dtl> showmsprojdetail(string proj_id);
        [OperationContract]
        Int64 Put(List<msproj_dtl> msproj_dtl11, string proj_id);
    }
    [DataContract]
    public class msproj_dtl
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int? seq { get; set; }
        [DataMember]
        public string level { get; set; }
        [DataMember]
        public DateTime start_date { get; set; }
        [DataMember]
        public DateTime end_date { get; set; }
        [DataMember]
        public int? duration { get; set; }
        [DataMember]
        public int? actual_dur { get; set; }
        [DataMember]
        public int? progress { get; set; }
        [DataMember]
        public decimal? cost { get; set; }
        [DataMember]
        public List<string> resource { get; set; }
        [DataMember]
        public List<int?> Revit_elements { get; set; }
        [DataMember]
        public List<int?> predecessor { get; set; }
        [DataMember]
        public List<int?> successor { get; set; }

    }
}

