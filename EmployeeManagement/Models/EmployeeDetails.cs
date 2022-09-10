using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class EmployeeDetails
    {
        public int EmpId { get; set; }

        [Display(Name = "Employee Name")]
        [Required(ErrorMessage ="Name Required")]
        public string EmpName { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage ="Gender Required")]
        public string gender { get; set; }
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email Required")]
        public string Email { get; set; }
        [Display(Name = "Job Role")]
        [Required(ErrorMessage = "Job Role Required")]
        public string Jobrole { get; set; }
        [Display(Name = "Team")]
        [Required(ErrorMessage = "Team Required")]
        public int TeamId { get; set; }
        [Display(Name = "Date Of Joining")]
        [Required(ErrorMessage = "DOJ Required")]
        public DateTime DateofJoining { get; set; } 

        public SelectList ToSelectList(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row[textField].ToString(),
                    Value = (row[valueField].ToString())
                });
            }

            return new SelectList(list, "Value", "Text");
        }


    }
  
    




}