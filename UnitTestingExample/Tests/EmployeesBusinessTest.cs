using AutoFixture;
using Moq;
using UnitTestingExample.Business;
using UnitTestingExample.Models;
using UnitTestingExample.Repository;
using Xunit;

namespace UnitTestingExample.Tests
{ 
    public class EmployeesBusinessTest
    {
        private readonly Fixture fixture;

        private readonly Mock<IEmployeesRepository> mockRepository;

        private readonly IEmployeesBusiness employeesBusiness;

        public EmployeesBusinessTest()
        {
            fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            mockRepository = new Mock<IEmployeesRepository>();
            employeesBusiness = new EmployeesBusiness(mockRepository.Object);
        }

        [Fact]
        public async Task GetEmployees_ShouldGetEmployees()
        {
            // Arrange
            var employees = fixture.CreateMany<Employee>();
            this.mockRepository.Setup(x => x.GetEmployees()).ReturnsAsync(employees);

            // Act
            var result = await employeesBusiness.GetEmployees();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employees.FirstOrDefault()?.Id, result.FirstOrDefault()?.Id);
        }

        [Fact]
        public async Task GetEmployeeById_ShouldGet()
        {
            // Arrange
            var employee = fixture.Create<Employee>();
            this.mockRepository.Setup(x => x.GetEmployeeById(It.IsAny<int?>())).ReturnsAsync(employee);

            // Act
            var result = await employeesBusiness.GetEmployeeById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employee.Id, result.Id);
        }

        [Fact]
        public async Task CreateEmployee_ShouldCreate()
        {
            // Arrange
            var employee = fixture.Create<Employee>();
            this.mockRepository.Setup(x => x.CreateEmployee(It.IsAny<Employee>())).ReturnsAsync(employee);

            // Act
            var result = await employeesBusiness.CreateEmployee(employee);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employee.Id, result.Id);
        }

        [Fact]
        public async Task EditEmployee_ShouldEdit()
        {
            // Arrange
            var employee = fixture.Create<Employee>();
            this.mockRepository.Setup(x => x.EditEmployee(It.IsAny<Employee>())).ReturnsAsync(employee);

            // Act
            var result = await employeesBusiness.EditEmployee(employee);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employee.Id, result.Id);
        }

        [Fact]
        public async Task DeleteEmployee_ShouldDelete()
        {
            // Arrange
            var employee = fixture.Create<Employee>();
            this.mockRepository.Setup(x => x.DeleteEmployee(It.IsAny<Employee>())).ReturnsAsync(employee);

            // Act
            var result = await employeesBusiness.DeleteEmployee(employee);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employee.Id, result.Id);
        }
    }
}
