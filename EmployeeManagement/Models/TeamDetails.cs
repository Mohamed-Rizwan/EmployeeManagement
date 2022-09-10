using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class TeamDetails
    {
        public int TeamId { get; set; }
        [Required(ErrorMessage ="Team Name Required")]
        public string TeamName { get; set; }
        [Required(ErrorMessage ="Manager Id Required")]
        public int managerid { get; set; }
    }
}