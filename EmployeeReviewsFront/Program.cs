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

            string saveData = System.IO.File.ReadAllText(@"C:\Users\zbgin\OneDrive\Documents\Visual Studio 2015\Projects\EmployeeReviews
                                                             \EmployeeReviewsFront\SaveData.txt");

            //Example text <%Zach's Palace%#,Zachary Ballard,,55000,,zbginji@gmail.com,,(479)650-5231,,true,,true,,review,#>

            var c = new Company();

            var allDepartmentData = new Regex(@"(?:<)(.+?)(?:>)");
            var departmentData = new Regex(@"(?:%)(.+?)(?:%)");
            var allEmployeeData = new Regex(@"(?:#)(.+?)(?:#)");
            var employeeData = new Regex(@"(?:,)(.+?)(?:,)");

            MatchCollection allDepartment = allDepartmentData.Matches(saveData);
            MatchCollection departmentName = departmentData.Matches(saveData);

            //as we work through the number of departments put a new one with name in departmentlist
            for (int i = 0; i < allDepartment.Count; i++)
            {
                string name = departmentName[i].ToString();
                c.DepartmentList.Add(new Department(name));

                //create list of all employees in current department
                MatchCollection allEmployee = allEmployeeData.Matches(allDepartment[i].ToString());

                //as we work through the employees in each department create a list of information for each and use for new employee
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
                        }
                        ;
                    }

                    c.DepartmentList[i].EmployeeList.Add(new Employee(empName, empSalary,empEmail,empPhoneNumber));
                    c.DepartmentList[i].EmployeeList[j].IsSatisfactory = empIsSatisfactory;
                    c.DepartmentList[i].EmployeeList[j].HasReview = empHasReview;
                    c.DepartmentList[i].EmployeeList[j].Review = empReview;
                }
            }

            while (true)//Main program loop
            {
                int selection = 0;
                
                //Display either department list

                //select department, create a department then add to company list, exit and save
                selection = DepartmentLevelChoice();

                //display employee list of selected department

                //perform actions on department level (add/remove employee, simulate review for employee, evaluate department, offer department raise)
                selection = EmployeeLevelChoice();
            }//Main program loop

            //save company profile on exit
        }

        private static int DepartmentLevelChoice()
        {
            throw new NotImplementedException();
        }

        private static int EmployeeLevelChoice()
        {
            throw new NotImplementedException();
        }
    }
}
