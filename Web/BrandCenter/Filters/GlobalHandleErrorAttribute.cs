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
    public class GlobalHandleErrorAttribute : HandleErrorAttribute
    {
        //public string ExceptionContent { get; set; }

        public override void OnException(ExceptionContext filterContext)
        {
            //ExceptionContent = filterContext.Exception.ToString();

            DateTime beginTime = DateTime.Now; // for time recording

            string LogHelperInfo = string.Empty;

            LogHelperInfo += beginTime.ToString();
            LogHelperInfo += Environment.NewLine;
            LogHelperInfo += "---------------------------------------------------------";
            LogHelperInfo += Environment.NewLine;

            LogHelperInfo += "OnException() { ";

            {
                LogHelperInfo +=  filterContext.Exception.ToString();

                base.OnException(filterContext);

                // Action의 결과가 Redirect면 목적지를 logging한다.
                if (filterContext.Result is RedirectToRouteResult)
                {
                    LogHelperInfo += string.Format("Redirecting to [{0} / {1}]",
                        ((RedirectToRouteResult)filterContext.Result).RouteValues["Controller"],
                        ((RedirectToRouteResult)filterContext.Result).RouteValues["Action"]
                    );
                }
                else if (filterContext.Result is ViewResult || filterContext.Result is PartialViewResult)
                {
                    string partial = (filterContext.Result is PartialViewResult) ? "Partial " : "";
                    LogHelperInfo += string.Format("{0}View({1}) is rendering...", partial, (filterContext.Result as ViewResult).ViewName);
                }
                else
                {
                    LogHelperInfo += string.Format("Yellow screen showed up.");
                }

                DateTime endTime = DateTime.Now;

                if (filterContext.Result is ViewResult || filterContext.Result is PartialViewResult)
                {
                    LogHelperInfo += string.Format("Rendered time: {0}s", (endTime - beginTime).TotalSeconds.ToString("f"));
                }
            }

            LogHelperInfo += "}";
            LogHelperInfo += Environment.NewLine;
            LogHelperInfo += "---------------------------------------------------------";
            LogHelperInfo += Environment.NewLine;

            new AppException(LogHelperInfo);


            filterContext.ExceptionHandled = true;
            var model = new HandleErrorInfo(filterContext.Exception, "Controller", "Action");

            filterContext.Result = new ViewResult()
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary(model)
            };

            //if (filterContext.Exception is DbException)
            //{
            //    filterContext.RequestContext.HttpContext.Response.Redirect("~/Error/Status?code=503");
            //}

        }
    }
}
