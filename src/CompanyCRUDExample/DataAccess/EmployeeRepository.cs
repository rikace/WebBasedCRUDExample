using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using TechStudioTest.Models;
using TechStudioTest.Utilities;

namespace TechStudioTest.DataAccess
{
    public class EmployeeRepository : IEmployeeRepository, IDisposable
    {
        private CompanyContext context;

        public EmployeeRepository(CompanyContext context)
        {
            this.context = context;
        }

        public async Task<List<Employee>> GetEmployeesAsync(params Expression<Func<Employee, object>>[] includes)
            => await context.Employees.IncludeMulti(includes).ToListAsync();

        public async Task<Employee> GetEmployeeByIDAsync(int id, params Expression<Func<Employee, object>>[] includes) 
            => await context.Employees.IncludeMulti(includes).FirstOrDefaultAsync(e => e.EmployeeID == id);

        public void InsertEmployee(Employee Employee) => context.Employees.Add(Employee);

        public async Task DeleteEmployeeAsync(int EmployeeID)
        {
            Employee Employee = await context.Employees.FindAsync(EmployeeID);
            context.Employees.Remove(Employee);
        }

        public void UpdateEmployee(Employee Employee)
            => context.Entry(Employee).State = EntityState.Modified;

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