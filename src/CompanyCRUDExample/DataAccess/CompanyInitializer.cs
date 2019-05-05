using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TechStudioTest.Models;

namespace TechStudioTest.DataAccess
{
    public class CompanyInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CompanyContext>
    {
        protected override void Seed(CompanyContext context)
        {
            var employees = new List<Employee>
            {
                new Employee { FirstName = "Carson", LastName = "Alexander",
                    Department = "Artificial Intelligence", HireDate = DateTime.Parse("2012-09-03") },
                new Employee { FirstName = "Meredith", LastName = "Alonso",
                    Department = "Image Processing", HireDate = DateTime.Parse("2012-09-01") },
                new Employee { FirstName = "Arturo", LastName = "Anand",
                    Department = "Augmented Reality", HireDate = DateTime.Parse("2013-09-01") },
                new Employee { FirstName = "Gytis", LastName = "Barzdukas",
                    Department = "Designing", HireDate = DateTime.Parse("2012-09-01") },
                new Employee { FirstName = "Yan", LastName = "Li",
                    Department = "Geofencing", HireDate = DateTime.Parse("2012-09-01") },
                new Employee { FirstName = "Peggy", LastName = "Justice",
                    Department = "Geographic Process", HireDate = DateTime.Parse("2011-09-01") },
                new Employee { FirstName = "Laura",    LastName = "Norman",
                    Department = "Map", HireDate = DateTime.Parse("2013-09-01") },
                new Employee { FirstName = "Nino",     LastName = "Olivetto",
                    Department = "3D Modelling", HireDate = DateTime.Parse("2005-09-01") }
            };
            employees.ForEach(e => context.Employees.Add(e));
            context.SaveChanges();

            var managers = new List<Manager>
            {
                new Manager { FirstName = "Jim", LastName = "Abercrombie" },
                new Manager { FirstName = "Fadi", LastName = "Fakhouri" },
                new Manager { FirstName = "Roger", LastName = "Harui" },
                new Manager { FirstName = "Candace", LastName = "Kapoor" },
                new Manager { FirstName = "Roger", LastName = "Zheng" }
            };
            managers.ForEach(m => context.Managers.Add(m));
            context.SaveChanges();

            var companies = new List<Company>
            {
                new Company { Name = "Rock Ltd", ManagerID = managers.Single(i => i.LastName == "Abercrombie").ManagerID },
                new Company { Name = "Meruc Ltd", ManagerID = managers.Single(i => i.LastName == "Fakhouri").ManagerID },
                new Company { Name = "Enginc Ltd", ManagerID = managers.Single(i => i.LastName == "Harui").ManagerID },
                new Company { Name = "Econ Ltd", ManagerID = managers.Single(i => i.LastName == "Kapoor").ManagerID }
            };
            companies.ForEach(c => context.Companies.Add(c));
            context.SaveChanges();

            var projects = new List<Project>
            {
                new Project { ProjectID = 1050, Title = "RestUni",
                    Description = "Restaurant Management System",
                    CompanyID = companies.Single(c => c.Name == "Enginc Ltd").CompanyID,
                    Managers = new List<Manager>()
                },
                new Project { ProjectID = 4022, Title = "Freebirds",
                    Description = "Restaurant HealthCare System",
                    CompanyID = companies.Single( s => s.Name == "Econ Ltd").CompanyID,
                    Managers = new List<Manager>()
                },
                new Project { ProjectID = 4041, Title = "SalesMerlyn",
                    Description = "Employee Management System",
                    CompanyID = companies.Single( s => s.Name == "Econ Ltd").CompanyID,
                    Managers = new List<Manager>()
                },
                new Project { ProjectID = 1045, Title = "JackInTheBox",
                    Description = "Restaurant Management System",
                    CompanyID = companies.Single( s => s.Name == "Meruc Ltd").CompanyID,
                    Managers = new List<Manager>()
                },
                new Project { ProjectID = 3141, Title = "MedExpress",
                    Description = "Medical HealthCare System",
                    CompanyID = companies.Single( s => s.Name == "Meruc Ltd").CompanyID,
                    Managers = new List<Manager>()
                },
                new Project { ProjectID = 2021, Title = "GameChanger",
                    Description = "Game Platform",
                    CompanyID = companies.Single( s => s.Name == "Rock Ltd").CompanyID,
                    Managers = new List<Manager>()
                },
                new Project { ProjectID = 2042, Title = "FunnelFlows",
                    Description = "Funnel Creators",
                    CompanyID = companies.Single( s => s.Name == "Rock Ltd").CompanyID,
                    Managers = new List<Manager>()
                },
            };
            projects.ForEach(s => context.Projects.Add(s));
            context.SaveChanges();

            var officeAssignments = new List<OfficeAssignment>
            {
                new OfficeAssignment {
                    ManagerID = managers.Single( i => i.LastName == "Fakhouri").ManagerID,
                    Location = "Smith 17"
                },
                new OfficeAssignment {
                    ManagerID = managers.Single( i => i.LastName == "Harui").ManagerID,
                    Location = "Gowan 27"
                },
                new OfficeAssignment {
                    ManagerID = managers.Single( i => i.LastName == "Kapoor").ManagerID,
                    Location = "Thompson 304"
                },
            };
            officeAssignments.ForEach(s => context.OfficeAssignments.Add(s));
            context.SaveChanges();

            AddOrUpdateManager(context, "RestUni", "Kapoor");
            AddOrUpdateManager(context, "RestUni", "Harui");
            AddOrUpdateManager(context, "Freebirds", "Zheng");
            AddOrUpdateManager(context, "Freebirds", "Zheng");

            AddOrUpdateManager(context, "SalesMerlyn", "Fakhouri");
            AddOrUpdateManager(context, "JackInTheBox", "Harui");
            AddOrUpdateManager(context, "MedExpress", "Abercrombie");
            AddOrUpdateManager(context, "GameChanger", "Abercrombie");

            context.SaveChanges();

            var employeeRoles = new List<EmployeeRole>
            {
                new EmployeeRole {
                    EmployeeID = employees.Single(s => s.LastName == "Alexander").EmployeeID,
                    ProjectID = projects.Single(c => c.Title == "RestUni").ProjectID,
                    Role = Role.Designer
                },
                new EmployeeRole {
                    EmployeeID = employees.Single(s => s.LastName == "Alexander").EmployeeID,
                    ProjectID = projects.Single(c => c.Title == "Freebirds").ProjectID,
                    Role = Role.Developer
                },
                new EmployeeRole {
                    EmployeeID = employees.Single(s => s.LastName == "Alexander").EmployeeID,
                    ProjectID = projects.Single(c => c.Title == "Freebirds").ProjectID,
                    Role = Role.DevOps
                },
                new EmployeeRole {
                    EmployeeID = employees.Single(s => s.LastName == "Alonso").EmployeeID,
                    ProjectID = projects.Single(c => c.Title == "SalesMerlyn").ProjectID,
                    Role = Role.Tester
                },
                new EmployeeRole {
                    EmployeeID = employees.Single(s => s.LastName == "Alonso").EmployeeID,
                    ProjectID = projects.Single(c => c.Title == "JackInTheBox" ).ProjectID,
                    Role = Role.Marketer
                },
                new EmployeeRole {
                    EmployeeID = employees.Single(s => s.LastName == "Alonso").EmployeeID,
                    ProjectID = projects.Single(c => c.Title == "MedExpress" ).ProjectID,
                    Role = Role.Developer
                },
                 new EmployeeRole {
                    EmployeeID = employees.Single(s => s.LastName == "Anand").EmployeeID,
                    ProjectID = projects.Single(c => c.Title == "RestUni" ).ProjectID
                 },
                 new EmployeeRole {
                    EmployeeID = employees.Single(s => s.LastName == "Anand").EmployeeID,
                    ProjectID = projects.Single(c => c.Title == "Freebirds").ProjectID,
                    Role = Role.DevOps
                 },
                new EmployeeRole {
                    EmployeeID = employees.Single(s => s.LastName == "Barzdukas").EmployeeID,
                    ProjectID = projects.Single(c => c.Title == "RestUni").ProjectID,
                    Role = Role.Developer
                 },
                 new EmployeeRole {
                     EmployeeID = employees.Single(s => s.LastName == "Li").EmployeeID,
                     ProjectID = projects.Single(c => c.Title == "MedExpress").ProjectID,
                     Role = Role.Marketer
                 },
                 new EmployeeRole {
                     EmployeeID = employees.Single(s => s.LastName == "Justice").EmployeeID,
                     ProjectID = projects.Single(c => c.Title == "GameChanger").ProjectID,
                     Role = Role.Designer
                 }
            };

            foreach (EmployeeRole e in employeeRoles)
            {
                var roleInDataBase = context.EmployeeRoles.Where(
                    s =>
                         s.Employee.EmployeeID == e.EmployeeID &&
                         s.Project.ProjectID == e.ProjectID).SingleOrDefault();
                if (roleInDataBase == null)
                {
                    context.EmployeeRoles.Add(e);
                }
            }
            context.SaveChanges();
        }

        void AddOrUpdateManager(CompanyContext context, string projectTitle, string managerName)
        {
            var crs = context.Projects.SingleOrDefault(c => c.Title == projectTitle);
            var inst = crs.Managers.SingleOrDefault(i => i.LastName == managerName);
            if (inst == null)
                crs.Managers.Add(context.Managers.Single(i => i.LastName == managerName));
        }
    }
}