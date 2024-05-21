using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService9" in both code and config file together.
    [ServiceContract]
    public interface RetrieveCost
    {
        [OperationContract]
        List<cost_dtl> readcostdtl(string proj_id, string proj_name, int proj_version);
        [OperationContract]
        List<cost_dtl_element> readcostdtlprojrevitelement(string proj_id, string proj_name, int proj_version);
        [OperationContract]
        List<cost_dtl_element> readcostdtlprojelement(string proj_id, string proj_name, List<Int64> element_id, int proj_version);
    }
    [DataContract]
    public class cost_dtl
    {
        [DataMember]
        public Int64? revit_element_type_id { get; set; }
        [DataMember]
        public Int64? revit_element_id { get; set; }
        [DataMember]
        public string ElementName { get; set; }
        [DataMember]
        public decimal? ElementVolume { get; set; }
        [DataMember]
        public int? f_virtual { get; set; }
        [DataMember]
        public bool? IsOutsourcing { get; set; }
        [DataMember]
        public bool? IsMaterial1 { get; set; }
        [DataMember]
        public bool? IsMaterial2 { get; set; }
        [DataMember]
        public bool? IsLabor1 { get; set; }
        [DataMember]
        public bool? IsLabor2 { get; set; }
        [DataMember]
        public bool? IsOther { get; set; }
        [DataMember]
        public decimal? OutsourcingCost { get; set; }
        [DataMember]
        public decimal? Material1Cost { get; set; }
        [DataMember]
        public decimal? Material2Cost { get; set; }
        [DataMember]
        public decimal? Labor1Cost { get; set; }
        [DataMember]
        public decimal? Labor2Cost { get; set; }
        [DataMember]
        public decimal? OtherCost { get; set; }
        [DataMember]
        public string Specifications { get; set; }
        [DataMember]
        public string Job { get; set; }
        [DataMember]
        public string Variance { get; set; }
        [DataMember]
        public string SubVariance { get; set; }
        [DataMember]
        public decimal? Quantity { get; set; }
        [DataMember]
        public string Unit { get; set; }
        [DataMember]
        public decimal? UnitCost { get; set; }
        [DataMember]
        public decimal? TotalCost { get; set; }
        [DataMember]
        public decimal? CalculatedCost { get; set; }
        [DataMember]
        public decimal? AssignedCost { get; set; }
        [DataMember]
        public bool? IsFinishCostApplied { get; set; }
        [DataMember]
        public decimal? FinishArea { get; set; }
        [DataMember]
        public decimal? FinishAreaCost { get; set; }
        [DataMember]
        public string ConcreteGradeDetails { get; set; }
        [DataMember]
        public string ConcreteGradeTotalCost { get; set; }
        [DataMember]
        public string ConcreteGradeAssignedCost { get; set; }
        [DataMember]
        public bool? isedited { get; set; }
        [DataMember]
        public decimal? UserInputQuantity { get; set; }
        [DataMember]
        public string ConcreteGradeUserInputQuantity { get; set; }
        [DataMember]
        public string ConcreteGradeIsQuantityEdited { get; set; }
        //[DataMember]
        //public string SubvariaceConcreteGrade { get; set; }
    }
    [DataContract]
    public class cost_dtl_element
    {
        [DataMember]
        public Int64? revit_element_type_id { get; set; }
        [DataMember]
        public Int64? revit_element_id { get; set; }
        [DataMember]
        public string ElementName { get; set; }
        [DataMember]
        public decimal? ElementVolume { get; set; }
        [DataMember]
        public decimal? Quantity { get; set; }
        [DataMember]
        public string Unit { get; set; }
        [DataMember]
        public decimal? UnitCost { get; set; }
        [DataMember]
        public decimal? ConcreteCost { get; set; }
        [DataMember]
        public decimal? RebarCost { get; set; }
        [DataMember]
        public decimal? FormWorkCost { get; set; }
        [DataMember]
        public decimal? TotalCost { get; set; }
        [DataMember]
        public decimal? CalculatedCost { get; set; }
        [DataMember]
        public decimal? AssignedCost { get; set; }
        [DataMember]
        public bool? IsFinishCostApplied { get; set; }
        [DataMember]
        public decimal? FinishArea { get; set; }
        [DataMember]
        public decimal? FinishAreaCost { get; set; }

    }
}
