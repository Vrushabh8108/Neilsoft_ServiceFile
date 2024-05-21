using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService22" in both code and config file together.
    [ServiceContract]
    public interface DesignRule
    {
        [OperationContract]
        List<design_rule_country> Getdesignrule();
        [OperationContract]
        List<design_rule> Getdesignrulecountry(string Country, string city);
        [OperationContract]
        int updatedesignrule([Optional]string city, [Optional]string country, design_rule design_rule, string created_by);
        [OperationContract]
        List<design_rule_country_project> Getdesignrule_Project(string country, string city, string proj_id, string proj_name);
        [OperationContract]
        int updatedesignrule_Project([Optional]string city, [Optional]string country, design_rule design_rule, string floor_name, string proj_id, string proj_name, string created_by);
        [OperationContract]
        string InsertFloor(string floorname, string copy_from_floor, string proj_id, string proj_name, string city, string country);
        [OperationContract]
        List<string> GetExistingFloors(string proj_id, string proj_name);
        [OperationContract]
        void Updateallfloors_Project();
        [OperationContract]
        void Updateallfloors_city();
        [OperationContract]
        void CopyRebarIndices(string country, string city, string proj_id, string proj_name);
        [OperationContract]
        void UpdateONEfloors_Project();

        //------Method Added by Vrushabh-----------------------
        [OperationContract]
        List<design_rule_country_project> Getdesignrule_Project_Test(string country, string city, string proj_id, string proj_name);
    }
    [DataContract]
    public class design_rule_country
    {
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public List<design_rule_city> design_rule_city { get; set; }
    }
    [DataContract]
    public class design_rule_country_project
    {
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public List<design_rule_city_project> design_rule_city { get; set; }
    }
    [DataContract]
    public class design_rule_city
    {
        [DataMember]
        public string city { get; set; }
        [DataMember]
        //public List<design_rule_floor> design_rule_floor_data { get; set; }
        public List<design_rule> design_rule { get; set; }
    }
    [DataContract]
    public class design_rule_city_project
    {
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public List<design_rule_project> design_rule_proj { get; set; }
    }

    [DataContract]
    public class design_rule_floor
    {
        [DataMember]
        public string floorname { get; set; }
        [DataMember]
        public List<design_rule> design_rule { get; set; }
    }

    [DataContract]
    public class design_rule_project
    {
        [DataMember]
        public string project_name { get; set; }
        [DataMember]
        public string project_id { get; set; }
        [DataMember]
        public List<design_rule_project_floor> design_rule_proj_floor { get; set; }
    }

    [DataContract]
    public class design_rule_project_floor
    {
        [DataMember]
        public string proj_floorname { get; set; }
        [DataMember]
        public List<design_rule> design_rule { get; set; }

    }

    [DataContract]
    public class design_rule
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string unit_of_measurement { get; set; }
        [DataMember]
        public decimal? value { get; set; }
        [DataMember]
        public string formula { get; set; }
        [DataMember]
        public int isRebar { get; set; }
    }

    [DataContract]
    public class desig_Rule_project
    {
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string Project { get; set; }
        [DataMember]
        public string Project_Id { get; set; }
        [DataMember]
        public string Floor { get; set; }
        [DataMember]
        public string floor_id { get; set; }
        [DataMember]
        public string DesignRuleName { get; set; }
        [DataMember]
        public string UnitOfMeasurement { get; set; }
        [DataMember]
        public decimal? Value { get; set; }
        [DataMember]
        public string Formula { get; set; }
        [DataMember]
        public int IsRebar { get; set; }

    }

}
