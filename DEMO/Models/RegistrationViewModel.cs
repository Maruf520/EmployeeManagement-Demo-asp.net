using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DEMO.Models
{
    public class RegistrationViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Mobile no not valid")]
        public string Mobile { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 6)]

        public string Password { get; set; }
        [Required]
        [NotMapped]
        [Compare("Password ")]
        public string ConfirmPassword { get; set; }

    }
}
