using DemoPractical.DataAccessLayer.Data;
using DemoPractical.Domain.Interface;
using DemoPractical.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoPractical.DataAccessLayer.Repositories
{
	public class DepartmentRepository : IDepartmentRepository
	{
		private readonly ApplicationDataContext _db;

		public DepartmentRepository(ApplicationDataContext db)
		{
			_db = db;
		}

		public async Task AddDepartment(Department department)
		{
			await _db.Departments.AddAsync(department);
			await _db.SaveChangesAsync();
		}

		public async Task DeleteDepartment(int depId)
		{
			Department dep = await GetDepartmentByIdAsync(depId);

			if (dep != null)
			{
				_db.Departments.Remove(dep);
				await _db.SaveChangesAsync();
			}

		}

		public async Task EditDepartment(Department department)
		{
			_db.Update(department);
			await _db.SaveChangesAsync();
		}

		public async Task<Department> GetDepartmentByIdAsync(int id)
		{
			Department? department = await _db.Departments.FirstOrDefaultAsync(x => x.Id == id);
			return department;

		}

		public async Task<IEnumerable<Department>> GetDepartmentsAsync()
		{
			var departments = await _db.Departments.ToListAsync();
			return departments;
		}
	}
}
