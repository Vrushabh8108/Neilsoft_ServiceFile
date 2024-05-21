using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BIM4D5D_service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService7" in both code and config file together.
    [ServiceContract]
    public interface Login
    {
        [OperationContract]
        country_access_dtl checklogin(string email, string pwd);
        [OperationContract]
        string UpdateCityDatainCostMaster();       

    }
    [DataContract]
    public class country_access_dtl
    {
        [DataMember]
        public int auth { get; set; }
        [DataMember]
        public int f_admin { get; set; }
        [DataMember]
        public List<country_access> access_dtl { get; set; }
    }


}
