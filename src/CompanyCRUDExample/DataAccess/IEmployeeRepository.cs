using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechStudioTest.DataAccess
{
    using System.Data.Entity;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using TechStudioTest.Models;

    public interface IEmployeeRepository : IDisposable
    {
        Task<List<Employee>> GetEmployeesAsync(params Expression<Func<Employee, object>>[] includes);
        Task<Employee> GetEmployeeByIDAsync(int employeeId, params Expression<Func<Employee, object>>[] includes);
        void InsertEmployee(Employee employee);
        Task DeleteEmployeeAsync(int employeeID);
        void UpdateEmployee(Employee employee);
        Task<int> SaveAsync();
    }
}