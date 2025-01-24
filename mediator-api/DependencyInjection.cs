using webapi_docker.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace webapi_docker
{
    /// <summary>
    /// Extension methods for dependency injection configuration
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds all required services to the dependency injection container
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The application configuration</param>
        /// <returns>The configured service collection</returns>
        public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddDbContext<ApiDbContext>(options =>
            {
                options.UseSqlServer(
                                configuration.GetConnectionString("DefaultConnection"),
                                b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)
                            );
            });

            return services;
        }
    }
}
