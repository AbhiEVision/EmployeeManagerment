using DemoPractical.Models.DTOs;
using DemoPractical.Models.Models;

namespace DemoPractical.Models.Mappers
{
	public static class MappingModels
	{
		public static EmployeeDetailsDTO MapEmployeeToDTO(Employee employee)
		{
			return new EmployeeDetailsDTO()
			{
				Email = employee.Email,
				Name = employee.Name,
				PhoneNumber = employee.PhoneNumber,
			};
		}

		public static Employee MapEmployeeDTOToEmployee(EmployeeDetailsDTO dto, Employee employee)
		{
			employee.Name = dto.Name;
			employee.PhoneNumber = dto.PhoneNumber;
			employee.Email = dto.Email;
			return employee;
		}

	}
}
