using BrandCenter.Models;
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
        public BaseController(DefaultContext db)
        {
            _db = db;
        }

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