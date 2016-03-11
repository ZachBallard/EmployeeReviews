using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmployeeReviewsFront
{
    class Program
    {
        static void Main(string[] args)
        {
            //load company profile

            string SaveData = System.IO.File.ReadAllText(@"C:\Users\zbgin\OneDrive\Documents\Visual Studio 2015\Projects\EmployeeReviews
                                                             \EmployeeReviewsFront\SaveData.txt");

            //Example text <%Zach's Palace%#,Zachary Ballard,,55000,,zbginji@gmail.com,,(479)650-5231,,true,,true,,review,#>

            var c = new Company();

            var allDepartmentData = new Regex(@"(?:<)(.+?)(?:>)");
            var departmentData = new Regex(@"(?:%)(.+?)(?:%)");
            var allEmployeeData = new Regex(@"(?:#)(.+?)(?:#)");
            var employeeData = new Regex(@"(?:,)(.+?)(?:,)");

            MatchCollection allDepartment = allDepartmentData.Matches(SaveData);
            MatchCollection departmentName = departmentData.Matches(SaveData);


            for (int i = 0; i < allDepartment.Count; i++)
            {
                string name = departmentName[i].ToString();
                c.DepartmentList.Add(new Department(name));

                MatchCollection allEmployee = allEmployeeData.Matches(allDepartment[i].ToString());

                for (int j = 0; j < allEmployee.Count; j++)
                {
                    MatchCollection employeeInfo = employeeData.Matches(allEmployee[j].ToString());

                    string empName = "";
                    decimal empSalary = 0m;
                    string empEmail = "";
                    string empPhoneNumber = "";
                    bool empIsSatisfactory = true;
                    bool empHasReview = true;
                    string empReview = "";

                    for (int k = 0; k < employeeInfo.Count; k++)
                    {
                        switch (k)
                        {
                            case 1:
                                empName = employeeInfo[k].ToString();
                                break;
                            case 2:
                                empSalary = decimal.Parse(employeeInfo[k].ToString());
                                break;
                            case 3:
                                empEmail = employeeInfo[k].ToString();
                                break;
                            case 4:
                                empPhoneNumber = employeeInfo[k].ToString();
                                break;
                            case 5:
                                empIsSatisfactory = bool.Parse(employeeInfo[k].ToString());
                                break;
                            case 6:
                                empHasReview = bool.Parse(employeeInfo[k].ToString());
                                break;
                            case 7:
                                empReview = employeeInfo[k].ToString();
                                break;
                        };
                    }

                    c.DepartmentList[i].EmployeeList.Add(new Employee(empName, empSalary,empEmail,empPhoneNumber));
                    c.DepartmentList[i].EmployeeList[j].IsSatisfactory = empIsSatisfactory;
                    c.DepartmentList[i].EmployeeList[j].HasReview = empHasReview;
                    c.DepartmentList[i].EmployeeList[j].Review = empReview;
                }
            }

            //select or create a department then add to company list

            //perform actions on department level (add/remove employee, simulate review for employee, evaluate department, offer department raise)

            //save company profile on exit
        }
    }
}
