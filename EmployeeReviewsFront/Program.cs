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
            bool isDepartmentLevel = true;

            while (!exit)//Main program loop
            {
                int selection = 0;

                if (isDepartmentLevel)
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

                                if (selection == 0)
                                {
                                    break;
                                }

                                //cannot remove if only one depatment???
                                if (loadedDepartments.Count >= selection-- && selection > 0)
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

                                if (loadedDepartments.Count >= departmentSelected && departmentSelected > 0)
                                {
                                    isDepartmentLevel = false;
                                    break;
                                }
                                if (departmentSelected == 0)
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

                if (!exit && !isDepartmentLevel)
                {
                    departmentSelected -= 1;

                    while (true)
                    {
                        //display employee list of selected department

                        DrawEmployees(loadedDepartments[departmentSelected].EmployeeList);

                        //perform actions on employee level (add/remove employee, simulate review for employee, 
                        //evaluate department, offer department raise)
                        selection = EmployeeLevelChoice();

                        if (selection == 1)
                        {
                            string name = "";
                            decimal salary = 0m;
                            string email = "";
                            string phonenNumber = "";

                            name = GetEmployeeName();
                            salary = GetEmployeeSalary();
                            email = GetEmployeeEmail();
                            phonenNumber = GetEmployeePhoneNumber();

                            loadedDepartments[departmentSelected].EmployeeList.Add(new Employee(name, salary, email, phonenNumber));
                        }

                        if (selection == 2)
                        {
                            if (loadedDepartments[departmentSelected].EmployeeList.Count > 0)
                            {
                                while (true)
                                {
                                    DrawEmployees(loadedDepartments[departmentSelected].EmployeeList);

                                    selection = RemoveEmployee();

                                    if (selection == 0)
                                    {
                                        break;
                                    }

                                    //cannot remove if only one Employee???
                                    if (loadedDepartments[departmentSelected].EmployeeList.Count >= selection-- && selection > 0)
                                    {
                                        loadedDepartments[departmentSelected].EmployeeList.RemoveAt(selection);
                                        break;
                                    }

                                    Console.WriteLine("\nCannot remove nonexisting employee. >Press Enter<");
                                    Console.ReadLine();
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nThere are no employees to remove! >Press Enter<");
                                Console.ReadLine();
                            }
                        }

                        if (selection == 3)
                        {
                            if (loadedDepartments[departmentSelected].EmployeeList.Count > 0)
                            {
                                while (true)
                                {
                                    DrawEmployees(loadedDepartments[departmentSelected].EmployeeList);

                                    selection = SimulateReviewForWhichEmployee();

                                    if (selection == 0)
                                    {
                                        break;
                                    }

                                    if (loadedDepartments[departmentSelected].EmployeeList.Count >= selection && selection > 0)
                                    {
                                        int typeOfReview = TypeOfReview();
                                        selection -= 1;

                                        if (typeOfReview == 1)
                                        {

                                            loadedDepartments[departmentSelected].EmployeeList[selection].Review =
                                                "good";
                                            loadedDepartments[departmentSelected].EmployeeList[selection].HasReview =
                                                true;
                                            break;
                                        }

                                        if (typeOfReview == 2)
                                        {
                                            loadedDepartments[departmentSelected].EmployeeList[selection].Review =
                                                "bad";
                                            loadedDepartments[departmentSelected].EmployeeList[selection].HasReview =
                                                true;
                                            break;
                                        }
                                    }

                                    Console.WriteLine("\nCannot simulate review with nonexisting employee. >Press Enter<");
                                    Console.ReadLine();
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nThere are no employees to simulate a review for! >Press Enter<");
                                Console.ReadLine();
                            }
                        }

                        if (selection == 4)
                        {
                            if (loadedDepartments[departmentSelected].EmployeeList.Count > 0)
                            {
                                while (true)
                                {
                                    DrawEmployees(loadedDepartments[departmentSelected].EmployeeList);

                                    loadedDepartments[departmentSelected].EvalutateEmployees();
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("There are no employees to evaluate! >Press Enter<");
                                Console.ReadLine();
                            }
                        }

                        if (selection == 5)
                        {
                            if (loadedDepartments[departmentSelected].EmployeeList.Count > 0)
                            {
                                while (true)
                                {
                                    DrawEmployees(loadedDepartments[departmentSelected].EmployeeList);

                                    loadedDepartments[departmentSelected].GiveRaise(GetRaiseAmount());
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("There are no employees to give a raise to! >Press Enter<");
                                Console.ReadLine();
                            }
                        }

                        if (selection == 6)
                        {
                            isDepartmentLevel = true;
                            break;
                        }
                    }
                }

            }//Main program loop

            //save company profile on exit
        }

        private static int TypeOfReview()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Employee Level Actions ====================================================");

                Console.WriteLine("\nWill this be a (g)ood review or a (b)ad review?");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "g":
                        return 1;
                    case "b":
                        return 2;
                    default:
                        Console.WriteLine("That was an invalid selection. >Press Enter<");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private static int SimulateReviewForWhichEmployee()
        {
            Console.WriteLine("==== Employee Level Actions ====================================================");

            while (true)
            {

                Console.WriteLine("\nPlease type the number of the employee to simulate review. (0 to go back)");
                string userInput = Console.ReadLine();

                var selection = 0;
                if (int.TryParse(userInput, out selection))
                {
                    return selection;
                }
            }
        }

        private static decimal GetRaiseAmount()
        {
            while (true)
            {
                Console.WriteLine("==== Employee Level Actions ====================================================");
                Console.WriteLine("\nPlease type a raise amount to offer this department's satisfactory employees.");
                string userInput = Console.ReadLine();
                decimal salary = 0m;

                if (decimal.TryParse(userInput, out salary))
                {
                    return salary;
                }

                Console.WriteLine("That is not a valid raise amount. >Press Enter<");
                Console.ReadLine();
            }
        }

        private static int RemoveEmployee()
        {
            Console.WriteLine("==== Employee Level Actions ====================================================");

            while (true)
            {

                Console.WriteLine("\nPlease type the number of the employee to delete. (0 to go back)");
                string userInput = Console.ReadLine();

                var selection = 0;
                if (int.TryParse(userInput, out selection))
                {
                    return selection;
                }
            }
        }

        private static string GetEmployeePhoneNumber()
        {
            Console.Clear();
            Console.WriteLine("==== Employee Level Actions ====================================================");
            Console.WriteLine("\nPlease type a phone number for the new employee.");
            string userInput = Console.ReadLine();

            return userInput;
        }

        private static decimal GetEmployeeSalary()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Employee Level Actions ====================================================");
                Console.WriteLine("\nPlease type a salary for the new employee.");
                string userInput = Console.ReadLine();
                decimal salary = 0m;
                if (decimal.TryParse(userInput, out salary))
                {
                    return salary;
                }

                Console.WriteLine("That is not a valid salary. >Press Enter<");
                Console.ReadLine();
            }
        }

        private static string GetEmployeeEmail()
        {
            Console.Clear();
            Console.WriteLine("==== Employee Level Actions ====================================================");
            Console.WriteLine("\nPlease type a email for the new employee.");
            string userInput = Console.ReadLine();

            return userInput;
        }

        private static string GetEmployeeName()
        {
            Console.Clear();
            Console.WriteLine("==== Employee Level Actions ====================================================");
            Console.WriteLine("\nPlease type a name for the new employee.");
            string userInput = Console.ReadLine();

            return userInput;
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
                Console.WriteLine($"   Has Review: {listToDisplay[i].HasReview}, Is Satisfactory: {listToDisplay[i].IsSatisfactory}");
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
