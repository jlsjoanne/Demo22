using Demo22.Models;
using Demo22.Models.VM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Demo22.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {

        private DbModel db = new DbModel();

        // GET: Admin/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVm login)
        {
            if (ModelState.IsValid)
            {
                var member = db.Members.FirstOrDefault(m => m.Account == login.Account);
                if(member == null)
                {
                    ModelState.AddModelError("","登入失敗");
                    return View(login);
                }
                
                //密碼加密對照
                var passwordSalt = member.PasswordSalt;
                var hashPassword = Utility.GenerateHashWithSalt(login.Password, passwordSalt);
                if(hashPassword != member.Password)
                {
                    ModelState.AddModelError("", "登入失敗");
                    return View(login);
                }

                // 產生表單驗證票
                var userData = JsonConvert.SerializeObject(member);
                Utility.SetAuthenTicket(userData, member.Id.ToString());

                return RedirectToAction("Index", "Home");
            }

            return View(login);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}