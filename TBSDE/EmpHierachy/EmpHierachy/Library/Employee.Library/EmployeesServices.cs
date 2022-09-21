﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Library
{
    public class EmployeesServices
    {
		private List<Employyee> Employees;
		public bool IsValid { get; private set; } = true;
		public List<Exception> ValidationErrors { get; private set; } = new List<Exception>();

		private EmployeesServices()
		{

		}

		public EmployeesServices(List<Employyee> employees)
		{
			Employees = employees ?? throw new ArgumentNullException(nameof(employees));
		}

		public void ValidateEmployees() => Parallel.Invoke(
				() => { CheckNumberOfCEOs(); },
				() => { CheckEmployeeWithMoreThanOneManger(); },
				() => { CheckAllManagersAreListed(); },
				() => { CheckCyclicReference(); }
				);

		private void CheckNumberOfCEOs()
		{
			if (Employees.Where(e => e.ManagerId == string.Empty || e.ManagerId == null).Count() > 1)
			{
				IsValid = false;
				ValidationErrors.Add(new Exception("More than one CEO listed"));
			}
		}

		private void CheckAllManagersAreListed()
		{
			foreach (var _ in Employees.Where(r => r.ManagerId != null && r.ManagerId != string.Empty)
				.Select(e => e.ManagerId).Where(manager => Employees.FirstOrDefault(e => e.Id == manager) == null).Select(manager => new { }))
			{
				IsValid = false;
				ValidationErrors.Add(new Exception("Some Managers are not listed"));
			}
		}

		private void CheckEmployeeWithMoreThanOneManger()
		{
			foreach (var Id in Employees.Select(e => e.Id).Distinct().Where(empId => Employees.Where(i => i.Id == empId)
			.Select(m => m.ManagerId).Distinct().Count() > 1).Select(empId => empId))
			{
				IsValid = false;
				ValidationErrors.Add(new Exception($"Employee {Id} has more than one manager"));
			}
		}

		private void CheckCyclicReference()
		{
			foreach (var _ in from employee in Employees.Where(e => e.ManagerId != string.Empty && e.ManagerId != null)
							  let manager = Employees.Where(e => e.ManagerId != string.Empty && e.ManagerId != null)
						.FirstOrDefault(e => e.Id == employee.ManagerId)
							  where manager != null
							  where manager.ManagerId == employee.Id
							  select new { })
			{
				IsValid = false;
				ValidationErrors.Add(new Exception("Cyclic Reference detected"));
			}
		}

		public long GetManagersBudget(string managerId)
		{
			if (string.IsNullOrWhiteSpace(managerId)) throw new ArgumentNullException(nameof(managerId));
			long total = 0;
			total += Employees.FirstOrDefault(e => e.Id == managerId).Salary;
			foreach (Employyee item in Employees.Where(e => e.ManagerId == managerId))
			{
				if (IsManager(item.Id))
				{
					total += GetManagersBudget(item.Id);
				}
				else
				{
					total += item.Salary;
				}
			}
			return total;
		}

		private bool IsManager(string id) => Employees.Where(e => e.ManagerId == id).Count() > 0;
	}
}
