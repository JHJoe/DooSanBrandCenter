using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

using System.Configuration;
//using Security;
using DooSan.BrandCenter.FrameWork.Static;
using BrandCenter.Models;

namespace BrandCenter.Helper
{
    public class GetSession
    {
        //app start시 
       //private DefaultContext db = null; // new DefaultContext();
        private static SessionClass ReturnSession;

        public GetSession()
        {
        }

        #region 속성

        public static SessionClass SessionClass
        {
            get
            {
                if (HttpContext.Current.Session["UserSession"] == null)
                    ReFill();

                ReturnSession = (SessionClass)HttpContext.Current.Session["UserSession"];

                return ReturnSession;
            }
            set
            {
                HttpContext.Current.Session["UserSession"] = value;
            }
        }

        public static string LoginID
        {
            get
            {
                if (HttpContext.Current.Session["UserSession"] == null)
                    ReFill();

                ReturnSession = (SessionClass)HttpContext.Current.Session["UserSession"];

                return ReturnSession.LoginID;
            }
            set
            {
                if (HttpContext.Current.Session["UserSession"] == null)
                    return;

                if (ReturnSession == null)
                    ReturnSession = (SessionClass)HttpContext.Current.Session["UserSession"];

                ReturnSession.LoginID = value;
                HttpContext.Current.Session["UserSession"] = ReturnSession;

            }
        }

        public static string EmpID
        {
            get
            {
                if (HttpContext.Current.Session["UserSession"] == null)
                    ReFill();

                ReturnSession = (SessionClass)HttpContext.Current.Session["UserSession"];

                return ReturnSession.EmpID;
            }
            set
            {
                if (HttpContext.Current.Session["UserSession"] == null)
                    return;

                if (ReturnSession == null)
                    ReturnSession = (SessionClass)HttpContext.Current.Session["UserSession"];

                ReturnSession.EmpID = value;
                HttpContext.Current.Session["UserSession"] = ReturnSession;

            }
        }

        public static string USERNAME
        {
            get
            {
                if (HttpContext.Current.Session["UserSession"] == null)
                    ReFill();

                ReturnSession = (SessionClass)HttpContext.Current.Session["UserSession"];

                return ReturnSession.USERNAME;
            }
            set
            {
                if (HttpContext.Current.Session["UserSession"] == null)
                    return;

                if (ReturnSession == null)
                    ReturnSession = (SessionClass)HttpContext.Current.Session["UserSession"];

                ReturnSession.USERNAME = value;
                HttpContext.Current.Session["UserSession"] = ReturnSession;

            }
        }

        public static LangType Language
        {
            get
            {
                if (HttpContext.Current.Session["UserSession"] == null)
                    ReFill();

                ReturnSession = (SessionClass)HttpContext.Current.Session["UserSession"];

                return ReturnSession.Language;
            }
            set
            {
                if (HttpContext.Current.Session["UserSession"] == null)
                    return;

                if (ReturnSession == null)
                    ReturnSession = (SessionClass)HttpContext.Current.Session["UserSession"];

                ReturnSession.Language = value;
                HttpContext.Current.Session["UserSession"] = ReturnSession;

            }
        }

        public static string EMail
        {
            get
            {
                if (HttpContext.Current.Session["UserSession"] == null)
                    ReFill();

                ReturnSession = (SessionClass)HttpContext.Current.Session["UserSession"];

                return ReturnSession.EMail;
            }
            set
            {
                if (HttpContext.Current.Session["UserSession"] == null)
                    return;

                if (ReturnSession == null)
                    ReturnSession = (SessionClass)HttpContext.Current.Session["UserSession"];

                ReturnSession.EMail = value;
                HttpContext.Current.Session["UserSession"] = ReturnSession;

            }
        }
   

        public static string GROUP_ID
        {
            get
            {
                if (HttpContext.Current.Session["UserSession"] == null)
                    ReFill();

                ReturnSession = (SessionClass)HttpContext.Current.Session["UserSession"];

                return ReturnSession.GROUP_ID;
            }
            set
            {
                if (HttpContext.Current.Session["UserSession"] == null)
                    return;

                if (ReturnSession == null)
                    ReturnSession = (SessionClass)HttpContext.Current.Session["UserSession"];

                ReturnSession.GROUP_ID = value;
                HttpContext.Current.Session["UserSession"] = ReturnSession;

            }
        }

        public static string DisplayName
        {
            get
            {
                if (HttpContext.Current.Session["UserSession"] == null)
                    ReFill();

                ReturnSession = (SessionClass)HttpContext.Current.Session["UserSession"];

                return ReturnSession.DisplayName;
            }
            set
            {
                if (HttpContext.Current.Session["UserSession"] == null)
                    return;

                if (ReturnSession == null)
                    ReturnSession = (SessionClass)HttpContext.Current.Session["UserSession"];

                ReturnSession.DisplayName = value;
                HttpContext.Current.Session["UserSession"] = ReturnSession;

            }
        }

        

