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

namespace BrandCenter.Controllers
{
    public class AdminController : Controller
    {
        private BrandCenter.DAL.BrandCenterContext db = new BrandCenter.DAL.BrandCenterContext();
        private DefaultContext dbcnxt = new DefaultContext();
        // private GroupTest dbtest = new GroupTest();
        //상속샘플
            //private DooSan.BrandCenter.BrandCenterDBConext.BrandCenterEntities dbEntities = new DooSan.BrandCenter.BrandCenterDBConext.BrandCenterEntities();

        #region 조회샘플
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

            var group = from s in dbcnxt.tblGroup
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                group = group.Where(s => s.Name.Contains(searchString)
                                       || s.Descript.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "id_desc":
                    group = group.OrderByDescending(s => s.GroupId);
                    break;
                case "Name":
                    group = group.OrderBy(s => s.Name);
                    break;
                case "Name_desc":
                    group = group.OrderByDescending(s => s.Name);
                    break;
                default:  // id ascending 
                    group = group.OrderBy(s => s.GroupId);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            //jojo paging 샘플
            ViewBag.Page = pageNumber;

            return View(group.ToPagedList(pageNumber, pageSize));
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
            Convert.ToInt32("d");

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
        /*
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
        */
        //상속샘플 - 지금은 참조 제거하고 따로 선언해서 만듬
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

        #endregion

        #region Edit Sample

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = dbcnxt.tblGroup.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Admin/GroupCreate
        public ActionResult GroupCreate()
        {
            return RedirectToAction("GroupEdit",
                        new { id = (int?)null, isCreate  = true });
        }


        // GET: Admin/GroupEdit/5
        public ActionResult GroupEdit(int? id, bool isCreate = true)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            if (isCreate)
            {
                //ViewBag.Title = "Group Create";
                return View();
            }

            ViewBag.IsCreate = isCreate;

            var student = dbcnxt.tblGroup.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Group/GroupEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
//        public ActionResult GroupEdit([Bind(Include = "LastName, FirstMidName, EnrollmentDate")]tblGroup group)
        public ActionResult GroupEdit(tblGroup group)
        {
            try
            {
                ViewBag.IsCreate = group.GroupId == null ? true: false;

                //수동 오류
                if (group.Name == "error")
                {
                    ModelState.AddModelError("Name", "이름이 error면 에러 발생.");
                }


                if (ModelState.IsValid)
                {
                    using (var tran = dbcnxt.Database.BeginTransaction())
                    {
                        try
                        {

                            if (ViewBag.IsCreate)
                                dbcnxt.tblGroup.Add(group);
                            else
                            {
                                //복잡한건 아래처럼 하고 
                                UpdateGroupModel(group);
                                //일반적인건 아래처럼 처리
                                //var groupToUpdate = dbcnxt.tblGroup.Find(group.GroupId);
                                //TryUpdateModel(groupToUpdate, "", new string[] { "Name", "Descript" });
                            }

                            dbcnxt.SaveChanges();
                            tran.Commit();

                            //시스템적으로 에러를 낼 경우 아래와 같이 지정
                            //ModelState.AddModelError("", "가상에러");
                            //return View(group);

                            return RedirectToAction("Group");
                            //return Json(new { Success = true, SuccessUrl = Url.Action("Edit", "PDI_L2H", new { id = entity.L2H_LEVELTWOHEADERID }) });
                        }
                        catch (Exception e)
                        {
                            //Console.WriteLine(e.ToString());
                            tran.Rollback();

                            //return Json(new { Success = false });
                        }
                    }
                }
                else
                {
                    var test = ModelState.Where(x => x.Value.Errors.Any());
                }



            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(group);
        }

        private void UpdateGroupModel(tblGroup group)
        {
            var groupToUpdate = dbcnxt.tblGroup.Find(group.GroupId);
            groupToUpdate.Name = group.Name;
            groupToUpdate.Descript = group.Descript;

        }

        // POST: Group/GroupDelete?id=5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GroupDelete(int id)
        {
            try
            {
                var group = dbcnxt.tblGroup.Find(id);
                dbcnxt.tblGroup.Remove(group);
                dbcnxt.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                //Log the error 
                ModelState.AddModelError("", ex.Message);

                return Json(new { Result = "Fail", ReturnMessage =  ex.Message});
            }
        }






        #endregion


        #region Edit Sample GroupUser

        public ActionResult GroupUserDetails(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var groupuser = from s in dbcnxt.tblGroupUser
                           join gm in dbcnxt.tblGroup on s.GroupId equals gm.GroupId
                           where s.GroupUserId == id
                           select new ViewModels.GroupUser
                           {
                               GroupUserId = s.GroupUserId,
                               USERID = s.UserId,
                               NAME = gm.Name,
                               GROUPID = gm.GroupId
                           };

            var viewmodel = groupuser.Single();

            //var student = dbcnxt.tblGroupUser.Find(id);
            if (viewmodel == null)
            {
                return HttpNotFound();
            }
            return View(viewmodel);
        }

        // GET: Admin/GroupCreate
        public ActionResult GroupUserCreate()
        {
            return RedirectToAction("GroupUserEdit",
                        new { id = (int?)null, isCreate = true });
        }


        // GET: Admin/GroupUserEdit/5
        public ActionResult GroupUserEdit(int? id, bool isCreate = true)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            if (isCreate)
            {
                //ViewBag.Title = "GroupUser Create";
                return View();
            }

            ViewBag.IsCreate = isCreate;

            var groupuserQuery = from s in dbcnxt.tblGroupUser
                            join gm in dbcnxt.tblGroup on s.GroupId equals gm.GroupId
                            where s.GroupUserId == id
                            select new ViewModels.GroupUser
                            {
                                GroupUserId = s.GroupUserId,
                                USERID = s.UserId,
                                NAME = gm.Name,
                                GROUPID = gm.GroupId,
                                JobId = "06"  //콤보 샘플이라서
                            };
            var groupuser = groupuserQuery.Single(); //FirstOrDefault

            if (groupuser == null)
            {
                return HttpNotFound();
            }

            ViewBag.JOBSelectList = GetCodeMasterDropDownList(dbcnxt, "10");

            return View(groupuser);
        }
        public static SelectList GetCodeMasterDropDownList(DefaultContext db, string largeDiv, object selected = null)
        {
            var countryQuery = from c in db.tblCodeMaster
                               where c.LARG_DIVS == largeDiv
                               orderby c.SMLL_DIVS, c.SMLL_DIVS
                               select c;

            return new SelectList(countryQuery, "SMLL_DIVS", "NAME", selected);
        }

        // POST: Group/GroupUserEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        //        public ActionResult GroupUserEdit([Bind(Include = "LastName, FirstMidName, EnrollmentDate")]tblGroupUser group)
        public ActionResult GroupUserEdit(GroupUser group)
        {
            try
            {
                ViewBag.IsCreate = group.GroupUserId == null ? true : false;

                //수동 오류
                if (group.GroupUserId == 6666)
                {
                    ModelState.AddModelError("GroupId", "이름이 6666  에러 발생.");
                }

                if (ModelState.IsValid)
                {
                    using (var tran = dbcnxt.Database.BeginTransaction())
                    {
                        try
                        {
                            if (ViewBag.IsCreate)
                            {
                                //var newtblGroupUser = new tblGroupUser() { GroupUserId = group.GroupUserId, GroupId = group.GROUPID, UserId = group.USERID };
                                //혹은 아래 유틸을 쓰면 한방에 복사가 된다.         
                                var newtblGroupUser = ReflectionUtil.CopyProperties<GroupUser, tblGroupUser>(group);

                                
                                dbcnxt.tblGroupUser.Add(newtblGroupUser);
                            }
                            else
                            {
                                //복잡한건 아래처럼 하고 
                                UpdateGroupUserModel(group);
                                //일반적인건 아래처럼 처리
                                //var groupToUpdate = dbcnxt.tblGroupUser.Find(group.GroupUserId);
                                //TryUpdateModel(groupToUpdate, "", new string[] { "GroupId", "UserId" });
                            }

                            dbcnxt.SaveChanges();
                            tran.Commit();

                            return RedirectToAction("GroupUserList");
                            //return Json(new { Success = true, SuccessUrl = Url.Action("Edit", "PDI_L2H", new { id = entity.L2H_LEVELTWOHEADERID }) });
                        }
                        catch (DbEntityValidationException e)
                        {
                            //Console.WriteLine(e.ToString());
                            tran.Rollback();


                            //아래부분은 base controller에 들어갈 부분
                            //AddModelError 을 여러번 호출하면 여러 라인이 나온다. * 찍혀서
                            //ui에 걸린 에러가 submit 단계에서 출력된다.
                            //ModelState.AddModelError("", e.Message);
                            //string multiEntityError = string.Empty;
                            foreach (var eve in e.EntityValidationErrors)
                            {

                                // 이부분은 주석 처리 할 수도 있다.
                                ModelState.AddModelError("", string.Format("시스템 에러 메세지 Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                                    eve.Entry.Entity.GetType().Name, eve.Entry.State));
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    ModelState.AddModelError(ve.PropertyName, ve.ErrorMessage);
                                    //ModelState.AddModelError("", string.Format("- Property: \"{0}\", Error: \"{1}\"",
                                    //    ve.PropertyName, ve.ErrorMessage));
                                }
                            }
                            //                            throw;

                            return View(group);

                            //return Json(new { Success = false });
                        }
                    }
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach(var error in errors)
                    {
                        ModelState.AddModelError("", error.ErrorMessage);
                        //errorString += " " + error.ErrorMessage;
                    }
                    //ModelState.AddModelError("", errorString);
                    //var test = ModelState.Where(x => x.Value.Errors.Any());
                }



            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(group);
        }

        private void UpdateGroupUserModel(GroupUser groupuser)
        {
            var groupToUpdate = dbcnxt.tblGroup.Find(groupuser.GROUPID);
            //groupToUpdate.GroupId = groupuser.GROUPID;
            groupToUpdate.Name = groupuser.NAME;

            var groupUserToUpdate = dbcnxt.tblGroupUser.Find(groupuser.GroupUserId);
            groupUserToUpdate.GroupId = groupuser.GROUPID;
            groupUserToUpdate.UserId = groupuser.USERID;

        }

        // POST: GroupUser/GroupUserDelete?id=5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GroupUserDelete(int id)
        {
            try
            {
                var group = dbcnxt.tblGroupUser.Find(id);
                dbcnxt.tblGroupUser.Remove(group);
                dbcnxt.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                //Log the error 
                ModelState.AddModelError("", ex.Message);

                return Json(new { Result = "Fail", ReturnMessage = ex.Message });
            }
        }






        #endregion


    }
}