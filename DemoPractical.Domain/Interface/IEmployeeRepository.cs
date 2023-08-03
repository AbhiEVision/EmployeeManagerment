using DemoPractical.Models.DTOs;
using DemoPractical.Models.Models;

namespace DemoPractical.Domain.Interface
{
	public interface IEmployeeRepository
	{
		Task<IEnumerable<EmployeeDetailsDTO>> GetEmployeeDetailsAsync();

		Task<Employee> GetEmployeeById(int id);

		Task EditEmployeeDetails(int empId, EmployeeDetailsDTO employee);

		Task DeleteEmployee(Employee employee);

		Task CreateEmployee(Employee employee);

		Task<Department> GetEmployeeDepartment(int empId);

		Task ChangeEmployeeDepartment(int empId, int depId);

		Task<EmployeeDetailsDTO> GetEmployeeDetails(int empId);
	}
}
