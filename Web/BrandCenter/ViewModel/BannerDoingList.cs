using BrandCenter.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using FluentValidation.Attributes;
using System;
//using DataAnnotationsExtensions;
using Foolproof;
using System.Web.WebPages.Html;

namespace BrandCenter.ViewModels
{
    [NotMapped]
    public class BannerDoingList
    {
        public int? BannerId { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string StatusCode { get; set; }
        public string StatusName { get; set; }
        public string UseCode { get; set; }
        public string UseName { get; set; }
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public string SizeCode { get; set; }
        public string RegisterDate { get; set; }
        //public Nullable<System.DateTime> RegisterDateTime { get; set; }
        //public string BannerLocation { get; set; }
        //public Nullable<int> BannerCount { get; set; }
        //public string ApproveRequestDate { get; set; }
        //public string ApproveRequestTime { get; set; }
        //public Nullable<bool> IsEmergency { get; set; }
        //public Nullable<decimal> ApprovalAmt { get; set; }
        //public string ApprovalComment { get; set; }
        //public string PrsHeadLine1 { get; set; }
        //public string PrsHeadLine2 { get; set; }
        //public string PrsHeader { get; set; }
        //public string PrsFooter { get; set; }

    }

 
}