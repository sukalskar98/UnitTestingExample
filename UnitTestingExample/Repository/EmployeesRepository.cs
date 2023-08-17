using Microsoft.EntityFrameworkCore;
using UnitTestingExample.Models;

namespace UnitTestingExample.Repository
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly DemoDb2Context _context;

        public EmployeesRepository(DemoDb2Context context)
        {
            _context = context;
        }

        public async Task<Employee> CreateEmployee(Employee employee)
        {
            this._context.Add(employee);
            await this._context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> DeleteEmployee(Employee employee)
        {
            this._context.Remove(employee);
            await this._context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> EditEmployee(Employee employee)
        {
            this._context.Update(employee);
            await this._context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> GetEmployeeById(int? id)
        {
            if (id == null) return null;
            return await _context.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var employees = await _context.Employees.AsNoTracking().ToListAsync();
            return employees;
        }
    }
}
