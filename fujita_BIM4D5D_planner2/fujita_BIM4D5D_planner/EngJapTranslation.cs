using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService24" in both code and config file together.
    [ServiceContract]
    public interface EngJapTranslation
    {
        [OperationContract]
        List<Eng_jap_translation> Gettranslation (string lang);
    }
    [DataContract]

    public class Eng_jap_translation
    {
        [DataMember]
        public string col_name { get; set; }
        [DataMember]
        public string text { get; set; }
    }
}
