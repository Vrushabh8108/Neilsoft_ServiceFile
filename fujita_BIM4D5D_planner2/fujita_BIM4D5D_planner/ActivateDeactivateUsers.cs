using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BIM4D5D_service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService13" in both code and config file together.
    [ServiceContract]
    public interface ActivateDeactivateUsers
    {
        [OperationContract]
        void activate_deactivate(String username, int flag,List<country_access> country_access, [Optional]int? f_admin);
    }
    [DataContract]
    public class country_access
    {
        [DataMember]
        public string country_code { get; set; }
        [DataMember]
        public Int16 access_dtl { get; set; }
    }
}
