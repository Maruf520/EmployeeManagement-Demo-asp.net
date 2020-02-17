using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DEMO.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string FirstName { get; set;}
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string  Email { get; set; }
        public string  Address { get; set; }
        public ICollection<Salary> Salaries { get; set; }
        public int DesignationId { get; set; }
        public Designation Designation { get; set; }
        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
        public Project Project { get; set; }
    }
}
