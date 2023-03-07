using Employee_Hiring_Sys.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Employee_Hiring_Sys.Controllers
{
    public class SearchController : Controller
    {
        db_EmployeeHiringEntities1 db=new db_EmployeeHiringEntities1();
        // GET: Search
        public ActionResult Index(string searchBy, string Search)
        {

            List<tbl_personal_info> alluser = db.tbl_personal_info.ToList();
            List<tbl_category> categories = db.tbl_category.ToList();
            List<tbl_experience> exp = db.tbl_experience.ToList();
            List<tbl_projects> project = db.tbl_projects.ToList();
            var data = from user in alluser
                       join c in categories on user.member_id equals c.member_id into table1
                       from c in table1.DefaultIfEmpty()
                       join e in exp on c.member_id equals e.member_id into table2
                       from e in table2.DefaultIfEmpty()
                       join proj in project on e.member_id equals proj.member_id into table3
                       from proj in table3.DefaultIfEmpty()
                       select new MainClass { personal_info = user, category = c, experience = e, AllProjects = proj };
            if (Search != null)
            {
                if (searchBy == "Category")
                {
                    var data5 = data.Where(model => model.category.cat_name.Contains(Search)).ToList();
                    return View(data5);
                }

                else if (searchBy == "Language")
                {
                    var data3 = data.Where(model => model.AllProjects.languages.Contains(Search)).ToList();
                    return View(data3);
                }
                else if (searchBy == "Name")
                {
                    var data4 = data.Where(model => model.personal_info.UserName.ToLower().StartsWith(Search.ToLower())).ToList();
                    return View(data4);
                }
                else { return View(data); }
            }
            else { return View(data); }

        }



        public ActionResult Details(int? id)
        {
            ////List<tbl_category> categories = db.tbl_category.ToList();
            ////List<tbl_experience> exp = db.tbl_experience.ToList();
            ////List<tbl_projects> project = db.tbl_projects.ToList();

            ////var model = new MainClass();


            //var projects = db.tbl_projects.Where(n => n.member_id == id);
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //var users = db.tbl_personal_info.Where(t => t.member_id == id);
            //return View(users.ToList());

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
            return View(data);

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

    }
}