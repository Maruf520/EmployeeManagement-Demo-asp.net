using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DEMO.Models
{
    public class EmployeeModel
    {
        public EmployeeModel()
        {
            DesignationList = new List<SelectListItem>();
        }
        public int EmployeeId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name="Designation")]
        
        public int DesignationId { get; set; }
        [Display(Name = "Designation")]
        public string designation { get; set; }
        [Display(Name="Phone")]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int DesingantionId { get; set; }
        //public string designation { get; set; }
        public ICollection<Salary> salaries { get; set; }
        public List<SelectListItem> DesignationList { get; set; }


    }
}
