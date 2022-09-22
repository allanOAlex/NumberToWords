using Employee.Library;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Employee.Tests
{
    public class CsvReaderTests
    {
        [Fact]
        public async Task GetEmployees_ReturnsListOfAmployees_WhenCalled()
        {
            CsvReader csvReader = new CsvReader();
            var result = await csvReader.GetEmployees("D:\\Repos\\TBSDE\\EmpHierachy\\EmpHierachy\\Files\\EmployeeFile.csv");
            Assert.Equal(5, result.Count);
        }
    }
}
