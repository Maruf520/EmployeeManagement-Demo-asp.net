using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DEMO.Models
{
    public class Designation
    {   
        [Key]
        public int DesignationId { get; set; }
        [Display(Name = "Designation")]
        public string designation { get; set; }
       
    }
}
