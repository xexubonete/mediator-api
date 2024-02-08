using webapi_docker.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace webapi_docker
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration config)
        {

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddDbContext<ApiDbContext>(options =>
            {
                options.UseSqlServer(
                                config.GetConnectionString("DefaultConnection"),
                                b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)
                            );
            });


            return services;
        }
    }
}
