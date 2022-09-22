using Employee.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EmployeeHierachy.Tests
{
    public class ReadFrom_CsvLineTests
    {
		[Fact]
		public void ReadEmployeeFromCSVLine_Returns_NewEmployee()
		{
			string line = "Employee1,Employee0,100";
			var employee = ReadFromCsvLine.ReadEmployeeFromCsvLine(line);
			Assert.Equal("Employee0", employee.ManagerId);
		}
		[Fact]
		public void ReadEmployeeFromCSVLine_ReturnsNull_WhenInputIsInvalid()
		{
			string line = "Employee1,Employee0100";
			var employee = ReadFromCsvLine.ReadEmployeeFromCsvLine(line);
			Assert.Null(employee);
		}
	}
}
