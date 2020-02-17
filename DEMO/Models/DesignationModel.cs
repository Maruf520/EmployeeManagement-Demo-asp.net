using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DEMO.Models
{
    public class DesignationModel
    {
        public int DesingantionId { get; set; }
        [Required]
        public string designation { get; set; }
       // public List<Designation> DesignationList { get; set; }
        public List<Designation> DesignationList { get; set; }
    }
}
