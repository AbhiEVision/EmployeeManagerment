using DemoPractical.Domain.Interface;
using DemoPractical.Models.DTOs;
using DemoPractical.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoPractical.API.Controllers
{
	/// <summary>
	/// Employee controller
	/// </summary>
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly IEmployeeRepository _employeeRepository;
		private readonly IDepartmentRepository _departmentRepository;

		public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
		{
			_employeeRepository = employeeRepository;
			_departmentRepository = departmentRepository;
		}

		/// <summary>
		/// Get all employees 
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetEmployees()
		{
			var employees = await _employeeRepository.GetEmployeeDetailsAsync();

			if (employees == null || employees.Count() == 0)
			{
				return Ok("No employees Found");
			}

			return Ok(employees);
		}

		/// <summary>
		/// Get the employee by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetEmployee(int id)
		{
			if (id == null || id <= 0)
			{
				return BadRequest("Please enter the valid id");
			}

			EmployeeDetailsDTO employee = await _employeeRepository.GetEmployeeDetails(id);

			if (employee == null)
			{
				return BadRequest("Requested data is not found!");
			}

			return Ok(employee);
		}

		/// <summary>
		/// Create employee
		/// </summary>
		/// <param name="employee"></param>
		[HttpPost]
		public async Task<IActionResult> CreateEmployee(Employee employee)
		{
			if (employee == null)
			{
				return BadRequest("Please Enter the valid data");
			}

			try
			{
				await _employeeRepository.CreateEmployee(employee);
				return Ok("Employee Created!");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// <summary>
		/// Edit the Employee
		/// </summary>
		/// <param name="employee"></param>
		/// <returns></returns>
		[HttpPut]
		public async Task<IActionResult> EditEmployee([FromQuery] int id, [FromBody] EmployeeDetailsDTO employee)
		{
			if (id == null || id <= 0)
			{
				return BadRequest("Please enter the valid id");
			}

			if (employee == null)
			{
				return BadRequest("Please Enter the Valid data");
			}

			Employee emp = await _employeeRepository.GetEmployeeById(id);

			if (emp == null)
			{
				return BadRequest("No such employee found!");
			}

			try
			{
				await _employeeRepository.EditEmployeeDetails(id, employee);
				return Ok($"{employee.Name} is successfully edited!");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}

		/// <summary>
		/// Delete the employee
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete]
		public async Task<IActionResult> DeleteEmployee(int id)
		{
			if (id == null || id <= 0)
			{
				return BadRequest("Please enter the valid data");
			}

			Employee employee = await _employeeRepository.GetEmployeeById(id);
			if (employee == null)
			{
				return BadRequest("No such data found!");
			}

			try
			{
				await _employeeRepository.DeleteEmployee(employee);
				return Ok($"{employee.Name} is successfully deleted!");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// <summary>
		/// Getting the department of employee
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetEmployeeDepartment(int id)
		{
			if (id == null || id <= 0)
			{
				return BadRequest("Please enter the Valida data");
			}

			Employee emp = await _employeeRepository.GetEmployeeById(id);

			if (emp == null)
			{
				return BadRequest("No such employee data found!");
			}

			if (emp.DepartmentId == null)
			{
				return Ok($"{emp.Name} is not in any department");
			}

			Department dep = await _employeeRepository.GetEmployeeDepartment(id);

			if (dep == null)
			{
				return BadRequest($"{emp.Name} is not in any department");
			}

			return Ok(dep);

		}

		/// <summary>
		/// Changing the department of the employee
		/// </summary>
		/// <param name="data"></param>
		[HttpPost]
		public async Task<IActionResult> ChangeEmployeeDepartment(EmployeeDepartment data)
		{
			if (data == null)
			{
				return BadRequest("Please enter the valid data!");
			}

			Employee emp = await _employeeRepository.GetEmployeeById(data.EmployeeId);
			Department newDep = await _departmentRepository.GetDepartmentByIdAsync(data.DepartmentId);
			Department oldDep = await _departmentRepository.GetDepartmentByIdAsync(emp.DepartmentId ?? 0);

			if (emp == null)
			{
				return BadRequest("No such employee found!");
			}

			if (newDep == null)
			{
				return BadRequest("No such department found!");
			}

			try
			{
				await _employeeRepository.ChangeEmployeeDepartment(data.EmployeeId, data.DepartmentId);
				return Ok($"{emp.Name}'s department changed from {oldDep.DepartmentName} to {newDep.DepartmentName}");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}