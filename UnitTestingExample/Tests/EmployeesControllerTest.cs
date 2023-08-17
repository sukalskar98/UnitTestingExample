using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using UnitTestingExample.Business;
using UnitTestingExample.Controllers;
using UnitTestingExample.Models;
using Xunit;

namespace UnitTestingExample.Tests
{
    public class EmployeesControllerTest
    {
        private readonly Fixture fixture;

        private readonly Mock<IEmployeesBusiness> mockBusiness;

        private readonly EmployeesController controller;

        public EmployeesControllerTest()
        {
            this.fixture = new Fixture();
            this.fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            this.mockBusiness = new Mock<IEmployeesBusiness>();
            this.controller = new EmployeesController(this.mockBusiness.Object);
        }

        [Fact]
        public async Task GetEmployees_ShouldGetAllEmployees()
        {
            // Arrange
            var employees = this.fixture.CreateMany<Employee>();
            this.mockBusiness.Setup(x => x.GetEmployees()).ReturnsAsync(employees);

            // Act
            var result = await this.controller.Index();
            var values = (result as ViewResult)?.Model as IEnumerable<Employee>;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employees.Count(), values?.Count());
            Assert.Equal(employees.FirstOrDefault()?.Id, values?.FirstOrDefault()?.Id);
        }

        [Fact]
        public async Task GetEmployees_ShouldReturnProblemOnNull()
        {
            // Arrage

            // Act
            var result = await this.controller.Index();
            var value = (result as ViewResult)?.Model as IEnumerable<Employee>;

            // Assert
            Assert.Empty(value ?? new List<Employee>());
        }

