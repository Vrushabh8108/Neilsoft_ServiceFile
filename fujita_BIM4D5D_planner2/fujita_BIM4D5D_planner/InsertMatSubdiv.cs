using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BIM4D5D_service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService5" in both code and config file together.
    [ServiceContract]
    public interface InsertMatSubdiv
    {
        //[OperationContract]
        //string ins_mat_subdiv(String mat_name, String mat_var_name, string mat_var_sub, string user_info, string unit_of_measurement, [Optional] Int64? prev_seq, [Optional]string revit_category, [Optional]string family_type, [Optional]string property, [Optional]string design_rule_factor, [Optional]string keywords, [Optional]string quantity_extraction_formula, [Optional]string specification, string city_name, string country_name, bool f_allCountry, bool f_allCity, bool f_singleCity, int f_all_elements, [Optional]string element_filter);

        //[OperationContract]
        //string ins_mat_subdiv(String mat_name, String mat_var_name, string mat_var_sub, string user_info, string unit_of_measurement, [Optional] Int64? prev_seq, [Optional]string revit_category, [Optional]string family_type, [Optional]string property, [Optional]string design_rule_factor, [Optional]string keywords, [Optional]string quantity_extraction_formula, [Optional]string specification, string city_name, string country_name, bool f_allCountry, bool f_allCity, bool f_singleCity, int f_all_elements, [Optional]string element_filter);
       
        //Above Code Commented For Adding sortingCondtions Parameter to the Method 
        [OperationContract]
        string ins_mat_subdiv(String mat_name, String mat_var_name, string mat_var_sub, string user_info, string unit_of_measurement, [Optional] Int64? prev_seq, [Optional]string revit_category, [Optional]string family_type, [Optional]string property, [Optional]string design_rule_factor, [Optional]string keywords, [Optional]string quantity_extraction_formula, [Optional]string specification, string city_name, string country_name, bool f_allCountry, bool f_allCity, bool f_singleCity, int f_all_elements, [Optional]string element_filter, string SortingCondtions);
        [OperationContract]
        Mat_var_subdiv_dtl view_mat_var_subdiv_dtl(String mat_name, String mat_var_name, string mat_var_sub, string proj_guid, string proj_name, string city_name, string country_name);
        //[OperationContract]
        //string ins_mat_subdiv_proj(String mat_name, String mat_var_name, string mat_var_sub, string user_info, string unit_of_measurement, [Optional] Int64? prev_seq, [Optional]string revit_category, [Optional]string family_type, [Optional]string property, [Optional]string design_rule_factor, [Optional]string keywords, [Optional]string quantity_extraction_formula, [Optional]string specification, string proj_id, string proj_name, int f_all_elements, [Optional]string element_filter);       
        [OperationContract]
        string ins_mat_subdiv_proj(String mat_name, String mat_var_name, string mat_var_sub, string user_info, string unit_of_measurement, [Optional] Int64? prev_seq, [Optional]string revit_category, [Optional]string family_type, [Optional]string property, [Optional]string design_rule_factor, [Optional]string keywords, [Optional]string quantity_extraction_formula, [Optional]string specification, string proj_id, string proj_name, int f_all_elements, [Optional]string element_filter,string sortingConditions);       
        //[OperationContract]
        //string upd_mat_var_subdiv_dtl_Project(String mat_name, String mat_var_name, string mat_var_sub, [Optional]string unit_of_measurement, [Optional] Int64? prev_seq, [Optional]string user_info, [Optional]string revit_category, [Optional]string family_type, [Optional]string property, [Optional]string design_rule_factor, [Optional]string keywords, [Optional]string quantity_extraction_formula, [Optional]string specification, string proj_guid, string proj_name, int f_all_elements, [Optional]string element_filter);
        [OperationContract]
        string upd_mat_var_subdiv_dtl_Project(String mat_name, String mat_var_name, string mat_var_sub, [Optional]string unit_of_measurement, [Optional] Int64? prev_seq, [Optional]string user_info, [Optional]string revit_category, [Optional]string family_type, [Optional]string property, [Optional]string design_rule_factor, [Optional]string keywords, [Optional]string quantity_extraction_formula, [Optional]string specification, string proj_guid, string proj_name, int f_all_elements, [Optional]string element_filter, string SortingCondtions);
     
        
        //[OperationContract]
        //string upd_mat_var_subdiv_dtl(String mat_name, String mat_var_name, string mat_var_sub, [Optional]string unit_of_measurement, [Optional] Int64? prev_seq, [Optional]string user_info, [Optional]string revit_category, [Optional]string family_type, [Optional]string property, [Optional]string design_rule_factor, [Optional]string keywords, [Optional]string quantity_extraction_formula, [Optional]string specification, string city_name, string country_name, bool f_allCountry, bool f_allCity, bool f_singleCity, int f_all_elements, [Optional]string element_filter);       
        [OperationContract]
        string upd_mat_var_subdiv_dtl(String mat_name, String mat_var_name, string mat_var_sub, [Optional]string unit_of_measurement, [Optional] Int64? prev_seq, [Optional]string user_info, [Optional]string revit_category, [Optional]string family_type, [Optional]string property, [Optional]string design_rule_factor, [Optional]string keywords, [Optional]string quantity_extraction_formula, [Optional]string specification, string city_name, string country_name, bool f_allCountry, bool f_allCity, bool f_singleCity, int f_all_elements, [Optional]string element_filter,string sortingCondtitions);       
            
    }
    [DataContract]
    public class Mat_var_subdiv_dtl
    {
        [DataMember]
        public string mat_var_subdiv_name { get; set; }
        [DataMember]
        public string specification { get; set; }
        [DataMember]
        public string revit_category { get; set; }
        [DataMember]
        public string family_type { get; set; }
        [DataMember]
        public string property { get; set; }
        [DataMember]
        public string design_rule_factor { get; set; }
        [DataMember]
        public string keywords { get; set; }
        [DataMember]
        public string quantity_extraction_formula { get; set; }
        [DataMember]
        public int f_all_elements { get; set; }
        [DataMember]
        public string element_filter { get; set; }
    }
}
