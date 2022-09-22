using Employee.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Employee.Tests
{
    public class EmployeesServicesTests
    {
		[Fact]
		public void ValidateEmployees_ThrowsException_WhenTheresMoreThanOneCeo()
		{
			List<Employyee> employees = new List<Employyee>
			{
				Employyee.Create("Employee1",null,100),
				Employyee.Create("Employee2","",100)
			};
			EmployeesServices services = new EmployeesServices(employees);

			services.ValidateEmployees();

			Assert.False(services.IsValid);

			Assert.Contains(services.ValidationErrors, m => m.Message == "More than one CEO listed");
		}


		[Fact]
		public void ValidateEmployees_ThrowsException_WhenSomeManagersAreNotListed()
		{
			List<Employyee> employees = new List<Employyee>
			{
				Employyee.Create("Employee1","Employee2",100),
				Employyee.Create("Employee2","Employee3",100)
			};

			EmployeesServices services = new EmployeesServices(employees);

			services.ValidateEmployees();

			Assert.False(services.IsValid);

			Assert.Contains(services.ValidationErrors, m => m.Message == "Some Managers not listed");
		}


		[Fact]
		public void ValidateEmployees_ThrowsException_WhenEmployeeHasMoreThanOneManager()
		{
			List<Employyee> employees = new List<Employyee>
			{
				Employyee.Create("Employee1","",100),
				Employyee.Create("Employee2","Employee1",100),
				Employyee.Create("Employee3","Employee1",100),
				Employyee.Create("Employee3","Employee2",100)
			};

			EmployeesServices services = new EmployeesServices(employees);

			services.ValidateEmployees();

			Assert.False(services.IsValid);

			Assert.Contains(services.ValidationErrors, m => m.Message == "Employee Employee3 has more than one manager");
		}


		[Fact]
		public void ValidateEmployees_ThrowsException_WhenEmployeesHaveCyclicReference()
		{
			List<Employyee> employees = new List<Employyee>
			{
				Employyee.Create("Employee0","",100),
				Employyee.Create("Employee2","Employee1",100),
				Employyee.Create("Employee1","Employee2",100)
			};

			EmployeesServices services = new EmployeesServices(employees);

			services.ValidateEmployees();

			Assert.False(services.IsValid);

			Assert.Contains(services.ValidationErrors, m => m.Message == "Cyclic Reference detected");
		}


		[Theory]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData(null)]
		public void GetManagersBudget_ThrowsArgumentNullException_WhenIdIsInvalid(string managerId)
		{
			EmployeesServices services = new EmployeesServices(new List<Employyee>());
			Assert.Throws<ArgumentNullException>(nameof(managerId), () => services.GetManagersBudget(managerId));
		}


		[Theory]
		[InlineData("Employee2", 1800)]
		[InlineData("Employee3", 500)]
		[InlineData("Employee1", 3800)]
		public void GetManagersBudget_ReturnsBudget(string managerId, long expected)
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

			EmployeesServices services = new EmployeesServices(employees);

			var result = services.GetManagersBudget(managerId);

			Assert.Equal(expected, result);
		}
	}
}
