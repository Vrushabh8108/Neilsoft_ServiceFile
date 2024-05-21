using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService2" in both code and config file together.
    [ServiceContract]
    public interface DeleteRevitProj
    {
        [OperationContract]
        void del_revit_model(string Project_id, Int64 element_id, string usr);
        [OperationContract]
        int del_revit_project(string Project_id, string name, string country, string city);
    }
}
