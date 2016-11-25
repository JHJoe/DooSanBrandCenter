using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BrandCenter.DAL;
using BrandCenter.Models;
using PagedList;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using BrandCenter.ViewModels;
using DooSan.BrandCenter.FrameWork.Utils;
using System.Data.Entity.Validation;
using BrandCenter.Helper;
using DooSan.BrandCenter.FrameWork.DbContextFactory;
using DooSan.BrandCenter.FrameWork.Static;

namespace BrandCenter.Controllers
{
    public class BannerController : Base.BaseController
    {
        public BannerController(DefaultContext db) : base(db)
        {
        }

        // GET: Banner
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult CreateBanner(string RequestType = "Create", int? EditId = null)
        {
            Banner model = new Banner();

            EditType editType;
            if (Enum.TryParse<EditType>(RequestType, out editType) == false)
                editType = EditType.Create;

            if (!RequestType.Equals("Create") && EditId != null)
            {
                var bannerQuery = from s in _db.tblBanner
                                where s.BannerId == EditId 
                            select new Banner
                            {
                                    BannerId = s.BannerId,
                                    Title = s.Title,
                                    RegisterDate = s.RegisterDate,
                                    StatusCode = s.StatusCode
                                };

                model = bannerQuery.Single(); //FirstOrDefault

            }


            model.editType = editType; // EditType.Create;


            //Dictionary<string, string> list = new Dictionary<string, string>();
            //list.Add("", "선택해주세요");

            //var codedics1 = CommonHelper.GetCodeDetailsToDictionary(_db, "14");
            //list = list.Concat(codedics1).ToDictionary(x => x.Key, x => x.Value);

            //model.UseCode = "02";

            model.RegionSelection = DropDownHelper.GetCodeMasterDropDownList(_db, "14", model.BannerMaker, Message.GetMessage("SelectText") );  //new SelectList(list, "Key", "Value", model.RegionCode); ;
            model.BannerMakerSelection = DropDownHelper.GetBlankDropDownList(); // new SelectList(list, "Key", "Value", model.RegionCode); ;
            model.UseSelection = CommonHelper.GetCodeDetailsList(_db, "03");
            model.TypeSelection = CommonHelper.GetCodeDetailsList(_db, "04");
            model.ColorSelection = CommonHelper.GetCodeDetailsList(_db, "15");

            model.FinishingWaySelection = DropDownHelper.GetCodeMasterDropDownList(_db, "09", model.MaterialCode, Message.GetMessage("SelectText"));
            model.MaterialSelection = DropDownHelper.GetCodeMasterDropDownList(_db, "10", model.MaterialCode, Message.GetMessage("SelectText"));
            model.SetUpSelection = DropDownHelper.GetCodeMasterDropDownList(_db, "11", model.MaterialCode, Message.GetMessage("SelectText"));
            model.DeliveryTypeSelection = DropDownHelper.GetCodeMasterDropDownList(_db, "12", model.MaterialCode, Message.GetMessage("SelectText"));


            return View(model);
        }

