using CompanyApi.Api.Data;
using CompanyApi.Api.Repository.Implementation;
using CompanyApi.Api.Repository.Interface;
using CompanyApi.Api.Services.Implementation;
using CompanyApi.Api.Services.Interface;
using CompanyApi.Api.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace CompanyApi.Api.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCompanyDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }

        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<ICompanyService, CompanyService>();
            return services;
        }
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            return services;
        }

        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CompanyDTOValidator>();
            return services;
        }

        public static IServiceCollection AddSecurityInfo(this IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<AppDBContext>();
            return services;
        }

        public static IServiceCollection SetUpSwaggerForSecurity(this IServiceCollection services)
        {
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Auth Demo", Version = "v1"
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
            return services;
        }
    }
}
