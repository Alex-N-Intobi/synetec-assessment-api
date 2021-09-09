using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SynetecAssessmentApi.Application.Interfaces.Services.BonusPool;
using SynetecAssessmentApi.Infrastructure.Services.BonusPool;
using SynetecAssessmentApi.Persistence;

namespace SynetecAssessmentApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IBonusPoolService, BonusPoolService>();
            return services;
        }
        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SynetecAssessmentApi", Version = "v1" });
            });
            return services;
        }
        public static IServiceCollection AddDatabase(this IServiceCollection services, 
            ServiceLifetime serviceLifetimeDatabase = ServiceLifetime.Scoped)
        {
            services.AddDbContext<AppDbContext>(options => {
                options.UseInMemoryDatabase(databaseName: "HrDb");
            }, serviceLifetimeDatabase);
            return services;
        }
    }
}
