using DemoPractical.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoPractical.DataAccessLayer.Data
{
	public class ApplicationDataContext : DbContext
	{
		public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
		{

		}

		public DbSet<Employee> Employees { get; set; }

		public DbSet<Department> Departments { get; set; }

		public DbSet<EmployeeType> EmployeeTypes { get; set; }

		public DbSet<PermentEmployee> PermentEmployees { get; set; }

		public DbSet<ConractBaseEmployee> ConractBaseEmployees { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ConractBaseEmployee>().HasKey(x => x.EmployeeID);
			modelBuilder.Entity<PermentEmployee>().HasKey(x => x.EmployeeId);
		}

	}
}
