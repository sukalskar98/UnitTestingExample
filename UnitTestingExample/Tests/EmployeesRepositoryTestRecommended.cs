using AutoFixture;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using UnitTestingExample.Models;
using UnitTestingExample.Repository;
using Xunit;

namespace UnitTestingExample.Tests
{
    public class EmployeesRepositoryTestRecommeneded
    {
        private readonly Fixture fixture;

        private readonly DemoDb2Context context;

        private readonly IEmployeesRepository employeesRepository;

        public EmployeesRepositoryTestRecommeneded()
        {
            fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();
            var contextOptions = new DbContextOptionsBuilder<DemoDb2Context>()
                .UseSqlite(_connection)
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            context = new DemoDb2Context(contextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var employee = new Employee { Id = 1, FirstName = "Demo1", LastName = "Demo1" };
            context.Add(employee);
            context.SaveChanges();
            context.Entry(employee).State = EntityState.Detached;

            employeesRepository = new EmployeesRepository(context);
        }

        [Fact]
        public async Task GetEmployees_ShouldGetEmployees()
        {
            // Arrange

            // Act
            var result = await employeesRepository.GetEmployees();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.FirstOrDefault()?.Id);
        }

        [Fact]
        public async Task GetEmployeeById_ShouldGetEmployeeById()
        {
            // Arrange

            // Act
            var result = await employeesRepository.GetEmployeeById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task CreateEmployee_ShouldCreateEmployee()
        {
            // Arrange
            var employee = this.fixture.Create<Employee>();

            // Act
            var result = await employeesRepository.CreateEmployee(employee);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employee.Id, result.Id);
        }

        [Fact]
        public async Task EditEmployee_ShouldEditEmployee()
        {
            // Arrange
            var employee = this.fixture.Create<Employee>();
            employee.Id = 1;

            // Act
            var result = await employeesRepository.EditEmployee(employee);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employee.Id, result.Id);
        }

        [Fact]
        public async Task DeleteEmployee_ShouldDeleteEmployee()
        {
            // Arrange
            var employee = this.fixture.Create<Employee>();
            employee.Id = 1;

            // Act
            var result = await employeesRepository.DeleteEmployee(employee);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(employee.Id, result.Id);
        }
    }
}
