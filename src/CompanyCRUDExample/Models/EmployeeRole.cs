using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TechStudioTest.Models
{
    public enum Role
    {
        Developer, Designer, Tester, Marketer, DevOps
    }

    public class EmployeeRole
    {
        public int EmployeeRoleID { get; set; }
        public int ProjectID { get; set; }
        public int EmployeeID { get; set; }

        [DisplayFormat(NullDisplayText = "No role")]
        public Role? Role { get; set; }

        public virtual Project Project { get; set; }
        public virtual Employee Employee { get; set; }
    }
}