using System;
using System.Linq.Expressions;
using EmployeeReviewsFront;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmployeeReviews
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddEmployee()
        {
            var d = new Department("Zach's Place");
            var e = new Employee("Zach Ballard", 55000, "zbginji@gmail.com", "(479)650-5231");

            d.AddEmployee(e);

            Assert.AreEqual(1, d.EmployeeList.Count);
        }

        [TestMethod]
        public void TotalSalary()
        {
            var d = new Department("Zach's Place");
            d.AddEmployee(new Employee("Zach Ballard", 55000.50m, "zbginji@gmail.com", "(479)650 - 5231"));
            d.AddEmployee(new Employee("Daniel Pollock", 250000m, "daniel@theironyard.com", "(123)456-7890"));
            d.AddEmployee(new Employee("Dingo FraggleStack", 40000m, "stacksoffraggle@hotmail.com", "(123)456-7890"));

            Assert.AreEqual(345000.50m, d.TotalSalary());
        }

        [TestMethod]
        public void EvaluateEmployeeGood()
        {
            var d = new Department("Zach's Place");
            d.AddEmployee(new Employee("Zach Ballard", 55000.50m, "zbginji@gmail.com", "(479)650-5231"));

            d.EmployeeList[0].Review =
                "Xavier is a huge asset to SciMed and is a pleasure to work with.  He quickly knocks out tasks assigned to him, " +
                "implements code that rarely needs to be revisited, and is always willing to help others despite his heavy workload.  " +
                "When Xavier leaves on vacation, everyone wishes he didn't have to go. Last year, the only concerns with Xavier performance " +
                "were around ownership.  In the past twelve months, he has successfully taken full ownership of both Acme and Bricks, " +
                "Inc.Aside from some false starts with estimates on Acme, clients are happy with his work and responsiveness, which is " +
                "everything that his managers could ask for.";

            d.EvalutateEmployees();

            Assert.AreEqual(true, d.EmployeeList[0].IsSatisfactory);

        }

        [TestMethod]
        public void EvaluateEmployeeBad()
        {
            var d = new Department("Zach's Place");
            d.AddEmployee(new Employee("Zach Ballard", 55000.50m, "zbginji@gmail.com", "(479)650-5231"));

            d.EmployeeList[0].Review =
                "Zeke is a very positive person and encourages those around him, but he has not done well technically this year." +
                "There are two areas in which Zeke has room for improvement.First, when communicating verbally(and sometimes in writing), " +
                "he has a tendency to use more words than are required.This conversational style does put people at ease, which is valuable, " +
                "but it often makes the meaning difficult to isolate, and can cause confusion. Second, when discussing new requirements with " +
                "project managers, less of the information is retained by Zeke long - term than is expected.This has a few negative " +
                "consequences: 1) time is spent developing features that are not useful and need to be re - run, 2) bugs are introduced " +
                "in the code and not caught because the tests lack the same information, and 3) clients are told that certain features " +
                "are complete when they are inadequate.  This communication limitation could be the fault of project management, " +
                "but given that other developers appear to retain more information, this is worth discussing further.";

            d.EvalutateEmployees();

            Assert.AreEqual(false, d.EmployeeList[0].IsSatisfactory);
        }

        [TestMethod]
        public void GiveRaise()
        {
            var d = new Department("Zach's Place");
            d.AddEmployee(new Employee("Zach Ballard", 55000.50m, "zbginji@gmail.com", "(479)650-5231"));
            d.AddEmployee(new Employee("Daniel Pollock", 250000m, "daniel@theironyard.com", "(123)456-7890"));
            d.AddEmployee(new Employee("Dingo FraggleStack", 40000m, "stacksoffraggle@hotmail.com", "(123)456-7890"));

            d.EmployeeList[0].IsSatisfactory = true;
            d.EmployeeList[1].IsSatisfactory = true;
            d.EmployeeList[2].IsSatisfactory = false;

            d.GiveRaise(2000m);

            Assert.AreEqual(40000m, d.EmployeeList[2].Salary);
            Assert.AreEqual(56000.50m, d.EmployeeList[0].Salary);
        }
    }
}