        public static bool ADMIN
        {
            get
            {
                if (HttpContext.Current.Session["UserSession"] == null)
                    ReFill();

                ReturnSession = (SessionClass)HttpContext.Current.Session["UserSession"];

                return ReturnSession.ADMIN;
            }
            set
            {
                if (HttpContext.Current.Session["UserSession"] == null)
                    return;

                if (ReturnSession == null)
                    ReturnSession = (SessionClass)HttpContext.Current.Session["UserSession"];

                ReturnSession.ADMIN = value;
                HttpContext.Current.Session["UserSession"] = ReturnSession;

            }
        }



        #endregion

        private static void ReFill()
        {
            //if (!Request.IsAuthenticated)
            //    throw new InvalidOperationException("certification error.");
            if (!string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
            {
                string id = HttpContext.Current.User.Identity.Name;

                //LangType resultLangType = (LangType)Enum.Parse(typeof(LangType), langtype);
                //bool IsShop = (shop == "true" ? true : false);

                if (string.IsNullOrEmpty(id)) throw new InvalidOperationException("certification error.");

                FillSession(id);
                //GetSession.LANGUAGE = resultLangType;
                //else면 throw. 세션 만기로
            }


        }

        ///// <summary>
        ///// test 용 인증
        ///// </summary>
        ///// <param name="strUSER_ID"></param>
        ///// <param name="langtype"></param>
        ///// <param name="IsShop"></param>
        ///// <param name="EPEncript"></param>
        ///// <returns></returns>
        //public static string FillSession(string strUSER_ID, LangType langtype = LangType.ko, bool IsShop = false, string EPEncript = "")
        //{
        //    return FillSession(strUSER_ID, out dtLastLogin, false, langtype, IsShop, EPEncript);
        //}

        /// <summary>
        /// 인증 및 세션 가져오기 
        /// </summary>
        /// <returns></returns>
        public static string FillSession(string UserId, bool bRefill = true, bool IsAdmin = false)
        {
            string strOutput = string.Empty;

            SessionClass UserSession = new SessionClass();
            // test login 을 위해서 싱글에서 다시 가져오는건 피한다.

            UserSession.LoginID = UserId; ; // HttpContext.Current.User.Identity.Name; //ad id

            DefaultContext db = new DefaultContext();

            var user = db.tblUser.Find(UserId);
            if (user == null)
            {
                throw new InvalidOperationException("certification error - User not found for : " + UserId);
            }


            UserSession.LoginID = user.LoginID;
            UserSession.EmpID = user.EmpID;
            UserSession.EMail = user.EMail;
            LangType lang;
            if (Enum.TryParse<LangType>(user.MyLang, out lang) == false)
                UserSession.Language = LangType.bl;
            //            UserSession.LANGUAGE = (LangType)Enum.Parse(typeof(LangType), user.MyLang);
            UserSession.DisplayName = user.DisplayName;

            var groupuser = db.tblGroupUser.Where(u => u.UserId == UserId).SingleOrDefault(); //.ToList();
            if (groupuser == null)
            {
                throw new InvalidOperationException("certification error - The users does not belong to a group : " + UserId);
            }
            UserSession.GROUP_ID= groupuser.GroupId.ToString();
           

//            UserSession.ADMIN = (dt.Rows[0]["ADMIN_YN"].ToString() == "True" ? true : false);


            HttpContext.Current.Session["UserSession"] = UserSession;

            //로그인 로깅
            //if (!bRefill)
            //    LoginLogging(IsShop, UserSession);

            //                return strOutput;
            //}
            db.Dispose();

            return "SUCCESS";
        }

        //public static void LoginLogging(bool IsShop, SessionClass UserSession)
        //{
        //    if (ConfigurationManager.AppSettings["PageLogging"] == null)
        //        return;
        //    string TraceMode = ConfigurationManager.AppSettings["PageLogging"];

        //    if (!TraceMode.Equals("on"))
        //        return;

        //    string logintype = "CMSUSER";

        //    if (IsShop)
        //        logintype = "CMSSTORE";

        //    string strHostIP = HttpContext.Current.Request.UserHostAddress;

        //    int fileind = HttpContext.Current.Request.FilePath.LastIndexOf("/");
        //    string PageFileName = HttpContext.Current.Request.FilePath.Substring(fileind + 1).ToUpper().Replace(".ASPX", "").Replace("SF", "");

        //    CII.CMS.Framework.DataUtil.DataUtilDirectIn dbcall = new CII.CMS.Framework.DataUtil.DataUtilDirectIn("DBConn");
        //    System.Collections.Hashtable ht = new System.Collections.Hashtable();
        //    ht.Add("@USER_IP", strHostIP);
        //    ht.Add("@EMPNO", UserSession.LoginID);
        //    ht.Add("@LOGINTYPE", logintype);
        //    ht.Add("@USER", UserSession.USERID);
        //    dbcall.ExecuteNonQuerySp("SP_COMMON_iLOGINHISTORY ", ht);

        //}


    }

}