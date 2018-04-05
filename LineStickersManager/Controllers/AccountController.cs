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
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View("frmAccount");
        }
        public ActionResult NewUser() {
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
                    FormsAuthentication.SetAuthCookie(inputLineID, false);
                }
                else
                    _Info = "密碼錯誤";
            }


            ViewBag.Info = _Info;
            return View("frmAccount");
        }
    }
}