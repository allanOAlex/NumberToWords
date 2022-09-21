using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Library
{
    public class Employees
    {
		private string CsvPath;
		private readonly ICsvReader CSVReader;

		public Employees(string csvPath, ICsvReader csvReader)
		{
			CsvPath = csvPath;
			CSVReader = csvReader;
		}

		public static Employyee Create(string empId, string managerId, long salary)
		{
			return new Employyee(empId, managerId, salary);
		}

		public async Task<List<Employyee>> GetEmployeeRecords()
		{
			var employees = await CSVReader.GetEmployees(CsvPath);

			if (employees != null)
			{
				EmployeesServices services = new EmployeesServices(employees);

				services.ValidateEmployees();

				if (services.IsValid)
				{
					return employees;
				}

				throw new AggregateException(services.ValidationErrors);
			}
			return null;
		}

		public async Task<long> GetManagerBudget(string managerId)
		{
			var employees = await CSVReader.GetEmployees(CsvPath);

			if (employees != null)
			{
				EmployeesServices services = new EmployeesServices(employees);
				return services.GetManagersBudget(managerId);
			}
			return 0;
		}
	}
}
