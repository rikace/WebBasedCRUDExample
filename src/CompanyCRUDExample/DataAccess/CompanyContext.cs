using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using TechStudioTest.Models;

namespace TechStudioTest.DataAccess
{
    public class CompanyContext : DbContext
    {
        public CompanyContext() : base("name=CompanyContext")
        {
            // Lazy loading can be turned off for all entities in the context by setting this flag 
            // "LazyLoadingEnabled" to false 
            this.Configuration.LazyLoadingEnabled = false;
        }


        public DbSet<Project> Projects { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Project>()
                .HasMany(c => c.Managers).WithMany(i => i.Projects)
                .Map(t => t.MapLeftKey("ProjectID")
                    .MapRightKey("ManagerID")
                    .ToTable("ProjectManager"));
        }
    }
}
