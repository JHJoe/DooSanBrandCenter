using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DooSan.BrandCenter.FrameWork.Utils;
using System.Text;

namespace BrandCenter.Filters
{
    public class GlobalActionFilterAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            var actiontime = DateTime.Now;
            string currentUrl = filterContext.HttpContext.Request.Url.OriginalString;
            if (filterContext.HttpContext.Request.UrlReferrer != null)
            {
                string oldUrl = filterContext.HttpContext.Request.UrlReferrer.OriginalString;

                if (oldUrl == currentUrl)
                {
                    //로그를 남기지 않는다.
                    currentUrl = "(Postback)";
                }
            }


            base.OnActionExecuting(filterContext);



        }

    }
}