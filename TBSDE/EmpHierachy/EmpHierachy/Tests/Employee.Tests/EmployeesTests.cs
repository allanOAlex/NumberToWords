using Employee.Library;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeHierachy.Tests
{
    public class EmployeesTests
    {
		private readonly ICsvReader CsvReader;
		private readonly Employees Employees;
		private string CvsPath = @"D:\Repos\TBSDE\EmpHierachy\EmpHierachy\Files\EmployeeFile.csv";
		private readonly Mock<ICsvReader> CsvReaderMock = new Mock<ICsvReader>();


		public EmployeesTests()
		{
			Employees = new Employees(CvsPath, CsvReaderMock.Object);
		}


		[Theory]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData(null)]
		public void Employees_ThrowsArgumentNullException_WhenCSVPathIsInvalid(string path)
		{
			async Task csvPath() => new Employees(path, CsvReaderMock.Object);
			Assert.ThrowsAsync<ArgumentNullException>(csvPath);
		}


		[Fact]
		public async Task GetEmployeeRecords_ReturnsNull_WhenListIsNull()
		{
			CsvReaderMock.Setup(k => k.GetEmployees(CvsPath)).ReturnsAsync((List<Employyee>)null);

			var result = await Employees.GetEmployeeRecords();
			Assert.Null(result);
		}


		[Fact]
		public async Task Employees_ThrowsAggregateException_WhenEmployeesAreInvald()
		{
			List<Employyee> employees = new List<Employyee>
			{
				Employyee.Create("Employee0","",100),
				Employyee.Create("Employee2","Employee1",100),
				Employyee.Create("Employee1","Employee2",100),
				Employyee.Create("Employee1","",1100)
			};

			CsvReaderMock.Setup(k => k.GetEmployees(CvsPath)).ReturnsAsync(employees);

			await Assert.ThrowsAsync<AggregateException>(async () => await Employees.GetEmployeeRecords());
		}


		[Fact]
		public async Task GetEmployeeRecords_ReturnsEmployees()
		{
			List<Employyee> employees = new List<Employyee>
			{
				Employyee.Create("Employee1","",1000),
				Employyee.Create("Employee2","Employee1",800),
				Employyee.Create("Employee3","Employee1",500),
				Employyee.Create("Employee4","Employee2",500),
				Employyee.Create("Employee6","Employee2",500),
				Employyee.Create("Employee5","Employee1",500)
			};

			CsvReaderMock.Setup(k => k.GetEmployees(CvsPath)).ReturnsAsync(employees);

			var result = await Employees.GetEmployeeRecords();
			Assert.Equal(6, result.Count);
		}


		[Fact]
		public async Task GetManagerBudget_ReturnsZero_WhenEmployeeListIsNull()
		{
			CsvReaderMock.Setup(k => k.GetEmployees(CvsPath)).ReturnsAsync((List<Employyee>)null);

			var result = await Employees.GetManagerBudget("Employee1");
			Assert.Equal(0, result);
		}


		[Theory]
		[InlineData("Employee2", 1800)]
		[InlineData("Employee3", 500)]
		[InlineData("Employee1", 3800)]
		public async Task GetManagerBudget_ReturnsManagersBudget(string employeeId, long budget)
		{
			List<Employyee> employees = new List<Employyee>
			{
				Employyee.Create("Employee1","",1000),
				Employyee.Create("Employee2","Employee1",800),
				Employyee.Create("Employee3","Employee1",500),
				Employyee.Create("Employee4","Employee2",500),
				Employyee.Create("Employee6","Employee2",500),
				Employyee.Create("Employee5","Employee1",500)
			};

			CsvReaderMock.Setup(k => k.GetEmployees(CvsPath)).ReturnsAsync(employees);

			var result = await Employees.GetManagerBudget(employeeId);
			Assert.Equal(budget, result);
		}
	}
}
