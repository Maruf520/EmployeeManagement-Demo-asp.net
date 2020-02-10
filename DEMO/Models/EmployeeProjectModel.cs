using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DEMO.Models
{
    public class EmployeeProjectModel
    {
        public EmployeeProjectModel()
        {
            EmpoyeeList = new List<SelectListItem>();
            ProjectList = new List<SelectListItem>();
        }
        [Key]
        public int EmployeeId { get; set; }
        [Key]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string LastName { get; set; }
        public List<SelectListItem> EmpoyeeList { get; set; }
        public List<SelectListItem> ProjectList { get; set; }
        public Project Project { get; set; }

    }
}
