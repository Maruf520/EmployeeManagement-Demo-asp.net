using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEMO.Models
{
    public class Salary
    {
        //nternal object Employee;

        public int SalaryId { get; set; }
        public Double Balance { get; set; }
        public DateTime Date { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}
