using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DEMO.Models
{
    public class EmployeeProject
    {
        [Key]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [Key]
        public int ProjectId { get; set; }  
        public Project Project { get; set; }
    
       
    }
}
