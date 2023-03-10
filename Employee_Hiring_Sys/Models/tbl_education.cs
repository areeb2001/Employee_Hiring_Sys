//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Employee_Hiring_Sys.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class tbl_education
    {
        public int edu_id { get; set; }
        public int member_id { get; set; }

        [Required(ErrorMessage = "Degree is required")]
        [DisplayName("Degree")]
        public string Degree { get; set; }

        [Required(ErrorMessage = "College Name is required")]
        [DisplayName("College Name")]
        public string college { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime start_year { get; set; }

        [DisplayName("End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> end_year { get; set; }

        [Required(ErrorMessage = "Marks are required")]
        [DisplayName("Marks/Cgpa")]
        public string mark_cgpa { get; set; }
    
        public virtual tbl_personal_info tbl_personal_info { get; set; }
    }
}
