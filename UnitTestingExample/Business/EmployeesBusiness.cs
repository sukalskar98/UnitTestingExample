using UnitTestingExample.Models;
using UnitTestingExample.Repository;

namespace UnitTestingExample.Business
{
    public class EmployeesBusiness: IEmployeesBusiness
    {
        private readonly IEmployeesRepository _repository;
        public EmployeesBusiness(IEmployeesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await this._repository.GetEmployees();
        }

        public async Task<Employee> CreateEmployee(Employee employee)
        {
            return await this._repository.CreateEmployee(employee);
        }

        public async Task<Employee?> GetEmployeeById(int? id)
        {
            return await this._repository.GetEmployeeById(id);
        }

        public async Task<Employee> EditEmployee(Employee employee)
        {
            return await this._repository.EditEmployee(employee);
        }

        public async Task<Employee> DeleteEmployee(Employee employee)
        {
            return await this._repository.DeleteEmployee(employee);
        }
    }
}
