using DemoPractical.DataAccessLayer.Data;
using DemoPractical.Domain.Interface;
using DemoPractical.Models.DTOs;
using DemoPractical.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoPractical.DataAccessLayer.Repositories
{
	/// <summary>
	/// Employee Repository which maintains the working with the database operations related to the employee table
	/// </summary>
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly ApplicationDataContext _db;
		private readonly IDepartmentRepository _departmentRepository;

		public EmployeeRepository(ApplicationDataContext db, IDepartmentRepository departmentRepository)
		{
			_db = db;
			_departmentRepository = departmentRepository;
		}

		/// <summary>
		/// Changing the department of employee
		/// </summary>
		/// <param name="empId">employee id for which we want to change the department</param>
		/// <param name="depId">department id first check if there is exists or not after that we will update it!</param>
		/// <returns>Nothing!</returns>
		public async Task ChangeEmployeeDepartment(int empId, int depId)
		{
			bool depExists = await _departmentRepository.IsDepartmentExists(depId);
			if (depExists)
			{
				Employee employee = await _db.Employees.FirstOrDefaultAsync(x => x.Id == empId);
				if (employee == null)
				{
					return;
				}
				employee.DepartmentId = depId;
				await _db.SaveChangesAsync();
			}
			return;
		}

		/// <summary>
		/// Create employee
		/// </summary>
		/// <param name="employee">create this employee</param>
		public async Task CreateEmployee(Employee employee)
		{
			await _db.Employees.AddAsync(employee);
			await _db.SaveChangesAsync();
		}

		/// <summary>
		/// Delete Employee using class
		/// </summary>
		public async Task DeleteEmployee(Employee employee)
		{
			Employee getEmp = await _db.Employees.FirstOrDefaultAsync(x => x.Id == employee.Id);
			if (getEmp == null)
			{
				return;
			}

			_db.Remove(employee);
			await _db.SaveChangesAsync();
		}

		/// <summary>
		/// Edit the existing employee
		/// </summary>
		public async Task EditEmployeeDetails(int empId, EmployeeDetailsDTO employee)
		{
			Employee oldEmployee = await GetEmployeeById(empId);
			if (oldEmployee == null)
			{
				return;
			}
			oldEmployee.Name = employee.Name;
			oldEmployee.Email = employee.Email;
			oldEmployee.PhoneNumber = employee.PhoneNumber;
			_db.Update(oldEmployee);
			await _db.SaveChangesAsync();
		}

		/// <summary>
		/// Returns the employee using its id
		/// </summary>
		/// <param name="id">id for fetch</param>
		/// <returns >Employee object from database </returns>
		public async Task<Employee> GetEmployeeById(int id)
		{
			Employee employee = await _db.Employees.FirstOrDefaultAsync(x => x.Id == id);
			return employee;
		}

		/// <summary>
		/// Returns the department of the employee
		/// </summary>
		/// <param name="empId"></param>
		/// <returns>The Department object</returns>
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

		/// <summary>
		/// Returns the all Employees
		/// </summary>
		/// <returns>List of Employee</returns>
		public async Task<IEnumerable<EmployeeDetailsDTO>> GetEmployeeDetailsAsync()
		{
			var employees = await _db.Employees.ToListAsync();

			return MapListToEmployeeDetails(employees);
		}

		private List<EmployeeDetailsDTO> MapListToEmployeeDetails(List<Employee> employees)
		{
			var employeeDTOs = new List<EmployeeDetailsDTO>();

			foreach (var employee in employees)
			{
				employeeDTOs.Add(new EmployeeDetailsDTO()
				{
					Email = employee.Email,
					Name = employee.Name,
					PhoneNumber = employee.PhoneNumber,
				});
			}

			return employeeDTOs;

		}

		private EmployeeDetailsDTO MapEmployeeToItsDTO(Employee employee)
		{
			var employeeDTO = new EmployeeDetailsDTO();
			employeeDTO.Email = employee.Email;
			employeeDTO.Name = employee.Name;
			employeeDTO.PhoneNumber = employee.PhoneNumber;

			return employeeDTO;
		}

		public async Task<EmployeeDetailsDTO> GetEmployeeDetails(int empId)
		{
			Employee employee = await _db.Employees.FirstOrDefaultAsync(x => x.Id == empId);
			if (employee == null)
			{
				return null;
			}

			return MapEmployeeToItsDTO(employee);
		}
	}
}
