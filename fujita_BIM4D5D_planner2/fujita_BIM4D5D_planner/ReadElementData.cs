using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService4" in both code and config file together.
    [ServiceContract]
    public interface ReadElementData
    {
        [OperationContract]
        element_dtl showallelm(string proj_id, string proj_name, int proj_version);
    }
    [DataContract]
    public class element_dtl
    {
        [DataMember]
        public DataTable element_detail
        {
            get;
            set;
        }
    }

}
