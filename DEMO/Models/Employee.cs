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
        [Required]
        [Display(Name="Name")]
        
        public string FullName { get; set; }

        [Column(TypeName = "varchar(10)")]
        [DisplayName("Emp. Code")]
        public string EmpCode { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Positon { get; set; }
        [Column(TypeName = "varchar(100)")]
        [DisplayName("Office Location")]
        public string OfficeLocation { get; set; }

        public string Designation { get; set; }
        public ICollection<Salary> Salaries { get; set; }
    }
}
