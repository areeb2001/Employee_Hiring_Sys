using Employee_Hiring_Sys.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;

namespace Employee_Hiring_Sys.Controllers
{
    public class ProjectsController : Controller
    {
        db_EmployeeHiringEntities1 db = new db_EmployeeHiringEntities1();
        // GET: Projects
        public ActionResult Index(int? id)
        {
            var data = db.tbl_projects.ToList();
            return View(data);
        }

        public ActionResult Create(int? member_id)
        {
            ViewBag.member_id = new SelectList(db.tbl_projects, "member_id", "firsst_name", member_id);
            return View();
        }

        [HttpPost]
        public ActionResult Create(tbl_projects proj)
        {
            if (ModelState.IsValid == true)
            {

                string filename = Path.GetFileNameWithoutExtension(proj.ImageFile.FileName);
                string extension = Path.GetExtension(proj.ImageFile.FileName);
                HttpPostedFileBase postedfile = proj.ImageFile;
                int length = postedfile.ContentLength;

                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                {
                    if (length <= 1000000)
                    {
                        filename = filename + extension;
                        proj.image_path = "~/Images/" + filename;
                        filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                        proj.ImageFile.SaveAs(filename);
                        db.tbl_projects.Add(proj);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["CreateMsg"] = "<script>alert('Record Inserted')</script>";
                            return RedirectToAction("Index", "Projects", new { id = proj.member_id });
                        }
                        else
                        {
                            TempData["CreateMsg"] = "<script>alert('Record Not Inserted')</script>";
                        }
                    }

                    else
                    {
                        TempData["SizeMsg"] = "<script>alert('image size is greater then 1Mb')</script>";
                    }
                }
                else
                {
                    TempData["EXtMsg"] = "<script>alert('format not supported')</script>";
                }
            }
            return View();
        }

        public ActionResult Edit(int? id, int? user)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_projects proj = db.tbl_projects.Find(id);
            if (proj == null)
            {
                return HttpNotFound();
            }
            ViewBag.member_id = new SelectList(db.tbl_personal_info, "member_id", "firsst_name", proj.member_id);
            ViewBag.id = HttpUtility.HtmlEncode(user);
            Session["Image"] = proj.image_path;
            return View(proj);
        }

        [HttpPost]
        public ActionResult Edit(tbl_projects proj)
        {
            if (ModelState.IsValid == true)
            {
                if (proj.ImageFile != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(proj.ImageFile.FileName);
                    string extension = Path.GetExtension(proj.ImageFile.FileName);
                    HttpPostedFileBase postedfile = proj.ImageFile;
                    int length = postedfile.ContentLength;

                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                    {
                        if (length <= 1000000)
                        {
                            filename = filename + extension;
                            proj.image_path = "~/Images/" + filename;
                            filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                            proj.ImageFile.SaveAs(filename);
                            db.Entry(proj).State = EntityState.Modified;

                            int a = db.SaveChanges();
                            if (a > 0)
                            {
                                TempData["Message"] = "<script>alert('Data Updated')</script>";
                                ModelState.Clear();
                                return RedirectToAction("Index", new { id = proj.member_id });
                            }
                            else
                            {
                                TempData["Message"] = "<script>alert('Record Not Inserted')</script>";
                            }
                        }
                        else
                        {
                            ViewBag.SizeMessage = "<script>alert('Image Size Must Less Then 1mb')</script>";
                        }
                    }
                    else
                    {
                        ViewBag.ExtensionMessage = "<script>alert('Image Not Supported')</script>";
                    }
                }

                else
                {
                    proj.image_path = Session["Image"].ToString();
                    db.Entry(proj).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = proj.member_id });
                }
                
            }
            return View();
        }


        public ActionResult Details(int? id, int? user)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_projects proj = db.tbl_projects.Find(id);
            if (proj == null)
            {
                return HttpNotFound();
            }
            Session["Image"] = proj.image_path;
            ViewBag.id = HttpUtility.HtmlEncode(user);
            return View(proj);
        }


        public ActionResult Delete(int? id,int?user)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_projects proj = db.tbl_projects.Find(id);
            if (proj == null)
            {
                return HttpNotFound();
            }
            Session["Image"] = proj.image_path;
            ViewBag.id = HttpUtility.HtmlEncode(user);
            return View(proj);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_projects proj = db.tbl_projects.Find(id);
            db.tbl_projects.Remove(proj);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = proj.member_id });
        }
    }
}