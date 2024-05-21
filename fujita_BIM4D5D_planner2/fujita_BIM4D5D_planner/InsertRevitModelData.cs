using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface InsertRevitModelData
    {

        [OperationContract]
        void ins_revit_model(string Project_id,string proj_name, int? element_type_id, int? element_id, string elemnt_dtl, string usr, [Optional]int f_virtual, [Optional] string virtual_element_name, string country, string city, int proj_version, [Optional] int? isQuantitySaved, [Optional] int? isInputCostSaved);
        [OperationContract]
        Int64? maxprojversion(string Project_id, string proj_name);
        [OperationContract]
        List<version_dtl> showallrevision(string Project_id, string proj_name);
        [OperationContract]
        Int16 revit_model_data_copy(string Project_id, string proj_name, string country, string city, int proj_version);
        [OperationContract]
        save_dtl readsavedtl(string Project_id, string proj_name, int proj_version);
        [OperationContract]
        void ins_revit_model_Test(List<DBElementdetails> lstElementDetails);      
      
    }

    [DataContract]
    public class version_dtl
    {
        [DataMember]
        public Int64 version { get; set; }
        [DataMember]
        public DateTime created_on { get; set; }

    }
    [DataContract]
    public class save_dtl
    {
        [DataMember]
        public Int16? isQuantitySaved { get; set; }
        [DataMember]
        public Int16? isInputCostSaved { get; set; }

    }

    [DataContract]
    public class DBElementdetails
    {
        [DataMember]
        public string ProjGUID { get; set; }

        [DataMember]
        public string ProjName { get; set; }

        [DataMember]
        public int TypeID { get; set; }

        [DataMember]
        public int ElementID { get; set; }

        [DataMember]
        public string Elementdetails { get; set; }
       
        [DataMember]
        public string User { get; set; }

        [DataMember]
        public int IsVirtualElement { get; set; }

        [DataMember]
        public string VirtualElemetName { get; set; }

        [DataMember]
        public string ProjectCountry { get; set; }

        [DataMember]
        public string ProjectCity { get; set; }

        [DataMember]
        public int ProjectVersion { get; set; }

        [DataMember]
        public short? IsProjectQuantitySaved { get; set; }

        [DataMember]
        public short? IsProjectInputCostSaved { get; set; }
    }

}
