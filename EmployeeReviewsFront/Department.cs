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

        public decimal TotalSalary()
        {
            return EmployeeList.Sum(e => e.Salary);
        }

        public void EvalutateEmployees()
        {
            foreach (var e in EmployeeList)
            {
                e.isSatisfactory = EvaluationCheck(e);
            }
        }

        public void GiveRaise(decimal r)
        {
            decimal empraise = r/EmployeeList.Count;
            foreach (var e in EmployeeList)
            {
                e.Salary += r;
            }
        }

        public void AddEmployee(Employee e)
        {
            EmployeeList.Add(e);
        }

        public bool EvaluationCheck(Employee e)
        {
            return true;
        }
    }
}
