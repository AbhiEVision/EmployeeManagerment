using DemoPractical.Models.Models;

namespace DemoPractical.Domain.Interface
{
	public interface IEmployeeRepository
	{
		Task<IEnumerable<Employee>> GetEmployeesAsync();

		Task<Employee> GetEmployeeById(int id);

		Task EditEmployee(Employee employee);

		Task DeleteEmployee(Employee employee);

		Task CreateEmployee(Employee employee);

		Task<Department> GetEmployeeDepartment(int empId);

		Task ChangeEmployeeDepartment(int empId, int depId);
	}
}
