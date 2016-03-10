using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeReviewsFront
{
    public class Employee
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public string Email { get; set; }
        public string PhoneNum { get; set; }

        public string Review { get; set; }
        public bool isSatisfactory { get; set; }

        public Employee(string n, decimal s, string e, string p)
        {
            Name = n;
            Salary = s;
            Email = e;
            PhoneNum = p;
        }
    }
}
