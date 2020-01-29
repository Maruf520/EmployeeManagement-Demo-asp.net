using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DEMO.Models
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string FullName { get; set; }
        public string Position { get; set; }
        public string EmpCode { get; set; }
        public string Designation { get; set; }
        public string OfficeLocation { get; set; }
        public ICollection<Salary> salaries { get; set; }
    }
}
