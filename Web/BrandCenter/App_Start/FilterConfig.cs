using BrandCenter.Filters;
using System.Web;
using System.Web.Mvc;

namespace BrandCenter
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new GlobalHandleErrorAttribute(), 2);

            //shared의 에러페이지로 전달
            //filters.Add(new HandleErrorAttribute
            //{
            //    View = "Error"
            //}, 1);


        }
    }
}
