using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStudioTest.Models
{
    public class Manager
    {
        public int ManagerID { get; set; }

        [Display(Name = "Last Name"), StringLength(50, MinimumLength = 1)]
        public string LastName { get; set; }

        [Column("FirstName"), Display(Name = "First Name"), StringLength(50, MinimumLength = 1)]
        public string FirstName { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public virtual ICollection<Project> Projects { get; set; }
        public virtual OfficeAssignment OfficeAssignment { get; set; }
    }
}