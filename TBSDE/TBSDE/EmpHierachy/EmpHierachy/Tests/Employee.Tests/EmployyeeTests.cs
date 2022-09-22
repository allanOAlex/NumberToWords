using Employee.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EmployeeHierachy.Tests
{
    public class EmployyeeTests
    {
		[Theory]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData(null)]
		public void Create_ThrowsArgumentNullException_WhenIdIsInvalid(string id)
		{
			Assert.Throws<ArgumentNullException>(nameof(id), () => Employyee.Create(id, null, default(int)));
		}

		[Fact]
		public void Create_ThrowsArgumentOutOfRangeException_WhenSalaryIsInvalid()
		{
			int salary = -1;
			Assert.Throws<ArgumentOutOfRangeException>(nameof(salary), () => Employyee.Create("Emp1", null, salary));
		}
		[Fact]
		public void Create_CreatesNewEmployee()
		{
			var employee = Employyee.Create("Employee1", "", 100);
			Assert.Equal("Employee1", employee.Id);
			Assert.Equal(100, employee.Salary);
		}
	}
}
