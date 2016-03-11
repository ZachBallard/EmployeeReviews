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

            var saveData = System.IO.File.ReadAllLines(@"App_Data\SaveData.txt")
                                    .Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            var loadedDepartments = new List<Department>();

            if (saveData.Any() && saveData.First().StartsWith("#"))
            {
                //Load the file
                //structure:
                //#Department Name
                //<Employee info>
                //#Department Name 2
                //....

                Department currDepartment = null;

                foreach (var row in saveData)
                {
                    if (row.StartsWith("#"))
                    {
                        //department row!
                        currDepartment = new Department(row.Replace("#", ""));
                        loadedDepartments.Add(currDepartment);
                    }
                    else
                    {
                        //employee info row!
                        string[] columns = row.Split(',');
                        Employee currEmployee = new Employee(columns[0], Convert.ToDecimal(columns[1]), columns[2],
                            columns[3]);
                        currDepartment.AddEmployee(currEmployee);
                    }
                }
            }
            else
            {
                //DON'T LOAD
            }

            while (true)//Main program loop
            {
                int selection = 0;

                //Display department list
                drawDepartments(loadedDepartments);

                //select department, create a department then add to company list, delete department, exit and save
                selection = DepartmentLevelChoice();

                if (selection == 1)
                {

                }

                if (selection == 2 && loadedDepartments.Count > 0)
                {

                }
                else
                {
                    Console.WriteLine("There are no departments to remove!");
                }
                if (selection == 3)
                {

                }
                if (selection == 4)
                {

                }
                //display employee list of selected department

                //perform actions on department level (add/remove employee, simulate review for employee, evaluate department, offer department raise)
                selection = EmployeeLevelChoice();
            }//Main program loop

            //save company profile on exit
        }

        private static int DepartmentLevelChoice()
        {
            while (true)
            {
                Console.WriteLine("==== Department Level Actions ===============================================");
                Console.WriteLine("\n(c)reate department (r)emove department, (s)elect department, (e)xit and save");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "c":
                        return 1;
                    case "r":
                        return 2;
                    case "s":
                        return 3;
                    case "e":
                        return 4;
                    default:
                        Console.WriteLine("> Invalid Selection <");
                        break;
                }
            }
        }

        private static void drawDepartments(List<Department> loadedDepartments)
        {
            for (int i = 0; i < loadedDepartments.Count; i++)
            {
                Console.WriteLine($"i){loadedDepartments[i].Name}");
            }
        }

        private static int EmployeeLevelChoice()
        {
            throw new NotImplementedException();
        }
    }
}
