using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService12" in both code and config file together.
    [ServiceContract]
    public interface ProjectExcludeCategory
    {
        [OperationContract]
        Int16 ins_excl_category(string Project_guid,string proj_name, [Optional] Int64? version, string exclude_category, [Optional] Int64? proj_version);
        [OperationContract]
        List<string> get_excl_category(string Project_guid,string proj_name, [Optional] Int64? version, [Optional] Int64? proj_version);
        [OperationContract]
        Int16 del_excl_category(string Project_guid, string proj_name, [Optional] Int64? version, List<string> exclude_category, [Optional] Int64? proj_version);
    }
}
