
namespace BrandCenter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    [Table("tb_User")]
    public class tblUser
    {
        public string EmpID { get; set; }
        public string CurrentEmpID { get; set; }
        [Key]
        public string LoginID { get; set; }
        public string EMail { get; set; }
        public string CompanyCode { get; set; }
        public string MainDeptCode { get; set; }
        public string CreatedDT { get; set; }
        public string EndDate { get; set; }
        public string DisplayName { get; set; }
        public string DisplayYN { get; set; }
        public string DutyCode { get; set; }
        public string JobCode { get; set; }
        public string RankCode { get; set; }
        public string FaxNumber { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string ExtensionNumber { get; set; }
        public string Sort { get; set; }
        public string Interface { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string ObjectDetailCategory { get; set; }
        public string ObjectCategory { get; set; }
        public string Person_ID { get; set; }
        public string ExtensionNumber2 { get; set; }
        public string DooDreamYN { get; set; }
        public string etcUser { get; set; }
        public string classify { get; set; }
        public string JOB_FULL_NM { get; set; }
        public string EMP_TYPE { get; set; }
        public string MyLang { get; set; }
        public string Location { get; set; }
        public string JOB_DETAIL { get; set; }
        public string HIDE_YellowPage_YN { get; set; }
        public string DOC_CHECK_YN { get; set; }
        public Nullable<int> Ver { get; set; }
        public string PersonnelAreaCode { get; set; }
        public string OrgUnitID { get; set; }
        public string DoopisDeptCode { get; set; }
    }
}