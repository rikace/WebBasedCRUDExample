using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStudioTest.Models
{
    public class OfficeAssignment
    {
        [Key]
        [ForeignKey("Manager")]
        public int ManagerID { get; set; }
        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string Location { get; set; }

        public virtual Manager Manager { get; set; }
    }
}