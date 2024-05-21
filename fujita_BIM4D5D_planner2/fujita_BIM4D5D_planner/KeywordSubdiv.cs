using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService31" in both code and config file together.
    [ServiceContract]
    public interface SubdivisionKeyword
    {
        [OperationContract]
        List<subdiv_keyword_details> GetAllKeyword(bool isJPN);
        [OperationContract]
        subdiv_keyword_details GetSpecificKeyword(Int64 key_index);
        [OperationContract]
        string UpdateKeyword(Int64 key_index, string keyword_eng, string keyword_jpn, string user);
    }

    [DataContract]
    public class subdiv_keyword_details
    {
        [DataMember]
        public Int64 keyword_index { get; set; }
        [DataMember]
        public string propname_eng { get; set; }
        [DataMember]
        public string propname_jpn { get; set; }
        [DataMember]
        public string specification { get; set; }
    }
}
