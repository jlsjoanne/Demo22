using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Demo22.Areas.Admin.ViewModels;
using Demo22.Models;
using Demo22.App_Start;

namespace Demo22.Areas.Admin.Controllers
{
    public class MemberMgmtController : Controller
    {
        private DbModel db = new DbModel();

        // GET: Admin/MemberMgmt
        public ActionResult Index()
        {
            var members = db.Members;
            return View(members);
        }

        // GET: Admin/MemberMgmt/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }

            return View(member);
        }

        //// GET: Admin/MemberMgmt/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Admin/MemberMgmt/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Account,Identity")] MemberMgmt memberMgmt)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.MemberMgmts.Add(memberMgmt);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(memberMgmt);
        //}

        // GET: Admin/MemberMgmt/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            var memberMgmt = AutoMapperConfig.Mapper.Map<MemberMgmt>(member);
            return View(memberMgmt);
        }

        // POST: Admin/MemberMgmt/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MemberMgmt memberMgmt)
        {
            if (ModelState.IsValid)
            {
                var existingMember = db.Members.FirstOrDefault(m => m.Id == memberMgmt.Id);
                if (existingMember != null)
                {
                    AutoMapperConfig.Mapper.Map(memberMgmt, existingMember);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(memberMgmt);
        }

        // GET: Admin/MemberMgmt/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Admin/MemberMgmt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var member = db.Members.Find(id);
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
