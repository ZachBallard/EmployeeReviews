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
        public List<Employee> EmployeeList { get; set; } = new List<Employee>();

        public Department(string name)
        {
            Name = name;
        }
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
            var empsEligible = EmployeeList.Count(e => e.IsSatisfactory);

            var empRaise = r/empsEligible;

            foreach (var e in EmployeeList.Where(e => e.IsSatisfactory))
            {
                e.Salary += decimal.Round(empRaise, 2, MidpointRounding.AwayFromZero);
            }
        }

        public void RemoveEmployee(Employee e)
        {
            EmployeeList.Remove(e);
        }

        public void AddEmployee(Employee e)
        {
            EmployeeList.Add(e);
        }

    }
}
