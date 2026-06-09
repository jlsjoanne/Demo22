using Demo22.Models;
using MvcPaging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using static Demo22.Models.EmumList;

namespace Demo22.Areas.Admin.Controllers
{
    
    public class MemberController : Controller
    {
        private DbModel db = new DbModel();

        // GET: Admin/Member
        public ActionResult Index(int? page)
        {
            var size = 1;
            
            if(!page.HasValue || page.Value == 0)
            {
                page = 1;
            }

            //var members = db.Members.AsQueryable();

            //var result = members.OrderBy(x => x.Id).Skip((page.Value - 1 ) * size).Take(size).ToList();

            var members = db.Members.AsQueryable();

            //列舉下拉選單製作
            IList<SelectListItem> list = Enum.GetValues(typeof(Gender)).Cast<Gender>().Select(x =>
                new SelectListItem
                {
                    Text = x.ToString(),
                    Value = ((int)x).ToString(),

                }).ToList();
            ViewBag.Gender = list;

            // 判斷搜尋條件是否存在
            if (!string.IsNullOrWhiteSpace(Session["name"]?.ToString()))
            {
                var name = Session["name"]?.ToString();
                members = members.Where(m => m.Name.Contains(name));
                ViewBag.Name = name;
            }

            if (!string.IsNullOrWhiteSpace(Session["gender"]?.ToString()))
            {
                var gender = (Gender)Session["gender"];
                members = members.Where(m => m.Gender == gender);
                IList<SelectListItem> selectedItem = Enum.GetValues(typeof(Gender)).Cast<Gender>().Select(x =>
                new SelectListItem
                {
                    Text = x.ToString(),
                    Value = ((int)x).ToString(),
                    Selected = x.Equals(gender)

                }).ToList();
                ViewBag.Gender = selectedItem;
            }

            var result = members.OrderBy(x => x.Id).ToPagedList(page.Value-1,size);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string name, Gender? gender)
        {

            if (!string.IsNullOrWhiteSpace(name))
            {
                Session["name"] = name;
            }
            else
            {
                Session["name"] = null;
            }

            if (gender.HasValue)
            {
                Session["gender"] = (int)gender; 
            }
            else
            {
                Session["gender"] = null;
            }

            return RedirectToAction("Index");
        }

        // GET: Admin/Member/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Admin/Member/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Member/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Member member)
        {
            if (ModelState.IsValid)
            {
                // 新增密碼鹽
                member.PasswordSalt = Utility.CreateSalt();
                // 加密密碼
                member.Password = Utility.GenerateHashWithSalt(member.Password,member.PasswordSalt);

                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }

        // GET: Admin/Member/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Admin/Member/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Member member, string newPassword)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(newPassword))
                {
                    // 記得檢查密碼原則
                    member.Password = Utility.GenerateHashWithSalt(newPassword, member.PasswordSalt);
                }
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: Admin/Member/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Admin/Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
