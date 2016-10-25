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

namespace BrandCenter.Controllers
{
    public class AdminController : Controller
    {
        private BrandCenter.DAL.BrandCenterContext db = new BrandCenter.DAL.BrandCenterContext();
        private DefaultContext dbcnxt = new DefaultContext();
        // private GroupTest dbtest = new GroupTest();
        private DooSan.BrandCenter.BrandCenterDBConext.BrandCenterEntities dbEntities = new DooSan.BrandCenter.BrandCenterDBConext.BrandCenterEntities();

        // GET: Admin
        public ViewResult Group(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var students = from s in db.tblGroup
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString)
                                       || s.Descript.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "id_desc":
                    students = students.OrderByDescending(s => s.GroupId);
                    break;
                case "Name":
                    students = students.OrderBy(s => s.Name);
                    break;
                case "Name_desc":
                    students = students.OrderByDescending(s => s.Name);
                    break;
                default:  // id ascending 
                    students = students.OrderBy(s => s.GroupId);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            //jojo paging 샘플
            ViewBag.Page = pageNumber;

            return View(students.ToPagedList(pageNumber, pageSize));
        }

        public ViewResult GroupList(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var students = from s in dbcnxt.tblGroup
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString)
                                       || s.Descript.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "id_desc":
                    students = students.OrderByDescending(s => s.GroupId);
                    break;
                case "Name":
                    students = students.OrderBy(s => s.Name);
                    break;
                case "Name_desc":
                    students = students.OrderByDescending(s => s.Name);
                    break;
                default:  // id ascending 
                    students = students.OrderBy(s => s.GroupId);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            //jojo paging 샘플
            ViewBag.Page = pageNumber;

            return View(students.ToPagedList(pageNumber, pageSize));
        }



        public ViewResult GroupUserList(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var students = from s in dbcnxt.tblGroupUser
                           join gm in dbcnxt.tblGroup on s.GroupId equals gm.GroupId
                           select new ViewModels.GroupUser
                           {
                              GroupUserId = s.GroupUserId,
                               USERID = s.UserId,
                               NAME = gm.Name,
                               GROUPID = gm.GroupId
                           };
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.NAME.Contains(searchString) );
            }
            switch (sortOrder)
            {
                case "id_desc":
                    students = students.OrderByDescending(s => s.GROUPID);
                    break;
                case "Name":
                    students = students.OrderBy(s => s.NAME);
                    break;
                default:  // id ascending 
                    students = students.OrderBy(s => s.GROUPID);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            //jojo paging 샘플
            ViewBag.Page = pageNumber;

            return View(students.ToPagedList(pageNumber, pageSize));
        }

        //edmx 이용해서 해본건데 역시나 코드 퍼스트 구조에서 같이 돌릴려니 손이 겁나 많이 감. 자동 생성 클래스인걸
        //감안하면 OnModelCreating 을 매번 고치고 table클래스들에 key를 넣는건 번거로움. 아예 db first 구조로 새로 만드는게 나아 보이는데
        //validation 작업등을 새로 하는건 번거로움. 일단 이 방식은 edmx를 새로고치면서 동작하지 않음. 동작은 확인했었음
        public ViewResult GroupUserListEDMX(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var students = from s in dbEntities.tblGroupUser
                           join gm in dbEntities.tblGroup on s.GroupId equals gm.GroupId
                           select new ViewModels.GroupUser
                           {
                               GroupUserId = s.GroupUserId,
                               USERID = s.UserId,
                               NAME = gm.Name,
                               GROUPID = gm.GroupId
                           };
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.NAME.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "id_desc":
                    students = students.OrderByDescending(s => s.GROUPID);
                    break;
                case "Name":
                    students = students.OrderBy(s => s.NAME);
                    break;
                default:  // id ascending 
                    students = students.OrderBy(s => s.GROUPID);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            //jojo paging 샘플
            ViewBag.Page = pageNumber;

            return View(students.ToPagedList(pageNumber, pageSize));
        }

        public ViewResult CodeMasterListEDMXInherit(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var students = from s in dbcnxt.tblCodeMaster
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.NAME.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "id_desc":
                    students = students.OrderByDescending(s => s.LARG_DIVS);
                    break;
                case "Name":
                    students = students.OrderBy(s => s.NAME);
                    break;
                default:  // id ascending 
                    students = students.OrderBy(s => s.LARG_DIVS);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            //jojo paging 샘플
            ViewBag.Page = pageNumber;

            return View(students.ToPagedList(pageNumber, pageSize));
        }

        public ViewResult GroupUserListSP(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            var functionId = "%";
            //var result = db.Database.SqlQuery<GetFunctionByID>("GetFunctionByID @FunctionId", new SqlParameter("@FunctionId", functionId)).ToList());

            //var students = dbcnxt.Database.SqlQuery<ViewModels.GroupUser>("exec SP_ADM_sGROUPUSER @GROUPID ", "%").ToList<ViewModels.GroupUser>();
            //var students = dbcnxt.Database.SqlQuery<ViewModels.GroupUser>(" SP_ADM_sGROUPUSER @GROUPID ", new SqlParameter("@GROUPID", "%") ).ToList<ViewModels.GroupUser>();
            var students = dbcnxt.Database.SqlQuery<ViewModels.GroupUser>(" SP_ADM_sGROUPUSER @GROUPID ", new SqlParameter("@GROUPID", "%")).ToList();
            //페이징등이 필요없다면 괜찮지만 그렇지 않다면 QUERYABLE 이 실제 페이징이 되므로 (가져온 데이터가 아니라) 낫다. 

            return View(students);
        }



        


        public ViewResult GroupTest(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //ViewBag.CurrentSort = sortOrder;
            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            //if (searchString != null)
            //{
            //    page = 1;
            //}
            //else
            //{
            //    searchString = currentFilter;
            //}

            //ViewBag.CurrentFilter = searchString;

            //var students = from s in dbtest.tblGroup
            //               select s;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    students = students.Where(s => s.Name.Contains(searchString)
            //                           || s.Descript.Contains(searchString));
            //}
            //switch (sortOrder)
            //{
            //    case "id_desc":
            //        students = students.OrderByDescending(s => s.Id);
            //        break;
            //    case "Name":
            //        students = students.OrderBy(s => s.Name);
            //        break;
            //    case "Name_desc":
            //        students = students.OrderByDescending(s => s.Name);
            //        break;
            //    default:  // id ascending 
            //        students = students.OrderBy(s => s.Id);
            //        break;
            //}

            //int pageSize = 3;
            //int pageNumber = (page ?? 1);
            ////jojo paging 샘플
            //ViewBag.Page = pageNumber;

            //return View(students.ToPagedList(pageNumber, pageSize));
            return View();
        }
    }
}