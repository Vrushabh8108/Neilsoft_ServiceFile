using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BIM4D5D_service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService3" in both code and config file together.
    [ServiceContract]
    public interface cost_insert_update
    {
        [OperationContract]
        void cost_ins_upd(String mat_name, String mat_var_name, string mat_var_sub, string mat_var_opt, decimal cost, string user_info, string country_code,string city_name);

        [OperationContract]
        void cost_ins_upd_proj(String mat_name, String mat_var_name, string mat_var_sub, string mat_var_opt, decimal cost, string user_info, string country_code, string city_name, string proj_id, string proj_name);
    }
}
