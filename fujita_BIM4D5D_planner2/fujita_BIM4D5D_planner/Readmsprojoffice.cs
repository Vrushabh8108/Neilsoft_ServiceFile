﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService7" in both code and config file together.
    [ServiceContract]
    public interface Readmsprojoffice
    {
        [OperationContract]
        msproj_ver_dtl showmsprojdetail(string proj_id, string proj_name, Int64 proj_ver, Int64 version);
        [OperationContract]
        Int64 showmaxversion(string proj_id, string proj_name, int proj_version);
        [OperationContract]
        void projrevreject(string proj_id, string proj_name, Int64 version, Int16 version_status, Int64 proj_ver);
        [OperationContract]
        Int64 Put(List<msproj_dtl> msproj_dtl11, string proj_id, string proj_name, bool? saveonly, Int64 proj_version, bool f_save);
        //Int64 Put(List<msproj_dtl> msproj_dtl11, string proj_id, string proj_name, bool? saveonly, Int64 proj_version);
        //[OperationContract]
        //Int64 Put(List<msproj_dtl> msproj_dtl11, string proj_id, string proj_name, bool? saveonly, Int64 proj_version, Int64 cur_version);
        [OperationContract]
        Int16 projbaseversion(string proj_id, string proj_name, Int64 version, Int64 proj_ver, Int64 base_flag);
        [OperationContract]
        Int16 projbasecreate(string proj_id, string proj_name, Int64 version, Int64 proj_ver, Int64 basecreate_flag);
        [OperationContract]
        Int16 projbaseupd(string proj_id, string proj_name, Int64 version, Int64 proj_ver, Int64 baseupd_flag);
        [OperationContract]
        Int16 projlock(string proj_id, string proj_name, Int64 version, Int64 proj_ver, Int64 lock_flag);
    }
    [DataContract]
    public class msproj_dtl
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int? seq { get; set; }
        [DataMember]
        public string level { get; set; }
        [DataMember]
        public DateTime start_date { get; set; }
        [DataMember]
        public DateTime end_date { get; set; }
        [DataMember]
        public int? duration { get; set; }
        [DataMember]
        public DateTime plan_start_date { get; set; }
        [DataMember]
        public DateTime plan_end_date { get; set; }
        [DataMember]
        public int? plan_duration { get; set; }
        [DataMember]
        public int? actual_dur { get; set; }
        [DataMember]
        public int? progress { get; set; }
        [DataMember]
        public string successor_relation_type { get; set; }
        [DataMember]
        public string predecessor_relation_type { get; set; }
        [DataMember]
        public decimal? cost { get; set; }
        [DataMember]
        public List<string> resource { get; set; }
        [DataMember]
        public List<int?> Revit_elements { get; set; }
        [DataMember]
        public List<int?> predecessor { get; set; }
        [DataMember]
        public List<int?> successor { get; set; }

    }
    [DataContract]
    public class msproj_ver_dtl
    {
        [DataMember]
        public Int64 f_base_version { get; set; }
        [DataMember]
        public Int64 f_locked { get; set; }
        [DataMember]
        public Int64 f_baseversion_created { get; set; }
        [DataMember]
        public Int64 f_baseversion_updated { get; set; }
        [DataMember]
        public List<msproj_dtl> msproj_dtl1 { get; set; }
    }
    }
