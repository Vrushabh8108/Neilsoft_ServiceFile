using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService16" in both code and config file together.
    [ServiceContract]
    public interface AddCost
    {
        [OperationContract]
        addcost_dtl readcostdtlprojelement(string proj_id, string proj_name, List<Int64> element_id, string country_code, string city, int proj_version);
    }
     [DataContract]
    public class addcost_dtl
    {
        [DataMember]
        public DataTable addcost_detail
        {
            get;
            set;
        }
    }
}
