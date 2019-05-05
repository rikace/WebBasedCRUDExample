using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStudioTest.Models
{
    public class Project
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int ProjectID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name = "Company")]
        public int CompanyID { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<EmployeeRole> EmployeeRoles { get; set; }
        public virtual ICollection<Manager> Managers { get; set; }
    }
}