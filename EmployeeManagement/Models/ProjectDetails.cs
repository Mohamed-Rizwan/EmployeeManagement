using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class ProjectDetails
    {
        public int ProjectId { get; set; }
        [Display(Name ="Project Name")]
        [Required(ErrorMessage ="Project Name Required")]
        public string ProjectName { get; set; }
        [Display(Name ="Team")]
        [Required(ErrorMessage ="Team Required")]
        public int TeamId { get; set; }
        [Display(Name ="Project Status")]
        public string projectstatus { get; set; }  
    }
}