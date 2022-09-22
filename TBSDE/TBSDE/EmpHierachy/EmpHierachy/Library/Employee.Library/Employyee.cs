using System;
using System.Collections.Generic;
using System.Text;

namespace Employee.Library
{
    public class Employyee
    {
		public string Id { get; private set; }

		public string ManagerId { get; private set; }

		public long Salary { get; private set; }

		public Employyee()
		{

		}

		public Employyee(string empId, string managerId, long salary)
		{
			Id = empId.Trim();
			ManagerId = managerId;
			Salary = salary;
		}

		public static Employyee Create(string empId, string managerId, long salary)
		{
			return new Employyee(empId, managerId, salary);
		}
	}
}
