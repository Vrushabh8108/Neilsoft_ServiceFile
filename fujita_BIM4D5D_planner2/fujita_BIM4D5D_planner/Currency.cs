using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService19" in both code and config file together.
    [ServiceContract]
    public interface Currency
    {
        [OperationContract]
        List<currency_dtl> Getcurrency();
    }
    [DataContract]
    public class currency_dtl
    {
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public string currency { get; set; }
        [DataMember]
        public string basecurrency { get; set; }
        [DataMember]
        public decimal? rate { get; set; }
    }
    [DataContract]
    public class currency
    {
        [DataMember]
        public currency_rate rates { get; set; }
        [DataMember]
        public string @base {get;set; }
        [DataMember]
        public DateTime date { get; set; } 

    }
    [DataContract]
    public class currency_rate
    {
        [DataMember]
        public decimal CAD { get; set; }
        [DataMember]
        public decimal HKD { get; set; }
        [DataMember]
        public decimal ISK { get; set; }
        [DataMember]
        public decimal PHP { get; set; }
        [DataMember]
        public decimal DKK { get; set; }
        [DataMember]
        public decimal HUF { get; set; }
        [DataMember]
        public decimal CZK { get; set; }
        [DataMember]
        public decimal GBP { get; set; }
        [DataMember]
        public decimal RON { get; set; }
        [DataMember]
        public decimal SEK { get; set; }
        [DataMember]
        public decimal IDR { get; set; }
        [DataMember]
        public decimal INR { get; set; }
        [DataMember]
        public decimal BRL { get; set; }
        [DataMember]
        public decimal RUB { get; set; }
        [DataMember]
        public decimal HRK { get; set; }
        [DataMember]
        public decimal JPY { get; set; }
        [DataMember]
        public decimal THB { get; set; }
        [DataMember]
        public decimal CHF { get; set; }
        [DataMember]
        public decimal EUR { get; set; }
        [DataMember]
        public decimal MYR { get; set; }
        [DataMember]
        public decimal BGN { get; set; }
        [DataMember]
        public decimal TRY { get; set; }
        [DataMember]
        public decimal CNY { get; set; }
        [DataMember]
        public decimal NOK { get; set; }
        [DataMember]
        public decimal NZD { get; set; }
        [DataMember]
        public decimal ZAR { get; set; }
        [DataMember]
        public decimal USD { get; set; }
        [DataMember]
        public decimal MXN { get; set; }
        [DataMember]
        public decimal SGD { get; set; }
        [DataMember]
        public decimal AUD { get; set; }
        [DataMember]
        public decimal ILS { get; set; }
        [DataMember]
        public decimal KRW { get; set; }
        [DataMember]
        public decimal PLN { get; set; }

    }
    }
