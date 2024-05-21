using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService13" in both code and config file together.
    [ServiceContract]
    public interface GlobalSettings
    {
        [OperationContract]
        global_setting_dtl showglobalsetting();
        [OperationContract]
        void ins_glb_detail( string web_server_name, string site_manager_name, string site_manager_email, string designer_name, string designer_email,string database_server_path);
    }
    [DataContract]
    public class global_setting_dtl
    {
        [DataMember]
        public DataTable global_setting_detail
        {
            get;
            set;
        }
    }
}
