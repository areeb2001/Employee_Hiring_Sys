using Employee_Hiring_Sys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employee_Hiring_Sys.Models
{
    public class MainClass
    {
        public tbl_personal_info personal_info { get; set; }
        public tbl_category category { get; set; }

        public tbl_experience experience { get; set; }
        public tbl_projects AllProjects { get; set; }
    }
}