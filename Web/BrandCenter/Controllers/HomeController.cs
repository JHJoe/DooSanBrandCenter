using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrandCenter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            //WindowsIdentity clientId = (WindowsIdentity)HttpContext.Current.User.Identity;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}