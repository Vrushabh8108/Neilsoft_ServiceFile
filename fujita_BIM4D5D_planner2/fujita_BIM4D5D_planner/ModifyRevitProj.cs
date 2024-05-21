using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService3" in both code and config file together.
    [ServiceContract]
    public interface ModifyRevitProj
    {
        [OperationContract]
        void mod_revit_model(string elmt_dtl, string Project_id, Int64 element_id, Int64 element_no, string usr);
    }
}
