using InMemoryDBSpecificationRepositoryUOWProject.Context;
using System.Text.Json;
using InMemoryDBSpecificationRepositoryUOWProject.Respositories;
using InMemoryDBSpecificationRepositoryUOWProject.Services;
using InMemoryDBSpecificationRepositoryUOWProject.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace InMemoryDBSpecificationRepositoryUOWProject.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Add your application services here
            services.AddDbContext();
            services.AddUnitOfWork();
            services.AddRepositories();
            services.AddServices();
            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            services.AddControllers()
                        .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<InMemoryDBContext>(options =>
            {
                options.UseInMemoryDatabase("EmployeeDB");
            });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // Add your repository services here
            services.AddScoped<IEmployeeReadRepository, EmployeeReadRepository>();
            services.AddScoped<IEmployeeWriteRepository, EmployeeWriteRepository>();
            services.AddScoped<ICountryReadRepository, CountryReadRepository>();
            services.AddScoped<ICountryWriteRepository, CountryWriteRepository>();
            services.AddScoped<IStatusReadRepository, StatusReadRepository>();
            services.AddScoped<IStatusWriteRepository, StatusWriteRepository>();
            services.AddScoped<IProvinceReadRepository, ProvinceReadRepository>();
            services.AddScoped<IProvinceWriteRepository, ProvinceWriteRepository>();
            services.AddScoped<ICityReadRepository, CityReadRepository>();
            services.AddScoped<ICityWriteRepository, CityWriteRepository>();

            return services;
        }

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            return services;
        }
    }
}
