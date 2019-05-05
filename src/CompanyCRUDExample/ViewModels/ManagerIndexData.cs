using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TechStudioTest.Models;

namespace TechStudioTest.ViewModels
{
    public class ManagerIndexData
    {
        public IEnumerable<Manager> Managers { get; set; }
        public IEnumerable<Project> Projects { get; set; }
        public IEnumerable<EmployeeRole> EmployeeRoles { get; set; }
    }
}