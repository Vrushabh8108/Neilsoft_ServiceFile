using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService11" in both code and config file together.
    [ServiceContract]
    public interface GetElementStartEndDate
    {
        [OperationContract]
        List<Rev_element_dtl> readelementdtl(string proj_id,string proj_name, Int64 ver, List<Int64> element_id, Int64 Proj_ver);
    }

        [DataContract]
        public class Rev_element_dtl
        {

            [DataMember]
            public Int64? revit_element_id { get; set; }
            [DataMember]
            public decimal? cost { get; set; }
            [DataMember]
            public DateTime? start_date { get; set; }
            [DataMember]
            public DateTime? end_date { get; set; }
        }
}
