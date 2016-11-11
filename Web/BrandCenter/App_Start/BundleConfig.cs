using System.Web;
using System.Web.Optimization;

namespace BrandCenter
{
    public class BundleConfig
    {
        // 번들 작성에 대한 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=301862 링크를 참조하십시오.
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
            //"~/Scripts/jquery-ui*"));

            bundles.Add(new StyleBundle("~/bundles/jquery-uicss").Include(
            "~/Scripts/jquery-ui.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/jquery-uijs").Include(
            "~/Scripts/jquery-ui.min.js"));


            //bundles.Add(new ScriptBundle("~/bundles/jquery-uijs").Include(
            //"~/scripts/jquery-ui-1.9.2.custom/js/jquery-ui-1.9.2.custom.min.js"));
            //bundles.Add(new StyleBundle("~/bundles/jquery-uicss").Include(
            //"~/Scripts/jquery-ui-1.9.2.custom/css/custom-theme/jquery-ui-1.9.2.custom.min.css"));


            bundles.Add(new ScriptBundle("~/bundles/Common").Include(
            "~/Scripts/Common.js"));
            bundles.Add(new ScriptBundle("~/bundles/Group").Include(
            "~/Scripts/Group/GroupDetail.js"));
            

            // Modernizr의 개발 버전을 사용하여 개발하고 배우십시오. 그런 다음
            // 프로덕션할 준비가 되면 http://modernizr.com 링크의 빌드 도구를 사용하여 필요한 테스트만 선택하십시오.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));




            //이걸 켜면 datetimepicker 경로를 못찾아옴
        //BundleTable.EnableOptimizations = true; //이거 해줘야 iis 셋팅해줄때도 스타일이 적용됨. //상관없게 수정함. 아마 stylebundle 같은 문제같은데 잘 모르겠음

        }
    }
}
