using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TechStudioTest.Models;

namespace TechStudioTest.DataAccess
{
    public class UnitOfWork : IDisposable
    {
        private CompanyContext context = new CompanyContext();
    
        private Repository<Project> projectRepository;
        private Repository<Company> companyRepository;
        private Repository<EmployeeRole> employeeRoleRepository;
        private Repository<Manager> managerRepository;
        private Repository<OfficeAssignment> officeAssignmentRepository;


        public Repository<Project> ProjectRepository
        {
            get
            {

                if (this.projectRepository == null)
                {
                    this.projectRepository = new Repository<Project>(context);
                }
                return projectRepository;
            }
        }
        public Repository<Company> CompanyRepository
        {
            get
            {

                if (this.companyRepository == null)
                {
                    this.companyRepository = new Repository<Company>(context);
                }
                return companyRepository;
            }
        }
        public Repository<EmployeeRole> EmployeeRoleRepository
        {
            get
            {

                if (this.employeeRoleRepository == null)
                {
                    this.employeeRoleRepository = new Repository<EmployeeRole>(context);
                }
                return employeeRoleRepository;
            }
        }
        public Repository<Manager> ManagerRepository
        {
            get
            {

                if (this.managerRepository == null)
                {
                    this.managerRepository = new Repository<Manager>(context);
                }
                return managerRepository;
            }
        }
        public Repository<OfficeAssignment> OfficeAssignmentRepository
        {
            get
            {
                if (this.officeAssignmentRepository == null)
                {
                    this.officeAssignmentRepository = new Repository<OfficeAssignment>(context);
                }
                return officeAssignmentRepository;
            }
        }

        public async Task<int> SaveAsync()
            => await context.SaveChangesAsync();

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}