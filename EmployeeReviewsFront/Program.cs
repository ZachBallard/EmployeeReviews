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

            bool exit = false;
            int departmentSelected = 0;

            while (!exit)//Main program loop
            {
                int selection = 0;

                if (departmentSelected == 0)
                {
                    //Display department list
                    DrawLDepartment(loadedDepartments);

                    //select department, create a department then add to company list, delete department, exit and save
                    selection = DepartmentLevelChoice();

                    if (selection == 1)
                    {
                        DrawLDepartment(loadedDepartments);
                        loadedDepartments.Add(new Department(CreateDepartment()));
                    }

                    if (selection == 2)
                    {
                        if (loadedDepartments.Count > 0)
                        {
                            while (true)
                            {
                                DrawLDepartment(loadedDepartments);

                                selection = RemoveWhichDepartment();

                                // will not allow you to delete dept if only 1???

                                if (selection == 0)
                                {
                                    break;
                                }

                                if (loadedDepartments.Count >= selection-- && selection >= 0)
                                {
                                    loadedDepartments.RemoveAt(selection);
                                    break;
                                }

                                Console.WriteLine("\nCannot remove nonexisting department. >Press Enter<");
                                Console.ReadLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nThere are no departments to remove! >Press Enter<");
                            Console.ReadLine();
                        }
                    }

                    if (selection == 3)
                    {
                        if (loadedDepartments.Count > 0)
                        {
                            while (true)
                            {
                                DrawLDepartment(loadedDepartments);

                                departmentSelected = SelectWhichDepartment();

                                if (loadedDepartments.Count >= departmentSelected)
                                {
                                    break;
                                }

                                Console.WriteLine("\nInvalid department selection. >Press Enter<");
                                Console.ReadLine();
                            }
                        }
                    }
                    if (selection == 4)
                    {
                        exit = true;
                    }
                }


                if (!exit && departmentSelected > 0)
                {
                    while (true)
                    {
                        //display employee list of selected department
                        if (loadedDepartments[departmentSelected].EmployeeList.Any())
                        {
                            DrawEmployees(loadedDepartments[departmentSelected].EmployeeList);
                        }
                        //perform actions on employee level (add/remove employee, simulate review for employee, 
                        //evaluate department, offer department raise)
                        selection = EmployeeLevelChoice();

                        if (selection == 1)
                        {
                            
                        }

                        if (selection == 2)
                        {
                            
                        }

                        if (selection == 3)
                        {
                            
                        }

                        if (selection == 4)
                        {
                            
                        }

                        if (selection == 5)
                        {
                            
                        }

                        if (selection == 6)
                        {
                            departmentSelected = 0;
                            break;
                        }
                    }
                }

            }//Main program loop

            //save company profile on exit
        }

        private static int SelectWhichDepartment()
        {
            Console.WriteLine("\n==== Department Level Actions ===============================================");

            while (true)
            {
                Console.WriteLine("\nPlease type the number of the department to explore. (0 to go back)");
                string userInput = Console.ReadLine();

                var selection = 0;
                if (int.TryParse(userInput, out selection))
                {
                    return selection;
                }

                Console.WriteLine("\nInvalid selection. >Press Enter<");
                Console.ReadLine();
            }
        }

        private static int RemoveWhichDepartment()
        {
            Console.WriteLine("==== Department Level Actions ===============================================");

            while (true)
            {

                Console.WriteLine("\nPlease type the number of the department to delete. (0 to go back)");
                string userInput = Console.ReadLine();

                var selection = 0;
                if (int.TryParse(userInput, out selection))
                {
                    return selection;
                }
            }
        }

        private static string CreateDepartment()
        {
            Console.WriteLine("==== Department Level Actions ===============================================");
            Console.WriteLine("\nPlease type a name for the new department.");
            string userInput = Console.ReadLine();

            return userInput;
        }

        private static int DepartmentLevelChoice()
        {
            Console.WriteLine("==== Department Level Actions ===============================================");

            while (true)
            {

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
                        Console.WriteLine("Invalid Selection. >Press Enter<");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private static void DrawEmployees(List<Employee> listToDisplay)
        {
            Console.Clear();

            for (int i = 0; i < listToDisplay.Count; i++)
            {
                Console.WriteLine($"{i + 1}){listToDisplay[i].Name}");
                Console.WriteLine($"   ${listToDisplay[i].Salary}/yr, {listToDisplay[i].Email}, {listToDisplay[i].PhoneNum}");
                Console.WriteLine($"   Has Review: {listToDisplay[i].HasReview}, Is Satisfactory:{listToDisplay[i].IsSatisfactory}");
            }
        }

        private static void DrawLDepartment(List<Department> listToDisplay)
        {
            Console.Clear();

            for (int i = 0; i < listToDisplay.Count; i++)
            {
                Console.WriteLine($"{i + 1}){listToDisplay[i].Name}");
            }
        }

        private static int EmployeeLevelChoice()
        {

            Console.WriteLine("==== Employee Level Actions ====================================================");

            while (true)
            {

                Console.WriteLine("\n(a)dd employee, (r)emove employee, (s)imulate review, (d)epartment eval, (g)ive raises. (0 to go back)");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "a":
                        return 1;
                    case "r":
                        return 2;
                    case "s":
                        return 3;
                    case "d":
                        return 4;
                    case "g":
                        return 5;
                    case "0":
                        return 6;
                    default:
                        Console.WriteLine("Invalid Selection. >Press Enter<");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
