using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BIM4D5D_service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService14" in both code and config file together.
    [ServiceContract]
    public interface EntityCost
    {
        [OperationContract]
        Nullable<Decimal> find_cost(string mat, string mat_var, string mat_var_sub, string mat_opt, string country_code, string city);
        [OperationContract]
        Nullable<Decimal> find_cost_proj(string mat, string mat_var, string mat_var_sub, string mat_opt, string country_code, string city, string proj_id, string proj_name);
        [OperationContract]
        Nullable<Decimal> find_cost_categoryid(Int64 mat_id, Int64 mat_var, Int64 mat_var_sub, string mat_opt, string country_code, string city);
        [OperationContract]
        Nullable<Decimal> find_cost_categoryid_name(Int64 mat_id, string mat_var, string mat_var_sub, string mat_opt, string country_code, string city);
        [OperationContract]
        Nullable<Decimal> find_cost_categoryid_varianceid(Int64 mat_id, Int64 mat_var, string mat_var_sub, string mat_opt, string country_code, string city);
    }
}
