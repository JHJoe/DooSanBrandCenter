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
using System.Web.Mvc;
using BrandCenter.Helper;
using DooSan.BrandCenter.FrameWork.Static;

namespace BrandCenter.ViewModels
{
    [NotMapped]
    public class Banner : tblBanner
    {
        //{
        //    get
        //    {
        //        CommonHelper.GetCodeDetailsToDictionary()
        //        list.Add("", Constants.SelectionSelect);

        //        return new SelectList(EwDAO.GetCommissionRateList(this.ManufacturerId, (this.IsNewMode ? "NEW" : this.CommissionRateId)), "Value", "Text", this.CommissionRateId);
        //    }
        //}

        public SelectList StatusSelection { get; set; }
        public SelectList RegionSelection { get; set; }
        public SelectList BannerMakerSelection { get; set; }
        public List<CodeItem> UseSelection { get; set; }
        public List<CodeItem> TypeSelection { get; set; }
        public SelectList SizeSelection { get; set; }
        public List<CodeItem> ColorSelection { get; set; }
        public SelectList FinishingWaySelection { get; set; }
        public SelectList MaterialSelection { get; set; }
        public SelectList SetUpSelection { get; set; }
        public SelectList DeliveryTypeSelection { get; set; }

        public string StatusText { get; set; }
        public string RegionText { get; set; }
        public string BannerMakerText { get; set; }
        public string UseText { get; set; }
        public string TypeText { get; set; }
        public string SizeText { get; set; }
        public string ColorText { get; set; }
        public string FinishingWayText { get; set; }
        public string MaterialText { get; set; }
        public string SetUpText { get; set; }
        public string DeliveryTypeText { get; set; }

        public EditType editType { get; set; }
    }

 
}