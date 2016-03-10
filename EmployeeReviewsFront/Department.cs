using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeReviewsFront
{
    public class Department
    {
        public string Name { get; set; }
        public List<Employee> EmployeeList { get; set; }
        public string SaveLocation { get; set; }

        public decimal TotalSalary()
        {
            return EmployeeList.Sum(e => e.Salary);
        }

        public void EvalutateEmployees()
        {
            foreach (var e in EmployeeList)
            {
                e.IsSatisfactory = e.Evaluate();
            }
        }

        public void GiveRaise(decimal r)
        {
            var empraise = r/EmployeeList.Count;
            foreach (var e in EmployeeList)
            {
                e.Salary += r;
            }
        }

        public void AddEmployee(Employee e)
        {
            EmployeeList.Add(e);
        }

    }
}
