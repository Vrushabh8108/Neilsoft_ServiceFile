using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService6" in both code and config file together.
    [ServiceContract]
    public interface InsertProjDetails
    {
        [OperationContract]
        void ins_proj_dtl(string Project_guid, string proj_name, string revit_project_file, string ms_project_file, string site_manager_name, string site_manager_email, string designer_name, string designer_email, string country, string city, int f_create_base, [Optional] string construction_type, [Optional] DateTime construction_start_date, [Optional] string ms_proj_file_path, string revit_version);
    }
}
