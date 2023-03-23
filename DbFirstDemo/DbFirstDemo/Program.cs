using DbFirstDemo.Data.Models;
using System;
using System.Linq;
using System.Text;

namespace DbFirstDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new SoftUniContext();
            var result = AllEmployees(context);
        }

        private static object AllEmployees(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(x => new
                {
                    x.EmployeeId,
                    x.FirstName,
                    x.LastName,
                    x.MiddleName,
                    x.JobTitle,
                    x.Salary
                })
                .OrderBy(x => x.EmployeeId).ToList();
            var sb = new StringBuilder();
            foreach(var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:f2}");
            }
            var result = sb.ToString().TrimEnd();
            return result;
        }
        private static object EmployeesWithSalaryOver5000(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(x => new
                {
                    x.FirstName,
                    x.Salary
                })
                .OrderBy(x => x.FirstName)
                .Where(x => x.Salary > 5000).ToList();
            var sb = new StringBuilder();
            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} - {employee.Salary:f2}");
            }
            var result = sb.ToString().TrimEnd();
            return result;
        }
        private static object GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.Salary,
                    DepartmentName = x.Department.Name
                })
                .OrderBy(x => x.Salary)
                .ThenByDescending(x => x.FirstName)
                .ToList();
            var sb = new StringBuilder();
            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} from {employee.DepartmentName} - {employee.Salary:F2}");
            }
            var result = sb.ToString().TrimEnd();
            return result;
        }
        public static string GetEmployeesWithFirstNameStartWithN(SoftUniContext context)
        {
            var employees = context.Employees
                 .Select(x => new
                 {
                     x.FirstName,
                     x.LastName,
                     x.Salary
                 })
                 .OrderBy(x => x.FirstName)
                 .Where(x => x.FirstName.StartsWith("N"))
                .ToList();
            var sb = new StringBuilder();
            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} {employee.Salary:f2}");
            }
            var result = sb.ToString().TrimEnd();
            return result;
        }
        public static string GetFirstTenEmployeesWithDepartment(SoftUniContext context)
        {
            var employees = context.Employees
                 .Select(x => new
                 {
                     x.FirstName,
                     x.LastName,
                     DepartmentName = x.Department.Name
                 })
                 .OrderBy(x => x.FirstName)
                 .ThenBy(x => x.LastName)
                 .Take(10)
                .ToList();
            var sb = new StringBuilder();
            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} {employee.DepartmentName}");
            }
            var result = sb.ToString().TrimEnd();
            return result;
        }
        public static string GetEmployeesFromSalesAndMarketing(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var employees = context.Employees
                             .Select(x => new
                             {
                                 x.FirstName,
                                 x.LastName,
                                 x.Salary,
                                 DepartmentName = x.Department.Name
                             })
                             .Where(x => x.DepartmentName == "Sales" || x.DepartmentName == "Marketing").OrderBy(x => x.DepartmentName).ThenByDescending(x => x.Salary);
            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} from {employee.DepartmentName} - {employee.Salary:f2}");
            }
            return sb.ToString().Trim();
        }
        public static void AddNewProject(SoftUniContext context)
        {
            var project = new Project()
            {
             Name = "Judge SYstem",
             StartDate = DateTime.UtcNow
            };

            context.Projects.Add(project);
            context.SaveChanges();

        }
        public static void AddTown(SoftUniContext context)
        {
            var project = new Town()
            {
                Name = "New Town"
            };

            context.Towns.Add(project);
            context.SaveChanges();
        }
        public static void AddEmployeeWithProject(SoftUniContext context)
        {
            var employee = new Employee
            {
                FirstName = "Ani",
                LastName = "Ivanova",
                JobTitle = "Designer",
                HireDate = DateTime.UtcNow,
                Salary = 2000,
                DepartmentId = 2
            };
            context.Employees.Add(employee);

            employee.EmployeesProjects.Add(new EmployeesProject
            {
                Project = new Project
                {
                    Name = "TTT",
                    StartDate = DateTime.UtcNow
                }
            }
            );
            context.SaveChanges();
        }
        public static void UpdateEmployee(SoftUniContext context)
        {
            var employee = context.Employees.FirstOrDefault();
            employee.FirstName = "Alex";
            context.SaveChanges();
        }

        public static void UpdateProject(SoftUniContext context)
        {
            var project = context.Projects.FirstOrDefault(x => x.Name == "TTT");
            project.Name = "Shkolo System";
            project.StartDate = new DateTime(2021, 12, 22);
        }

        public static void DeleteEmployeeProject(SoftUniContext context)
        {
            var employeeProject = context.EmployeesProjects.OrderBy(x => x.EmployeeId == 14).First();

            context.EmployeesProjects.Remove(employeeProject);
            context.SaveChanges();
        }
    }
}
