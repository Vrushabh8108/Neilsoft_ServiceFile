using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BIM4D5D_service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService8" in both code and config file together.
    [ServiceContract]
    public interface Delete_Subdiv
    {
        [OperationContract]
        void delete_subdiv(String mat_name, String mat_var_name, String mat_var_sub, string user, string city_name, string country_name, bool f_allCountry, bool f_allCity, bool f_singleCity);

        [OperationContract]
        void delete_subdiv_project(String mat_name, String mat_var_name, String mat_var_sub, string user, string proj_guid, string proj_name);
    
        [OperationContract]
        bool SubdivExistsInMaster(String mat_name, String mat_var_name, String mat_var_sub, string user, string proj_guid, string proj_name, string city_name, string country_name);
            
    }
}
