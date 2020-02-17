using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DEMO.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        [Display(Name="Project Name")]
        [Required]
        public string ProjectName { get; set; }
        public ICollection<EmployeeProject> EmployeeProjects { get; set; }

    }
}