        [Fact]
        public async Task GetEmployeeById_ShouldReturnEmployeeId()
        {
            // Arrage
            var employee = this.fixture.Create<Employee>();
            this.mockBusiness.Setup(x => x.GetEmployeeById(It.IsAny<int?>())).ReturnsAsync(employee);

            // Act
            var result = await this.controller.Details(1);
            var value = (result as ViewResult)?.Model as Employee;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employee.Id, value?.Id);
        }

        [Fact]
        public async Task GetEmployeeById_ShouldReturnNotFoundOnNullId()
        {
            // Arrage
            int notFoundStatusCode = 404;

            // Act
            var result = await this.controller.Details(null);
            var value = (result as NotFoundResult)?.StatusCode;

            // Assert
            Assert.Equal(notFoundStatusCode, value);
        }

        [Fact]
        public async Task GetEmployeeById_ShouldReturnNotFoundOnNullResult()
        {
            // Arrage
            int notFoundStatusCode = 404;

            // Act
            var result = await this.controller.Details(1);
            var value = (result as NotFoundResult)?.StatusCode;

            // Assert
            Assert.Equal(notFoundStatusCode, value);
        }

        [Fact]
        public async Task CreateEmployee_ShouldCreateEmployee()
        {
            // Arrage
            var employee = this.fixture.Create<Employee>();
            this.mockBusiness.Setup(x => x.CreateEmployee(It.IsAny<Employee>())).ReturnsAsync(employee);

            // Act
            var result = await this.controller.Create(employee);
            var value = result as RedirectToActionResult;

            // Assert
            Assert.NotNull(value);
        }

        [Fact]
        public async Task EditEmployee_ShouldEditEmployee()
        {
            // Arrage
            var employee = this.fixture.Create<Employee>();
            this.mockBusiness.Setup(x => x.GetEmployeeById(It.IsAny<int>())).ReturnsAsync(employee);

            // Act
            var result = await this.controller.Edit(1);
            var value = (result as ViewResult)?.Model as Employee;

            // Assert
            Assert.NotNull(value);
            Assert.Equal(employee.Id, value.Id);
        }

        [Fact]
        public async Task EditEmployee_ShouldReturnNotFoundOnNullId()
        {
            // Arrage
            int notFoundStatusCode = 404;

            // Act
            var result = await this.controller.Edit(null);
            var value = (result as NotFoundResult)?.StatusCode;

            // Assert
            Assert.Equal(notFoundStatusCode, value);
        }

        [Fact]
        public async Task EditEmployee_ShouldReturnNotFoundOnNullResult()
        {
            // Arrage
            int notFoundStatusCode = 404;

            // Act
            var result = await this.controller.Edit(1);
            var value = (result as NotFoundResult)?.StatusCode;

            // Assert
            Assert.Equal(notFoundStatusCode, value);
        }

        [Fact]
        public async Task EditEmployee_ShouldEditEmployeeAndUpdate()
        {
            // Arrage
            var employee = this.fixture.Create<Employee>();
            this.mockBusiness.Setup(x => x.EditEmployee(It.IsAny<Employee>())).ReturnsAsync(employee);

            // Act
            var result = await this.controller.Edit(employee.Id, employee);
            var value = result as RedirectToActionResult;

            // Assert
            Assert.NotNull(value);
        }

        [Fact]
        public async Task EditEmployee_ShouldReturnNotFoundOnException()
        {
            // Arrage
            int notFoundStatusCode = 404;
            var employee = this.fixture.Create<Employee>();
            var employee2 = this.fixture.Create<Employee>();
            var exception = new DbUpdateConcurrencyException();
            this.mockBusiness.Setup(x => x.EditEmployee(It.IsAny<Employee>())).ThrowsAsync(exception);
            this.mockBusiness.Setup(x => x.GetEmployees()).ReturnsAsync(new List<Employee> { employee2 });

            // Act
            var result = await this.controller.Edit(employee.Id, employee);
            var value = (result as NotFoundResult)?.StatusCode;

            // Assert
            Assert.Equal(notFoundStatusCode, value);
        }

        [Fact]
        public async Task EditEmployee_ShouldReturnNotFoundOnException2()
        {
            // Arrage
            var employee = this.fixture.Create<Employee>();
            var exception = new DbUpdateConcurrencyException();
            this.mockBusiness.Setup(x => x.EditEmployee(It.IsAny<Employee>())).ThrowsAsync(exception);
            this.mockBusiness.Setup(x => x.GetEmployees()).ReturnsAsync(new List<Employee> { employee });

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(() => this.controller.Edit(employee.Id, employee));   
        }

        [Fact]
        public async Task EditEmployee_ShouldReturnNotFoundOnInvalidId()
        {
            // Arrage
            int notFoundStatusCode = 404;
            var employee = this.fixture.Create<Employee>();

            // Act
            var result = await this.controller.Edit(1, employee);
            var value = (result as NotFoundResult)?.StatusCode;

            // Assert
            Assert.Equal(notFoundStatusCode, value);
        }

        [Fact]
        public async Task DeleteEmployee_ShouldEditEmployee()
        {
            // Arrage
            var employee = this.fixture.Create<Employee>();
            this.mockBusiness.Setup(x => x.GetEmployeeById(It.IsAny<int>())).ReturnsAsync(employee);

            // Act
            var result = await this.controller.Delete(1);
            var value = (result as ViewResult)?.Model as Employee;

            // Assert
            Assert.NotNull(value);
            Assert.Equal(employee.Id, value.Id);
        }

        [Fact]
        public async Task DeleteEmployee_ShouldReturnNotFoundOnNullId()
        {
            // Arrage
            int notFoundStatusCode = 404;

            // Act
            var result = await this.controller.Delete(null);
            var value = (result as NotFoundResult)?.StatusCode;

            // Assert
            Assert.Equal(notFoundStatusCode, value);
        }

        [Fact]
        public async Task DeleteEmployee_ShouldReturnNotFoundOnNullResult()
        {
            // Arrage
            int notFoundStatusCode = 404;

            // Act
            var result = await this.controller.Delete(1);
            var value = (result as NotFoundResult)?.StatusCode;

            // Assert
            Assert.Equal(notFoundStatusCode, value);
        }

        [Fact]
        public async Task DeleteEmployee_ShouldDeleteAndUpdate()
        {
            // Arrange
            var employee = this.fixture.Create<Employee>();
            this.mockBusiness.Setup(x => x.GetEmployees()).ReturnsAsync(new List<Employee> { employee });
            this.mockBusiness.Setup(x => x.GetEmployeeById(It.IsAny<int>())).ReturnsAsync(employee);
            this.mockBusiness.Setup(x => x.DeleteEmployee(It.IsAny<Employee>())).ReturnsAsync(employee);

            // Act
            var result = await this.controller.DeleteConfirmed(employee.Id);
            var value = result as RedirectToActionResult;

            // Assert
            Assert.NotNull(value);
        }

        [Fact]
        public async Task DeleteEmployee_ShouldReturnProblem()
        {
            // Arrange

            // Act
            var result = await this.controller.DeleteConfirmed(1);
            var value = (result as ViewResult)?.Model as IEnumerable<Employee>;

            // Assert
            Assert.Empty(value ?? new List<Employee>());
        }
    }
}
