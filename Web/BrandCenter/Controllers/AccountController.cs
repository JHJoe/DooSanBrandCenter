using BrandCenter.Helper;
using BrandCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 
using BrandCenter.DAL;

namespace BrandCenter.Controllers
{
    public class AccountController : Base.BaseController
    {
        public AccountController(DefaultContext db) : base(db)
        {
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginSubmit(string LoginUserId)
        {
            try
            {
                GetSession.FillSession(LoginUserId, false, false);
                return RedirectToAction("../BAnner/BannerRequestList");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("LoginUserId", "로그인 실패" + ex.Message);
            }
            return View("Login");
        }

        //다른 테스트 용으로 submit 생길거 같아
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string LoginUserId)
        {
            try
            {
                return View(); 
                GetSession.FillSession(LoginUserId, false, false);
                return RedirectToAction("../admin/GroupUserList");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("LoginUserId", "로그인 실패" + ex.Message);
            }
            return View();
        }

    }
}