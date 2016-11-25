using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity.Infrastructure.Interception;
using BrandCenter.DAL;
using DooSan.BrandCenter.FrameWork.DbContextFactory;

namespace BrandCenter
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            System.Data.Entity.Database.SetInitializer<DefaultContext>(null);
//            System.Data.Entity.Database.SetInitializer<DefaultContext>(new System.Data.Entity.CreateDatabaseIfNotExists());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //DbInterception.Add(new InterceptorTransientErrors());
            DbInterception.Add(new InterceptorLogging());
            //Application["DbContextScopeFactory"] = new DbContextScopeFactory();
        }

    }
}
