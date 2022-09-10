using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Models
{
    public class payslip
    {
        public int Payslipid { get; set; }  
         public DateTime Date { get; set; }
        public  int salaryid { get; set; }

        public string salarystatus { get; set; }
    }
}