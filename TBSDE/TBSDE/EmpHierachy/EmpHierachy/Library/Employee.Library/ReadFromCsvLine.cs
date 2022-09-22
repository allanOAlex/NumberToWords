using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.Library
{
    public static class ReadFromCsvLine
    {
		static public Employyee ReadEmployeeFromCsvLine(this string csvLine)
		{
			string[] parts = csvLine.Split(',');

			if (parts.Length == 3)
			{
				string Id = parts[0];
				string ManagerId = parts[1];
				string Salary = parts[2];

				long.TryParse(Salary, out long salary);

				return Employyee.Create(Id, ManagerId, salary);
			}

			return null;
		}
	}
}
