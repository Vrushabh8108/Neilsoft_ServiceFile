using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService18" in both code and config file together.
    [ServiceContract]
    public interface Addcountry
    {
        [OperationContract]
        List<country_dtl> FindCountry();
        [OperationContract]
        int ins_country(country_dtl country_dtl, string to_city, string from_country,string from_city);
        [OperationContract]
        List<country_dtl_lst> FindCountryList();
    }
    [DataContract]
    public class country_dtl
    {
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public string currency { get; set; }
        [DataMember]
        public string code { get; set; }
    }
    [DataContract]
    public class country_dtl_lst
    {
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public string currency { get; set; }
    }
}
