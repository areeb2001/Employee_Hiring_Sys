using Employee_Hiring_Sys.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Employee_Hiring_Sys.Controllers
{
    public class ExperienceController : Controller
    {
        // GET: Experience
        db_EmployeeHiringEntities1 db = new db_EmployeeHiringEntities1();

        public ActionResult Index(int? id)
        {
            var user_Id = id;
            //var user = db.tbl_experience.Find(user_Id);
            var project = db.tbl_experience.Where(t => t.member_id == user_Id);
            ViewBag.id = HttpUtility.HtmlEncode(id);
            return View(project.ToList());
        }

        // GET: Experience/Details/5
        public ActionResult Details(int? id, int? user)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_experience tbl_experience = db.tbl_experience.Find(id);
            if (tbl_experience == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = HttpUtility.HtmlEncode(user);
            return View(tbl_experience);
        }

        // GET: Experience/Create
        public ActionResult Create(int? member_id)
        {
            ViewBag.member_id = new SelectList(db.tbl_personal_info, "member_id", "firsst_name", member_id);
            ViewBag.id = HttpUtility.HtmlEncode(member_id);
            return View();
        }

        // POST: Experience/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "exper_id,member_id,company,Starts_date,end_date,position,experience_time")] tbl_experience tbl_experience)
        {
            if (ModelState.IsValid)
            {
                db.tbl_experience.Add(tbl_experience);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = tbl_experience.member_id });
            }
            ViewBag.member_id = new SelectList(db.tbl_personal_info, "member_id", "firsst_name", tbl_experience.member_id);
            return View(tbl_experience);
        }

        // GET: Experience/Edit/5
        public ActionResult Edit(int? id, int? user)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_experience tbl_experience = db.tbl_experience.Find(id);
            if (tbl_experience == null)
            {
                return HttpNotFound();
            }
            ViewBag.member_id = new SelectList(db.tbl_personal_info, "member_id", "firsst_name", tbl_experience.member_id);
            ViewBag.id = HttpUtility.HtmlEncode(user);
            return View(tbl_experience);
        }

        // POST: Experience/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "exper_id,member_id,company,Starts_date,end_date,position,experience_time")] tbl_experience tbl_experience)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_experience).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = tbl_experience.member_id });
            }
            ViewBag.member_id = new SelectList(db.tbl_personal_info, "member_id", "firsst_name", tbl_experience.member_id);
            return View(tbl_experience);
        }

        // GET: Experience/Delete/5
        public ActionResult Delete(int? id, int? user)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_experience tbl_experience = db.tbl_experience.Find(id);
            if (tbl_experience == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = HttpUtility.HtmlEncode(user);
            return View(tbl_experience);
        }

        // POST: Experience/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_experience tbl_experience = db.tbl_experience.Find(id);
            db.tbl_experience.Remove(tbl_experience);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = tbl_experience.member_id });
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
