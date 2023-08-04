using DemoPractical.API.Swagger;
using DemoPractical.DataAccessLayer.Data;
using DemoPractical.DataAccessLayer.Repositories;
using DemoPractical.DataAccessLayer.ValidationClass;
using DemoPractical.Domain.Interface;
using DemoPractical.Models.DTOs;
using DemoPractical.Models.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

#region Services Register


builder.Services.AddDbContext<ApplicationDataContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DemoApp"));
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Fluent Validation 
builder.Services.AddFluentValidationAutoValidation();

// Register class for fluent validations 
builder.Services.AddScoped<IValidator<ConractBaseEmployee>, ContractBaseEmployeeValidate>();
builder.Services.AddScoped<IValidator<Department>, DepartmentValidation>();
builder.Services.AddScoped<IValidator<Employee>, EmployeeValidation>();
builder.Services.AddScoped<IValidator<PermentEmployee>, PermanentEmployeeValidation>();
builder.Services.AddScoped<IValidator<CreateEmployeeDTO>, CreateEmployeeDTOValidation>();

// Register Repositories
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

// Swagger options registrations
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigurationOptions>();

//// API Versing
//builder.Services.AddApiVersioning(opt =>
//{
//	opt.AssumeDefaultVersionWhenUnspecified = true;
//	opt.DefaultApiVersion = new ApiVersion(1, 0);
//	opt.ReportApiVersions = true;
//});

////Setup API explorer that is API version aware
//builder.Services.AddVersionedApiExplorer(setup =>
//{
//	setup.GroupNameFormat = "'v'VVV";
//	setup.SubstituteApiVersionInUrl = true;
//});

#endregion

var app = builder.Build();

#region Adding Middleware

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion