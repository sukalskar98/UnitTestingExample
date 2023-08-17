using Microsoft.EntityFrameworkCore.Internal;
using UnitTestingExample.Models;

namespace UnitTestingExample.Repository
{
    public interface IEmployeesRepository
    {
        public Task<IEnumerable<Employee>> GetEmployees();

        public Task<Employee> CreateEmployee(Employee employee);

        public Task<Employee?> GetEmployeeById(int? id);

        public Task<Employee> EditEmployee(Employee employee);

        public Task<Employee> DeleteEmployee(Employee employee);
    }
}
