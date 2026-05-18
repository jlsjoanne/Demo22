using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Demo22.Models;
using Demo22.ViewModels;
using Demo22.App_Start;

namespace Demo22.Controllers
{
    public class ViewMemberController : Controller
    {
        private DbModel db = new DbModel();

        // GET: ViewMember
        public ActionResult Index()
        {
            var members = db.Members;
            return View(members.ToList());
        }

        // GET: ViewMember/Details/5
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

        // GET: ViewMember/Create
        public ActionResult Create()
        {
            var viewMember = new ViewMember();
            return View(viewMember);
        }

        // POST: ViewMember/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewMember viewMember)
        {
            if (ModelState.IsValid)
            {
                var member = AutoMapperConfig.Mapper.Map<Member>(viewMember);
                member.Identity = 0;

                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viewMember);
        }

        // GET: ViewMember/Edit/5
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
            var viewMember = AutoMapperConfig.Mapper.Map<ViewMember>(member);
            return View(viewMember);
        }

        // POST: ViewMember/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViewMember viewMember)
        {
            if (ModelState.IsValid)
            {
                var existingMember = db.Members.FirstOrDefault(m => m.Id == viewMember.Id);
                if(existingMember != null)
                {
                    AutoMapperConfig.Mapper.Map(viewMember, existingMember);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(viewMember);
        }

        //// GET: ViewMember/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ViewMember viewMember = db.ViewMembers.Find(id);
        //    if (viewMember == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(viewMember);
        //}

        //// POST: ViewMember/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ViewMember viewMember = db.ViewMembers.Find(id);
        //    db.ViewMembers.Remove(viewMember);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
