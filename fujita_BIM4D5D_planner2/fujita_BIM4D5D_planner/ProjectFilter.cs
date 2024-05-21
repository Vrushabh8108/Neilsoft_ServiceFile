using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService10" in both code and config file together.
    [ServiceContract]
    public interface ProjectFilter
    {
        [OperationContract]
        Int16 ins_proj_filter(string Project_guid,string proj_name, [Optional] Int64? version, string filter_name, List<filter_element> filter_dtl, [Optional] Int64? proj_version);
        [OperationContract]
        List<filter_dtl> showallfilter(string proj_id, string proj_name, [Optional] Int64? version, [Optional] Int64? proj_version);
    }
    [DataContract]
    public class filter_dtl
    {
        [DataMember]
        public string filter_name { get; set; }
        [DataMember]
        public List<filter_element> filter_element_dtl { get; set; }

    }
    public class filter_element
    {
        [DataMember]
        public string filter_category { get; set; }
        [DataMember]
        public string filter_sign { get; set; }
        [DataMember]
        public string filter_value { get; set; }

    }
    }
