using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService28" in both code and config file together.
    [ServiceContract]
    public interface AddJobVariance
    {
        [OperationContract]
        int AddJobVartypes(string name, string mat_name, string created_by, [Optional]Int64? category_id, [Optional]Int64? prev_seq);
    }
}
