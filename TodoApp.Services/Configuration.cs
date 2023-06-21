using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using TodoApp.Database;

namespace TodoApp.Services
{
    public static class Configure
    {
        public static void ConfigureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(ServiceAssembly).Assembly);
            services.RegisterAssemblyPublicNonGenericClasses(typeof(ServiceAssembly).Assembly)
                .Where(s => !s.IsAbstract)
                .AsPublicImplementedInterfaces();
        }
    }
}