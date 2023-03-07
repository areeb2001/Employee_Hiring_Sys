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
    public class CategoryController : Controller
    {
        // GET: Category
       db_EmployeeHiringEntities1 db =new db_EmployeeHiringEntities1();

        public ActionResult Index(int? id)
        {
            var user_Id = id;
            //var user = db.tbl_experience.Find(user_Id);
            var category = db.tbl_category.Where(t => t.member_id == user_Id);
            ViewBag.id = HttpUtility.HtmlEncode(id);
            return View(category.ToList());
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id, int? user)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_category tbl_category = db.tbl_category.Find(id);
            if (tbl_category == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = HttpUtility.HtmlEncode(user);
            return View(tbl_category);
        }

        // GET: Category/Create
        public ActionResult Create(int? member_id)
        {
            var list = new List<string>() { "web developer", "Graphic Designer", "Game Developer", "Andiod App Developer", "Logo Designer" };
            ViewBag.member_id = new SelectList(db.tbl_category, "member_id", "firsst_name", member_id);
            ViewBag.id = HttpUtility.HtmlEncode(member_id);
            ViewBag.list = list;
            tbl_category cat = new tbl_category();
            cat.cat_name = "Web Developer";
            return View(cat);

        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create([Bind(Include = "id,member_id,cat_name")] tbl_category tbl_category)
        {
            if (ModelState.IsValid)
            {
                db.tbl_category.Add(tbl_category);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = tbl_category.member_id });
            }

            ViewBag.member_id = new SelectList(db.tbl_personal_info, "member_id", "firsst_name", tbl_category.member_id);
            return View(tbl_category);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int? id, int? user)
        {
            var list = new List<string>() { "web developer", "Graphic Designer", "Game Developer", "Andiod App Developer", "Logo Designer" };
            ViewBag.member_id = new SelectList(db.tbl_category, "member_id", "firsst_name", id);
            ViewBag.id = HttpUtility.HtmlEncode(id);
            ViewBag.list = list;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_category tbl_category = db.tbl_category.Find(id);
            tbl_category cat = new tbl_category();
            cat.cat_name = tbl_category.cat_name;
            if (tbl_category == null)
            {
                return HttpNotFound();
            }
            ViewBag.member_id = new SelectList(db.tbl_personal_info, "member_id", "firsst_name", tbl_category.member_id);
            ViewBag.id = user;
            return View(cat);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id,member_id,cat_name")] tbl_category tbl_category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = tbl_category.member_id });
            }
            ViewBag.member_id = new SelectList(db.tbl_personal_info, "member_id", "firsst_name", tbl_category.member_id);
            return View(tbl_category);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int? id, int? user)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_category tbl_category = db.tbl_category.Find(id);
            if (tbl_category == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = user;
            return View(tbl_category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_category tbl_category = db.tbl_category.Find(id);
            db.tbl_category.Remove(tbl_category);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = tbl_category.member_id });
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