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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService5" in both code and config file together.
    [ServiceContract]
    public interface ReadProjDetails
    {
        [OperationContract]
        proj_dtl showallprojdetail([Optional]string proj_id,[Optional] string proj_name, [Optional] int? f_show_all);
        [OperationContract]
        DataTable showallprojCountry(string country, string city);
    }
    [DataContract]
    public class proj_dtl
    {
        [DataMember]
        public DataTable proj_detail
        {
            get;
            set;
        }
    }
}
