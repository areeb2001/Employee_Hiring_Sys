using Employee_Hiring_Sys;
using Employee_Hiring_Sys.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Employee_Hiring_Sys.Controllers
{
    public class EducationController : Controller
    {
        // GET: Education
        db_EmployeeHiringEntities1 db=new db_EmployeeHiringEntities1();

        public ActionResult Index(int? id)
        {
            var userId = id;
            var user = db.tbl_personal_info.Find(userId);
            var education = db.tbl_education.Where(t => t.member_id == userId);
            //Session["UserId"] = HttpUtility.HtmlEncode(id);
            ViewBag.id = HttpUtility.HtmlEncode(id);
            return View(education.ToList());

            // var tbl_education = db.tbl_education.Include(t => t.tbl_personal_info);
            // return View(tbl_education.ToList());
        }

        // GET: Education/Details/5
        public ActionResult Details(int? id, int? user)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_education tbl_education = db.tbl_education.Find(id);
            if (tbl_education == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = HttpUtility.HtmlEncode(user);
            return View(tbl_education);
        }

        // GET: Education/Create
        public ActionResult Create(int? member_id, int? user)
        {
            
            ViewBag.member_id = new SelectList(db.tbl_personal_info, "member_id", "firsst_name", member_id);
            ViewBag.id = HttpUtility.HtmlEncode(member_id);
            
            return View();
        }

        // POST: Education/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create([Bind(Include = "edu_id,member_id,Degree,college,start_year,end_year,mark_cgpa")] tbl_education tbl_education)
        {
            if (ModelState.IsValid)
            {
                if (tbl_education.start_year > DateTime.Now)
                {
                    ViewBag.ErrorMessage = "<script>alert('Start Year must less then Cureent Date')</script>";
                    RedirectToAction("Create", new { id = tbl_education.member_id });
                }
                else
                {
                    if (tbl_education.mark_cgpa.AsInt() > 0 && tbl_education.end_year > tbl_education.start_year)
                    {
                        db.tbl_education.Add(tbl_education);
                        db.SaveChanges();
                        TempData["SuccessMsg"] = "<script>alert('Record added successfully')</script>";
                        var user = db.tbl_personal_info.Find(tbl_education.member_id).UserName;
                        //Session["UserId"] = tbl_education.member_id;
                        Session["Username"] = user.ToString();
                        return RedirectToAction("Index", new { id = tbl_education.member_id });
                        
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "<script>alert('Marks/Cgpa must greater then 0 OR start year is larger then end year')</script>";
                        RedirectToAction("Create", new { id = tbl_education.member_id });
                    }

                }

            }

            ViewBag.member_id = new SelectList(db.tbl_personal_info, "member_id", "firsst_name", tbl_education.member_id);
            return View(tbl_education);
        }

        // GET: Education/Edit/5
        public ActionResult Edit(int? id, int? user)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_education tbl_education = db.tbl_education.Find(id);
            if (tbl_education == null)
            {
                return HttpNotFound();
            }
            ViewBag.member_id = new SelectList(db.tbl_personal_info, "member_id", "firsst_name", tbl_education.member_id);
            ViewBag.id = HttpUtility.HtmlEncode(user);
            return View(tbl_education);
        }

        // POST: Education/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "edu_id,member_id,Degree,college,start_year,end_year,mark_cgpa")] tbl_education tbl_education)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_education).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = tbl_education.member_id });
            }
            ViewBag.member_id = new SelectList(db.tbl_personal_info, "member_id", "firsst_name", tbl_education.member_id);
            return View(tbl_education);
        }

        // GET: Education/Delete/5
        public ActionResult Delete(int? id, int? user)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_education tbl_education = db.tbl_education.Find(id);
            if (tbl_education == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = HttpUtility.HtmlEncode(user);
            return View(tbl_education);
        }

        // POST: Education/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_education tbl_education = db.tbl_education.Find(id);
            db.tbl_education.Remove(tbl_education);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = tbl_education.member_id });
        }

    }
}