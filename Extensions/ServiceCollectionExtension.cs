using System.Text.Json;
using InMemoryDBSpecificationRepositoryUOWProject.Services;
using Microsoft.EntityFrameworkCore;
using InMemoryDBSpecificationRepositoryUOWProject.Models;
using Microsoft.AspNetCore.Mvc;
using InMemoryDBSpecificationRepositoryUOWProject.DTOs;

namespace InMemoryDBSpecificationRepositoryUOWProject.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, ConfigurationManager _configuration)
        {
            // Add your application services here
            services.AddDbContext(_configuration);
            services.AddUnitOfWork();
            services.AddRepositories();
            services.AddServices();
            services.Validation();
            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddControllers()
                        .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<AppDBContext>(options =>
            {
                options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection")
                    );
            });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // Add your repository services here
            //services.AddScoped<IEmployeeReadRepository, EmployeeReadRepository>();
            //services.AddScoped<IEmployeeWriteRepository, EmployeeWriteRepository>();
            //services.AddScoped<ICountryReadRepository, CountryReadRepository>();
            //services.AddScoped<ICountryWriteRepository, CountryWriteRepository>();
            //services.AddScoped<IStatusReadRepository, StatusReadRepository>();
            //services.AddScoped<IStatusWriteRepository, StatusWriteRepository>();
            //services.AddScoped<IProvinceReadRepository, ProvinceReadRepository>();
            //services.AddScoped<IProvinceWriteRepository, ProvinceWriteRepository>();
            //services.AddScoped<ICityReadRepository, CityReadRepository>();
            //services.AddScoped<ICityWriteRepository, CityWriteRepository>();

            return services;
        }

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            //services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ICompanyService, CompanyService>();

            return services;
        }

        public static IServiceCollection Validation(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .Select(x => x.Value.Errors.Select(e => e.ErrorMessage));

                    var response = new ApiResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = errors.First().First()
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }
    }
}
