using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BIM4D5D_service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService10" in both code and config file together.
    [ServiceContract]
    public interface SignUp
    {
        [OperationContract]
        void sign_up(String usrname, string pwd, string first_name, string last_name, string email, long phone, long emp_id);
    }
}
