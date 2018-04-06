using Helpers;
using LineStickersManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LineStickersManager.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Main");

            return View("frmAccount");
        }
        public ActionResult NewUser()
        {
            return View("frmNewUser");
        }
        [HttpPost]
        public ActionResult Login(string inputLineID, string inputPassword, string rememberMe)
        {
            string _Info = string.Empty;
            LineStickersManagerEntities db = new LineStickersManagerEntities();
            tblUser _User = (
                                from user
                                in db.tblUsers
                                where user.LineID == inputLineID
                                select user).FirstOrDefault();
            if (_User == null)
                _Info = "查無使用者";
            else
            {
                if (EncryptionHelper.EncryptPassword(inputPassword) == _User.Password)
                {
                    _Info = "登入成功";
                    /*Method 1*/
                    FormsAuthenticationTicket _FormsAuthenticationTicket = new FormsAuthenticationTicket(1, inputLineID, DateTime.Now, DateTime.Now.AddMinutes(30), false, inputLineID, FormsAuthentication.FormsCookiePath);
                    string _Ticket = FormsAuthentication.Encrypt(_FormsAuthenticationTicket);
                    HttpCookie _Cookie = new HttpCookie(FormsAuthentication.FormsCookieName, _Ticket);
                    Response.Cookies.Add(_Cookie);
                    /*Method 2*/
                    //FormsAuthentication.SetAuthCookie(inputLineID, false);
                    return RedirectToAction("Index", "Main");
                }
                else
                    _Info = "密碼錯誤";
            }


            ViewBag.Info = _Info;
            return View("frmAccount");
        }
        public ActionResult CreateUser(tblUser user)
        {
            ViewBag.Info = string.Empty;
            if (user != null)
            {
                #region 資料驗證
                if (
                    string.IsNullOrEmpty(user.LineID) ||
                    string.IsNullOrEmpty(user.Password) ||
                    string.IsNullOrEmpty(user.Name) ||
                    string.IsNullOrEmpty(user.PhoneNumber)
                   )
                {
                    ViewBag.Info = "資料驗證錯誤!";
                    return View("frmNewUser");
                }
                #endregion

                LineStickersManagerEntities db = new LineStickersManagerEntities();
                if (db.tblUsers.Any(m => m.LineID == user.LineID))
                {
                    ViewBag.Info = "相同的Line ID已存在, 請登入。";
                    return View("frmAccount");
                }
                else
                {
                    user.Password = EncryptionHelper.EncryptPassword(user.Password);
                    user.ModiTime = DateTime.Now;
                    db.tblUsers.Add(user);
                    db.SaveChanges();
                    ViewBag.Info = "帳號註冊成功，請登入。";
                    return View("frmAccount");
                }
                
            }
            return View("frmAccount");
        }
        public ActionResult Logout() {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Account");
        }
    }
}