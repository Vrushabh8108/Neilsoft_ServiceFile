using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BIM4D5D_service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService4" in both code and config file together.
    [ServiceContract]
    public interface material_subdiv
    {
        [OperationContract]
        //void DoWork();
        Mat_sub_div Findmaterialsub(string mat, string mat_var, string city_name, string country_name);
        [OperationContract]
        Mat_sub_div Findmaterialsub_proj(string mat, string mat_var, string proj_id, string proj_name);
        [OperationContract]
        Mat_sub_div Findmaterialsub_categoryid(Int64 mat_id, Int64 mat_var);
    }
    [DataContract]
    public class Mat_sub_div
    {
        [DataMember]
        public DataTable Mat_sub_division
        {
            get;
            set;
        }
    }
}
