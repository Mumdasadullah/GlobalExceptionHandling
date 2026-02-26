using System.Text.Json;
using InMemoryDBSpecificationRepositoryUOWProject.Services;
using Microsoft.EntityFrameworkCore;
using InMemoryDBSpecificationRepositoryUOWProject.Models;
using Microsoft.AspNetCore.Mvc;
using InMemoryDBSpecificationRepositoryUOWProject.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using InMemoryDBSpecificationRepositoryUOWProject.AppSettingsModels;
using InMemoryDBSpecificationRepositoryUOWProject.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace InMemoryDBSpecificationRepositoryUOWProject.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, ConfigurationManager _configuration)
        {
            // Add your application services here
            services.AddDbContext(_configuration);
            services.AddAppSettingsModels(_configuration);
            services.AddJwtValidation(_configuration);
            services.AddUnitOfWork();
            services.AddRepositories();
            services.AddServices();
            services.Validation();
            services.AddSwaggerConfiguration();
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

        public static IServiceCollection AddAppSettingsModels(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<JWTSettings>(
                configuration.GetSection("JWTSettings"));

            services.AddSingleton(resolver =>
                resolver.GetRequiredService<IOptions<JWTSettings>>().Value);

            services.AddHttpContextAccessor();
            services.AddScoped<ClaimService>();

            return services;
        }

        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "RSATokenValidation",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                    });
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
            services.AddScoped<IRoleService, RoleService>();

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

        public static IServiceCollection AddJwtValidation(this IServiceCollection services, ConfigurationManager _configuration)
        {
            var jwtSettings = _configuration.GetSection("JWTSettings");
            var signingKey = jwtSettings.GetSection("SigningKey").Value;
            var issuer = jwtSettings.GetSection("Issuer").Value;
            var audience = jwtSettings.GetSection("Audience").Value;
            var rsaKeyPath = jwtSettings.GetSection("RSAKeyPath").Value;
            var path = Path.Combine(Directory.GetCurrentDirectory(), rsaKeyPath);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new RsaSecurityKey(JWTHelper.LoadRSAKeys(path))
                };
            });

            return services;
        }
    }
}