        //제작 신청 목록           
        public ViewResult BannerRequestList(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.Title = "CI 제작물 신청";

            sortOrder = string.IsNullOrEmpty(sortOrder) ? "" : sortOrder;

            ViewBag.CurrentSort = sortOrder;
            if (!string.IsNullOrEmpty(sortOrder))
//                ViewBag.SortArrow = "";
//            else
                ViewBag.SortArrow = !string.IsNullOrEmpty(sortOrder) && sortOrder.IndexOf("desc")  > 0 ? Constants.AscendingArrow : Constants.DescendingArrow;

            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "Date_desc" : "";
            ViewBag.NameSortParm = sortOrder == "Title" ? "Title_desc" : "Title";

            #region Count Filter
            ViewBag.RequestCount = _db.tblBanner.Count(me => me.StatusCode == "01" ).ToString() ;
            ViewBag.ArrovalCount = _db.tblBanner.Count(me => me.StatusCode == "04" || me.StatusCode == "02" || me.StatusCode == "03").ToString();

            var statuss = _db.tblBanner.Where(u => new[] { "05", "06", "07", "08", "09", "10", "11" }.Contains(u.StatusCode));
            
            ViewBag.ProductionCount = statuss.Count().ToString();



            ViewBag.SubmittedCount = _db.tblBanner.Count(me => me.StatusCode == "02").ToString();
            ViewBag.WithdrawnCount = _db.tblBanner.Count(me => me.StatusCode == "03").ToString();
            ViewBag.ReturnedCount = _db.tblBanner.Count(me => me.StatusCode == "04").ToString();

            ViewBag.Approved = _db.tblBanner.Count(me => me.StatusCode == "05").ToString();
            ViewBag.ProductionRequested = _db.tblBanner.Count(me => me.StatusCode == "06").ToString();
            ViewBag.Productionprogress       = _db.tblBanner.Count(me => me.StatusCode == "07").ToString();
            ViewBag.ReviewRequested = _db.tblBanner.Count(me => me.StatusCode == "08").ToString();
            ViewBag.Canceled = _db.tblBanner.Count(me => me.StatusCode == "09").ToString();
            ViewBag.Requested = _db.tblBanner.Count(me => me.StatusCode == "10").ToString();
            ViewBag.Reviewed = _db.tblBanner.Count(me => me.StatusCode == "11").ToString();

            //to do 매핑 테이블에서 로딩해서 항목 가져오는거 검토.
            //ViewData["StatusList"] = 
            //            @foreach(var item in ViewData["PhotoList"] as IEnumerable<TestingMVCQA.Models.Photos>)
            //{
            //                //@item.FileName //instead .FileName use property which you have used in your model class
            //            }△	▲	▽	▼
            #endregion

            var codedics = CommonHelper.GetCodeDetailsToDictionary(_db, "01");
            ViewData["StatusDic"] = codedics;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var viewmodel = from s in _db.tblBanner
                            join us in _db.tblUser on s.UserId equals us.LoginID
                            join gm in _db.tblCodeMaster on s.StatusCode equals gm.SMLL_DIVS where gm.LARG_DIVS == "01"
                            join gm2 in _db.tblCodeMaster on s.UseCode equals gm2.SMLL_DIVS where gm2.LARG_DIVS == "03" //용도
                            join gm3 in _db.tblCodeMaster on s.TypeCode equals gm3.SMLL_DIVS  where gm3.LARG_DIVS == "04" //유형

                            select new ViewModels.BannerDoingList
                            {
                                BannerId = s.BannerId,
                                Title = s.Title,
                                UserId = s.UserId,
                                UserName = us.DisplayName,
                                StatusName = gm.NAME,
                                UseName = gm2.NAME,
                                TypeName = gm3.NAME,
                                RegisterDate = s.RegisterDate,
                                StatusCode = s.StatusCode
                            };
                              //          .Join(_db.tblCodeMaster,
                              //sc => sc.StatusCode,
                              //soc => soc.SMLL_DIVS,
                              //(sc, soc) => new ViewModels.GroupUser
                              //{
                              //    SomeClass = sc,
                              //    SomeOtherClass = soc
                              //})
                              //;


            if (!String.IsNullOrEmpty(searchString))
            {
                switch (searchString)
                {
                    case "234":
                        viewmodel = viewmodel.Where(u => new[] { "02", "03", "04"}.Contains(u.StatusCode));
                        break;
                    case "5678910111":
                        viewmodel = viewmodel.Where(u => new[] { "05", "06", "07", "08", "09", "10", "11" }.Contains(u.StatusCode));
                        break;
                    default:
                        viewmodel = viewmodel.Where(s => s.StatusCode.Equals(searchString));
                        break;
                }
            }
            switch (sortOrder)
            {
                case "Title":
                    viewmodel = viewmodel.OrderBy(s => s.Title);
                    break;
                case "Title_desc":
                    viewmodel = viewmodel.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    viewmodel = viewmodel.OrderBy(s => s.RegisterDate);
                    break;
                case "Date_desc":
                    viewmodel = viewmodel.OrderByDescending(s => s.RegisterDate);
                    break;
                default:  // id desc
                    viewmodel = viewmodel.OrderByDescending(s => s.RegisterDate);
                    //viewmodel = viewmodel.OrderByDescending(s => s.BannerId);
                    break;
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);

            ViewBag.Page = pageNumber;

            return View(viewmodel.ToPagedList(pageNumber, pageSize));
        }


    }
}