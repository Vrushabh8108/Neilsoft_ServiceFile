using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BIM4D5D_service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService12" in both code and config file together.
    [ServiceContract]
    public interface showalluser
    {
        [OperationContract]
        List<User_dtl> showallusr(int flag);
    }
    [DataContract]
    public class user_dtl
    {
        [DataMember]
        public DataTable user_detail
        {
            get;
            set;
        }
    }
    [DataContract]
    public class User_dtl
    {
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string first_name { get; set; }
        [DataMember]
        public string last_name { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string phone { get; set; }
        [DataMember]
        public string emp_id { get; set; }
        [DataMember]
        public int f_admin { get; set; }
        [DataMember]
        public List<country_access> country_access { get; set; }
    }

}
