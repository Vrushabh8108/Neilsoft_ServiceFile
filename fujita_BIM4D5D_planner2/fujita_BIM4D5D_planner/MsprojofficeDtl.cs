using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService14" in both code and config file together.
    [ServiceContract]
    public interface MsprojofficeDtl
    {
        [OperationContract]
        Int16 ins_msprojoffice_diff(string Project_guid, string proj_name, string diff_guid, Int64 first_ver, Int64 second_ver, Int64 proj_ver);
        [OperationContract]
        int showpending(string proj_id, string proj_name, Int64 proj_ver);
        [OperationContract]
        diff_dtl readdiffdtl(string comparison_id);
        [OperationContract]
        Int16 upd_msprojoffice_diff(string Project_guid, string proj_name, string diff_guid, Int16 f_approved, Int64 proj_ver);

    }
    [DataContract]
    public class diff_dtl
    {
        [DataMember]
        public string proj_guid { get; set; }
        [DataMember]
        public string proj_name { get; set; }
        [DataMember]
        public Int64 from_ver { get; set; }
        [DataMember]
        public Int64 to_ver { get; set; }
        [DataMember]
        public Int64 f_approved { get; set; }
        [DataMember]
        public Int64 version { get; set; }
    }
}
