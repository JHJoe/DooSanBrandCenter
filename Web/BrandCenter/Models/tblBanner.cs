using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrandCenter.Models
{
    public class tblBanner
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int? BannerId { get; set; }
        

        //public string RegionCode { get; set; }
        //public string ActiveDateFr { get; set; }
        //public Nullable<System.DateTime> ActiveDateTimeFr { get; set; }
        //public string ActiveDateTo { get; set; }
        //public Nullable<System.DateTime> ActiveDateTimeTo { get; set; }
        //public string BannerMaker { get; set; }

        public string Title { get; set; }
        public string UserId { get; set; }
        public string StatusCode { get; set; }
        public string RegionCode { get; set; }
        public string BannerMaker { get; set; }
        public string UseCode { get; set; }
        public string TypeCode { get; set; }
        public string SizeCode { get; set; }
        public string ColorCode { get; set; }
        public Nullable<int> Height { get; set; }
        public Nullable<int> Width { get; set; }

        public string ActiveDateFr { get; set; }
        public Nullable<System.DateTime> ActiveDateTimeFr { get; set; }
        public string ActiveDateTo { get; set; }
        public Nullable<System.DateTime> ActiveDateTimeTo { get; set; }

        public string RegisterDate { get; set; }
        public Nullable<System.DateTime> RegisterDateTime { get; set; }
        public string FinishingWayCode { get; set; }
        public string MaterialCode { get; set; }
        public string SetUpCode { get; set; }
        public string DeliveryTypeCode { get; set; }
        public string DeliveryLocation { get; set; }
        public string BannerLocation { get; set; }
        public Nullable<int> BannerCount { get; set; }
        public string ApproveRequestDate { get; set; }
        public string ApproveRequestTime { get; set; }
        public Nullable<bool> IsEmergency { get; set; }
        public Nullable<decimal> ApprovalAmt { get; set; }
        public string ApprovalComment { get; set; }
        public string PrsHeader { get; set; }
        public string PrsHeadLine1 { get; set; }
        public string PrsHeadLine2 { get; set; }
        public string PrsFooter { get; set; }
        public string SetUpLocation { get; set; }
        public string RequestComment { get; set; }
        public string INPUT_USER { get; set; }
        public Nullable<System.DateTime> INPUT_DATE { get; set; }
        public string UPDATE_USER { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
    }
}