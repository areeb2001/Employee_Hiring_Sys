using Employee_Hiring_Sys;
using Employee_Hiring_Sys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace Portfolio.Controllers
{
    public class LoginController : Controller
    {
        db_EmployeeHiringEntities1 db=new db_EmployeeHiringEntities1();
        
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login_act()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login_act(tbl_personal_info personal)
        {
            var user = db.tbl_personal_info.Where(model => model.UserName == personal.UserName && model.Pass == personal.Pass).FirstOrDefault();
            if (user != null)
            {
                Session["UserId"] = personal.member_id;
                Session["Username"] = personal.UserName.ToString();
                TempData["LoginSuccessMsg"] = "<script>alert('Login Successfull')</script>";
                return RedirectToAction("Dashboard_act", "Login", new { id = user.member_id });
            }
            else
            {
                ViewBag.ErrorMessage = "<script>alert('UserName or Password isincorrect')</script>";
                return View();
            }
        }

        public ActionResult Register_act()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register_act(tbl_personal_info personal)
        {
            if (ModelState.IsValid == true)
            {
                db.tbl_personal_info.Add(personal);
                int save = db.SaveChanges();
                if (save > 0)
                {
                    TempData["RegisteredSuccessMsg"] = "<script>alert('Registerd Successfully')</script>";
                    ModelState.Clear();
                    return RedirectToAction("Login_act", "Login");
                }
                else
                {
                    ViewBag.InsertMsg = "<script>alert('Registration Failed')</script>";
                }
            }
            return View();
        }


        public ActionResult Dashboard_act(int? id)
        {
            if (Session["UserId"] != null)
            {
                var user = GetPersonal().Where(t => t.member_id == id);
                var edu = GetEducation().Where(t => t.member_id == id);
                var exper = GetExperience().Where(t => t.member_id == id);
                var proj = GetProjects().Where(t => t.member_id == id);
                var cat = GetCategory().Where(t => t.member_id == id);
                MultiModelData data = new MultiModelData();
                data.personal = user.ToList();
                data.education = edu.ToList();
                data.experience = exper.ToList();
                data.projects = proj.ToList();
                data.category = cat.ToList();
                Session["UserId"] = id;
                return View(data);
            }
            else
            {
                return RedirectToAction("Login_act");
            }
        }

        public List<tbl_personal_info> GetPersonal()
        {
            return db.tbl_personal_info.ToList();
        }

        public List<tbl_education> GetEducation()
        {
            return db.tbl_education.ToList();
        }

        public List<tbl_experience> GetExperience()
        {
            return db.tbl_experience.ToList();
        }

        public List<tbl_projects> GetProjects()
        {
            return db.tbl_projects.ToList();
        }

        public List<tbl_category> GetCategory()
        {
            return db.tbl_category.ToList();
        }


        public ActionResult Logout_act()
        {
            Session.Abandon();
            return RedirectToAction("Login_act", "Login");
        }



    }
}