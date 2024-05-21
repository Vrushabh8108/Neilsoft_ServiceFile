using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService29" in both code and config file together.
    [ServiceContract]
    public interface Projectlock
    {
        [OperationContract]
        int updateprojlock(string proj_guid, string proj_name, int f_proj_lock, string locked_by, string city, string country);
        [OperationContract]
        proj_lock Projlockdtl(string proj_guid, string proj_name, string city, string country);
    }
    [DataContract]
    public class proj_lock
    {
        [DataMember]
        public int f_locked { get; set; }
        [DataMember]
        public string locked_by { get; set; }
    }
}
