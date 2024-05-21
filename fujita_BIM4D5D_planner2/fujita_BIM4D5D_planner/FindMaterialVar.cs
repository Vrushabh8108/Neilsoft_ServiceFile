using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BIM4D5D_service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFindMaterialOption" in both code and config file together.
    [ServiceContract]
    public interface IFindMaterialOption
    {
        [OperationContract]
        List<string> FindMaterialVar(String Mat);
    }
    public interface mat_opt
    {
        string mat_var { get; set; }
        string mat_var_sub { get; set; }
    }
}
