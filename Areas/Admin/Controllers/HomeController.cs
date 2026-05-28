using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Demo22.Models;

namespace Demo22.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {

            //取得 UserData
            string userData = ((FormsIdentity)(HttpContext.User.Identity)).Ticket.UserData;
            Member member = JsonConvert.DeserializeObject<Member>(userData);

            ViewBag.Name = member.Name;

            return View();
        }
    }
}