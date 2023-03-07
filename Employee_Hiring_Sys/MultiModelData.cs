using Employee_Hiring_Sys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employee_Hiring_Sys
{
    public class MultiModelData
    {
        public List<tbl_personal_info> personal { get; set; }
        public List<tbl_projects> projects { get; set; }
        public List<tbl_education> education { get; set; }
        public List<tbl_experience> experience { get; set; }
        public List<tbl_category> category { get; set; }

    }
}