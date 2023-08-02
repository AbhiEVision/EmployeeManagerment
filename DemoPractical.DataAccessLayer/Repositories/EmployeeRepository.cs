using DemoPractical.DataAccessLayer.Data;
using DemoPractical.Domain.Interface;
using DemoPractical.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoPractical.DataAccessLayer.Repositories
{
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly ApplicationDataContext _db;
		private readonly IDepartmentRepository _departmentRepository;

		public EmployeeRepository(ApplicationDataContext db, IDepartmentRepository departmentRepository)
		{
			_db = db;
			_departmentRepository = departmentRepository;
		}

		public Task ChangeEmployeeDepartment(int empId, int depId)
		{
			throw new NotImplementedException();
		}

		public Task CreateEmployee(Employee employee)
		{
			throw new NotImplementedException();
		}

		public Task DeleteEmployee(Employee employee)
		{
			throw new NotImplementedException();
		}

		public async Task EditEmployee(Employee employee)
		{
			_db.Update<Employee>(employee);
			await _db.SaveChangesAsync();
		}

		public async Task<Employee> GetEmployeeById(int id)
		{
			Employee employee = await _db.Employees.FirstOrDefaultAsync(x => x.Id == id);
			return employee;
		}

		public async Task<Department> GetEmployeeDepartment(int empId)
		{
			Employee emp = await GetEmployeeById(empId);
			if (emp == null || emp.DepartmentId == null)
			{
				return null;
			}

			Department department = await _departmentRepository.GetDepartmentByIdAsync(emp.DepartmentId ?? 1);
			return department;
		}

		public async Task<IEnumerable<Employee>> GetEmployeesAsync()
		{
			var employees = await _db.Employees.ToListAsync();
			return employees;
		}
	}
}
