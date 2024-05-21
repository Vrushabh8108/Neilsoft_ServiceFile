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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService26" in both code and config file together.
    [ServiceContract]
    public interface ReportAccessDetails
    {
        [OperationContract]
        void ins_ReportAccessDetail(string reportName, string accessBy, [Optional] DateTime accessTime);
        [OperationContract]
        reportAccess_dtl retrieve_ReportAccessDetail();


    }

    [DataContract]
    public class reportAccess_dtl
    {
        [DataMember]
        public DataTable access_dtl
        {
            get;
            set;
        }

    }

}
