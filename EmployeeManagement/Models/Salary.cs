using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models {
    public class Salary
    {
        public int SalaryId { get; set; }

        [Display(Name = "Employee ID")]
        [Required(ErrorMessage = "Employee ID required")]
        public int EmployeeId { get; set; }

        [Display(Name = "Salary")]
        [Required(ErrorMessage = "Salary Required")]
        public long SalaryAmount { get; set; }
    }
}

