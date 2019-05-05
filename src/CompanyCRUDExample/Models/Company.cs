using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStudioTest.Models
{
    public class Company
    {
        public int CompanyID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Administrator")]
        public int? ManagerID { get; set; }

        public virtual Manager Administrator { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}