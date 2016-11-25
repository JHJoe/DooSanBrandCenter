using BrandCenter.DAL;
using BrandCenter.Models;
using DooSan.BrandCenter.FrameWork.DbContextFactory;
using DooSan.BrandCenter.FrameWork.DbContextFactory.Implementations;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrandCenter.Controllers.Base
{
    public class BaseController : Controller
    {
        protected DefaultContext _db;
        protected IDbContextScopeFactory _dbContextScopeFactory;

        public BaseController(DefaultContext db)
        {
            _db = db;
            _dbContextScopeFactory = SingletoneInstance.GetDbContextScopeFactory(); //GetDBContextScopeFactory();
        }

        //        public BaseController(IDbContextScopeFactory ContextScopeFactory)
        public BaseController() //ninject안쓸때는 위 생성자 주석 처리하고 상속받은곳도 다 주석처리 하고 쓰면 된다.
        {
            _dbContextScopeFactory = SingletoneInstance.GetDbContextScopeFactory(); //GetDBContextScopeFactory();
        }

        //protected IDbContextScopeFactory GetDBContextScopeFactory()
        //{
        //    DbContextScopeFactory dbContextScopeFactory = new DbContextScopeFactory();

        //    return dbContextScopeFactory;
        //}

        //public BaseController()
        //{
        //    db = new DefaultContext();
        //}
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        // GET: Base


        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    string actionName = filterContext.ActionDescriptor.ActionName;
        //    string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
         
                
        //    base.OnActionExecuting(filterContext);
        //}

        protected void ThrowModelError(DbEntityValidationException e)
        {
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
        }

        protected bool ThrowValidateError(IEnumerable< DbEntityValidationResult> elist, bool IsException = true)
        {
            bool IsRaisError = false;
            foreach (var eve in elist)
            {
                IsRaisError = true;
                if (IsException)
                    ModelState.AddModelError("", string.Format("시스템 에러 메세지 Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));

                foreach (var ve in eve.ValidationErrors)
                {
                    ModelState.AddModelError(ve.PropertyName, ve.ErrorMessage);
                    //ModelState.AddModelError("", string.Format("- Property: \"{0}\", Error: \"{1}\"",
                    //    ve.PropertyName, ve.ErrorMessage));
                }
            }
            return IsRaisError;
        }

    }
}